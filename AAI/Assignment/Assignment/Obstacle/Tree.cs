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

		public Tree(Location location, double rotation) : base(location, rotation)
		{
			food = 100;
			CollisionCircles.Add(new ObstacleCircle(Location, 30));
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

		public override void Render(Graphics g)
		{
            //base.Render(g);
			g.FillEllipse(Brushes.Green, (float) Location.X - 10, (float) Location.Y - 20, 30, 30);
			g.FillEllipse(Brushes.Green, (float) Location.X - 20, (float) Location.Y - 5, 25, 25);
			g.FillEllipse(Brushes.Green, (float) Location.X, (float) Location.Y - 10, 20, 20);
		}
	}
}
