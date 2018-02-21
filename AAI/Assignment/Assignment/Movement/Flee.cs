﻿using Assignment.Entity;
using Assignment.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Movement
{
	class Flee : BaseSteering
	{
		public EntityType FleeFrom;
		public double Radius;
		public double Multiplier = 5;

		public override SteeringForce Calculate(BaseEntity entity)
		{
			var fleeFromEntities = GameWorld.Instance.EntitiesInArea(entity.Location, Radius);
			SteeringForce force = new SteeringForce();
			int forcesCount = 0;
			foreach(var fleeFromEntity in fleeFromEntities)
			{
				if(fleeFromEntity.Type == FleeFrom)
				{
					var direction = Utilities.Direction(entity.Location, fleeFromEntity.Location) - Math.PI;
					var distance = Utilities.Distance(entity.Location, fleeFromEntity.Location);
					force += new SteeringForce(direction, (1 / distance) * Multiplier);

					forcesCount++;
				}
			}

			force = force / forcesCount;

			return force;
		}
	}
}
