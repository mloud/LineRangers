using UnityEngine;
using System.Collections.Generic;

namespace lr 
{
	namespace battle
	{
	
		public class AI : MonoBehaviour
		{
			[SerializeField]
			float SpawnCheckInterval = 8.0f;

			data.Player Enemy { get; set; }

			List<EnemyController> EnemyControllers { get; set; }


			private float SpawnCheckTimer { get; set; }

			public class EnemyController
			{

				private float TimerEnd { get; set; }
				private float TimerStart { get; set; }
				
				public enum State
				{
					Inactive,
					Producing,
					Finished
				}
				
				public State CurrentState { get; set; }
				

				public data.CharacterInstance CharInstance { get; private set; }
				
				public void Init(data.CharacterInstance charInstance)
				{
					CharInstance = charInstance;
					CurrentState = State.Inactive;
				}
				
				public void StartProducing()
				{
					core.Dbg.Assert (CurrentState == State.Inactive);
					
					if (CurrentState == State.Inactive)
					{
						TimerStart = Time.time;
						TimerEnd = TimerStart + CharInstance.RefreshTime;
						CurrentState = State.Producing;
					}
				}
				

				
				public void Update()
				{
					if (CurrentState == State.Producing)
					{
						if (Time.time > TimerEnd)
						{
							CurrentState = State.Finished;
						}
					}
				}
				
				float GetProgress()
				{
					return (Time.time - TimerStart) / (TimerEnd - TimerStart);
				}
				
				public void Release()
				{
					Game.Instance.Battle.ReleaseCharacter (CharInstance, obj.Types.Side.Enemy);
									
					CurrentState = State.Inactive;
				}


//				public void OnClick()
//				{
//					if (CurrentState == State.Finished)
//					{
//						Game.Instance.Battle.ReleaseCharacter (CharInstance, obj.Types.Side.Player);
//						
//						CurrentState = State.Inactive;
//					}
//					else if (CurrentState == State.Inactive)
//					{
//						StartProducing();
//					}
//				}
				
			}

			public void Init(data.Player enemy)
			{
				Enemy = enemy;
				SpawnCheckTimer = Time.time;
				EnemyControllers = new List<EnemyController> ();

				for (int i = 0; i < Enemy.Characters.Count; ++i)
				{
					EnemyController enemyController = new EnemyController();
					enemyController.Init(Enemy.Characters[i]);

					EnemyControllers.Add(enemyController);

					enemyController.StartProducing();
				}
			}


			public void Update()
			{

				if (Time.time > SpawnCheckTimer)
				{

					List<EnemyController> finished = new List<EnemyController>();
					List<EnemyController> toProduce = new List<EnemyController>();

					for (int i = 0; i < EnemyControllers.Count; ++i)
					{
						EnemyControllers[i].Update();

						if (EnemyControllers[i].CurrentState == EnemyController.State.Finished)
							finished.Add(EnemyControllers[i]);

						if (EnemyControllers[i].CurrentState == EnemyController.State.Inactive)
							toProduce.Add(EnemyControllers[i]);

					}


					if (finished.Count > 0)
					{
						int rndIndex = Random.Range(0, finished.Count);

						finished[rndIndex].Release();
					}

					if (finished.Count > 0)
					{
						int rndIndex = Random.Range(0, finished.Count);
						
						finished[rndIndex].StartProducing();
					}

					SpawnCheckTimer = Time.time + Random.Range(0.8f, 1.2f) * SpawnCheckInterval;
				}



			}	
		}
	}
}
