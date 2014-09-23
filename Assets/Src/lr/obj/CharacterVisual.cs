using UnityEngine;
using System.Collections;

namespace lr 
{
	namespace obj
	{

		public class CharacterVisual : MonoBehaviour, IDestroyable, battle.IBattleListener
		{
			[SerializeField]
			private Animator animator;

			public Types.Side Side { set; get; }

			public Vector3 Position { get { return transform.position; } }

			public int Hp { get { return InstanceClone.Hp; } }

			private enum State
			{
				Walking,
				Fighting,
				Idle,
				Dying,
				Inactive
			}

			// actual statemachine state
			private State CurrentState { get; set; }

		
			// actual target, if null try to search for any
			private IDestroyable Target { get; set; }

			// battle context
			private lr.battle.Context Context { get; set; }

			private float Timer { get; set; }

			private data.CharacterInstance InstanceClone { get; set; }

			private ui.HealthBarController HealthBar { get; set; }

			private float HpFull { get; set; }

			public void Init(battle.Context context, data.CharacterInstance characterInstance, Types.Side side)
			{

				Context = context;
				InstanceClone = characterInstance.Clone ();
				Timer = -1;
				Side = side;

				Context.Battle.RegisterListener (this);



				HealthBar = gameObject.GetComponentInChildren<ui.HealthBarController> ();
				HpFull = InstanceClone.Hp;

			}

			public void OnDestroy()
			{
				Context.Battle.UnRegisterListener (this);
			}

			void Start () 
			{}
		
			public void Init()
			{
				SetInitPosition ();

				SelectTarget ();

				UpdateHealthBar ();
			}

			void Update () 
			{
				switch (CurrentState)
				{

				case State.Walking:
					UpdateWalking();
					break;
				
				case State.Fighting:
					UpdateFighting();
					break;

				case State.Idle:
					UpdateIdle();
					break;
				}


			}

			private void UpdateHealthBar()
			{
				if (HealthBar != null)
				{
					HealthBar.SetValue((float)InstanceClone.Hp / HpFull);
				}
			}

			private void SetInitPosition()
			{
				transform.position = Context.Battle.GetTower (Side).OutputPosition.position;
			}

			private void SelectTarget()
			{
				Target = Context.Battle.FindNearestDestroyable (transform.position, Side == Types.Side.Enemy ? Types.Side.Player : Types.Side.Enemy);
			
				if (Target == null)
				{
					core.Dbg.Log("Character.SelectTarget() no target found");

					SetState(State.Idle);
				}
				else
				{
					SetState(State.Walking);	
				}
			}


			private void SetState(State state)
			{
				CurrentState = state;

				switch(CurrentState)
				{
				case State.Fighting:
					Timer = Time.time;
					break;

				case State.Walking:
					animator.SetTrigger("walk");
					break;
				case State.Idle:
					animator.SetTrigger("idle");
					Timer = Time.time;
					break;

				case State.Dying:
					animator.SetTrigger("death");
					break;

				case State.Inactive:
					animator.SetTrigger("idle");
					break;
				}

			}

			private void UpdateWalking()
			{
				//ckeck first for nearest target
				SelectTarget ();

				if ( (transform.position - Target.Position).sqrMagnitude < 1.0f * 1.0f)
				{
					animator.SetTrigger("idle");
					SetState(State.Fighting);					
				}
				else
				{
					transform.position += (Target.Position - transform.position).normalized * 0.01f; // todo speed
				}
			}

			private void UpdateFighting()
			{
				if (Time.time > Timer)
				{
					animator.SetTrigger("fight");

					Target.OnHit(InstanceClone.Attack);
					Timer = Time.time + InstanceClone.AttackSpeed;
				}
			}

			private void UpdateIdle()
			{
				if (Timer > 0 && Time.time > Timer)
				{
					SelectTarget();

					Timer = Time.time + 1.0f;
				}
			}

#region interface imlepentation
			public void OnHit(int hitPoints)
			{
				InstanceClone.Hp -= hitPoints;

				// I was killed
				if (InstanceClone.Hp <= 0)
				{
					SetState(State.Dying);

					InstanceClone.Hp = 0;

					Context.Battle.OnCharacterDie(this);

					if (HealthBar != null)
					{
						HealthBar.gameObject.SetActive(false);
					}
				}

				UpdateHealthBar ();
			}
			
		
			public void OnBattleEnded()
			{
				SetState (State.Inactive);
			}

			public void OnDestroyableDie(obj.IDestroyable destroyable)
			{
				if (Target == destroyable)
				{
					Target = null;
					SetState(State.Idle);
				}
			}

#endregion
		}
	}
}
