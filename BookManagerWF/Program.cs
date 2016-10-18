/* BookManager - (C) 2016 Premysl Fara 
 
BookManager is available under the zlib license:

This software is provided 'as-is', without any express or implied
warranty.  In no event will the authors be held liable for any damages
arising from the use of this software.

Permission is granted to anyone to use this software for any purpose,
including commercial applications, and to alter it and redistribute it
freely, subject to the following restrictions:

1. The origin of this software must not be misrepresented; you must not
   claim that you wrote the original software. If you use this software
   in a product, an acknowledgment in the product documentation would be
   appreciated but is not required.
2. Altered source versions must be plainly marked as such, and must not be
   misrepresented as being the original software.
3. This notice may not be removed or altered from any source distribution.
 
 */

namespace BookManagerWF
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Threading;
    using System.Windows.Forms;

    using Injektor;

    using BookManager.DataLayer;
    using BookManager.DataObjects;


    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Do not catch exceptions in the debug mode.
            //if (!AppDomain.CurrentDomain.FriendlyName.EndsWith("vshost.exe"))
            if (Debugger.IsAttached == false)
            {
                // Add the event handler for handling UI thread exceptions to the event.
                Application.ThreadException += new ThreadExceptionEventHandler(UIThreadExceptionHandler);

                // Set the unhandled exception mode to force all Windows Forms errors to go through our handler.
                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

                // Add the event handler for handling non-UI thread exceptions to the event. 
                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

                // Connect to the localdb.
                Initializer.InitializeLayers(new SimpleDb.Files.Database(DataDirectoryPath));
            }

            // Connect to the localdb.
            Initializer.InitializeLayers(new SimpleDb.Files.Database(DataDirectoryPath));

            // Load the application config.
            LoadAppConfig();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }


        /// <summary>
        /// Handle the UI exceptions by showing a dialog box, and asking the user whether or not they wish to abort execution.
        /// </summary>
        /// <param name="sender">A sender.</param>
        /// <param name="t">Event arguments.</param>
        private static void UIThreadExceptionHandler(object sender, ThreadExceptionEventArgs t)
        {
            DialogResult result = DialogResult.Cancel;
            try
            {
                result = ShowThreadExceptionDialog("Windows Forms Error", t.Exception);
            }
            catch
            {
                try
                {
                    MessageBox.Show("Fatal Windows Forms Error", "Fatal Windows Forms Error", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Stop);
                }
                finally
                {
                    Application.Exit();
                }
            }

            // Exits the program when the user clicks Abort.
            if (result == DialogResult.Abort)
            {
                Application.Exit();
            }
        }

        /// <summary>
        /// Creates the error message and displays it.
        /// </summary>
        /// <param name="title">A dialog title.</param>
        /// <param name="e">An exception.</param>
        /// <returns>A dialog result.</returns>
        private static DialogResult ShowThreadExceptionDialog(string title, Exception e)
        {
            var errorMsg = "An application error occurred. Please contact the adminstrator with the following information:\n\n";

            errorMsg = errorMsg + e.Message + "\n\nStack Trace:\n" + e.StackTrace;

            return MessageBox.Show(errorMsg, title, MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Stop);
        }

        // Handle the UI exceptions by showing a dialog box, and asking the user whether
        // or not they wish to abort execution.
        // NOTE: This exception cannot be kept from terminating the application - it can only 
        // log the event, and inform the user about it. 
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                var ex = (Exception)e.ExceptionObject;
                var errorMsg = "An application error occurred. Please contact the adminstrator with the following information:\n\n";

                // Since we can't prevent the app from terminating, log this to the event log.
                if (!EventLog.SourceExists("ThreadException"))
                {
                    EventLog.CreateEventSource("ThreadException", "Application");
                }

                // Create an EventLog instance and assign its source.
                var myLog = new EventLog
                {
                    Source = "ThreadException"
                };

                myLog.WriteEntry(errorMsg + ex.Message + "\n\nStack Trace:\n" + ex.StackTrace);
            }
            catch (Exception exc)
            {
                try
                {
                    MessageBox.Show(
                        "Fatal Non-UI Error", 
                        "Fatal Non-UI Error. Could not write the error to the event log. Reason: " + exc.Message, 
                        MessageBoxButtons.OK, 
                        MessageBoxIcon.Stop);
                }
                finally
                {
                    Application.Exit();
                }
            }
        }
         
        /// <summary>
        /// Returns absolute path to the Data directory.
        /// </summary>
        public static string DataDirectoryPath
        {
            get { return Path.Combine(Directory.GetCurrentDirectory(), "Data"); }
        }

        /// <summary>
        /// A lock object for thread synchronizing/locking.
        /// </summary>
        private static readonly object AppLocker = new object();


        private static Configuration _config;

        /// <summary>
        /// An application config loaded from a configuration template.
        /// </summary>
        public static Configuration Config
        {
            get
            {
                lock (AppLocker)
                {
                    return _config;
                }
            }

            set
            {
                lock (AppLocker)
                {
                    _config = value;
                }
            }
        }

        /// <summary>
        /// Loads user config from the file
        /// </summary>
        private static void LoadAppConfig()
        {
            Config = Registry.Get<ConfigurationDataLayer>().Get(1);
            if (Config == null)
            {
                Config = new Configuration();
                Registry.Get<ConfigurationDataLayer>().Save(Config);
            }
        }

        /// <summary>
        /// Saves current app. state to the configuration template.
        /// </summary>
        /// <param name="window">A MainWindow instance.</param>
        public static void SaveAppConfig(Form window)
        {
            Config.IsMaximized = window.WindowState == FormWindowState.Maximized;
            Config.Top = window.Top;
            Config.Left = window.Left;
            Config.Width = window.Width;
            Config.Height = window.Height;

            Registry.Get<ConfigurationDataLayer>().Save(Config);
        }
    }
}
