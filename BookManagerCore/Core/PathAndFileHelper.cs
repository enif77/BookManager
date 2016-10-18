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
    using System.Diagnostics;
    using System.IO;
    

    public static class PathAndFileHelper
    {
        /// <summary>
        /// Validates a path.
        /// </summary>
        /// <param name="path">A path.</param>
        /// <returns>True, if the path is a valid path.</returns>
        public static bool IsPathValid(string path)
        {
            try
            {
                return Path.GetFullPath(path) == path;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Retuns a relative path from the fromFolderPath to the toFilePath.
        /// </summary>
        /// <param name="fromFolderPath">A folder, where the resulting relative path begins.</param>
        /// <param name="toFilePath">A path to a file.</param>
        /// <returns>A relative path from fromFolderPath to the toFilePath.</returns>
        public static string GetRelativePath(string fromFolderPath, string toFilePath)
        {
            var toFilePathUri = new Uri(toFilePath);

            // A folder must end with a slash.
            if (fromFolderPath.EndsWith(Path.DirectorySeparatorChar.ToString()) == false)
            {
                fromFolderPath += Path.DirectorySeparatorChar;
            }

            return Uri.UnescapeDataString(new Uri(fromFolderPath).MakeRelativeUri(toFilePathUri).ToString().Replace('/', Path.DirectorySeparatorChar));
        }

        /// <summary>
        /// Opens a file with a default application.
        /// </summary>
        /// <param name="path"></param>
        public static void OpenFile(string path)
        {
            try
            {
                Process.Start(path);
            }
            catch (Exception)
            {
                ;  // Can fail silently.
            }
        }
    }
}
