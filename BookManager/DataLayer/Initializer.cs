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
            
            //Registry.RegisterInstance(new AutorDataLayer(database));
            //Registry.RegisterInstance(new DiloDataLayer(database));
            //Registry.RegisterInstance(new KurzovniListekDataLayer(database));
            //Registry.RegisterInstance(new MajitelDataLayer(database));
            //Registry.RegisterInstance(new MenaDataLayer(database));
            //Registry.RegisterInstance(new OceneniDataLayer(database));
            //Registry.RegisterInstance(new ProdejniMistoDataLayer(database));
            //Registry.RegisterInstance(new TechnikaDataLayer(database));
            //Registry.RegisterInstance(new TypDilaDataLayer(database));
            //Registry.RegisterInstance(new UmisteniDataLayer(database));

            // Initialize DALs, load current data to memory.
            Registry.Get<ConfigurationDataLayer>().GetAll();

            //Registry.Get<MenaDataLayer>().GetAll();
            //Registry.Get<OceneniDataLayer>().GetAll();
            //Registry.Get<ProdejniMistoDataLayer>().GetAll();
            //Registry.Get<TechnikaDataLayer>().GetAll();
            //Registry.Get<TypDilaDataLayer>().GetAll();
            //Registry.Get<UmisteniDataLayer>().GetAll();
            //Registry.Get<KurzovniListekDataLayer>().GetAll();
            //Registry.Get<MajitelDataLayer>().GetAll();
            //Registry.Get<AutorDataLayer>().GetAll();
            //Registry.Get<DiloDataLayer>().GetAll();
        }
    }
}
