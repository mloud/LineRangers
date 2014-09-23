using UnityEngine;
using System.Collections;

namespace lr 
{
	namespace data
	{
		public class MissilePrototype : UnityEngine.MonoBehaviour
		{
			public string Name;
			public int Attack;
			public float Speed;
			public float RefreshTime;
			public float Radius;

			public GameObject BattleVisual;
			public GameObject UIVisual;
		}
	}
}
