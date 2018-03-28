using Assignment.Entity;
using Assignment.Utilities;
using Assignment.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Assignment.Movement
{
	public class Seek : BaseSteering
	{
		public BaseEntity ChaseEntity;
		public double MaxDistance;
		public double Force = 5;

		public Seek() : base()
		{
			Priority = 0.4;
		}

		public override SteeringForce Calculate(BaseEntity entity)
		{
			if(ChaseEntity == null || entity == ChaseEntity)
			{
				BehaviorDone = true;
				return new SteeringForce();
			}

			var distance = Utility.Distance(entity.Location, ChaseEntity.Location);
			if (distance > MaxDistance)
			{
				BehaviorDone = true;
				return new SteeringForce();
			}

			var direction = Utility.Direction(entity.Location, ChaseEntity.Location);

			return new SteeringForce(direction, Force);
		}

		public override void Render(Graphics g, BaseEntity entity)
		{
			g.DrawLine(Pens.White, (float) entity.Location.X, (float) entity.Location.Y, (float) ChaseEntity.Location.X, (float) ChaseEntity.Location.Y);
		}
	}
}
