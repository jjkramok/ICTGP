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

		public override bool Render(Graphics g)
		{
            if (!base.Render(g))
            {
                int size = 10;
                g.FillEllipse(Brushes.Red, (int)Location.X - (size / 2), (int)Location.Y - (size / 2), size, size);

				int x = (int) (Location.X + (Math.Cos(Direction) * 30));
				int y = (int) (Location.Y + (Math.Sin(Direction) * 30));

				g.DrawLine(Pens.Red, (int) Location.X, (int) Location.Y, x, y);
			}
           
            return true;
		}

		public override void Update(int tick)
		{
			StateMachine.Execute(this);
			CalculateSteeringForce();
		}
	}
}
