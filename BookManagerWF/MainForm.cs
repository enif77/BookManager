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

using BookManagerWF.Forms;

namespace BookManagerWF
{
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;

    using Registry = Injektor.Registry;

    using BookManager.DataLayer;
    using BookManager.DataObjects;


    public partial class MainForm : Form
    {
        private bool _initialized = false;
        private readonly GlobalDataObject _gdo;


        public GlobalDataObject Gdo
        {
            get { return _gdo; }
        }


        public BookFilter Filter
        {
            get { return _gdo.Filter; }
        }


        public MainForm()
        {
            _gdo = new GlobalDataObject();

            InitializeComponent();

            Load += MainWindow_Load;
            Closing += MainWindow_Closing;

            Registry.RegisterInstance(this);

            //BookAuthorComboBox.ItemsSource = Gdo.UpdateBookAuthorCollection();
            //BookPlacementComboBox.ItemsSource = Gdo.UpdateBookPlacementCollection();
        }


        private void MainWindow_Load(object sender, EventArgs e)
        {
            if (_initialized) return;

            var config = Program.Config;

            if (config.IsMaximized) WindowState = FormWindowState.Maximized;

            Top = config.Top;
            Left = config.Left;
            Width = config.Width;
            Height = config.Height;

            //BookDataGrid.Loaded += BookDataGrid_Loaded;

            //DataContext = Filter;
            //BookDataGrid.ItemsSource = Gdo.BookDataGridItems;

            //Update();

            _initialized = true;
        }


        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            // TODO: Zeptat se, zda uživatel chce zavřít okno.
            // TODO: Uložit datagrid column info.

            // Save base configuration values.
            Program.SaveAppConfig(this);

            e.Cancel = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BookTypeEditorForm.Open(new BookType());
        }
    }
}
