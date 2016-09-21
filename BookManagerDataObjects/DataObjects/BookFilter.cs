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
    using SimpleDb.Shared;


    public sealed class BookFilter : ADataObject
    {
        #region fields

        private int? _purchasedWhenYearFrom;
        private int? _purchasedWhenYearTo;

        private int _bookAuthorId;
        private int _bookPlacementId;

        // Accumulators.
        private int _booksCountAcc;
        private decimal _purchasedPriceAcc;

        #endregion


        #region properties

        public int? PurchasedWhenYearFrom
        {
            get { return _purchasedWhenYearFrom; }
            set
            {
                if (value != _purchasedWhenYearFrom)
                {
                    _purchasedWhenYearFrom = value;
                    OnPropertyChanged("PurchasedWhenYearFrom");
                }
            }
        }

        public int? PurchasedWhenYearTo
        {
            get { return _purchasedWhenYearTo; }
            set
            {
                if (value != _purchasedWhenYearTo)
                {
                    _purchasedWhenYearTo = value;
                    OnPropertyChanged("PurchasedWhenYearTo");
                }
            }
        }

        public int BookAuthorId
        {
            get { return _bookAuthorId; }
            set
            {
                if (value != _bookAuthorId)
                {
                    _bookAuthorId = value;
                    OnPropertyChanged("BookAuthorId");
                }
            }
        }

        public int BookPlacementId
        {
            get { return _bookPlacementId; }
            set
            {
                if (value != _bookPlacementId)
                {
                    _bookPlacementId = value;
                    OnPropertyChanged("BookPlacementId");
                }
            }
        }


        public int BooksCountAcc
        {
            get { return _booksCountAcc; }
            set
            {
                if (value != _booksCountAcc)
                {
                    _booksCountAcc = value;
                    OnPropertyChanged("BooksCountAcc");
                }
            }
        }

        public decimal PurchasedPriceAcc
        {
            get { return _purchasedPriceAcc; }
            set
            {
                if (value != _purchasedPriceAcc)
                {
                    _purchasedPriceAcc = value;
                    OnPropertyChanged("PurchasedPriceAcc");
                }
            }
        }

        #endregion


        #region ctor

        public BookFilter()
        {
            PurchasedWhenYearFrom = null;
            PurchasedWhenYearTo = null;

            Clear();
        }

        #endregion


        #region public methods

        public void Clear()
        {
            BooksCountAcc = 0;
            PurchasedPriceAcc = 0;
        }

        public void ClearAllExceptDates()
        {
            Clear();
            ClearSingleSelects();
        }

        #endregion


        #region non-public methods

        private void ClearSingleSelects()
        {
            BookAuthorId = 0;
            BookPlacementId = 0;
        }

        #endregion
    }
}
