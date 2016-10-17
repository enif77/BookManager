/* BookManager - (C) 2016 Premysl Fara 
 
BookManager are available under the zlib license:

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

namespace BookManagerWF.Forms
{
    using System;
    using System.Windows.Forms;


    public partial class UnhandledErrorForm : Form
    {
        #region fields

        private static UnhandledErrorForm _form;

        private Exception _ex;

        #endregion


        #region ctor

        public UnhandledErrorForm()
        {
            InitializeComponent();
        }

        #endregion


        #region public methods

        public static void Open(Exception exception)
        {
            _form = new UnhandledErrorForm
            {
                MessageTextBox = { Text = exception.Message },
                _ex = exception
            };

            _form.ShowDialog();
            _form = null;
        }

        #endregion


        #region non-public methods

        private void ShowDetailsButton_Click(object sender, EventArgs e)
        {
            ShowDetailsButton.Enabled = false;
            if (_ex != null)
            {
                MessageTextBox.Text += Environment.NewLine + _ex;
            }
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            _form.Close();
        }

        #endregion
    }
}
