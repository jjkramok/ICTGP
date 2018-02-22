using Assignment.Movement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Entity
{
	public class Herbivore : BaseEntity
	{
		public Herbivore() : base()
		{
			MaxSpeed = 10;
			SteeringBehaviours.Add(new Wander());
		}

		public override void Update(int tick)
		{
			CalculateSteeringForce();
		}
	}
}
