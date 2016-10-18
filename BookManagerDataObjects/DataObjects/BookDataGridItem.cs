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

namespace BookManager.DataObjects
{
    using System;

    using SimpleDb.Shared;


    public sealed class BookDataGridItem : AIdDataObject, IUpdatable<BookDataGridItem>
    {
        #region fields

        // ID - AIdDataObject.

        private string _bookName;
        private string _bookAuthorFirstName;
        private string _bookAuthorLastName;
        private string _bookCategoryName;
        private string _bookGenreName;
        private string _bookPublisherName;
        private string _bookTypeName;
        private string _bookPlacementName;
        private int _yearOfPublication;
        private int _purchasedWhenYear;
        private decimal _purchasedPrice;
        private string _isbnCode;

        #endregion


        public string BookName
        {
            get { return _bookName; }
            set
            {
                if (_bookName != value)
                {
                    _bookName = value;
                    OnPropertyChanged("BookName");
                }
            }
        }

        public string BookAuthorFirstName
        {
            get { return _bookAuthorFirstName; }
            set
            {
                if (_bookAuthorFirstName != value)
                {
                    _bookAuthorFirstName = value;
                    OnPropertyChanged("BookAuthorFirstName");
                }
            }
        }

        public string BookAuthorLastName
        {
            get { return _bookAuthorLastName; }
            set
            {
                if (_bookAuthorLastName != value)
                {
                    _bookAuthorLastName = value;
                    OnPropertyChanged("BookAuthorLastName");
                }
            }
        }

        public string BookCategoryName
        {
            get { return _bookCategoryName; }
            set
            {
                if (_bookCategoryName != value)
                {
                    _bookCategoryName = value;
                    OnPropertyChanged("BookCategoryName");
                }
            }
        }

        public string BookGenreName
        {
            get { return _bookGenreName; }
            set
            {
                if (_bookGenreName != value)
                {
                    _bookGenreName = value;
                    OnPropertyChanged("BookGenreName");
                }
            }
        }

        public string BookPublisherName
        {
            get { return _bookPublisherName; }
            set
            {
                if (_bookPublisherName != value)
                {
                    _bookPublisherName = value;
                    OnPropertyChanged("BookPublisherName");
                }
            }
        }

        public string BookTypeName
        {
            get { return _bookTypeName; }
            set
            {
                if (_bookTypeName != value)
                {
                    _bookTypeName = value;
                    OnPropertyChanged("BookTypeName");
                }
            }
        }

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

        public string BookPlacementName
        {
            get { return _bookPlacementName; }
            set
            {
                if (_bookPlacementName != value)
                {
                    _bookPlacementName = value;
                    OnPropertyChanged("BookPlacementName");
                }
            }
        }


        public bool NeedsUpdate(BookDataGridItem source)
        {
            if (Id != source.Id) return true;
            if (BookName != source.BookName) return true;
            if (BookAuthorFirstName != source.BookAuthorFirstName) return true;
            if (BookAuthorLastName != source.BookAuthorLastName) return true;
            if (BookCategoryName != source.BookCategoryName) return true;
            if (BookGenreName != source.BookGenreName) return true;
            if (BookPublisherName != source.BookPublisherName) return true;
            if (BookTypeName != source.BookTypeName) return true;
            if (YearOfPublication != source.YearOfPublication) return true;
            if (PurchasedWhenYear != source.PurchasedWhenYear) return true;
            if (PurchasedPrice != source.PurchasedPrice) return true;
            if (IsbnCode != source.IsbnCode) return true;
            if (BookPlacementName != source.BookPlacementName) return true;

            return false;
        }


        public void Update(BookDataGridItem source)
        {
            throw new NotImplementedException();
        }
    }
}
