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

    using SimpleDb.Shared;


    public class BaseLookupEditorForm : BaseEditorForm
    {
        #region ctor

        protected BaseLookupEditorForm(ILookup dataObject, string dataObjectTitle)
            : base(dataObject, dataObjectTitle)
        {
            Text = (dataObject.Id <= 0)
                ? ("Book Manager - New " + dataObjectTitle)
                : String.Format("Book Manager - {0} Id:{1}", dataObjectTitle, dataObject.Id);
        }

        #endregion


        #region public methods

        public static bool Open(ILookup dataObject, string dataObjectTitle)
        {
            if (dataObject == null) throw new ArgumentNullException("dataObject");

            var dialog = new BaseLookupEditorForm(dataObject, dataObjectTitle);
                        
            dialog.LoadDataToControls();
            dialog.ShowDialog();

            return dialog.DialogResult == DialogResult.OK;
        }
         

        public override void LoadDataToControls()
        {
            var dataObject = (ILookup)DataObject;

            // TODO: Automatic data binding.
            NameTextBox.Text = dataObject.Name;
            DescriptionTextBox.Text = dataObject.Description;
        }


        public override void SaveDataToDataObject()
        {
            var dataObject = (ILookup)DataObject;

            // TODO: Automatic data binding.
            dataObject.Name = NameTextBox.Text;
            dataObject.Description = DescriptionTextBox.Text;
        }


        public override void ValidateDataObject()
        {
            var dataObject = (IValidatable)DataObject;

            dataObject.Validate();
        }

        #endregion



        #region protected

        protected override void InitializeComponent()
        {
            var table = CreateLayoutTable(3);

            table.SuspendLayout();

            NameTextBox = CreateTextboxWithLabel(table, 0, "Name", "Name");
            DescriptionTextBox = CreateTextboxWithLabel(table, 1, "Description", "Description", DefaultTextBoxMinCharsPerLine, DefaultMultilineTextBoxMinLines);
            CreateButtons(table, 2);

            table.ResumeLayout();
        }

        #endregion


        #region private

        protected TextBox NameTextBox;
        protected TextBox DescriptionTextBox;

        #endregion
    }
}
