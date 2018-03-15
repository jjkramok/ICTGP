using Assignment.World;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Obstacle
{
	public abstract class BaseObstacle : BaseObject
	{
		public List<ObstacleCircle> CollisionCircles;

		public BaseObstacle(Location location, double rotation)
		{
			Location = location;
			Direction = rotation;

			CollisionCircles = new List<ObstacleCircle>();
		}
		public virtual void Render(Graphics g)
        {
            int size = 10;

            Image sprite = ImageManager.Instance.GetImage(GetType().Name, Direction);
            if (sprite == null)
            {
                g.FillEllipse(Brushes.Gray, (int)Location.X - 10, (int)Location.Y - 10, 20, 20);
            }
            else
            {
                g.DrawImage(sprite, (int)Location.X - (size / 2), (int)Location.Y - (size / 2), size, size);
            }
        }
	}
}
