using Assignment.World;
using System;
using System.Drawing;

namespace Assignment.Obstacle
{
	public class Tree : BaseObstacle
	{
		private double food;
		private long lastTickUpdate;
		private double maxFood = 150;
		private double foodPerTick = 0.5;
        private ObstacleCircle[] treeTops;
        private Color Color;
        private readonly double TreeStumpRadius;
        
		public Tree(Location location, double rotation) : base(location, rotation)
		{
            var rand = GameWorld.Instance.Random;

            TreeStumpRadius = 5f;
            food = 100;
            Color = Color.FromArgb(125, 77, 158 + rand.Next(-25, 25), 58);
            treeTops = new ObstacleCircle[3];
            CollisionCircles.Add(new ObstacleCircle(location, TreeStumpRadius));
            for (int i = 0; i < treeTops.Length; i++)
            {
                Location treetopLocation = new Location(location.X + rand.Next(-10, 10), location.Y + rand.Next(-10, 10));
                ObstacleCircle treetop = new ObstacleCircle(treetopLocation, 30 + rand.Next(-10, 5));
                treeTops[i] = treetop;
            }
        }

		public double EatFood(double amount)
		{
			if(amount <= 0)
			{
				return 0;
			}

			CalculateCurrentFood();

			if(food < amount)
			{
				var result = food;
				food = 0;
				return result;
			}

			food -= amount;

			return amount;
		}

		private void CalculateCurrentFood()
		{
			long ticksPast = GameWorld.Instance.TickCounter - lastTickUpdate;
			food += foodPerTick * ticksPast;
			food = Math.Min(food, maxFood);

			lastTickUpdate = GameWorld.Instance.TickCounter;
		}

		public override bool Render(Graphics g)
		{
            if (!base.Render(g))
            {
                for (int i = 0; i < treeTops.Length; i++)
                {
                    var color = Color.FromArgb(Color.A + i * 25, Color.R, Color.G + i * 5, Color.B);
                    g.FillEllipse(new SolidBrush(color),
                        (float)(treeTops[i].Location.X - treeTops[i].Radius / 2),
                        (float)(treeTops[i].Location.Y - treeTops[i].Radius / 2),
                        (float)treeTops[i].Radius,
                        (float)treeTops[i].Radius);
                }
                var trunkColor = Color.FromArgb(80, 160, 82, 45);
                g.FillEllipse(new SolidBrush(trunkColor),
                    (float)(Location.X - TreeStumpRadius / 2),
                    (float)(Location.Y - TreeStumpRadius / 2),
                    (float)TreeStumpRadius, (float)TreeStumpRadius);
            }
            return true;
        }
	}
}
