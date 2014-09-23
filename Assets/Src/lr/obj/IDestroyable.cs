using UnityEngine;
using System.Collections;

namespace lr 
{
	namespace obj
	{

		public interface IDestroyable
		{
			Types.Side Side { get; }

			Vector3 Position { get; }

			void OnHit(int hitPoints);

			void OnDestroy();

			int Hp { get; }
		}
	}
}
