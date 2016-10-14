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
    using System.Drawing;
    using System.Windows.Forms;

    using Injektor;

    using BookManagerWF.Forms.Controls;
    using SimpleDb.Shared;

    public partial class LookupEditorForm<T> : Form where T : ILookup, IValidatable
    {
        #region properties

        public T DataObject
        {
            get; set;
        }

        #endregion


        #region ctor

        public LookupEditorForm()
        {
            DialogResult = DialogResult.OK;
        }

        #endregion


        #region public methods

        protected static bool Open(T dataObject, string dataObjectTitle)
        {
            if (dataObject == null) throw new ArgumentNullException("dataObject");

            var dialog = CreateDialog(dataObject, dataObjectTitle);

            // TODO: Automatic data binding.
            dialog._nameTextBox.Text = dataObject.Name;
            dialog._descriptionTextBox.Text = dataObject.Description;

            dialog.ShowDialog();

            return dialog.DialogResult == DialogResult.OK;
        }

        #endregion


        private TextBox _nameTextBox;
        private TextBox _descriptionTextBox;


        private static LookupEditorForm<T> CreateDialog(T dataObject, string dataObjectTitle)
        {
            var dialog = new LookupEditorForm<T>()
            {
                DataObject = dataObject,
                Owner = Registry.Get<MainForm>(),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Text = (dataObject.Id <= 0)
                    ? ("Book Manager - New " + dataObjectTitle)
                    : String.Format("Book Manager - {0} Id:{1}", dataObjectTitle, dataObject.Id)
            };

            var table = new TableLayoutPanel()
            {
                Parent = dialog,
                Padding = new Padding(dialog.Font.Height),
                AutoSize = true,
                ColumnCount = 2,
                RowCount = 3
            };

            table.SuspendLayout();

            var controlsMargin = dialog.Font.Height / 2;
            var controlsMinWidth100 = new Size(100, 1);

            table.Controls.Add(new Label()
            {
                AutoSize = true,
                Margin = new Padding(controlsMargin),
                Text = "Name",
                MinimumSize = controlsMinWidth100
            }, 0, 0);

            dialog._nameTextBox = new TextBox()
            {
                Name = "Name",
                Margin = new Padding(controlsMargin),
                Size = new Size(383, 20),
            };

            table.Controls.Add(dialog._nameTextBox, 1, 0);


            table.Controls.Add(new Label()
            {
                AutoSize = true,
                Margin = new Padding(controlsMargin),
                Text = "Description",
                MinimumSize = controlsMinWidth100
            }, 0, 1);

            dialog._descriptionTextBox = new TextBox()
            {
                Name = "Description",
                AcceptsReturn = true,
                Multiline = true,
                Margin = new Padding(controlsMargin),
                Size = new Size(383, 69)
            };

            table.Controls.Add(dialog._descriptionTextBox, 1, 1);


            var buttonsPanel = new FlowLayoutPanel()
            {
                Anchor = AnchorStyles.Right,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Margin = new Padding(dialog.Font.Height),
            };
                        
            var saveOperationButton = new SaveButton(buttonsPanel, dialog.Font);
            saveOperationButton.Click += new EventHandler(dialog.SaveOperationButton_Click);

            var cancelOperationButton = new CancelButton(buttonsPanel, dialog.Font);
            cancelOperationButton.Click += new EventHandler(dialog.CancelButton_Click);
            
            table.Controls.Add(buttonsPanel, 1, 2);
            

            table.ResumeLayout();

            return dialog;
        }


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
                // TODO: Automatic data binding.
                DataObject.Name = _nameTextBox.Text;
                DataObject.Description = _descriptionTextBox.Text;

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
