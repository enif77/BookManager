﻿/* BookManager - (C) 2016 Premysl Fara 
 
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
    using System.Drawing;
    using System.Windows.Forms;

    using Injektor;

    using BookManagerWF.Forms.Controls;


    public abstract class BaseEditorForm : Form
    {
        #region constants

        public const int DefaultTextBoxMinCharsPerLine = 30;
        public const int DefaultMultilineTextBoxMinLines = 8;

        #endregion


        #region properties

        /// <summary>
        /// A data object.
        /// </summary>
        public object DataObject
        {
            get;
            set;
        }

        /// <summary>
        /// An editor title used for this data object.
        /// </summary>
        public string DataObjectTitle
        {
            get;
            set;
        }

        #endregion


        #region ctor

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="dataObject">A data object.</param>
        /// <param name="dataObjectTitle">A title used for a data object of this editor or null.</param>
        protected BaseEditorForm(object dataObject, string dataObjectTitle = null)
        {
            if (dataObject == null) throw new ArgumentNullException("dataObject");
            
            Owner = Registry.Get<MainForm>();
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            AutoScaleMode = AutoScaleMode.Font;
            Text = "Book Manager - Editor";


            // TODO: Move it to global constants.
            ControlsMargin = Font.Height / 2;
            ControlsMinWidth100 = new Size(100, 1);


            InitializeComponent();

            DataObject = dataObject;
            DataObjectTitle = dataObjectTitle ?? "Object";

            DialogResult = DialogResult.OK;
        }

        #endregion


        #region public methods
        
        /// <summary>
        /// Loads data trom the data object to controls of this editor.
        /// </summary>
        public abstract void LoadDataToControls();

        /// <summary>
        /// Saves data from controls of this editor into the data object.
        /// </summary>
        public abstract void SaveDataToDataObject();

        /// <summary>
        /// Validates the data object.
        /// </summary>
        public abstract void ValidateDataObject();

        #endregion

        
        #region protected

        /// <summary>
        /// Builds and initializes UI of this editor.
        /// </summary>
        protected abstract void InitializeComponent();

        /// <summary>
        /// Creates the main layout table.
        /// </summary>
        /// <param name="rowCount">A number of desired rows in this table. One by default and as the minimum.</param>
        /// <param name="columnCount">A number of desired columns in this table. Two by default and as the minimum.</param>
        /// <returns></returns>
        protected TableLayoutPanel CreateLayoutTable(int rowCount, int columnCount = 2)
        {
            if (rowCount < 1) throw new ArgumentException("The rowCount is too small.");
            if (columnCount < 2) throw new ArgumentException("The columnCount is too small.");

            return new TableLayoutPanel()
            {
                Parent = this,
                Padding = new Padding(Font.Height),
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                ColumnCount = columnCount,
                RowCount = rowCount
            };
        }

        /// <summary>
        /// Creates a textbox with a label.
        /// </summary>
        /// <param name="table">A layout table.</param>
        /// <param name="row">A row, at which this textbox should be created.</param>
        /// <param name="label">A text of this TextBox label.</param>
        /// <param name="bindingName">A binding name or null.</param>
        /// <param name="minCharsPerLIne">A minimal number of characters per line.</param>
        /// <param name="minLines">A minimal number of lines.</param>
        /// <returns>A created TextBox instance.</returns>
        protected TextBox CreateTextboxWithLabel(TableLayoutPanel table, int row, string label, string bindingName = null, int minCharsPerLine = DefaultTextBoxMinCharsPerLine, int minLines = 1)
        {
            if (table == null) throw new ArgumentNullException("table");
            if (row < 0 || row > table.RowCount) throw new ArgumentException("The row is out of allowed rows range.");
            if (label == null) throw new ArgumentNullException("label");

            table.Controls.Add(new Label()
            {
                AutoSize = true,
                Margin = new Padding(ControlsMargin),
                Text = label,
                MinimumSize = ControlsMinWidth100
            }, 0, row);

            var multiline = minLines > 1;

            var textBox = new TextBox()
            {
                AcceptsReturn = multiline,
                Multiline = multiline,
                Margin = new Padding(ControlsMargin),
                Size = new Size(
                    Font.Height * minCharsPerLine,  // About N+ characters per line.
                    Font.Height * minLines)         // N lines.
            };

            if (String.IsNullOrWhiteSpace(bindingName) == false)
            {
                textBox.Name = bindingName;

                // TODO: Add this textbox to the bindable textboxes list.
            }

            table.Controls.Add(textBox, 1, row);

            return textBox;
        }

        /// <summary>
        /// Creates the default set of editor buttons.
        /// </summary>
        /// <param name="table">A layout table.</param>
        /// <param name="row">A row, at which this textbox should be created.</param>
        protected void CreateButtons(TableLayoutPanel table, int row)
        {
            var buttonsPanel = new FlowLayoutPanel()
            {
                Anchor = AnchorStyles.Right,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Margin = new Padding(Font.Height),
            };

            var saveOperationButton = new SaveButton(buttonsPanel, Font);
            saveOperationButton.Click += new EventHandler(SaveOperationButton_Click);

            var cancelOperationButton = new CancelButton(buttonsPanel, Font);
            cancelOperationButton.Click += new EventHandler(CancelButton_Click);

            table.Controls.Add(buttonsPanel, 1, row);
        }

        #endregion


        #region private

        protected readonly int ControlsMargin;
        protected readonly Size ControlsMinWidth100;


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
                SaveDataToDataObject();
                ValidateDataObject();
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

        #endregion
    }
}
