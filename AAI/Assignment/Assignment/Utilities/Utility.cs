using Assignment.World;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Utilities
{
	static class Utility
	{
		public static double Distance(Location location1, Location location2)
		{
			return Math.Sqrt(
					(location1.X - location2.X) * (location1.X - location2.X) +
					(location1.Y - location2.Y) * (location1.Y - location2.Y)
				);
		}

		public static double Direction(Location from, Location to)
		{
			if(to.X > from.X)
				return Math.Atan((from.Y - to.Y) / (from.X - to.X));
			else
				return Math.Atan((from.Y - to.Y) / (from.X - to.X)) + Math.PI;
		}

		public static double BoundValue(double value, double min, double max)
		{
			return Math.Min(Math.Max(value, min), max);
		}

		public static double BoundValueMin(double value, double min)
		{
			return Math.Max(value, min);
		}

		public static double BoundValueMax(double value, double max)
		{
			return Math.Min(value, max);
		}

        /// <summary>
        /// src: https://stackoverflow.com/questions/2163829/how-do-i-rotate-a-picture-in-winforms
        /// method to rotate an image either clockwise or counter-clockwise
        /// </summary>
        /// <param name="img">the image to be rotated</param>
        /// <param name="rotationAngle">the angle (in degrees).
        /// NOTE: 
        /// Positive values will rotate clockwise
        /// negative values will rotate counter-clockwise
        /// </param>
        /// <returns></returns>
        public static Image RotateImage(Image img, float rotationAngle)
        {
            //create an empty Bitmap image
            Bitmap bmp = new Bitmap(img.Width, img.Height);

            //turn the Bitmap into a Graphics object
            Graphics gfx = Graphics.FromImage(bmp);

            //now we set the rotation point to the center of our image
            gfx.TranslateTransform((float)bmp.Width / 2, (float)bmp.Height / 2);

            //now rotate the image
            gfx.RotateTransform(rotationAngle);

            gfx.TranslateTransform(-(float)bmp.Width / 2, -(float)bmp.Height / 2);

            //set the InterpolationMode to HighQualityBicubic so to ensure a high
            //quality image once it is transformed to the specified size
            gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;

            //now draw our new image onto the graphics object
            gfx.DrawImage(img, new Point(0, 0));

            //dispose of our Graphics object
            gfx.Dispose();

            //return the image
            return bmp;
        }

        public static double RadToDeg(double rad)
        {
            return (360 * rad) / (2 * Math.PI);
        }
    }
}
