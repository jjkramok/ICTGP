using Assignment.World;
using System.Drawing;

namespace Assignment.Obstacle
{
	public class Bush : BaseObstacle
	{
		public Bush(Location location, double rotation) : base(location, rotation)
		{
		}

		public override bool Render(Graphics g)
		{
            return base.Render(g);
		}
	}
}
