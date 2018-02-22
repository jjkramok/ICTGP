using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment.Entity;
using Assignment.World;

namespace Assignment.Movement
{
	public class Wander : BaseSteering
	{
		public override SteeringForce Calculate(BaseEntity entity)
		{
			double direction = entity.Direction + GameWorld.Instance.Random.NextDouble() - 0.5;
			double force = GameWorld.Instance.Random.NextDouble();

			return new SteeringForce(direction, force);
		}
	}
}
