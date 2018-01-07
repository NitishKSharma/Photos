using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Windows.Media.Imaging;

namespace PhotoViewer.Exif
{
    public struct ExifMetadata
    {
        private string cameraModel;
        private ushort? colorRepresentation;
        private string creationSoftware;
        private string dateTaken;
        private string equipmentManufacturer;
        private decimal? exposureCompensation;
        private ushort? exposureMode;
        private decimal? exposureTime;
        private ushort? flashMode;
        private decimal? focalLength;
        private decimal? horizontalResolution;
        private ushort? isoSpeed;
        private decimal? lensAperature;
        private ushort? lightSource;
        private decimal? verticalResolution;

        public ExifMetadata(Uri imageUri)
        {
            BitmapFrame frame = BitmapFrame.Create(imageUri, BitmapCreateOptions.DelayCreation, BitmapCacheOption.None);
            BitmapMetadata metadata = (BitmapMetadata)frame.Metadata;
            this.cameraModel = metadata.CameraModel;
            this.creationSoftware = metadata.ApplicationName;
            this.dateTaken = metadata.DateTaken;
            this.equipmentManufacturer = metadata.CameraManufacturer;

            // http://msdn.microsoft.com/en-us/library/ee872003(VS.85).aspx
            this.colorRepresentation = QueryMetadata<ushort>(metadata, "System.Image.ColorSpace");
            this.exposureCompensation = QueryMetadata<decimal>(metadata, "System.Photo.ExposureBias");
            this.exposureMode = QueryMetadata<ushort>(metadata, "System.Photo.ProgramMode");
            this.exposureTime = QueryMetadata<decimal>(metadata, "System.Photo.ExposureTime");
            this.flashMode = QueryMetadata<ushort>(metadata, "System.Photo.Flash");
            this.focalLength = QueryMetadata<decimal>(metadata, "System.Photo.FocalLength");
            this.horizontalResolution = QueryMetadata<decimal>(metadata, "System.Image.VerticalResolution");
            this.isoSpeed = QueryMetadata<ushort>(metadata, "System.Photo.ISOSpeed");
            this.lensAperature = QueryMetadata<decimal>(metadata, "System.Photo.FNumber");
            this.lightSource = QueryMetadata<ushort>(metadata, "System.Photo.LightSource");
            this.verticalResolution = QueryMetadata<decimal>(metadata, "System.Image.HorizontalResolution");
        }

        public string CameraModel
        {
            get
            {
                return this.cameraModel;
            }
        }

        public string CreationSoftware
        {
            get
            {
                return this.creationSoftware;
            }
        }

        public DateTime? DateTaken
        {
            get
            {
                DateTime? result = null;
                if (!String.IsNullOrWhiteSpace(this.dateTaken))
                {
                    DateTime date;
                    if (DateTime.TryParse(this.dateTaken, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out date))
                    {
                        result = date;
                    }
                }

                return result;
            }
        }

        public string EquipmentManufacturer
        {
            get
            {
                return this.equipmentManufacturer;
            }
        }

        public decimal? ExposureCompensation
        {
            get
            {
                return this.exposureCompensation;
            }
        }

        public ExposureMode ExposureMode
        {
            get
            {
                ExposureMode value = Exif.ExposureMode.Unknown;
                if (this.exposureMode.HasValue)
                {
                    if (!String.IsNullOrWhiteSpace(Enum.GetName(typeof(ExposureMode), this.exposureMode)))
                    {
                        value = (ExposureMode)this.exposureMode;
                    }
                }

                return value;
            }
        }

        public decimal? ExposureTime
        {
            get
            {
                return this.exposureTime;
            }
        }

        public FlashMode FlashMode
        {
            get
            {
                FlashMode value = FlashMode.FlashDidNotFire;
                if (this.flashMode.HasValue && this.flashMode % 2 == 1)
                {
                    value = FlashMode.FlashFired;
                }

                return value;
            }
        }

        public decimal? FocalLength
        {
            get
            {
                return this.focalLength;
            }
        }

        public decimal? HorizontalResolution
        {
            get
            {
                return this.horizontalResolution;
            }
        }

        public ushort? IsoSpeed
        {
            get
            {
                return this.isoSpeed;
            }
        }

        public decimal? LensAperture
        {
            get
            {
                return this.lensAperature;
            }
        }

        public LightSource LightSource
        {
            get
            {
                LightSource value = Exif.LightSource.Unknown;
                if (this.lightSource.HasValue)
                {
                    if (!String.IsNullOrWhiteSpace(Enum.GetName(typeof(LightSource), this.lightSource)))
                    {
                        value = (LightSource)this.lightSource;
                    }
                }

                return value;
            }
        }

        public decimal? VerticalResolution
        {
            get
            {
                return this.verticalResolution;
            }
        }

        private static Nullable<T> QueryMetadata<T>(BitmapMetadata metadata, string query) where T : struct
        {
           throw new NotImplementedException();
        }

        public ColorRepresentation ColorRepresentation
        {
            get
            {
                ColorRepresentation value = Exif.ColorRepresentation.Uncaliberated;
                if(this.colorRepresentation.HasValue)
                {
                    if(! String.IsNullOrWhiteSpace(Enum.GetName(typeof(ColorRepresentation), this.colorRepresentation)))
                    {
                        value = (ColorRepresentation)this.colorRepresentation;
                    }
                }
                return value;
            }            
        }
    }
}
