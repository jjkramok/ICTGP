using Assignment.Movement;
using Assignment.State;
using Assignment.World;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Entity
{
	public class Omnivore : BaseEntity
	{
        public Omnivore() : base()
		{
            MaxSpeed = 5;
			Type = EntityType.Omnivore;
		}

		public override void Render(Graphics g)
		{
            base.Render(g);

			int x = (int) (Location.X + (Math.Cos(Direction) * 30));
			int y = (int) (Location.Y + (Math.Sin(Direction) * 30));

			g.DrawLine(Pens.Red, (int) Location.X, (int) Location.Y, x, y); 
		}

		public override void Update(int tick)
		{
			StateMachine.Execute(this);
			CalculateSteeringForce();
		}
	}
}
