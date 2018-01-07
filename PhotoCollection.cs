using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoViewer
{
    class PhotoCollection : ObservableCollection<IPhoto>
    {
        private string path;
        public PhotoCollection(string path) : base()
        {
            this.path = path;
        }

        public string Path
        {
            get
            {
                return this.path;
            }
            set
            {
                this.path = value;
            }
        }
    }
}
