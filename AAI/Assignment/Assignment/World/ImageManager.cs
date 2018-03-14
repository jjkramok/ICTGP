using System;
using System.Collections.Generic;
using System.Drawing;

namespace Assignment.World
{
    class ImageManager
    {
        private static ImageManager _instance;

        private Dictionary<string, Image> images;

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

        public ImageManager()
        {
            images = new Dictionary<string, Image>();
        }

        /// <summary>
        /// Retreives images stored in the ImageManager or add a new one to the manager.
        /// </summary>
        /// <param name="entityName">filename minus extension</param>
        /// <returns>Either null or the bitmap</returns>
        public Image GetImage(string entityName)
        {
            Image image = null;
            string flatName = entityName.ToLower();

            images.TryGetValue(flatName, out image);
            if (image == null)
            {
                try
                {
                    // Image hasn't been loaded yet, load it now and add it to the manager.
                    images.Add(flatName, new Bitmap(string.Format(".\\Images\\{0}.png", flatName)));
                    images.TryGetValue(flatName, out image);
                } catch (ArgumentException e)
                {
                    Console.WriteLine("Image {0}.png not found", flatName);
                }
            }
            return image;
        }

    }
}
