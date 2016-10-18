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

namespace BookManager.Forms
{
    using System;
    using System.Windows;

    using Injektor;

    using BookManager.DataObjects;


    /// <summary>
    /// Interaction logic for BookPlacementEditorWindow.xaml
    /// </summary>
    public partial class BookPlacementEditorWindow : Window
    {
        #region properties

        public DialogResultStateType DialogResultState { get; private set; }


        public BookPlacement DataObject
        {
            get { return DataContext as BookPlacement; }
        }

        #endregion


        #region ctor

        public BookPlacementEditorWindow()
        {
            InitializeComponent();

            DialogResultState = DialogResultStateType.Ok;
        }

        #endregion


        #region public methods

        public static bool Open(BookPlacement dataObject)
        {
            if (dataObject == null) throw new ArgumentNullException("dataObject");

            var dialog = new BookPlacementEditorWindow()
            {
                DataContext = dataObject,
                Owner = Registry.Get<MainWindow>(),
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Title = (dataObject.Id <= 0)
                    ? "Book Manager - New Book Placement"
                    : String.Format("Book Manager - Book Placement Edit {0}", dataObject.Id)
            };

            dialog.ShowDialog();

            return dialog.DialogResult.GetValueOrDefault();
        }

        #endregion


        private void Save_OnClick(object sender, RoutedEventArgs e)
        {
            if (SaveClick())
            {
                Close();
            }
        }


        private void Cancel_OnClick(object sender, RoutedEventArgs e)
        {
            CancelClick();
            Close();
        }


        private bool SaveClick()
        {
            try
            {
                ((BookPlacement)DataContext).Validate();
            }
            catch (Exception ex)
            {
                UnhandledErrorWindow.Open(ex);

                return false;
            }

            DialogResult = true;
            DialogResultState = DialogResultStateType.Ok;

            return true;
        }


        private void CancelClick()
        {
            DialogResult = false;
            DialogResultState = DialogResultStateType.Cancel;
        }
    }
}
