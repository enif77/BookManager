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

namespace BookManager
{
    using System;
    using System.ComponentModel;
    using System.IO;
    using System.IO.Compression;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Controls;
    using System.Windows.Threading;

    using Microsoft.Win32;

    using Registry = Injektor.Registry;

    using BookManager.DataLayer;
    using BookManager.DataObjects;
    using BookManager.Forms;
   

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private bool _initialized = false;
        private readonly GlobalDataObject _gdo;


        public GlobalDataObject Gdo
        {
            get { return _gdo; }
        }


        public BookFilter Filter
        {
            get { return _gdo.Filter; }
        }


        public MainWindow()
        {
            _gdo = new GlobalDataObject();

            InitializeComponent();

            SourceInitialized += MainWindow_SourceInitiated;
            Loaded += MainWindow_Loaded;
            Closing += MainWindow_Closing;

            Registry.RegisterInstance(this);

            if (!UIHelper.DesignMode)
            {
                BookAuthorComboBox.ItemsSource = Gdo.UpdateBookAuthorCollection();
                BookPlacementComboBox.ItemsSource = Gdo.UpdateBookPlacementCollection();
            }
        }


        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Invoke property changed event
        /// </summary>
        /// <param name="propertyName">Property name</param>
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion


        private void MainWindow_SourceInitiated(object sender, EventArgs e)
        {
            var config = App.Config;

            if (config.IsMaximized) WindowState = WindowState.Maximized;

            Top = config.Top;
            Left = config.Left;
            Width = config.Width;
            Height = config.Height;
        }


        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (_initialized) return;

            BookDataGrid.Loaded += BookDataGrid_Loaded;

            DataContext = Filter;
            BookDataGrid.ItemsSource = Gdo.BookDataGridItems;

            Update();

