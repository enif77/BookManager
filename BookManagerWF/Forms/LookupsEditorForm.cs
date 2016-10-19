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

    using BookManager.DataLayer;
    using Injektor;

    public class LookupsEditorForm : Form
    {
        private bool _initialized = false;
        private readonly GlobalDataObject _gdo;


        public GlobalDataObject Gdo
        {
            get { return _gdo; }
        }


        public LookupsEditorForm(GlobalDataObject gdo)
        {
            if (gdo == null) throw new ArgumentNullException("gdo");

            _gdo = gdo;

            InitializeComponent();

            Load += Window_Load;
        }


        private void Window_Load(object sender, EventArgs e)
        {
            if (_initialized) return;

            //BookAuthorDataGrid.ItemsSource = Gdo.BookAuthorCollection;
            //BookCategoryDataGrid.ItemsSource = Gdo.BookCategoryCollection;
            //BookGenreDataGrid.ItemsSource = Gdo.BookGenreCollection;
            //BookPlacementDataGrid.ItemsSource = Gdo.BookPlacementCollection;
            //BookPublisherDataGrid.ItemsSource = Gdo.BookPublisherCollection;
            //BookTypeDataGrid.ItemsSource = Gdo.BookTypeCollection;

            Update();

            _initialized = true;
        }
        

        private void InitializeComponent()
        {
            SuspendLayout();

            Owner = Registry.Get<MainForm>();
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.SizableToolWindow;
            AutoSize = false;
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1024, 800);
            Text = "Book Manager - Lookups Editor";


            var tabContror = new TabControl()
            {
                Dock = DockStyle.Fill,
                SizeMode = TabSizeMode.Fixed,
                Name = "tabContror",
                SelectedIndex = 0,
                TabIndex = 0
            };
            
            tabContror.SuspendLayout();
                
                        
            CreateBookAuthorTabPage(tabContror);
            CreateBookCategoryTabPage(tabContror);
                       
            

            Controls.Add(tabContror);

            tabContror.ResumeLayout(false);

            ResumeLayout(false);
        }
        

        private void CreateBookAuthorTabPage(TabControl tabContror)
        {
            var bookAuthorTabPage = new TabPage()
            {
                Name = "bookAuthorTabPage",
                TabIndex = 0,
                Text = "Book Authors",
                UseVisualStyleBackColor = true
            };

            bookAuthorTabPage.SuspendLayout();

            bookAuthorTabPage.Controls.Add(new Button()
            {
                Text = "Tab 1 button 1",
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink
            });

            bookAuthorTabPage.ResumeLayout(false);

            tabContror.Controls.Add(bookAuthorTabPage);
        }
        

        private void CreateBookCategoryTabPage(TabControl tabContror)
        {
            var bookCathegoryTabPage = new TabPage()
            {
                Name = "bookAuthorTabPage",
                TabIndex = 1,
                Text = "Book Cathegories",
                UseVisualStyleBackColor = true
            };

            bookCathegoryTabPage.SuspendLayout();

            var bcFlo = new FlowLayoutPanel()
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                AutoScrollMinSize = new System.Drawing.Size(800, 480),
                //AutoScrollMargin = new System.Drawing.Size(640, 480),
            };


            bcFlo.SuspendLayout();

            bcFlo.Controls.Add(new Button()
            {
                Text = "Tab 2 button 1",
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink
            });
            bcFlo.Controls.Add(new Button()
            {
                Text = "Tab 2 button 2",
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink
            });
            bcFlo.Controls.Add(new Button()
            {
                Text = "Tab 2 button 3",
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink
            });
            bcFlo.Controls.Add(new Button()
            {
                Text = "Tab 2 button 4",
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink
            });
            bcFlo.Controls.Add(new Button()
            {
                Text = "Tab 2 button 5",
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink
            });
            
            bookCathegoryTabPage.Controls.Add(bcFlo);

            bcFlo.ResumeLayout(false);
            bookCathegoryTabPage.ResumeLayout(false);

            tabContror.Controls.Add(bookCathegoryTabPage);
        }

    }
}
