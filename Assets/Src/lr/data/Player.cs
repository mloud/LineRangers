using UnityEngine;
using System.Collections.Generic;

namespace lr 
{
	namespace data
	{
		public class Player : UnityEngine.MonoBehaviour
		{
			[SerializeField]
			public string Name;


			[SerializeField]
			List<CharacterPrototype> InitCharacterPrototypes;

			[SerializeField]
			List<MissilePrototype> InitMissilePrototypes;

			public List<CharacterInstance> Characters;
			public List<MissileInstance> Missiles;

			
			public void Init()
			{
				InitDebugCharacters ();
			}

			void InitDebugCharacters()
			{
				Characters = new List<CharacterInstance> (InitCharacterPrototypes.Count);
				Missiles =  new List<MissileInstance> (InitMissilePrototypes.Count);

				for (int i = 0; i < InitCharacterPrototypes.Count; ++i)
				{
					Characters.Add(new CharacterInstance(InitCharacterPrototypes[i])); 
				}

				for (int i = 0; i < InitMissilePrototypes.Count; ++i)
				{
					Missiles.Add(new MissileInstance(InitMissilePrototypes[i])); 
				}
			}
		}
	}
}
