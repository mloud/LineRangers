using UnityEngine;
using System.Collections;

namespace lr 
{
	namespace battle
	{
		public interface IBattleListener
		{
			void OnBattleEnded();
			void OnDestroyableDie(obj.IDestroyable destroyable);
		}

		public interface IBattle
		{
			void RegisterListener (IBattleListener listener);

			void UnRegisterListener(IBattleListener listener);
		
			void Init();

			void ReleaseCharacter(data.CharacterInstance charInstance, obj.Types.Side side);

			void ReleaseMissile(data.MissileInstance missileInstance, obj.Types.Side side);
					
			obj.IDestroyable FindNearestDestroyable(Vector3 from, obj.Types.Side side);

	
			obj.Tower GetTower(obj.Types.Side side);

			void OnCharacterDie(obj.CharacterVisual charVisual);

			void OnTowerDie(obj.Tower towerVisual);


		}
	}
}
