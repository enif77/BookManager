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

namespace BookManager.Forms
{
    using System;
    using System.Windows;
    using System.Windows.Input;

    using BookManager.DataObjects;


    /// <summary>
    /// Interaction logic for LookupsEditorWindow.xaml
    /// </summary>
    public partial class LookupsEditorWindow : Window
    {
        private bool _initialized = false;
        private readonly GlobalDataObject _gdo;


        public GlobalDataObject Gdo
        {
            get { return _gdo; }
        }


        public LookupsEditorWindow(GlobalDataObject gdo)
        {
            if (gdo == null) throw new ArgumentNullException("gdo");

            _gdo = gdo;

            InitializeComponent();

            Loaded += Window_Loaded;
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (_initialized) return;

            BookAuthorDataGrid.ItemsSource = Gdo.BookAuthorCollection;
            BookCategoryDataGrid.ItemsSource = Gdo.BookCategoryCollection;
            BookGenreDataGrid.ItemsSource = Gdo.BookGenreCollection;
            BookPlacementDataGrid.ItemsSource = Gdo.BookPlacementCollection;
            BookPublisherDataGrid.ItemsSource = Gdo.BookPublisherCollection;
            BookTypeDataGrid.ItemsSource = Gdo.BookTypeCollection;

            Update();

            _initialized = true;
        }


        private void Update()
        {
            if (BookAuthorTabItem.IsSelected)
            {
                UIShared.UpdateBookAuthorCollection(this, Gdo);
            }
            else if (BookCategoryTabItem.IsSelected)
            {
                UIShared.UpdateBookCategoryCollection(this, Gdo);
            }
            else if (BookGenreTabItem.IsSelected)
            {
                UIShared.UpdateBookGenreCollection(this, Gdo);
            }
            else if (BookPlacementTabItem.IsSelected)
            {
                UIShared.UpdateBookPlacementCollection(this, Gdo);
            }
            else if (BookPublisherTabItem.IsSelected)
            {
                UIShared.UpdateBookPublisherCollection(this, Gdo);
            }
            else if (BookTypeTabItem.IsSelected)
            {
                UIShared.UpdateBookTypeCollection(this, Gdo);
            }
        }


        #region Buttons

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            Update();
        }


        private void NewItemButton_Click(object sender, RoutedEventArgs e)
        {
            if (BookAuthorTabItem.IsSelected)
            {
                EditBookAuthor(true);
            }
            else if (BookCategoryTabItem.IsSelected)
            {
                EditBookCategory(true);
            }
            else if (BookGenreTabItem.IsSelected)
            {
                EditBookGenre(true);
            }
            else if (BookPlacementTabItem.IsSelected)
            {
                EditBookPlacement(true);
            }
            else if (BookPublisherTabItem.IsSelected)
            {
                EditBookPublisher(true);
            }
            else if (BookTypeTabItem.IsSelected)
            {
                EditBookType(true);
            }
        }


        private void EditItemButton_Click(object sender, RoutedEventArgs e)
        {
            if (BookAuthorTabItem.IsSelected)
            {
                EditBookAuthor();
            }
            else if (BookCategoryTabItem.IsSelected)
            {
                EditBookCategory();
            }
            else if (BookGenreTabItem.IsSelected)
            {
                EditBookGenre();
            }
            else if (BookPlacementTabItem.IsSelected)
            {
                EditBookPlacement();
            }
            else if (BookPublisherTabItem.IsSelected)
            {
                EditBookPublisher();
            }
            else if (BookTypeTabItem.IsSelected)
            {
                EditBookType();
            }
        }


