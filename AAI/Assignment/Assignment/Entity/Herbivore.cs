﻿using Assignment.Movement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Assignment.World;

namespace Assignment.Entity
{
	public class Herbivore : BaseEntity
	{
		public Herbivore() : base()
		{
			MaxSpeed = 5;
			SteeringBehaviours.Add(new ObstacleAvoidance { Render = true });
			SteeringBehaviours.Add(new Flee { Radius = 200, FleeFrom = EntityType.Omnivore });
			//SteeringBehaviours.Add(new Arrive { ArriveLocation = new Location(50, 50), Render = true });
			SteeringBehaviours.Add(new Wander());
			Type = EntityType.Herbivore;
		}

		public override void Render(Graphics g)
		{
			int size = 10;
			g.FillEllipse(Brushes.Blue, (int) Location.X - (size / 2), (int) Location.Y - (size / 2), size, size);

			int x = (int) (Location.X + (Math.Cos(Direction) * 30));
			int y = (int) (Location.Y + (Math.Sin(Direction) * 30));

			g.DrawLine(Pens.Blue, (int) Location.X, (int) Location.Y, x, y);
		}

		public override void Update(int tick)
		{
			CalculateSteeringForce();
		}
	}
}
