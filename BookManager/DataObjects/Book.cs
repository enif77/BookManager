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
    using System;

    using SimpleDb.Shared;


    [DbTable("Book")]
    public sealed class Book : ALookupDataObject<Book>
    {
        #region fields

        // ID, Name, Description - ALookupDataObject.

        private int _bookAuthorId;
        private int _bookCathegoryId;
        private int _bookGenreId;
        private int _bookPublisherId;
        private int _bookTypeId;

        private int _yearOfPublication;
        private string _isbnCode;
        private int _bookPlacementId;
        private int _pagesCount;
        private string _dimensions;

        private int _purchasedWhenYear;
        private string _purchasedWhere;
        private decimal _purchasedPrice;
        
        private string _resourcesDir;
        private string _thumbnailName;

        #endregion


        #region ctor

        public Book()
        {
            IsbnCode = String.Empty;
            Dimensions = String.Empty;
            PurchasedWhere = String.Empty;
            ResourcesDir = String.Empty;
            ThumbnailName = String.Empty;
        }

        #endregion


        [DbColumn("BookAuthorId")]
        public int BookAuthorId
        {
            get { return _bookAuthorId; }
            set
            {
                if (_bookAuthorId != value)
                {
                    _bookAuthorId = value;
                    OnPropertyChanged("BookAuthorId");
                }
            }
        }

        [DbColumn("BookCathegoryId")]
        public int BookCathegoryId
        {
            get { return _bookCathegoryId; }
            set
            {
                if (_bookCathegoryId != value)
                {
                    _bookCathegoryId = value;
                    OnPropertyChanged("BookCathegoryId");
                }
            }
        }

        [DbColumn("BookGenreId")]
        public int BookGenreId
        {
            get { return _bookGenreId; }
            set
            {
                if (_bookGenreId != value)
                {
                    _bookGenreId = value;
                    OnPropertyChanged("BookGenreId");
                }
            }
        }

        [DbColumn("BookPublisherId")]
        public int BookPublisherId
        {
            get { return _bookPublisherId; }
            set
            {
                if (_bookPublisherId != value)
                {
                    _bookPublisherId = value;
                    OnPropertyChanged("BookPublisherId");
                }
            }
        }

        [DbColumn("BookTypeId")]
        public int BookTypeId
        {
            get { return _bookTypeId; }
            set
            {
                if (_bookTypeId != value)
                {
                    _bookTypeId = value;
                    OnPropertyChanged("BookTypeId");
                }
            }
        }

        [DbColumn("YearOfPublication")]
        public int YearOfPublication
        {
            get { return _yearOfPublication; }
            set
            {
                if (_yearOfPublication != value)
                {
                    _yearOfPublication = value;
                    OnPropertyChanged("YearOfPublication");
                }
            }
        }

        [DbColumn("IsbnCode", 250, DbColumnAttribute.ColumnOptions.Nullable)]
        public string IsbnCode
        {
            get { return _isbnCode; }
            set
            {
                if (_isbnCode != value)
                {
                    _isbnCode = value;
                    OnPropertyChanged("IsbnCode");
                }
            }
        }

        [DbColumn("BookPlacementId")]
        public int BookPlacementId
        {
            get { return _bookPlacementId; }
            set
            {
                if (_bookPlacementId != value)
                {
                    _bookPlacementId = value;
                    OnPropertyChanged("BookPlacementId");
                }
            }
        }

        [DbColumn("PagesCount")]
        public int PagesCount
        {
            get { return _pagesCount; }
            set
            {
                if (_pagesCount != value)
                {
                    _pagesCount = value;
                    OnPropertyChanged("PagesCount");
                }
            }
        }

        [DbColumn("Dimensions", Int32.MaxValue, DbColumnAttribute.ColumnOptions.Nullable)]
        public string Dimensions
        {
            get { return _dimensions; }
            set
            {
                if (_dimensions != value)
                {
                    _dimensions = value;
                    OnPropertyChanged("Dimensions");
                }
            }
        }

        [DbColumn("PurchasedWhenYear")]
        public int PurchasedWhenYear
        {
            get { return _purchasedWhenYear; }
            set
            {
                if (_purchasedWhenYear != value)
                {
                    _purchasedWhenYear = value;
                    OnPropertyChanged("PurchasedWhenYear");
                }
            }
        }

        [DbColumn("PurchasedWhere", Int32.MaxValue, DbColumnAttribute.ColumnOptions.Nullable)]
        public string PurchasedWhere
        {
            get { return _purchasedWhere; }
            set
            {
                if (_purchasedWhere != value)
                {
                    _purchasedWhere = value;
                    OnPropertyChanged("PurchasedWhere");
                }
            }
        }
             
        [DbColumn("PurchasedPrice")]
        public decimal PurchasedPrice
        {
            get { return _purchasedPrice; }
            set
            {
                if (_purchasedPrice != value)
                {
                    _purchasedPrice = value;
                    OnPropertyChanged("PurchasedPrice");
                }
            }
        }

        [DbColumn("ResourcesDir", Int32.MaxValue, DbColumnAttribute.ColumnOptions.Nullable)]
        public string ResourcesDir
        {
            get { return _resourcesDir; }
            set
            {
                if (_resourcesDir != value)
                {
                    _resourcesDir = value;
                    OnPropertyChanged("ResourcesDir");
                }
            }
        }

        [DbColumn("ThumbnailName", Int32.MaxValue, DbColumnAttribute.ColumnOptions.Nullable)]
        public string ThumbnailName
        {
            get { return _thumbnailName; }
            set
            {
                if (_thumbnailName != value)
                {
                    _thumbnailName = value;
                    OnPropertyChanged("ThumbnailName");
                }
            }
        }


        public override bool NeedsUpdate(Book source)
        {
            if (BookAuthorId != source.BookAuthorId) return true;
            if (BookCathegoryId != source.BookCathegoryId) return true;
            if (BookGenreId != source.BookGenreId) return true;
            if (BookPublisherId != source.BookPublisherId) return true;
            if (BookTypeId != source.BookTypeId) return true;
            if (YearOfPublication != source.YearOfPublication) return true;
            if (IsbnCode != source.IsbnCode) return true;
            if (BookPlacementId != source.BookPlacementId) return true;
            if (PagesCount != source.PagesCount) return true;
            if (Dimensions != source.Dimensions) return true;
            if (PurchasedWhenYear != source.PurchasedWhenYear) return true;
            if (PurchasedWhere != source.PurchasedWhere) return true;
            if (PurchasedPrice != source.PurchasedPrice) return true;
            if (ResourcesDir != source.ResourcesDir) return true;
            if (ThumbnailName != source.ThumbnailName) return true;

            return base.NeedsUpdate(source);
        }
    }
}
