using UnityEngine;
using System.Collections.Generic;

namespace lr 
{
	namespace battle
	{
		public class Context
		{
			public Context(Battle battle)
			{
				Battle = battle;
			}

			public IBattle Battle { get; set; }
		}
	}
}
