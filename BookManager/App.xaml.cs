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

namespace BookManager
{
    using System;
    using System.IO;
    using System.Windows;
    using System.Windows.Threading;
    
    using Injektor;

    using BookManager.DataLayer;
    using BookManager.DataObjects;
    using BookManager.Forms;


    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            DispatcherUnhandledException += DispatcherUnhandledExceptionHandler;

            // Connect to the localdb.
            Initializer.InitializeLayers(new SimpleDb.Files.Database(DataDirectoryPath));

            // Load the application config.
            LoadAppConfig();

            // Create the startup window
            var window = new MainWindow();

            // Show the window
            window.Show();
                
            /*
            // Setup date time format for application.
            Thread.CurrentThread.CurrentCulture = Culture.GetDefaultApplicationCultureInfo();
             */
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
            try
            {
                Config = Registry.Get<ConfigurationDataLayer>().Get(1);
                if (Config == null)
                {
                    Config = new Configuration();
                    Registry.Get<ConfigurationDataLayer>().Save(Config);
                }
            }
            catch (Exception)
            {
                ;  // Exceptions ignored

                // TODO: Log exception.
            }
        }

        /// <summary>
        /// Saves current app. state to the configuration template.
        /// </summary>
        /// <param name="window">A MainWindow instance.</param>
        public static void SaveAppConfig(Window window)
        {
            Config.IsMaximized = window.WindowState == WindowState.Maximized;
            Config.Top = (int)window.Top;
            Config.Left = (int)window.Left;
            Config.Width = (int)window.Width;
            Config.Height = (int)window.Height;

            Registry.Get<ConfigurationDataLayer>().Save(Config);
        }


        private static void DispatcherUnhandledExceptionHandler(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;

            // ignore datagrid clipboard copy error (content is copied sucessfully anyway):
            // http://stackoverflow.com/questions/12769264/openclipboard-failed-when-copy-pasting-data-from-wpf-datagrid
            var comException = e.Exception as System.Runtime.InteropServices.COMException;
            if (comException != null && comException.ErrorCode == -2147221040) return;

            UnhandledErrorWindow.Open(e.Exception);
        }
    }
}
