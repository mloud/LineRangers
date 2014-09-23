using UnityEngine;
using System.Collections;

namespace lr 
{
	namespace data
	{
		public class CharacterPrototype : UnityEngine.MonoBehaviour
		{
			public string Name;
			public int Attack;
			public int Hp;
			public float AttackSpeed;
			public float Speed;
			public float RefreshTime;

			public GameObject BattleVisual;
			public GameObject UIVisual;
		}
	}
}
