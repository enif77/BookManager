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
    using System.Windows.Controls;
    using System.Windows.Threading;

    using SimpleDb.Shared;

    using BookManager.DataObjects;


    public static class UIShared
    {
        #region BookAuthor

        public static BookAuthor EditBookAuthor(Window owner, GlobalDataObject gdo, object source, bool isNew = false, bool updateColection = true)
        {
            var entity = GetEntityForEditation<BookAuthor>(owner, source, gdo.GetBookAuthor, isNew);
            if (entity == null)
            {                                               
                return null;
            }
                
            if (BookAuthorEditorWindow.Open(entity))
            {
                gdo.SaveBookAuthor(entity);

                if (updateColection) UpdateBookAuthorCollection(owner, gdo);
            }

            return entity;
        }


        public static void DeleteBookAuthor(Window owner, DataGrid dataGrid, GlobalDataObject gdo)
        {
            DeleteSelectedItems<BookAuthor>(owner, dataGrid, gdo, gdo.DeleteBookAuthor, UpdateBookAuthorCollection);
        }


        public static void UpdateBookAuthorCollection(DispatcherObject dob, GlobalDataObject gdo)
        {
            dob.InvokeIfRequired(() =>
            {
                gdo.UpdateBookAuthorCollection();
            }, DispatcherPriority.Render);
        }

        #endregion


        #region BookCategory

        public static BookCategory EditBookCategory(Window owner, GlobalDataObject gdo, object source, bool isNew = false, bool updateColection = true)
        {
            var entity = GetEntityForEditation<BookCategory>(owner, source, gdo.GetBookCategory, isNew);
            if (entity == null)
            {
                return null;
            }

            if (BookCategoryEditorWindow.Open(entity))
            {
                gdo.SaveBookCategory(entity);

                if (updateColection) UpdateBookCategoryCollection(owner, gdo);
            }

            return entity;
        }


        public static void DeleteBookCategory(Window owner, DataGrid dataGrid, GlobalDataObject gdo)
        {
            DeleteSelectedItems<BookCategory>(owner, dataGrid, gdo, gdo.DeleteBookCategory, UpdateBookCategoryCollection);
        }


        public static void UpdateBookCategoryCollection(DispatcherObject dob, GlobalDataObject gdo)
        {
            dob.InvokeIfRequired(() =>
            {
                gdo.UpdateBookCategoryCollection();
            }, DispatcherPriority.Render);
        }

        #endregion


        #region BookGenre

        public static BookGenre EditBookGenre(Window owner, GlobalDataObject gdo, object source, bool isNew = false, bool updateColection = true)
        {
            var entity = GetEntityForEditation<BookGenre>(owner, source, gdo.GetBookGenre, isNew);
            if (entity == null)
            {
                return null;
            }

            if (BookGenreEditorWindow.Open(entity))
            {
                gdo.SaveBookGenre(entity);

                if (updateColection) UpdateBookGenreCollection(owner, gdo);
            }

            return entity;
        }


        public static void DeleteBookGenre(Window owner, DataGrid dataGrid, GlobalDataObject gdo)
        {
            DeleteSelectedItems<BookGenre>(owner, dataGrid, gdo, gdo.DeleteBookGenre, UpdateBookGenreCollection);
        }


        public static void UpdateBookGenreCollection(DispatcherObject dob, GlobalDataObject gdo)
        {
            dob.InvokeIfRequired(() =>
            {
                gdo.UpdateBookGenreCollection();
            }, DispatcherPriority.Render);
        }

        #endregion


        #region BookPlacement

        public static BookPlacement EditBookPlacement(Window owner, GlobalDataObject gdo, object source, bool isNew = false, bool updateColection = true)
        {
            var entity = GetEntityForEditation<BookPlacement>(owner, source, gdo.GetBookPlacement, isNew);
            if (entity == null)
            {
                return null;
            }

            if (BookPlacementEditorWindow.Open(entity))
            {
                gdo.SaveBookPlacement(entity);

                if (updateColection) UpdateBookPlacementCollection(owner, gdo);
            }

            return entity;
        }


        public static void DeleteBookPlacement(Window owner, DataGrid dataGrid, GlobalDataObject gdo)
        {
            DeleteSelectedItems<BookPlacement>(owner, dataGrid, gdo, gdo.DeleteBookPlacement, UpdateBookPlacementCollection);
        }


        public static void UpdateBookPlacementCollection(DispatcherObject dob, GlobalDataObject gdo)
        {
            dob.InvokeIfRequired(() =>
            {
                gdo.UpdateBookPlacementCollection();
            }, DispatcherPriority.Render);
        }

        #endregion


        #region BookPublisher

        public static BookPublisher EditBookPublisher(Window owner, GlobalDataObject gdo, object source, bool isNew = false, bool updateColection = true)
        {
            var entity = GetEntityForEditation<BookPublisher>(owner, source, gdo.GetBookPublisher, isNew);
            if (entity == null)
            {
                return null;
            }

            if (BookPublisherEditorWindow.Open(entity))
            {
                gdo.SaveBookPublisher(entity);

                if (updateColection) UpdateBookPublisherCollection(owner, gdo);
            }

            return entity; 
        }
                            

        public static void DeleteBookPublisher(Window owner, DataGrid dataGrid, GlobalDataObject gdo)
        {
            DeleteSelectedItems<BookPublisher>(owner, dataGrid, gdo, gdo.DeleteBookPublisher, UpdateBookPublisherCollection);
        }


        public static void UpdateBookPublisherCollection(DispatcherObject dob, GlobalDataObject gdo)
        {
            dob.InvokeIfRequired(() =>
            {
                gdo.UpdateBookPublisherCollection();
            }, DispatcherPriority.Render);
        }

        #endregion


        #region BookType

        public static BookType EditBookType(Window owner, GlobalDataObject gdo, object source, bool isNew = false, bool updateColection = true)
        {
            var entity = GetEntityForEditation<BookType>(owner, source, gdo.GetBookType, isNew);
            if (entity == null)
            {
                return null;
            }

            if (BookTypeEditorWindow.Open(entity))
            {
                gdo.SaveBookType(entity);

                if (updateColection) UpdateBookTypeCollection(owner, gdo);
            }

            return entity;
        }
                                            

        public static void DeleteBookType(Window owner, DataGrid dataGrid, GlobalDataObject gdo)
        {
            DeleteSelectedItems<BookType>(owner, dataGrid, gdo, gdo.DeleteBookType, UpdateBookTypeCollection);
        }


        public static void UpdateBookTypeCollection(DispatcherObject dob, GlobalDataObject gdo)
        {
            dob.InvokeIfRequired(() =>
            {
                gdo.UpdateBookTypeCollection();
            }, DispatcherPriority.Render);
        }

        #endregion


        #region common

        private static T GetEntityForEditation<T>(Window owner, object source, Func<int, T> getMethod, bool isNew)
            where T : class, IId, new()
        {
            T entity;

            if (isNew)
            {
                entity = new T();
            }
            else
            {
                var item = source as T;
                if (item == null)
                {
                    MessageBox.Show(owner, "Select an item for editation.", "Book Manager - Notice", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                    return null;
                }

                if (item.Id == 0)
                {
                    MessageBox.Show(owner, "This item can not be edited.", "Book Manager - Notice", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                    return null;
                }

                entity = getMethod(item.Id);
                if (entity == null)
                {
                    MessageBox.Show(owner, "The selected item can not be read for editation.", "Book Manager - Error", MessageBoxButton.OK, MessageBoxImage.Error);

                    return null;
                }
            }

            return entity;
        }


        private static void DeleteSelectedItems<T>(Window owner, DataGrid dataGrid, GlobalDataObject gdo, Action<T> deleteMethod, Action<DispatcherObject, GlobalDataObject> updateCollectionMethod)
            where T : class, IId, new()
        {
            if (dataGrid.SelectedItems.Count == 0)
            {
                MessageBox.Show(owner, "No item selected.", "Book Manager - Notice", MessageBoxButton.OK);

                return;
            }

            if (MessageBox.Show(owner, "Delete selected items?", "Book Manager - Items Deletion", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                foreach (var item in dataGrid.SelectedItems)
                {
                    var entity = item as T;
                    if (entity == null || entity.Id == 0)
                    {
                        MessageBox.Show("An item can not be deleted. Does not exists or has ID = 0.", "Art Manager - Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                        continue;
                    }

                    deleteMethod(entity);
                }

                updateCollectionMethod(owner, gdo);
            }
        }

        #endregion
    }
}
