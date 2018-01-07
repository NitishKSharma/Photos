using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhotoViewer.Exif
{
    public enum LightSource
    {
        Unknown = 0,
        Daylight = 1,
        Fluorescent = 2,
        Tungsten = 3,
        Flash = 10,
        StandardLightA = 17,
        StandardLightB = 18,
        StandardLightC = 19,
        D55 = 20,
        D65 = 21,
        D75 = 22,
    }
}
