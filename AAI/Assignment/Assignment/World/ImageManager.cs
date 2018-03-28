using Assignment.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Assignment.World
{
    class ImageManager
    {
        private static ImageManager _instance;

        private Dictionary<string, RotatedImageContainer> images;

        public static ImageManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ImageManager();
                }
                return _instance;
            }
        }

		public static void Delete()
		{
			_instance = null;
		}

        private ImageManager()
        {
            images = new Dictionary<string, RotatedImageContainer>();
        }

        /// <summary>
        /// Retreives images stored in the ImageManager or add a new one to the manager.
        /// </summary>
        /// <param name="entityName">filename minus extension</param>
        /// <param name="rotation">rotation of the desired sprite in radians</param>
        /// <returns>Either null or the bitmap</returns>
        public Image GetImage(string entityName, double rotation)
        {
            RotatedImageContainer image = null;
            string flatName = entityName.ToLower();

            if (!images.TryGetValue(flatName, out image))
            {
                try
                {
                    // Image hasn't been loaded yet, load it now and add it to the manager.
                    images.Add(flatName, new RotatedImageContainer(new Bitmap(string.Format(".\\Images\\{0}.png", flatName))));
                    images.TryGetValue(flatName, out image);
                } catch (ArgumentException e)
                {
                    // No image found, add a debug image
                    Console.WriteLine("Image {0}.png not found", flatName);
                    images.Add(flatName, null);
                }
            }
            return image?.GetClosestRotation(rotation);
        }

        /// <summary>
        /// A type used to store multiple pre-calculated rotations of an image to save computing time during runtime.
        /// </summary>
        class RotatedImageContainer
        {
            Image[] rotations;
            int AmountOfRotations = 8;

            /// <summary>
            /// Constructs the container based on a sprite. Calculates all possible rotations and stores them.
            /// </summary>
            /// <param name="original"></param>
            public RotatedImageContainer(Image original)
            {
                rotations = new Image[AmountOfRotations];
                for (int i = 0; i < rotations.Length; i++)
                {
                    rotations[i] = Utility.RotateImage(original, (float) (i * (360 / AmountOfRotations)) - 90);
                }
            }

            /// <summary>
            /// Retrieves the closest rotated image contained in the container.
            /// </summary>
            /// <param name="rads"></param>
            /// <returns></returns>
            public Image GetClosestRotation(double rads)
            {
                // Check for negative input. Force the angle on a single unit circle rotation and calculate its positive counterpart.
                rads %= Math.PI * 2;
                if (rads < 0)
                {   
                    rads = (2 * Math.PI) - rads;
                }

                int index = (int)Math.Round(rads / (Math.PI * 2 / AmountOfRotations)) % AmountOfRotations;
                return rotations[index];
            }
        }

    }
}
