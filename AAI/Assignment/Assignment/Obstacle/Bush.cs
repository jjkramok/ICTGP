using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment.World;
using System.Drawing;

namespace Assignment.Obstacle
{
	public class Bush : BaseObstacle
	{
		public Bush(Location location, double rotation) : base(location, rotation)
		{
		}

		public override void Render(Graphics g)
		{
            base.Render(g);
		}
	}
}
