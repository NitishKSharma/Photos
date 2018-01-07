using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Globalization;
using PhotoViewer.Exif;
using System.Windows;
using System.Windows.Media.Imaging;
using System.IO;

namespace PhotoViewer
{
   internal class ThumbnailConverter : IValueConverter
   {
      public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
      {
         if (value != null)
         {
            var thumbnailStream = (BitmapSource)value;
            var image = new BitmapImage();

            return image;
         }

         return DependencyProperty.UnsetValue;
      }

      public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
      {
         throw new NotImplementedException();
      }
   }

   /// <summary>
   /// Converts an exposure time from a decimal (e.g. 0.0125) into a string (e.g. 1/80)
   /// </summary>
   public class ExposureTimeConverter : IValueConverter
   {
      public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
      {
         object result = DependencyProperty.UnsetValue;

         if (value != null)
         {
            decimal exposure = (decimal)value;
            if (exposure > 0)
            {
               exposure = Math.Round(1 / exposure);
               result = String.Format("1/{0} sec.", exposure.ToString());
            }
         }

         return result;
      }

      public object ConvertBack(object value, Type targetTypes, object parameter, CultureInfo culture)
      {
         object result = DependencyProperty.UnsetValue;

         if (value != null)
         {
            string temp = ((string)value).Substring(2);
            decimal exposure = Decimal.Parse(temp);
            if (exposure > 0)
            {
               result = (1 / exposure);
            }
         }

         return result;
      }
   }

   /// <summary>
   /// Converts an exposure mode from an enum into a human-readable string (e.g. AperturePriority
   /// becomes "Aperture Priority")
   /// </summary>
   public class ExposureModeConverter : IValueConverter
   {
      public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
      {
         ExposureMode exposureMode = (ExposureMode)value;

         switch (exposureMode)
         {
            case ExposureMode.AperturePriority:
               return "Aperture Priority";

            case ExposureMode.HighSpeedMode:
               return "High Speed Mode";

            case ExposureMode.LandscapeMode:
               return "Landscape Mode";

            case ExposureMode.LowSpeedMode:
               return "Low Speed Mode";

            case ExposureMode.Manual:
               return "Manual";

            case ExposureMode.NormalProgram:
               return "Normal";

            case ExposureMode.PortraitMode:
               return "Portrait";

            case ExposureMode.ShutterPriority:
               return "Shutter Priority";

            default:
               return "Unknown";
         }
      }

      public object ConvertBack(object value, Type targetTypes, object parameter, CultureInfo culture)
      {
         throw new NotSupportedException();
      }
   }

   /// <summary>
   /// Converts a lens aperture from a decimal into a human-preferred string (e.g. 1.8 becomes F1.8)
   /// </summary>
   public class LensApertureConverter : IValueConverter
   {
      public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
      {
         if (value != null)
         {
            return String.Format("f/{0:##.0}", value);
         }
         else
         {
            return String.Empty;
         }
      }

      public object ConvertBack(object value, Type targetTypes, object parameter, CultureInfo culture)
      {
         if (!String.IsNullOrEmpty((string)value))
         {
            return Decimal.Parse(((string)value).Substring(1));
         }
         else
         {
            return null;
         }
      }
   }

   /// <summary>
   /// Converts a focal length from a decimal into a human-preferred string (e.g. 85 becomes 85mm)
   /// </summary>
   public class FocalLengthConverter : IValueConverter
   {
      public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
      {
         if (value != null)
         {
            return String.Format("{0} mm", value);
         }
         else
         {
            return String.Empty;
         }
      }

      public object ConvertBack(object value, Type targetTypes, object parameter, CultureInfo culture)
      {
         throw new NotSupportedException();
      }
   }
}