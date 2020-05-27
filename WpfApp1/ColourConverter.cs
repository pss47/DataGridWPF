using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using System.Diagnostics;
namespace WpfApp1
{
    /// <summary>
    /// The class has to inherit from IValueConverter to act as Converter
    /// Do not name it as ColorConverter since compiler assumes that the method 
    /// is from System.Windows.Media
    /// </summary>
    public class ColourConverter : IValueConverter
    {

        public object Convert(object value, Type targetType,
           object parameter, System.Globalization.CultureInfo culture)
        {

            float PriceRatio = (float)System.Convert.ToDouble(value);

            //Color changes varies from black to red for price varies from low to high
            Color color = new Color() { ScA = 1.0F, ScR = 1.0F - PriceRatio, ScB = 0.0F, ScG = 0.0F };

            //Brush has to be returned for ForeGround property not Color
            return new SolidColorBrush(color);

        }

        //ConvertBack is not implemented since conversion from color to priceratio is not required
        public object ConvertBack(object value, Type targetType,
             object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
