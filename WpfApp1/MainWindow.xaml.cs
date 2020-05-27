using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;
using System.Collections.ObjectModel;
using Microsoft.Win32;
using System.IO;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<BookData> bookList;
        private DataImport dataImport = new DataImport();
        private string fileName = "D:\\Downloads\\C# Dev - Programming Task\\Books.csv";

        /* Constructor of this mainWindow, initializes everything
         * Use the below code for directly loading data to datagrid without using open menu
         * Change the fileName value if it is necessary 
         */

        /*Uncomment below lines if you dont want 
             * to run the tool without opening the file 
              Change the file name and path in the above variable
         */
        public MainWindow()
        {
            InitializeComponent();

            //SetDataGridSource();



        }


        /* Enable or Disable Description based on click Event
         * boolean value of tooltip enabler is obtained from tooltip service
         * Reverse the boolean value and set it back
         * tooltip opened or closed using isOpen
         * when tooltip is opened, tooltip placetarget is set to its button
         * it is done because content is not displayed for the first time is clicked 
         */
        void OnClickDescription(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            ToolTip tt = btn.ToolTip as ToolTip;

            bool CurrBool = ToolTipService.GetIsEnabled(btn);
            CurrBool = !CurrBool;
            ToolTipService.SetIsEnabled(btn, CurrBool);

            tt.IsOpen = CurrBool;
            
            
            if (CurrBool)
            {
                /*Works with tooltip datacontext in XAML file
                *needed because the binding with description string is not invocked when button is pressed for the first time
                */
                tt.PlacementTarget = btn;
            }

        }

        /* Invocked when Open Menu is clicked
         * default folder during opening is set to   application folder  
         * Only reads CSV files
         * DataImport is used to store the data in observable collection
         * Datagrid itemsource is applied
         */
        void OnClickOpen(object sender, RoutedEventArgs e)
        {
            
            OpenFileDialog openFileDialog = new OpenFileDialog();

            string CurrFolder = System.AppDomain.CurrentDomain.BaseDirectory;
            openFileDialog.InitialDirectory = CurrFolder;
            openFileDialog.Filter = "CSV Files|*.CSV";

            if ((bool)openFileDialog.ShowDialog())
            {
                 fileName = openFileDialog.FileName;
            }



            
            if (fileName != null)
            {
                SetDataGridSource();
            }

        }

        /*Invocked when exit menu item is pressed.
         */
        void OnClickExit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /* Invocked when 'Remove out of stock' is pressed
         * New List is generated based on stockflag 
         * These items are removed from observable collection
         * loop through removableItems List during removal process
         */
        void OnClickRemoveNoBooks(object sender, RoutedEventArgs e)
        {
            //Do nothing when 'Remove out of Stock' button is pressed before opening the data
            if(bookList == null)
            {
                return;
            }

            var removableItems = bookList.Where(x => x.StockFlag == false).ToList();
            foreach (var item in removableItems)
            {
                bookList.Remove(item);
            }
            
        }

        private void SetDataGridSource()
        {
            bookList = dataImport.GetDataList(fileName);
            bl.ItemsSource = bookList;
        }
    }

}
