using UnityEngine;
using System.Collections;

namespace lr 
{
	namespace data
	{
		public class CharacterInstance
		{
			public CharacterInstance(CharacterPrototype prototype)
			{
				Prototype = prototype;
				Attack = prototype.Attack;
				AttackSpeed = prototype.AttackSpeed;
				Hp = prototype.Hp;
				RefreshTime = prototype.RefreshTime;
			}

			public string Name { get { return Prototype.Name; } }
			public GameObject BattleVisual { get { return Prototype.BattleVisual; } }
			public GameObject UIVisual { get { return Prototype.UIVisual; } }


			// data
			public CharacterPrototype Prototype { get; set; }

			public int Attack;
			public int Hp;
			public float AttackSpeed;
			public float Speed;
			public float RefreshTime;
			// end data

			public CharacterInstance Clone()
			{
				CharacterInstance instance = new CharacterInstance (Prototype);

				instance.Attack = Attack;
				instance.Hp = Hp;
				instance.AttackSpeed = AttackSpeed;
				instance.RefreshTime = RefreshTime;

				return instance;
			}
		}
	}
}
