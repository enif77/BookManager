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

namespace BookManagerWF.Forms
{
    using System;
    using System.Windows.Forms;

    using Injektor;

    using BookManager.DataObjects;


    public partial class BookCategoryEditorForm : Form
    {
        #region properties

        public BookCategory DataObject
        {
            get; set;
        }

        #endregion


        #region ctor

        public BookCategoryEditorForm()
        {
            InitializeComponent();

            DialogResult = DialogResult.OK;
        }

        #endregion


        #region public methods

        public static bool Open(BookCategory dataObject)
        {
            if (dataObject == null) throw new ArgumentNullException("dataObject");

            var dialog = new BookCategoryEditorForm()
            {
                DataObject = dataObject,
                Owner = Registry.Get<MainForm>(),
                StartPosition = FormStartPosition.CenterParent,
                Text = (dataObject.Id <= 0)
                    ? "Book Manager - New Book Category"
                    : String.Format("Book Manager - Book Category Edit {0}", dataObject.Id)
            };

            dialog.ShowDialog();

            return dialog.DialogResult == DialogResult.OK;
        }

        #endregion


        private void SaveOperationButton_Click(object sender, EventArgs e)
        {
            if (SaveClick())
            {
                Close();
            }
        }


        private void CancelButton_Click(object sender, EventArgs e)
        {
            CancelClick();
            Close();
        }


        private bool SaveClick()
        {
            try
            {
                DataObject.Name = NameTextBox.Text;
                DataObject.Description = DescriptionTextBox.Text;

                DataObject.Validate();
            }
            catch (Exception ex)
            {
                UnhandledErrorForm.Open(ex);

                return false;
            }

            DialogResult = DialogResult.OK;

            return true;
        }


        private void CancelClick()
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
