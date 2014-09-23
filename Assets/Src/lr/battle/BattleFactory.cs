using UnityEngine;
using System.Collections.Generic;

namespace lr 
{
	namespace battle
	{
		public class BattleFactory
		{

			public obj.CharacterVisual CreateCharacterBattleVisual(data.CharacterInstance fromInstance, battle.Context context, obj.Types.Side side)
			{
				GameObject visualGo = GameObject.Instantiate(fromInstance.BattleVisual) as GameObject;

				obj.CharacterVisual visual = visualGo.GetComponent<obj.CharacterVisual> ();

				visual.Init (context, fromInstance, side);

				return visual;
			}

			public obj.MissileVisual CreateMissileBattleVisual(data.MissileInstance fromInstance, battle.Context context, obj.Types.Side side)
			{
				GameObject visualGo = GameObject.Instantiate(fromInstance.BattleVisual) as GameObject;
				
				obj.MissileVisual visual = visualGo.GetComponent<obj.MissileVisual> ();
				
				visual.Init (context, fromInstance, side);
				
				return visual;
			}

		}


			
			

	}
}
