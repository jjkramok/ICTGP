using Assignment.Entity;
using Assignment.Obstacle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.World
{
	public class GridCell<T> where T : BaseObject
	{
		public List<T> Objects { get; private set; }

		public GridCell()
		{
			Objects = new List<T>();
		}	
	}
}