        private void DeleteItemButton_Click(object sender, RoutedEventArgs e)
        {
            if (BookAuthorTabItem.IsSelected)
            {
                DeleteBookAuthor();
            }
            else if (BookCategoryTabItem.IsSelected)
            {
                DeleteBookCategory();
            }
            else if (BookGenreTabItem.IsSelected)
            {
                DeleteBookGenre();
            }
            else if (BookPlacementTabItem.IsSelected)
            {
                DeleteBookPlacement();
            }
            else if (BookPublisherTabItem.IsSelected)
            {
                DeleteBookPublisher();
            }
            else if (BookTypeTabItem.IsSelected)
            {
                DeleteBookType();
            }
        }

        #endregion


        #region BookAuthor

        private void EditBookAuthor(bool isNew = false)
        {
            UIShared.EditBookAuthor(this, Gdo, BookAuthorDataGrid.SelectedItem, isNew);
        }


        private void DeleteBookAuthor()
        {
            UIShared.DeleteBookAuthor(this, BookAuthorDataGrid, Gdo);
        }


        private void BookAuthorTabItem_Loaded(object sender, RoutedEventArgs e)
        {
            UIShared.UpdateBookAuthorCollection(this, Gdo);
        }


        private void BookAuthorDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            EditBookAuthor();
        }

        #endregion


        #region BookCategory

        private void EditBookCategory(bool isNew = false)
        {
            UIShared.EditBookCategory(this, Gdo, BookCategoryDataGrid.SelectedItem, isNew);
        }


        private void DeleteBookCategory()
        {
            UIShared.DeleteBookCategory(this, BookCategoryDataGrid, Gdo);
        }


        private void BookCategoryTabItem_Loaded(object sender, RoutedEventArgs e)
        {
            UIShared.UpdateBookCategoryCollection(this, Gdo);
        }


        private void BookCategoryDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            EditBookCategory();
        }

        #endregion


        #region BookGenre

        private void EditBookGenre(bool isNew = false)
        {
            UIShared.EditBookGenre(this, Gdo, BookGenreDataGrid.SelectedItem, isNew);
        }


        private void DeleteBookGenre()
        {
            UIShared.DeleteBookGenre(this, BookGenreDataGrid, Gdo);
        }


        private void BookGenreTabItem_Loaded(object sender, RoutedEventArgs e)
        {
            UIShared.UpdateBookGenreCollection(this, Gdo);
        }


        private void BookGenreDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            EditBookGenre();
        }

        #endregion


        #region BookPlacement

        private void EditBookPlacement(bool isNew = false)
        {
            UIShared.EditBookPlacement(this, Gdo, BookPlacementDataGrid.SelectedItem, isNew);
        }


        private void DeleteBookPlacement()
        {
            UIShared.DeleteBookPlacement(this, BookPlacementDataGrid, Gdo);
        }


        private void BookPlacementTabItem_Loaded(object sender, RoutedEventArgs e)
        {
            UIShared.UpdateBookPlacementCollection(this, Gdo);
        }


        private void BookPlacementDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            EditBookPlacement();
        }

        #endregion


        #region BookPublisher

        private void EditBookPublisher(bool isNew = false)
        {
            UIShared.EditBookPublisher(this, Gdo, BookPublisherDataGrid.SelectedItem, isNew);
        }


        private void DeleteBookPublisher()
        {
            UIShared.DeleteBookPublisher(this, BookPublisherDataGrid, Gdo);
        }


        private void BookPublisherTabItem_Loaded(object sender, RoutedEventArgs e)
        {
            UIShared.UpdateBookPublisherCollection(this, Gdo);
        }


        private void BookPublisherDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            EditBookPublisher();
        }

        #endregion


        #region BookType

        private void EditBookType(bool isNew = false)
        {
            UIShared.EditBookType(this, Gdo, BookTypeDataGrid.SelectedItem, isNew);
        }


        private void DeleteBookType()
        {
            UIShared.DeleteBookType(this, BookTypeDataGrid, Gdo);
        }


        private void BookTypeTabItem_Loaded(object sender, RoutedEventArgs e)
        {
            UIShared.UpdateBookTypeCollection(this, Gdo);
        }


        private void BookTypeDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            EditBookType();
        }

        #endregion

    }
}
