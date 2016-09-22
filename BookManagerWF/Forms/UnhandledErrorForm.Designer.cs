namespace BookManagerWF.Forms
{
    partial class UnhandledErrorForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.MessageTextBox = new System.Windows.Forms.TextBox();
            this.ShowDetailsButton = new System.Windows.Forms.Button();
            this.OkButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 72.35142F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.64858F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 95F));
            this.tableLayoutPanel1.Controls.Add(this.MessageTextBox, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.ShowDetailsButton, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.OkButton, 2, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 85.25641F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.74359F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(484, 312);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // MessageTextBox
            // 
            this.MessageTextBox.AcceptsReturn = true;
            this.tableLayoutPanel1.SetColumnSpan(this.MessageTextBox, 3);
            this.MessageTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MessageTextBox.Location = new System.Drawing.Point(3, 3);
            this.MessageTextBox.Multiline = true;
            this.MessageTextBox.Name = "MessageTextBox";
            this.MessageTextBox.ReadOnly = true;
            this.MessageTextBox.Size = new System.Drawing.Size(478, 259);
            this.MessageTextBox.TabIndex = 0;
            // 
            // ShowDetailsButton
            // 
            this.ShowDetailsButton.Location = new System.Drawing.Point(284, 273);
            this.ShowDetailsButton.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.ShowDetailsButton.Name = "ShowDetailsButton";
            this.ShowDetailsButton.Size = new System.Drawing.Size(75, 23);
            this.ShowDetailsButton.TabIndex = 1;
            this.ShowDetailsButton.Text = "Show details";
            this.ShowDetailsButton.UseVisualStyleBackColor = true;
            this.ShowDetailsButton.Click += new System.EventHandler(this.ShowDetailsButton_Click);
            // 
            // OkButton
            // 
            this.OkButton.Location = new System.Drawing.Point(391, 273);
            this.OkButton.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(75, 23);
            this.OkButton.TabIndex = 2;
            this.OkButton.Text = "OK";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // UnhandledErrorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 312);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "UnhandledErrorForm";
            this.Text = "Book Manager - Something is wrong...";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox MessageTextBox;
        private System.Windows.Forms.Button ShowDetailsButton;
        private System.Windows.Forms.Button OkButton;
    }
}