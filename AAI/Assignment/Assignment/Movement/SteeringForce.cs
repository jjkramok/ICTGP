﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Movement
{
	class SteeringForce
	{
		public double Direction;
		public double Amount;

		public SteeringForce()
		{
			Amount = 0;
			Direction = 0;
		}

		public SteeringForce(double direction, double amount)
		{
			Direction = direction;
			Amount = amount;
		}

		public static SteeringForce operator +(SteeringForce force1, SteeringForce force2)
		{
			double x = Math.Cos(force1.Direction) * force1.Amount + Math.Cos(force2.Direction) * force2.Amount;
			double y = Math.Sin(force1.Direction) * force1.Amount + Math.Sin(force2.Direction) * force2.Amount;

			double amount = Math.Sqrt(x * x + y * y);
			double direction = Math.Tanh(y / x);

			return new SteeringForce(direction, amount);
		}

		public static SteeringForce operator /(SteeringForce force, double amount)
		{
			return new SteeringForce(force.Direction, force.Amount / amount);
		}
	}
}
