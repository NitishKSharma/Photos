using PhotoViewer.Exif;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace PhotoViewer
{
    class Photo : IPhoto
    {
        private bool exists;
        private BitmapFrame image;
        private Uri source;
        private ExifMetadata exifMetadata;
        public Photo(Uri path)
        {
            if(path.IsFile)
            {
                this.source = path;
                Refresh();
            }
        }
        public Photo(string path) : this(new Uri(path))
        {
            
        }
        public bool Exists
        {
            get
            {
                return this.exists;
            }
        }
        public BitmapFrame Image
        {
            get
            {
                return this.image;
            }
        }
        public Uri Source
        {
            get
            {
                return this.source;
            }
        }
        public ExifMetadata ExifMetadata
        {
            get
            {
                return this.exifMetadata;
            }
        }
        public override string ToString()
        {
            return source.ToString();
        }
        public void Refresh()
        {
            if(this.source != null)
            {
                this.image = BitmapFrame.Create(this.source);
                this.exifMetadata = new ExifMetadata(this.source);
                this.exists = true;
            }
            else
            {
                this.image = null;
                this.exifMetadata = new ExifMetadata();
                this.exists = false;
            }
        }
    }
}
