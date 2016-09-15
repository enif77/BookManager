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

namespace BookManager.DataObjects
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;

    using Injektor;

    using BookManager.Core;
    using BookManager.DataLayer;


    public class GlobalDataObject :
        ICollectionSynchronizer<BookDataGridItem>,
        ICollectionSynchronizer<BookAuthor>,
        ICollectionSynchronizer<BookCategory>,
        ICollectionSynchronizer<BookGenre>,
        ICollectionSynchronizer<BookPlacement>,
        ICollectionSynchronizer<BookPublisher>,
        ICollectionSynchronizer<BookType>,
        INotifyPropertyChanged
    {
        private readonly BookFilter _filter = new BookFilter();

        private readonly ExtendedObservableCollection<BookDataGridItem> _itemsCollection;
        private readonly ExtendedObservableCollection<BookAuthor> _bookAuthorCollection;
        private readonly ExtendedObservableCollection<BookCategory> _bookCategoryCollection;
        private readonly ExtendedObservableCollection<BookGenre> _bookGenreCollection;
        private readonly ExtendedObservableCollection<BookPlacement> _bookPlacementCollection;
        private readonly ExtendedObservableCollection<BookPublisher> _bookPublisherCollection;
        private readonly ExtendedObservableCollection<BookType> _bookTypeCollection;


        #region Properties

        public BookFilter Filter
        {
            get { return _filter; }
        }


        public ExtendedObservableCollection<BookDataGridItem> BookDataGridItems
        {
            get { return _itemsCollection; }
        }


        public ExtendedObservableCollection<BookAuthor> BookAuthorCollection
        {
            get { return _bookAuthorCollection; }
        }


        public ExtendedObservableCollection<BookCategory> BookCategoryCollection
        {
            get { return _bookCategoryCollection; }
        }


        public ExtendedObservableCollection<BookGenre> BookGenreCollection
        {
            get { return _bookGenreCollection; }
        }


        public ExtendedObservableCollection<BookPlacement> BookPlacementCollection
        {
            get { return _bookPlacementCollection; }
        }


        public ExtendedObservableCollection<BookPublisher> BookPublisherCollection
        {
            get { return _bookPublisherCollection; }
        }


        public ExtendedObservableCollection<BookType> BookTypeCollection
        {
            get { return _bookTypeCollection; }
        }

        #endregion


        #region ctor

        public GlobalDataObject()
        {
            _itemsCollection = new ExtendedObservableCollection<BookDataGridItem>();
            _bookAuthorCollection = new ExtendedObservableCollection<BookAuthor>();
            _bookCategoryCollection = new ExtendedObservableCollection<BookCategory>();
            _bookGenreCollection = new ExtendedObservableCollection<BookGenre>();
            _bookPlacementCollection = new ExtendedObservableCollection<BookPlacement>();
            _bookPublisherCollection = new ExtendedObservableCollection<BookPublisher>();
            _bookTypeCollection = new ExtendedObservableCollection<BookType>();
        }

        #endregion


        #region Implementation of INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;


        /// <summary>
        /// Invoke property changed event
        /// </summary>
        /// <param name="propertyName">Property name</param>
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion


        #region Collection Updaters

        public ExtendedObservableCollection<BookDataGridItem> UpdateBookCollection()
        {
            _itemsCollection.SynchronizeWith(new Collection<BookDataGridItem>(Registry.Get<BookDataLayer>().GetAll(Filter)), this);

            return BookDataGridItems;
        }


        public ExtendedObservableCollection<BookAuthor> UpdateBookAuthorCollection()
        {
            _bookAuthorCollection.SynchronizeWith(new Collection<BookAuthor>(Registry.Get<BookAuthorDataLayer>().GetAll().ToList()), this);

            return BookAuthorCollection;
        }


        public ExtendedObservableCollection<BookCategory> UpdateBookCategoryCollection()
        {
            _bookCategoryCollection.SynchronizeWith(new Collection<BookCategory>(Registry.Get<BookCategoryDataLayer>().GetAll().ToList()), this);

            return BookCategoryCollection;
        }


        public ExtendedObservableCollection<BookGenre> UpdateBookGenreCollection()
        {
            _bookGenreCollection.SynchronizeWith(new Collection<BookGenre>(Registry.Get<BookGenreDataLayer>().GetAll().ToList()), this);

            return BookGenreCollection;
        }


        public ExtendedObservableCollection<BookPlacement> UpdateBookPlacementCollection()
        {
            _bookPlacementCollection.SynchronizeWith(new Collection<BookPlacement>(Registry.Get<BookPlacementDataLayer>().GetAll().ToList()), this);

            return BookPlacementCollection;
        }

                                                 
        public ExtendedObservableCollection<BookPublisher> UpdateBookPublisherCollection()
        {
            _bookPublisherCollection.SynchronizeWith(new Collection<BookPublisher>(Registry.Get<BookPublisherDataLayer>().GetAll().ToList()), this);

            return BookPublisherCollection;
        }


        public ExtendedObservableCollection<BookType> UpdateBookTypeCollection()
        {
            _bookTypeCollection.SynchronizeWith(new Collection<BookType>(Registry.Get<BookTypeDataLayer>().GetAll().ToList()), this);

            return BookTypeCollection;
        }

        #endregion


        #region Data Operators

        public BookAuthor GetBookAuthor(int id)
        {
            return Registry.Get<BookAuthorDataLayer>().Get(id);
        }

        public void SaveBookAuthor(BookAuthor obj)
        {
            Registry.Get<BookAuthorDataLayer>().Save(obj);
        }

        public void DeleteBookAuthor(BookAuthor obj)
        {
            Registry.Get<BookAuthorDataLayer>().Delete(obj);
        }


        public BookCategory GetBookCategory(int id)
        {
            return Registry.Get<BookCategoryDataLayer>().Get(id);
        }

        public void SaveBookCategory(BookCategory obj)
        {
            Registry.Get<BookCategoryDataLayer>().Save(obj);
        }

        public void DeleteBookCategory(BookCategory obj)
        {
            Registry.Get<BookCategoryDataLayer>().Delete(obj);
        }


        public BookGenre GetBookGenre(int id)
        {
            return Registry.Get<BookGenreDataLayer>().Get(id);
        }

        public void SaveBookGenre(BookGenre obj)
        {
            Registry.Get<BookGenreDataLayer>().Save(obj);
        }

        public void DeleteBookGenre(BookGenre obj)
        {
            Registry.Get<BookGenreDataLayer>().Delete(obj);
        }


        public BookPlacement GetBookPlacement(int id)
        {
            return Registry.Get<BookPlacementDataLayer>().Get(id);
        }

        public void SaveBookPlacement(BookPlacement obj)
        {
            Registry.Get<BookPlacementDataLayer>().Save(obj);
        }

        public void DeleteBookPlacement(BookPlacement obj)
        {
            Registry.Get<BookPlacementDataLayer>().Delete(obj);
        }


        public BookPublisher GetBookPublisher(int id)
        {
            return Registry.Get<BookPublisherDataLayer>().Get(id);
        }

        public void SaveBookPublisher(BookPublisher obj)
        {
            Registry.Get<BookPublisherDataLayer>().Save(obj);
        }

        public void DeleteBookPublisher(BookPublisher obj)
        {
            Registry.Get<BookPublisherDataLayer>().Delete(obj);
        }


        public BookType GetBookType(int id)
        {
            return Registry.Get<BookTypeDataLayer>().Get(id);
        }

        public void SaveBookType(BookType obj)
        {
            Registry.Get<BookTypeDataLayer>().Save(obj);
        }

        public void DeleteBookType(BookType obj)
        {
            Registry.Get<BookTypeDataLayer>().Delete(obj);
        }

        #endregion


        #region Implementation of ICollectionSynchronizer<BookDataGridItem>

        /// <summary>
        /// Compares two entities and returns their natural order.
        /// </summary>
        /// <param name="a">A source entity (an entity in this collection).</param>
        /// <param name="b">A target entity (an entity in the other collection).</param>
        /// <returns>-1, 0, 1.</returns>
        public int GetOrder(BookDataGridItem a, BookDataGridItem b)
        {
            return a.Id.CompareTo(b.Id);
        }

        /// <summary>
        /// Compares two entities and detects, if they are the same or not.
        /// </summary>
        /// <param name="a">A source entity (an entity in this collection).</param>
        /// <param name="b">A target entity (an entity in the other collection).</param>
        /// <returns>True if both entities are the same.</returns>
        public bool NeedsUpdate(BookDataGridItem a, BookDataGridItem b)
        {
            return a.NeedsUpdate(b);
        }

        #endregion


        #region Implementation of ICollectionSynchronizer<BookAuthor>

        /// <summary>
        /// Compares two entities and returns their natural order.
        /// </summary>
        /// <param name="a">A source entity (an entity in this collection).</param>
        /// <param name="b">A target entity (an entity in the other collection).</param>
        /// <returns>-1, 0, 1.</returns>
        public int GetOrder(BookAuthor a, BookAuthor b)
        {
            return a.Id.CompareTo(b.Id);
        }

        /// <summary>
        /// Compares two entities and detects, if they are the same or not.
        /// </summary>
        /// <param name="a">A source entity (an entity in this collection).</param>
        /// <param name="b">A target entity (an entity in the other collection).</param>
        /// <returns>True if both entities are the same.</returns>
        public bool NeedsUpdate(BookAuthor a, BookAuthor b)
        {
            return a.NeedsUpdate(b);
        }

        #endregion


        #region Implementation of ICollectionSynchronizer<BookCategory>

        /// <summary>
        /// Compares two entities and returns their natural order.
        /// </summary>
        /// <param name="a">A source entity (an entity in this collection).</param>
        /// <param name="b">A target entity (an entity in the other collection).</param>
        /// <returns>-1, 0, 1.</returns>
        public int GetOrder(BookCategory a, BookCategory b)
        {
            return a.Id.CompareTo(b.Id);
        }

        /// <summary>
        /// Compares two entities and detects, if they are the same or not.
        /// </summary>
        /// <param name="a">A source entity (an entity in this collection).</param>
        /// <param name="b">A target entity (an entity in the other collection).</param>
        /// <returns>True if both entities are the same.</returns>
        public bool NeedsUpdate(BookCategory a, BookCategory b)
        {
            return a.NeedsUpdate(b);
        }

        #endregion


        #region Implementation of ICollectionSynchronizer<BookGenre>

        /// <summary>
        /// Compares two entities and returns their natural order.
        /// </summary>
        /// <param name="a">A source entity (an entity in this collection).</param>
        /// <param name="b">A target entity (an entity in the other collection).</param>
        /// <returns>-1, 0, 1.</returns>
        public int GetOrder(BookGenre a, BookGenre b)
        {
            return a.Id.CompareTo(b.Id);
        }

        /// <summary>
        /// Compares two entities and detects, if they are the same or not.
        /// </summary>
        /// <param name="a">A source entity (an entity in this collection).</param>
        /// <param name="b">A target entity (an entity in the other collection).</param>
        /// <returns>True if both entities are the same.</returns>
        public bool NeedsUpdate(BookGenre a, BookGenre b)
        {
            return a.NeedsUpdate(b);
        }

        #endregion


        #region Implementation of ICollectionSynchronizer<BookPlacement>

        /// <summary>
        /// Compares two entities and returns their natural order.
        /// </summary>
        /// <param name="a">A source entity (an entity in this collection).</param>
        /// <param name="b">A target entity (an entity in the other collection).</param>
        /// <returns>-1, 0, 1.</returns>
        public int GetOrder(BookPlacement a, BookPlacement b)
        {
            return a.Id.CompareTo(b.Id);
        }

        /// <summary>
        /// Compares two entities and detects, if they are the same or not.
        /// </summary>
        /// <param name="a">A source entity (an entity in this collection).</param>
        /// <param name="b">A target entity (an entity in the other collection).</param>
        /// <returns>True if both entities are the same.</returns>
        public bool NeedsUpdate(BookPlacement a, BookPlacement b)
        {
            return a.NeedsUpdate(b);
        }

        #endregion


        #region Implementation of ICollectionSynchronizer<BookPublisher>

        /// <summary>
        /// Compares two entities and returns their natural order.
        /// </summary>
        /// <param name="a">A source entity (an entity in this collection).</param>
        /// <param name="b">A target entity (an entity in the other collection).</param>
        /// <returns>-1, 0, 1.</returns>
        public int GetOrder(BookPublisher a, BookPublisher b)
        {
            return a.Id.CompareTo(b.Id);
        }

        /// <summary>
        /// Compares two entities and detects, if they are the same or not.
        /// </summary>
        /// <param name="a">A source entity (an entity in this collection).</param>
        /// <param name="b">A target entity (an entity in the other collection).</param>
        /// <returns>True if both entities are the same.</returns>
        public bool NeedsUpdate(BookPublisher a, BookPublisher b)
        {
            return a.NeedsUpdate(b);
        }

        #endregion


        #region Implementation of ICollectionSynchronizer<BookType>

        /// <summary>
        /// Compares two entities and returns their natural order.
        /// </summary>
        /// <param name="a">A source entity (an entity in this collection).</param>
        /// <param name="b">A target entity (an entity in the other collection).</param>
        /// <returns>-1, 0, 1.</returns>
        public int GetOrder(BookType a, BookType b)
        {
            return a.Id.CompareTo(b.Id);
        }

        /// <summary>
        /// Compares two entities and detects, if they are the same or not.
        /// </summary>
        /// <param name="a">A source entity (an entity in this collection).</param>
        /// <param name="b">A target entity (an entity in the other collection).</param>
        /// <returns>True if both entities are the same.</returns>
        public bool NeedsUpdate(BookType a, BookType b)
        {
            return a.NeedsUpdate(b);
        }

        #endregion
    }
}
