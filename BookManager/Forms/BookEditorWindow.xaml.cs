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

    using Injektor;

    using BookManager.DataObjects;


    /// <summary>
    /// Interaction logic for BookEditorWindow.xaml
    /// </summary>
    public partial class BookEditorWindow : Window
    {
        #region properties

        public DialogResultStateType DialogResultState { get; private set; }


        public Book DataObject
        {
            get { return DataContext as Book; }
        }


        public GlobalDataObject Gdo { get; set; }

        #endregion


        #region ctor

        public BookEditorWindow(GlobalDataObject gdo)
        {
            Gdo = gdo;

            InitializeComponent();

            if (!UIHelper.DesignMode)
            {
                BookAuthorComboBox.ItemsSource = Gdo.UpdateBookAuthorCollection();
                BookCategoryComboBox.ItemsSource = Gdo.UpdateBookCategoryCollection();
                BookGenreComboBox.ItemsSource = Gdo.UpdateBookGenreCollection();
                BookPublisherComboBox.ItemsSource = Gdo.UpdateBookPublisherCollection();
                BookTypeComboBox.ItemsSource = Gdo.UpdateBookTypeCollection();
                BookPlacementComboBox.ItemsSource = Gdo.UpdateBookPlacementCollection();
            }

            DialogResultState = DialogResultStateType.Ok;
        }

        #endregion


        #region public methods

        public static bool Open(GlobalDataObject gdo, Book dataObject)
        {
            if (gdo == null) throw new ArgumentNullException("gdo");
            if (dataObject == null) throw new ArgumentNullException("dataObject");

            var dialog = new BookEditorWindow(gdo)
            {
                DataContext = dataObject,
                Owner = Registry.Get<MainWindow>(),
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Title = (dataObject.Id <= 0)
                    ? "Book Manager - New Book"
                    : String.Format("Book Manager - Book Edit {0}", dataObject.Id)
            };

            dialog.ShowDialog();

            return dialog.DialogResult.GetValueOrDefault();
        }

        #endregion


        private void Save_OnClick(object sender, RoutedEventArgs e)
        {
            if (SaveClick())
            {
                Close();
            }
        }


        private void Cancel_OnClick(object sender, RoutedEventArgs e)
        {
            CancelClick();
            Close();
        }


        private bool SaveClick()
        {
            try
            {
                ((Book)DataContext).Validate();
            }
            catch (Exception ex)
            {
                UnhandledErrorWindow.Open(ex);

                return false;
            }

            DialogResult = true;
            DialogResultState = DialogResultStateType.Ok;

            return true;
        }


        private void CancelClick()
        {
            DialogResult = false;
            DialogResultState = DialogResultStateType.Cancel;
        }



        private void EditBookAuthor_OnClick(object sender, RoutedEventArgs e)
        {
            UIShared.EditBookAuthor(this, Gdo, BookAuthorComboBox.SelectedItem, false, false);
        }

        private void NewBookAuthor_OnClick(object sender, RoutedEventArgs e)
        {
            var entity = UIShared.EditBookAuthor(this, Gdo, null, true);
            if (entity != null)
            {
                DataObject.BookAuthorId = entity.Id;
            }
        }


        private void EditBookCategory_OnClick(object sender, RoutedEventArgs e)
        {
            UIShared.EditBookCategory(this, Gdo, BookCategoryComboBox.SelectedItem, false, false);
        }

        private void NewBookCategory_OnClick(object sender, RoutedEventArgs e)
        {
            var entity = UIShared.EditBookCategory(this, Gdo, null, true);
            if (entity != null)
            {
                DataObject.BookCategoryId = entity.Id;
            }
        }


        private void EditBookGenre_OnClick(object sender, RoutedEventArgs e)
        {
            UIShared.EditBookGenre(this, Gdo, BookGenreComboBox.SelectedItem, false, false);
        }

        private void NewBookGenre_OnClick(object sender, RoutedEventArgs e)
        {
            var entity = UIShared.EditBookGenre(this, Gdo, null, true);
            if (entity != null)
            {
                DataObject.BookGenreId = entity.Id;
            }
        }


        private void EditBookPublisher_OnClick(object sender, RoutedEventArgs e)
        {
            UIShared.EditBookPublisher(this, Gdo, BookPublisherComboBox.SelectedItem, false, false);
        }
                                   
        private void NewBookPublisher_OnClick(object sender, RoutedEventArgs e)
        {
            var entity = UIShared.EditBookPublisher(this, Gdo, null, true);
            if (entity != null)
            {
                DataObject.BookPublisherId = entity.Id;
            }
        }


        private void EditBookType_OnClick(object sender, RoutedEventArgs e)
        {
            UIShared.EditBookType(this, Gdo, BookTypeComboBox.SelectedItem, false, false);
        }
                                              
        private void NewBookType_OnClick(object sender, RoutedEventArgs e)
        {
            var entity = UIShared.EditBookType(this, Gdo, null, true);
            if (entity != null)
            {
                DataObject.BookTypeId = entity.Id;
            }
        }


        private void EditBookPlacement_OnClick(object sender, RoutedEventArgs e)
        {
            UIShared.EditBookPlacement(this, Gdo, BookPlacementComboBox.SelectedItem, false, false);
        }                        

        private void NewBookPlacement_OnClick(object sender, RoutedEventArgs e)
        {
            var entity = UIShared.EditBookPlacement(this, Gdo, null, true);
            if (entity != null)
            {
                DataObject.BookPlacementId = entity.Id;
            }
        }

    }
}
