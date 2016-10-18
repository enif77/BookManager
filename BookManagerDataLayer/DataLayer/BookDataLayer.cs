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

namespace BookManager.DataLayer
{
    using System;
    using System.Collections.Generic;

    using SimpleDb.Files;
    using Injektor;

    using BookManager.DataObjects;


    public class BookDataLayer : LookupDataLayer<Book>
    {
        public BookDataLayer(Database database)
            : base(database)
        {
        }


        public List<BookDataGridItem> GetAll(BookFilter filter)
        {
            if (filter == null) throw new ArgumentNullException("filter");

            GetAll();

            var res = new List<BookDataGridItem>();

            var bookAuthorDal = Registry.Get<BookAuthorDataLayer>();
            var bookCategoryDal = Registry.Get<BookCategoryDataLayer>();
            var bookGenreDal = Registry.Get<BookGenreDataLayer>();
            var bookPublisherDal = Registry.Get<BookPublisherDataLayer>();
            var bookTypeDal = Registry.Get<BookTypeDataLayer>();
            var bookPlacementDal = Registry.Get<BookPlacementDataLayer>();

            int booksCountAcc = 0;
            decimal purchasedPriceAcc = 0;

            foreach (var d in DataObjects.Values)
            {
                if (filter.PurchasedWhenYearFrom.HasValue && d.PurchasedWhenYear < filter.PurchasedWhenYearFrom.Value) continue;
                if (filter.PurchasedWhenYearTo.HasValue && d.PurchasedWhenYear > filter.PurchasedWhenYearTo.Value) continue;
                if (filter.BookAuthorId != 0 && filter.BookAuthorId != d.BookAuthorId) continue;
                if (filter.BookPlacementId != 0 && filter.BookPlacementId != d.BookPlacementId) continue;

                var i = new BookDataGridItem
                {
                    Id = d.Id,
                    BookName = d.Name,
                    BookAuthorFirstName = GetBookAuthorFirstName(bookAuthorDal, d.BookAuthorId),
                    BookAuthorLastName = GetBookAuthorLastName(bookAuthorDal, d.BookAuthorId),
                    BookCategoryName = GetBookCategoryName(bookCategoryDal, d.BookCategoryId),
                    BookGenreName = GetBookGenreName(bookGenreDal, d.BookGenreId),
                    BookPublisherName = GetBookPublisherName(bookPublisherDal, d.BookPublisherId),
                    BookTypeName = GetBookTypeName(bookTypeDal, d.BookTypeId),
                    BookPlacementName = GetBookPlacementName(bookPlacementDal, d.BookTypeId),
                    YearOfPublication = d.YearOfPublication,
                    PurchasedWhenYear = d.PurchasedWhenYear,
                    PurchasedPrice = d.PurchasedPrice,
                    IsbnCode = d.IsbnCode,
                };

                res.Add(i);

                booksCountAcc++;
                purchasedPriceAcc += d.PurchasedPrice;
            }

            filter.BooksCountAcc = booksCountAcc;
            filter.PurchasedPriceAcc = purchasedPriceAcc;

            return res;
        }


        private string GetBookAuthorFirstName(BookAuthorDataLayer dal, int id)
        {
            if (id > 0)
            {
                var d = dal.Get(id);
                return (d == null) ? String.Empty : d.FirstName;
            }

            return String.Empty;
        }

        private string GetBookAuthorLastName(BookAuthorDataLayer dal, int id)
        {
            if (id > 0)
            {
                var d = dal.Get(id);
                return (d == null) ? String.Empty : d.LastName;
            }

            return String.Empty;
        }

        private string GetBookCategoryName(BookCategoryDataLayer dal, int id)
        {
            if (id > 0)
            {
                var d = dal.Get(id);
                return (d == null) ? String.Empty : d.Name;
            }

            return String.Empty;
        }
           
        private string GetBookGenreName(BookGenreDataLayer dal, int id)
        {
            if (id > 0)
            {
                var d = dal.Get(id);
                return (d == null) ? String.Empty : d.Name;
            }

            return String.Empty;
        }
        
        private string GetBookPublisherName(BookPublisherDataLayer dal, int id)
        {
            if (id > 0)
            {
                var d = dal.Get(id);
                return (d == null) ? String.Empty : d.Name;
            }

            return String.Empty;
        }

        private string GetBookTypeName(BookTypeDataLayer dal, int id)
        {
            if (id > 0)
            {
                var d = dal.Get(id);
                return (d == null) ? String.Empty : d.Name;
            }

            return String.Empty;
        }

        private string GetBookPlacementName(BookPlacementDataLayer dal, int id)
        {
            if (id > 0)
            {
                var d = dal.Get(id);
                return (d == null) ? String.Empty : d.Name;
            }

            return String.Empty;
        }


        public override int GetIdByName(string name, bool bypassCache = false)
        {
            throw new NotImplementedException();
        }
    }
}
