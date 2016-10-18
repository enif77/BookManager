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
            // TODO: Implement UI.
        }

    }
}
