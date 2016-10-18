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


    /// <summary>
    /// A book author.
    /// </summary>
    [DbTable("BookAuthor")]
    public sealed class BookAuthor : ALookupDataObject<BookAuthor>
    {
        #region fields

        private string _firstName;
        private string _lastName;

        #endregion


        /// <summary>
        /// The name used in the lists.
        /// </summary>
        [DbColumn("Name", Int32.MaxValue, DbColumnAttribute.ColumnOptions.Ignored)]
        public override string Name
        {
            get { return base.Name; }
            set { base.Name = value; }
        }

        /// <summary>
        /// The first name of an author.
        /// </summary>
        [DbColumn("FirstName", Int32.MaxValue, DbColumnAttribute.ColumnOptions.Nullable)]
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                if (_firstName != value)
                {
                    _firstName = value;
                    OnPropertyChanged("FirstName");

                    Updatename();
                }
            }
        }

        /// <summary>
        /// The last name of an author.
        /// </summary>
        [DbColumn("LastName", Int32.MaxValue, DbColumnAttribute.ColumnOptions.Nullable)]
        public string LastName
        {
            get { return _lastName; }
            set
            {
                if (_lastName != value)
                {
                    _lastName = value;
                    OnPropertyChanged("LastName");

                    Updatename();
                }
            }
        }

        [DbColumn("Description", Int32.MaxValue, DbColumnAttribute.ColumnOptions.Nullable)]
        public override string Description
        {
            get { return base.Description; }
            set { base.Description = value; }
        }


        /// <summary>
        /// Implements the ToString() method for the SingleSelect control.
        /// </summary>
        /// <returns>A string representation of this instance.</returns>
        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        /// Updates the Name property, when the FirstName property or the LastName property changes.
        /// </summary>
        private void Updatename()
        {
            Name = String.Format("{0} {1}",
                LastName ?? String.Empty,
                FirstName ?? String.Empty);
        }

        /// <summary>
        /// Checks, if this instance differs from the source instance.
        /// </summary>
        /// <param name="source">A source instance.</param>
        /// <returns>True, if this instance differs from the source instance.</returns>
        public override bool NeedsUpdate(BookAuthor source)
        {
            if (FirstName != source.FirstName) return true;
            if (LastName != source.LastName) return true;

            return base.NeedsUpdate(source);
        }


        public override void Validate()
        {
            base.Validate();

            if (String.IsNullOrWhiteSpace(Name))
            {
                throw new ValidationException("The Name property can not be empty. Enter at least a First name or a Last name.");
            }
        }
    }
}
