using Assignment.World;
using System.Drawing;

namespace Assignment.Obstacle
{
	public class Rock : BaseObstacle
	{
		public Rock(Location location, double rotation) : base(location, rotation)
		{
			CollisionCircles.Add(new ObstacleCircle(Location, 10));
		}

		public override void Render(Graphics g)
		{
			g.FillEllipse(Brushes.Gray, (int) Location.X - 10, (int) Location.Y - 10, 20, 20);
		}
	}
}
