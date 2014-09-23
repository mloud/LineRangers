using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace core
{

	public static class GOUtils
	{

		public static List<I> FindObjectsOfInterface<I>() where I : class
		{

			MonoBehaviour[] monoBehaviours = GameObject.FindObjectsOfType<MonoBehaviour>();
			List<I> list = new List<I>();
			
			foreach(MonoBehaviour behaviour in monoBehaviours)
			{
				I component = behaviour.GetComponent(typeof(I)) as I;
				
				if(component != null)
				{
					list.Add(component);
				}
			}
			
			return list;
		}
	}
}