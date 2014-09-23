using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

namespace lr 
{
	namespace ui
	{
		public class HealthBarController : UnityEngine.MonoBehaviour
		{
			[SerializeField]
			SpriteRenderer sprFill;

			private float Value { get; set; }

			public void SetValue(float value)
			{
				core.Dbg.Assert (value >= 0 && value <= 1, "HealthBarController.SetValue() exceeds limits <0,1>");			
			
				Vector3 scale = sprFill.transform.localScale;

				scale.x = value;

				sprFill.transform.localScale = scale;
			}
		}
	}
}
