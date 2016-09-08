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

namespace BookManager.Forms
{
    using System;
    using System.Windows;
    using System.Windows.Controls;


    /// <summary>
    /// Interaction logic for UnhandledErrorWindow.xaml
    /// </summary>
    public partial class UnhandledErrorWindow : Window
    {
        #region fields

        private static UnhandledErrorWindow _window;

        #endregion


        #region ctor

        public UnhandledErrorWindow()
        {
            InitializeComponent();
        }

        #endregion


        #region public methods

        public static void Open(Exception exception)
        {
            _window = new UnhandledErrorWindow
            {
                _errorTextBlock = { Text = exception.Message },
                _errorTextBox = { Text = exception.ToString() }
            };

            _window.ShowDialog();
        }

        #endregion


        #region non-public methods

        private void OkClick(object sender, RoutedEventArgs e)
        {
            _window.Close();
        }


        private void DetailsClick(object sender, RoutedEventArgs e)
        {
            ((Button)sender).IsEnabled = false;
            _errorTextBox.Visibility = Visibility.Visible;
        }

        #endregion
    }
}
