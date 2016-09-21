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

namespace BookManager.Core
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;


    public class ImagePreview
    {
        /// <summary>
        /// A path to this image preview.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// The loaded image.
        /// </summary>
        public ImageSource Image { get; set; }


        /// <summary>
        /// Loads all JPEG images from a directori into the list of ImagePreview instances.
        /// </summary>
        /// <param name="resourcesDir">A directory with images.</param>
        /// <param name="maxImagesCount">The maximum images, that can loaded. If less than 0, all images are loaded.</param>
        /// <returns>A list of ImagePreview instances.</returns>
        public static ExtendedObservableCollection<ImagePreview> LoadImages(string resourcesDir, int maxImagesCount = 20)
        {
            // If nothing to do...
            if (maxImagesCount == 0)
            {
                return new ExtendedObservableCollection<ImagePreview>();
            }

            var imageFilesList = new List<string>();

            try
            {
                var count = 0;
                var dirInfo = new DirectoryInfo(resourcesDir);
                foreach (var file in dirInfo.GetFiles("*.jp*", SearchOption.TopDirectoryOnly))
                {
                    var fn = file.Name.ToLower();

                    // Only JPEG images.
                    if (fn.EndsWith(".jpg") || fn.EndsWith(".jpeg"))
                    {
                        imageFilesList.Add(file.FullName);

                        // Are we unlimited?
                        if (maxImagesCount < 0)
                        {
                            continue;
                        }

                        count++;
                        if (count >= maxImagesCount)
                        {
                            // No more than maxImagesCount images should be loaded into the preview.
                            break;
                        }
                    }
                }
            }
            catch (Exception)
            {
                ;  // Exceptions can be ignored.
            }

            var results = new ExtendedObservableCollection<ImagePreview>();

            // No images found?
            if (imageFilesList.Count == 0)
            {
                return results;
            }

            foreach (var fileName in imageFilesList)
            {
                results.Add(new ImagePreview() { Path = fileName, Image = LoadImage(fileName) });
            }

            return results;
        }

        /// <summary>
        /// Loads an image from a file into an ImageSource instance. 
        /// </summary>
        /// <param name="fileName">A path to a image file.</param>
        /// <returns>An image loaded into an ImageSource instance.</returns>
        public static ImageSource LoadImage(string fileName)
        {
            var image = new BitmapImage();

            using (var stream = new FileStream(fileName, FileMode.Open))
            {
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = stream;
                image.EndInit();
            }

            return image;
        }
    }
}
