using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
	/// <summary>
	/// All raw data and data necessary for datagrid are stored here
	/// </summary>
    public class BookData
    {
		private string _bookTitle;
		private string _author;
		private string _year;
		private string _price;
		private string _stock;
		private string _binding;
		private string _description;
		private bool _stockFlag;
		private double  _priceFloat;
		//Static array to be constant acrross all objects
		private static double _minPrice = double.MaxValue, _maxPrice = double.MinValue;
		private HashSet<string> _bindingType = new HashSet<string>();


		public  string BookTitle
		{
			get { return _bookTitle; }
			set { _bookTitle = value; }
		}
		public string Author
		{
			get { return _author; }
			set { _author = value; }
		}
		public string Year
		{
			get { return _year; }
			set {
				if (!value.All(char.IsDigit)) throw new ArgumentOutOfRangeException();
				_year = value; 
			}
		}
		
		/// <summary>
		/// Returns the price as string.
		/// '0's are added to the left end which helps sorting the price string
		/// when the price is single digit
		/// need to increase number of zeros when 3 or more digit prices are present
		/// </summary>
		public string Price
		{
			get { return _price; }
			set {
				_price = value.PadLeft(5, '0');
				_priceFloat = Convert.ToDouble(value.Replace(",", "."));

				//Calculates min and max for all objects using static array 
				if (_priceFloat < _minPrice) _minPrice = _priceFloat;
				if (_priceFloat > _maxPrice) _maxPrice = _priceFloat;

			}
		}
		
		/// <summary>
		/// sets the stockFlag to True if the string value is "yes" or "true"
		/// </summary>
		public string InStock
		{
			get { return _stock; }
			set { 
				_stock = value;
				_stock = _stock.ToLower();

				if (value.Equals("yes") || value.Equals("true"))
					_stockFlag = true;
				else if (value.Equals("no") || value.Equals("false"))
					_stockFlag = false;
				else
					throw new ArgumentOutOfRangeException();
			}
		}

		/// <summary>
		/// Stores both binding and adds to binding set
		/// binding set is not static hence varies for each object
		/// Hash set is used to avoid duplicates
		/// </summary>
		public string BindingType
		{
			get { return _binding; }
			set { 
				_binding = value;
				_bindingType.Add(value);
			}
		}
		public HashSet<string> GetBindingSet
		{
			get { return _bindingType; }
		}
		public string Description
		{
			get { return _description; }
			set { _description = value; }
		}
		public bool StockFlag
		{
			get { return _stockFlag; }
			set { _stockFlag = value; }

		}

		/// <summary>
		/// Calculates PriceRatio varies between 0 and 1
		/// used for calculating Price font color
		/// </summary>
		public double PriceRatio
		{
			get {
				double _priceRatio = (_maxPrice - _priceFloat) / (_maxPrice - _minPrice);
				return _priceRatio; }
		}

	}
}
