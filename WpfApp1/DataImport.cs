using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp1
{
    /// <summary>
    /// Observable collection allows peaking from XAML
    /// reads the file and feeds the data to Bookdata class
    /// then fed object is added to list
    /// </summary>
    class DataImport
    {

        public ObservableCollection<BookData> GetDataList(string fileName)
        {

            ObservableCollection<BookData> bookDataList = new ObservableCollection<BookData>();

            try
            {
                using (StreamReader reader = new StreamReader(fileName, Encoding.Default)) {

                    //Checking if the file is empty
                    if (reader.Peek() == -1) throw new NullReferenceException();

                    //Header from the text is omitted
                    string headerLine = reader.ReadLine();

                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(';');


                        //Checks if there are any empty strings
                        if (values.Any(x => String.IsNullOrEmpty(x)))
                            throw new NullReferenceException();

                        BookData bookData = AppendValues(values);

                        bookDataList.Add(bookData);
                    }
                }
            }
            catch (InvalidCastException)
            {
                PrintErrorMessage("Invalid Data is present in the input", bookDataList);
                return bookDataList;

            }
            catch (IndexOutOfRangeException)
            {
                PrintErrorMessage("Some Data is missing in a line", bookDataList);
                return bookDataList;
            }
            catch (NullReferenceException)
            {
                PrintErrorMessage("Some Data is missing ", bookDataList);
                return bookDataList;
            }
            catch (ArgumentOutOfRangeException)
            {
                PrintErrorMessage("Invalid Data is present", bookDataList);
                return bookDataList;
            }

            return bookDataList;
        }

        private BookData AppendValues(string[] values)
        {
            BookData bookData = new BookData
            {
                BookTitle = values[0],
                Author = values[1],
                Year = values[2],
                Price = values[3],
                InStock = values[4],
                BindingType = values[5],
                Description = values[6]
            };
            return bookData;
        }

        //Displays the message box and clears the list
        private void PrintErrorMessage(string errorMsg, ObservableCollection<BookData> bookDataList)
        {
            MessageBox.Show(errorMsg, "Error Message");
            bookDataList.Clear();
        }
    }
}
