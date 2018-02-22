using Assignment.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.World
{
	public class GridCell
	{
		public List<BaseEntity> Entities { get; private set; }

		public GridCell()
		{
			Entities = new List<BaseEntity>();
		}
	}
}