            _initialized = true;
        }


        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            // TODO: Zeptat se, zda uživatel chce zavřít okno.
            // TODO: Uložit datagrid column info.

            // Save base configuration values.
            App.SaveAppConfig(this);
        }

        private void BookDataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            // TODO: Aplikovat datagrid column info.
        }

        private void MainWindow_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            Keyboard.ClearFocus();
        }

        private void ExitMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void NewCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            AddBook();
        }

        private void PlayCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ManageLookupsMenuItem_OnClick(sender, e);
        }

        private void OpenCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            EditBook();
        }

        private void RefreshCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Update();
        }

        private void NewBookMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            AddBook();
        }

        private void EditBookMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            EditBook();
        }

        private void ManageLookupsMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            var lookupsWindow = new LookupsEditorWindow(Gdo)
            {
                Owner = this,
                ShowInTaskbar = false,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };

            lookupsWindow.ShowDialog();
        }

        private void RefreshMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            Update();
        }

        private void SaveDataToCsvMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            ExportToCsv();
        }

        private void CopyDataToClipboardMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            ExportToClipboard();
        }

        private void BackupDataMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var openFileDialog = new OpenFileDialog
                {
                    Filter = "Backup files (*.zip)|*.zip|All files (*.*)|*.*",
                    CheckFileExists = false,
                    AddExtension = true,
                    Title = "Selecte a backup file",
                    InitialDirectory = "Backups",
                    FileName = "BookManagerBackup_" + DateTime.Now.ToString("yyyyMMdd")
                };

                if (openFileDialog.ShowDialog() == true)
                {
                    if (File.Exists(openFileDialog.FileName))
                    {
                        if (MessageBox.Show(this, "Overwrite the backup?", "Book Manager - Backup Owerwrite", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                        {
                            return;
                        }

                        File.Delete(openFileDialog.FileName);
                    }

                    ZipFile.CreateFromDirectory(App.DataDirectoryPath, openFileDialog.FileName, CompressionLevel.Optimal, true);

                    MessageBox.Show("Data backup successful.", "Book Manager - Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Data backup canceled.", "Book Manager - Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Data backup failed: " + ex.Message, "Book Manager - Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PurchasedWhenYearFromOnLostFocus(object sender, RoutedEventArgs e)
        {
            CheckYears();

            if (Filter.PurchasedWhenYearFrom > Filter.PurchasedWhenYearTo)
            {
                Filter.PurchasedWhenYearFrom = Filter.PurchasedWhenYearTo;
            }
        }

        private void PurchasedWhenYearToOnLostFocus(object sender, RoutedEventArgs e)
        {
            CheckYears();

            if (Filter.PurchasedWhenYearTo < Filter.PurchasedWhenYearFrom)
            {
                Filter.PurchasedWhenYearTo = Filter.PurchasedWhenYearFrom;
            }
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            EditBook();
        }

        private void NewBookButton_Click(object sender, RoutedEventArgs e)
        {
            AddBook();
        }

        private void EditBookButton_Click(object sender, RoutedEventArgs e)
        {
            EditBook();
        }

        private void DeleteBookButton_Click(object sender, RoutedEventArgs e)
        {
            DeleteBook();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            Update();
        }


        public void Update()
        {
            this.InvokeIfRequired(() =>
            {
                PrepareFilter();

                Gdo.UpdateBookCollection();
            }, DispatcherPriority.Render);
        }

        public void ClearFilter()
        {
            Filter.ClearAllExceptDates();
        }


        private void PrepareFilter()
        {
            this.InvokeIfRequired(() =>
            {
                CheckYears();
            }, DispatcherPriority.Render);
        }

        private void CheckYears()
        {
            // TODO: Validate year texboxes - should be empty or integers.

            if (String.IsNullOrWhiteSpace(PurchasedWhenYearFromTextBox.Text))
            {
                Filter.PurchasedWhenYearFrom = null;
            }

            if (String.IsNullOrWhiteSpace(PurchasedWhenYearToTextBox.Text))
            {
                Filter.PurchasedWhenYearTo = null;
            }
        }


        private void AddBook()
        {
            //var obj = new Book();
            //if (BookEditor.Open(Gdo, obj))
            //{
            //    Registry.Get<BookDataLayer>().Save(obj);

            //    Update();
            //}
        }
                             
        private void EditBook()
        {
            var item = BookDataGrid.SelectedItem as BookDataGridItem;
            if (item == null)
            {
                MessageBox.Show("Select a book for editation.", "Book Manager - Notice", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                return;
            }

            // We Reload here to make sure, that we get, what is  in the DB.   
            var entity = Registry.Get<BookDataLayer>().Reload(item.Id);
            if (entity == null)
            {
                MessageBox.Show("The selected book can not be read for editation.", "Book Manager - Error", MessageBoxButton.OK, MessageBoxImage.Error);

                return;
            }

            //if (BookEditor.Open(Gdo, entity))
            //{
            //    Registry.Get<BookDataLayer>().Save(entity);

            //    Update();
            //}
        }


        private void DeleteBook()
        {
            if (BookDataGrid.SelectedItems.Count == 0)
            {
                MessageBox.Show(this, "No book is selected.", "Book Manager - Notice", MessageBoxButton.OK, MessageBoxImage.Information);

                return;
            }

            if (MessageBox.Show(this, "Delete selected books?", "Book Manager - Delete Books", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                var dal = Registry.Get<BookDataLayer>();
                foreach (var item in BookDataGrid.SelectedItems)
                {
                    var entity = Registry.Get<BookDataLayer>().Get((item as BookDataGridItem).Id);
                    if (entity == null)
                    {
                        continue;
                    }

                    dal.Delete(entity);
                }

                Update();
            }
        }


        private void ExportToClipboard()
        {
            UIHelper.CopyToClipboard(BookDataGrid);
        }


        private void ExportToCsv()
        {
            var dialog = new SaveFileDialog
            {
                RestoreDirectory = true,
                OverwritePrompt = true,
                FileName = DateTime.Now.ToString("yyyy-MM-dd-HH-mm"),
                DefaultExt = ".csv",
                Filter = "Comma-separated files (.csv)|*.csv"
            };

            // Operation canceled.
            if (dialog.ShowDialog().GetValueOrDefault() == false) return;

            // Convert data to CSV and store them to the clipboard.
            UIHelper.CopyToClipboard(BookDataGrid);

            // Get the CSV data from the clipboard...
            var data = Clipboard.GetData(DataFormats.CommaSeparatedValue);

            try
            {
                using (var outfile = new StreamWriter(dialog.FileName))
                {
                    // ... and write the to the file.
                    outfile.Write(data.ToString());
                }
            }
            catch (Exception ex)
            {
                UnhandledErrorWindow.Open(ex);
            }
        }

    }
}
