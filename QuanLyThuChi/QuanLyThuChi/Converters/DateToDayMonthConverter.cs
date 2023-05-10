using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace QuanLyThuChi.Converters
{
    public class DateToDayMonthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is DateTime date)
            {
                return date.ToString("dd/MM");
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
