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

namespace BookManagerWF.Forms
{
    using System;
    using System.Windows.Forms;

    using BookManager.DataObjects;
    

    /// <summary>
    /// An example, how to override the LookupEditorForm.
    /// </summary>
    public class BookAuthorEditorForm : BaseLookupEditorForm
    {
        #region ctor

        public BookAuthorEditorForm(BookAuthor dataObject, string dataObjectTitle) 
            : base(dataObject, dataObjectTitle)
        {
        }

        #endregion


        #region public methods

        public static bool Open(BookAuthor dataObject)
        {
            if (dataObject == null) throw new ArgumentNullException("dataObject");

            var dialog = new BookAuthorEditorForm(dataObject, "Book Author");

            dialog.LoadDataToControls();
            dialog.ShowDialog();

            return dialog.DialogResult == DialogResult.OK;
        }

        #endregion


        public override void LoadDataToControls()
        {
            var dataObject = (BookAuthor)DataObject;

            FirstNameTextBox.Text = dataObject.FirstName;
            LastNameTextBox.Text = dataObject.LastName;
            DescriptionTextBox.Text = dataObject.Description;
        }


        public override void SaveDataToDataObject()
        {
            var dataObject = (BookAuthor)DataObject;

            dataObject.FirstName = FirstNameTextBox.Text;
            dataObject.LastName = LastNameTextBox.Text;
            dataObject.Description = DescriptionTextBox.Text;
        }


        protected override void InitializeComponent()
        {
            // Main layout table.
            var table = CreateLayoutTable(4);

            table.SuspendLayout();

            FirstNameTextBox = CreateTextboxWithLabel(table, 0, "First name", "FirstName");
            LastNameTextBox = CreateTextboxWithLabel(table, 1, "Last name", "LastName");
            DescriptionTextBox = CreateTextboxWithLabel(table, 2, "Note", "Description", DefaultTextBoxMinCharsPerLine, DefaultMultilineTextBoxMinLines);
            CreateButtons(table, 3);

            table.ResumeLayout();
        }
       

        private TextBox FirstNameTextBox;
        private TextBox LastNameTextBox;
    }
}
