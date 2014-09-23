using UnityEngine;
using System.Collections;

namespace lr 
{
	namespace obj
	{

		public class Tower : MonoBehaviour, IDestroyable
		{
			[SerializeField]
			public Transform OutputPosition;

			[SerializeField]
			int hp;

			[SerializeField]
			Types.Side side;

			[SerializeField]
			ui.HealthBarController healthBar;

		
			public Types.Side Side { get { return side; } }

			public Vector3 Position { get { return OutputPosition.position; } }

			public int Hp { get { return hp; } }


			private int FullHp { get; set; }

			void Start () 
			{
				FullHp = Hp;

				healthBar.SetValue (hp / (float)FullHp);
			}
		
			void Update () 
			{
			
			}

#region interface imlepentation
			public void OnHit(int hitPoints)
			{
				hp -= hitPoints;

				if (hp <= 0)
				{
					hp = 0;

					Game.Instance.Battle.OnTowerDie(this);
				}

				healthBar.SetValue (hp / (float)FullHp);
			}
			
			public void OnDestroy()
			{
				
			}
#endregion
		}
	}
}
