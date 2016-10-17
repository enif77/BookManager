/* BookManager - (C) 2016 Premysl Fara 
 
BookManager are available under the zlib license:

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

namespace BookManager.Forms
{
    using System;
    using System.ComponentModel;
    using System.IO;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media.Imaging;
    using System.Windows.Threading;

    using BookManager.Core;


    /// <summary>
    /// UI helper class.
    /// </summary>
    public static class UIHelper
    {
        /// <summary>
        /// Loads an image into a preview controll.
        /// </summary>
        /// <param name="resourcesDir">A directory containing an image. Can be empty or null.</param>
        /// <param name="fileName">The image file name. Can be empty or null.</param>
        /// <param name="previewDisplay">The preview controll. Can be null.</param>
        /// <param name="previewDescription">The preview description controll. Can be null.</param>
        public static void LoadPreviewImage(string resourcesDir, string fileName, Image previewDisplay, Label previewDescription)
        {
            if (String.IsNullOrWhiteSpace(resourcesDir))
            {
                resourcesDir = App.DataDirectoryPath;
            }

            var imageName = String.IsNullOrWhiteSpace(fileName)
                ? "Thumbnail.jpg"
                : fileName;
            var imgagePath = Path.Combine(resourcesDir, imageName);

            if (previewDisplay != null) previewDisplay.Source = File.Exists(imgagePath) ? ImagePreview.LoadImage(imgagePath) : new BitmapImage();
            if (previewDescription != null) previewDescription.Content = "<Documents>\\" + imageName;
        }

        /// <summary>
        /// Invokes a method, if it is required.
        /// </summary>
        /// <param name="control">A DispatcherObject instance.</param>
        /// <param name="methodcall">An System.Action instance.</param>
        /// <param name="priorityForCall">A DispatcherPriority value.</param>
        public static void InvokeIfRequired(this DispatcherObject control, Action methodcall, DispatcherPriority priorityForCall = DispatcherPriority.Normal)
        {
            // Check, if we need to Invoke call to the Dispatcher thread.
            if (control.Dispatcher.Thread != Thread.CurrentThread)
            {
                control.Dispatcher.Invoke(priorityForCall, methodcall);
            }
            else
            {
                methodcall();
            }
        }

        /// <summary>
        /// Returns true, if we are in the Visual Studio UI designer.
        /// </summary>
        public static bool DesignMode
        {
            get { return DesignerProperties.GetIsInDesignMode(new DependencyObject()); }
        }

        /// <summary>
        /// Exports data from a DataGrid to CSV format stored in the clipboard.
        /// </summary> 
        /// <param name="grid">A DataGrid instance.</param>
        public static void CopyToClipboard(DataGrid grid)
        {
            var oldSelectionMode = grid.SelectionMode;
            var oldClipboardCopyMode = grid.ClipboardCopyMode;

            grid.SelectionMode = DataGridSelectionMode.Extended;
            grid.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;

            grid.SelectAllCells();

            ApplicationCommands.Copy.Execute(null, grid);

            grid.UnselectAllCells();

            grid.SelectionMode = oldSelectionMode;
            grid.ClipboardCopyMode = oldClipboardCopyMode;
        }
    }
}
