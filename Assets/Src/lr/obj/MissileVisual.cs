using UnityEngine;
using System.Collections.Generic;

namespace lr 
{
	namespace obj
	{

		public class MissileVisual : MonoBehaviour, battle.IBattleListener
		{
			public Types.Side Side { set; get; }

			public Vector3 Position { get { return transform.position; } }


			private Vector3 TargetPos;

			// battle context
			private lr.battle.Context Context { get; set; }


			private data.MissileInstance InstanceClone { get; set; }

			private List<IDestroyable> HitDestroyables;

			public void Init(battle.Context context, data.MissileInstance missileInstance, Types.Side side)
			{
				HitDestroyables = new List<IDestroyable> ();
				Context = context;
				InstanceClone = missileInstance.Clone ();
				Side = side;

				Context.Battle.RegisterListener (this);
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
			}

			void Update () 
			{
				transform.position += (TargetPos - transform.position).normalized * InstanceClone.Speed * Time.deltaTime;
			
				obj.IDestroyable destroyable = Context.Battle.FindNearestDestroyable (transform.position, Side == Types.Side.Player ? Types.Side.Enemy : Types.Side.Player);

				if (destroyable != null && Mathf.Abs(destroyable.Position.x - transform.position.x) < 0.1f && !HitDestroyables.Contains(destroyable))
				{
					HitDestroyables.Add(destroyable);

					destroyable.OnHit(InstanceClone.Attack);
				}

				if ( (transform.position - TargetPos).sqrMagnitude < 0.2f * 0.2f)
				{
					Destroy(gameObject);
				}
			}


			private void SetInitPosition()
			{
				transform.position = Context.Battle.GetTower (Side).OutputPosition.position;
			}

			private void SelectTarget()
			{
				TargetPos = Context.Battle.GetTower (Side == Types.Side.Enemy ? Types.Side.Player : Types.Side.Enemy).Position;
			}

#region interface imlepentation
			public void OnHit(int hitPoints)
			{

			}
			
		
			public void OnBattleEnded()
			{
				Destroy (gameObject);
			}

			public void OnDestroyableDie(obj.IDestroyable destroyable)
			{

			}

#endregion
		}
	}
}
