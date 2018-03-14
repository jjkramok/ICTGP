﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment.Entity;
using System.Drawing;
using Assignment.World;
using Assignment.Utilities;

namespace Assignment.Movement
{
	public class Flocking : BaseSteering
	{
		public double searchRadius = 40;
		public double SeperationStrength = 0.5;
		public double CohesionStrength = 0.5;
		public double AlignmentStrength = 10;

		public override SteeringForce Calculate(BaseEntity entity)
		{
			var closeEntities = GameWorld.Instance.EntitiesInArea(entity.Location, searchRadius);
			closeEntities = closeEntities.Where(x => x.Type == entity.Type && x != entity).ToList();

			return Alignment(entity, closeEntities) + Seperation(entity, closeEntities) + Cohesion(entity, closeEntities);
		}

		public override void Render(Graphics g, BaseEntity entity)
		{
		}

		private SteeringForce Alignment(BaseEntity entity, List<BaseEntity> closeEntities)
		{
			var force = new SteeringForce();
			foreach (var closeEntity in closeEntities)
			{
				force += new SteeringForce(closeEntity.Direction, AlignmentStrength);
			}
			return force / closeEntities.Count;
		}

		private SteeringForce Seperation(BaseEntity entity, List<BaseEntity> closeEntities)
		{
			var force = new SteeringForce();
			foreach (var closeEntity in closeEntities)
			{
				var direction = Utility.Direction(entity.Location, closeEntity.Location);
				var distance = Utility.Distance(entity.Location, closeEntity.Location);

				force += new SteeringForce(direction + Math.PI, (searchRadius - distance) * SeperationStrength);
			}
			return force / closeEntities.Count;
		}

		private SteeringForce Cohesion(BaseEntity entity, List<BaseEntity> closeEntities)
		{
			var avgLocation = new Location(0, 0);
			foreach (var closeEntity in closeEntities)
			{
				avgLocation.X += closeEntity.Location.X;
				avgLocation.Y += closeEntity.Location.Y;
			}
			avgLocation.X = avgLocation.X / closeEntities.Count;
			avgLocation.Y = avgLocation.Y / closeEntities.Count;
			return new SteeringForce(Utility.Direction(entity.Location, avgLocation), Utility.Distance(entity.Location, avgLocation) * CohesionStrength);
		}
	}
}
