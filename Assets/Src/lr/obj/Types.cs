using UnityEngine;
using System.Collections;

namespace lr 
{
	namespace obj
	{

		public static class Tag
		{
			public static string TowerPlayer = "TowerPlayer";
			public static string TowerEnemy = "TowerEnemy";
		}

		public static class Types
		{
			public enum Side
			{
				Player = 0,
				Enemy = 1
			}
		}

	}
}
