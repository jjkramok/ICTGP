using Assignment.Movement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Assignment.World;
using Assignment.State;

namespace Assignment.Entity
{
	public class Herbivore : BaseEntity
	{
        public Herbivore() : base()
		{
			MaxSpeed = 5;
			Type = EntityType.Herbivore;
		}

        public override void Render(Graphics g)
		{
            base.Render(g);

            int x = (int)(Location.X + (Math.Cos(Direction) * 30));
            int y = (int)(Location.Y + (Math.Sin(Direction) * 30));

            g.DrawLine(Pens.Blue, (int)Location.X, (int)Location.Y, x, y);
        }

		public override void Update(int tick)
		{
			StateMachine.Execute(this);
			CalculateSteeringForce();
		}
	}
}
