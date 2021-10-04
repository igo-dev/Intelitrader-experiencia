using Intelitrader_Mobile.Dtos;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Intelitrader_Mobile.Converters
{
    public class DateBirthToAgeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var dateBirthStr = value as string;
            if (string.IsNullOrEmpty(dateBirthStr))
                return "null";

            var bDate = DateTime.Parse(dateBirthStr);
            var now = DateTime.Now;
            var age = now.Year - bDate.Year;
            if (bDate > now.AddYears(-age)) age--;
            return age.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
