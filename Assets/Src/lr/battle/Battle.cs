using UnityEngine;
using System.Collections.Generic;

namespace lr 
{
	namespace battle
	{

		public class Battle : MonoBehaviour, IBattle
		{
			[SerializeField]
			private Transform characterContainer;

			private List<obj.Tower> Towers { get; set; }

			private List<lr.obj.CharacterVisual> Characters { get; set; }

			private BattleFactory BattleFactory { get; set; }

			private AI Ai { get; set; }

			private List<IBattleListener> Listeners { get; set; }

			public void Init()
			{
				Listeners = new List<IBattleListener>();

				BattleFactory = new BattleFactory ();

				// create GameObject as container for battle objects
				Transform containerTr = transform.FindChild ("Container");
				if (containerTr != null)
					Destroy(containerTr.gameObject);

				GameObject containerGo = new GameObject ("Container");
				containerGo.transform.parent = transform;
				characterContainer = containerGo.transform;

				// prepare list for characters
				Characters = new List<lr.obj.CharacterVisual> ();

				Towers = new List<lr.obj.Tower> (2);

				// set player tower
				GameObject towerPlayerGo = GameObject.FindGameObjectWithTag (lr.obj.Tag.TowerPlayer);

				core.Dbg.Assert (towerPlayerGo != null, "BattleInit.Init() - no TowerPlayer found");

				Towers.Add (towerPlayerGo.GetComponent<lr.obj.Tower> ());


				// set enemy tower
				GameObject towerEnemyGo = GameObject.FindGameObjectWithTag (lr.obj.Tag.TowerEnemy);
				
				core.Dbg.Assert (towerEnemyGo != null, "BattleInit.Init() - no TowerEnemy found");

				Towers.Add (towerEnemyGo.GetComponent<lr.obj.Tower> ());


				Ai = GameObject.FindGameObjectWithTag ("AI").GetComponent<AI> ();

				GameObject enemyGo = GameObject.FindGameObjectWithTag ("Enemy");
				data.Player enemy = enemyGo.GetComponent<data.Player> ();
				enemy.Init ();
				Ai.Init (enemy);
			}


			void Update()
			{

			}


			public obj.IDestroyable FindNearestDestroyable(Vector3 from, obj.Types.Side side)
			{
				List<lr.obj.IDestroyable> destroyables = core.GOUtils.FindObjectsOfInterface<lr.obj.IDestroyable> ();

				List<lr.obj.IDestroyable> sortedDestroyables = new List<lr.obj.IDestroyable> (destroyables.FindAll(x=>x.Side == side && x.Hp > 0)); 
				
				sortedDestroyables.Sort (delegate(lr.obj.IDestroyable x, lr.obj.IDestroyable y) {
					if ((x.Position - from).sqrMagnitude < (y.Position - from).sqrMagnitude)
						return -1;
					
					return 1;
				});
				
				return sortedDestroyables.Count > 0 ? sortedDestroyables [0] : null;
			}

			public obj.Tower GetTower(obj.Types.Side side)
			{
				return Towers[(int)side];
			}

			private void BattleEnded()
			{
				for (int i = 0; i < Listeners.Count; ++i)
				{
					Listeners[i].OnBattleEnded();
				}

				Ai.gameObject.SetActive (false);
			}

			public void ReleaseCharacter(data.CharacterInstance charInstance, obj.Types.Side side)
			{
				obj.CharacterVisual charVisual = BattleFactory.CreateCharacterBattleVisual (charInstance, new Context(this), side);

				charVisual.Init ();
			}


			public void ReleaseMissile(data.MissileInstance missileInstance, obj.Types.Side side)
			{
				obj.MissileVisual missileVisual = BattleFactory.CreateMissileBattleVisual (missileInstance, new Context(this), side);
				
				missileVisual.Init ();
			}


			public void OnCharacterDie(obj.CharacterVisual charVisual)
			{

				for (int i = 0; i < Listeners.Count; ++i)
				{
					Listeners[i].OnDestroyableDie(charVisual);
				}
				Destroy (charVisual.gameObject, 2.0f);
			}

			public void OnTowerDie(obj.Tower towerVisual)
			{
				if (towerVisual.Side == lr.obj.Types.Side.Player)
				{
					Game.Instance.UI.OpenDialog("DlgYouLost");
				}
				else
				{
					Game.Instance.UI.OpenDialog("DlgYouWon");
				}

				BattleEnded ();
			}

			public void RegisterListener(IBattleListener listener)
			{
				if (!Listeners.Contains(listener))
				{
					Listeners.Add(listener);
				}
			}

			public void UnRegisterListener(IBattleListener listener)
			{
				if (Listeners.Contains(listener))
				{
					Listeners.Remove(listener);
				}
			}

		}

	}
}
