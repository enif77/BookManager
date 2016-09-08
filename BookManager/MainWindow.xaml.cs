/* BookManager - (C) 2016 Premysl Fara 
 
Helpers are available under the zlib license :

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
    using System.ComponentModel;
    using System.Windows.Input;
    using System.Windows;

    using Injektor;


    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool _initialized = false;


        public MainWindow()
        {
            InitializeComponent();

            SourceInitialized += MainWindow_SourceInitiated;
            Loaded += MainWindow_Loaded;
            Closing += MainWindow_Closing;

            Registry.RegisterInstance(this);
        }


        private void MainWindow_SourceInitiated(object sender, EventArgs e)
        {
            var config = App.Config;

            if (config.IsMaximized) WindowState = WindowState.Maximized;

            Top = config.Top;
            Left = config.Left;
            Width = config.Width;
            Height = config.Height;
        }


        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (_initialized) return;

            // TODO: Implement.

            _initialized = true;
        }


        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            // TODO: Zeptat se, zda uživatel chce zavřít okno.

            // Save base configuration values.
            App.SaveAppConfig(this);
        }


        private void MainWindow_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            Keyboard.ClearFocus();
        }
    }
}
