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

namespace BookManager.Core
{
    using System;
    using System.IO;


    /// <summary>
    /// Represent a directory tree item.
    /// </summary>
    public class TreeItem
    {
        public string Name { get; set; }

        public string Path { get; set; }


        public static void OpenFile(TreeItem item)
        {
            PathAndFileHelper.OpenFile(item.Path);
        }
    }


    /// <summary>
    /// A file in a directory items tree.
    /// </summary>
    public class FileItem : TreeItem
    {

    }


    /// <summary>
    /// A directory in a directory items tree.
    /// </summary>
    public class DirectoryItem : TreeItem
    {
        public ExtendedObservableCollection<TreeItem> Items { get; set; }


        public DirectoryItem()
        {
            Items = new ExtendedObservableCollection<TreeItem>();
        }
    }


    /// <summary>
    /// A directory tree provider class.
    /// </summary>
    public class ItemProvider
    {
        /// <summary>
        /// Gets all items in a direcory and all it's subdirectories.
        /// </summary>
        /// <param name="path">A path to a directory.</param>
        /// <returns>A tree of items in a directory.</returns>
        public static ExtendedObservableCollection<TreeItem> GetItems(string path)
        {
            var items = new ExtendedObservableCollection<TreeItem>();

            try
            {
                var dirInfo = new DirectoryInfo(path);

                foreach (var directory in dirInfo.GetDirectories())
                {
                    var item = new DirectoryItem
                    {
                        Name = directory.Name,
                        Path = directory.FullName,
                        Items = GetItems(directory.FullName)
                    };

                    items.Add(item);
                }

                foreach (var file in dirInfo.GetFiles())
                {
                    var item = new FileItem
                    {
                        Name = file.Name,
                        Path = file.FullName
                    };

                    items.Add(item);
                }
            }
            catch (Exception ex)
            {
                var item = new FileItem
                {
                    Name = ex.Message,
                    Path = ex.ToString()
                };

                items.Add(item);
            }

            return items;
        }
    }
}
