using UnityEngine;
using System.Collections;

namespace lr 
{
	namespace data
	{
		public class MissileInstance
		{
			public MissileInstance(MissilePrototype prototype)
			{
				Prototype = prototype;
			}

			public string Name { get { return Prototype.Name; } }
			public GameObject BattleVisual { get { return Prototype.BattleVisual; } }
			public GameObject UIVisual { get { return Prototype.UIVisual; } }
			public float Radius { get { return Prototype.Radius; } }
			public int Attack { get { return Prototype.Attack; } }
			public float Speed { get { return Prototype.Speed; } }
			public float RefreshTime { get { return Prototype.RefreshTime; } }

			// data
			public MissilePrototype Prototype { get; set; }

			// end data

			public MissileInstance Clone()
			{
				MissileInstance instance = new MissileInstance (Prototype);

				return instance;
			}
		}
	}
}
