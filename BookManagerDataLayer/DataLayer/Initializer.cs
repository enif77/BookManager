﻿/* BookManager - (C) 2016 Premysl Fara 
 
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

    using SimpleDb.Files;
    using Injektor;


    public static class Initializer
    {
        public static void InitializeLayers(Database database)
        {
            if (database == null) throw new ArgumentNullException("database");

            // Register DALs.
            Registry.RegisterInstance(new ConfigurationDataLayer(database));

            Registry.RegisterInstance(new BookDataLayer(database));
            Registry.RegisterInstance(new BookAuthorDataLayer(database));
            Registry.RegisterInstance(new BookCategoryDataLayer(database));
            Registry.RegisterInstance(new BookGenreDataLayer(database));
            Registry.RegisterInstance(new BookPlacementDataLayer(database));
            Registry.RegisterInstance(new BookPublisherDataLayer(database));
            Registry.RegisterInstance(new BookTypeDataLayer(database));
            
            // Initialize DALs, load current data to memory.
            Registry.Get<ConfigurationDataLayer>().GetAll();

            Registry.Get<BookAuthorDataLayer>().GetAll();
            Registry.Get<BookCategoryDataLayer>().GetAll();
            Registry.Get<BookGenreDataLayer>().GetAll();
            Registry.Get<BookPlacementDataLayer>().GetAll();
            Registry.Get<BookPublisherDataLayer>().GetAll();
            Registry.Get<BookTypeDataLayer>().GetAll();
            Registry.Get<BookDataLayer>().GetAll();
        }
    }
}
