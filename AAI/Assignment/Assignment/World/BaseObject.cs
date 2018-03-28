using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.World
{
    public abstract class BaseObject
    {
        private double _direction;

        public Location Location;
        public double Direction
        {
            set
            {
                // Reminder: direction can still be between 2pi rads and -2pi rads
                _direction = value;
                _direction %= 2 * Math.PI;
            }
            get
            {
                return _direction;
            }
        }
    }
}
