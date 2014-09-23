using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

namespace lr 
{
	namespace ui
	{
		public class UI : UnityEngine.MonoBehaviour
		{
			[SerializeField]
			GameObject dlgYouWonPrefab;
			
			[SerializeField]
			GameObject dlgYouLostPrefab;

			private Canvas Canvas { get; set; }

			private CharacterListController CharListController { get; set; }

			public void Awake()
			{
				Canvas = GameObject.FindObjectOfType<Canvas> ();
			}

			public void InitForBattle()
			{
				CharListController = GameObject.FindObjectOfType<CharacterListController> ();

				core.Dbg.Assert (CharListController != null, "UI.InitForBattle() no CharacterListController found!");


				CharListController.Init (Game.Instance.Player);
			}


			public void OpenDialog(string name)
			{
				GameObject prefab = null;
				if (name == "DlgYouWon")
				{
					prefab = dlgYouWonPrefab;
				}
				else if (name == "DlgYouLost")
				{
					prefab = dlgYouLostPrefab;
				}

				core.Dbg.Assert (prefab != null, "UI.OpenDialog() no such dialog " + name);

				GameObject dialogGo = Instantiate (prefab) as GameObject;



				dialogGo.transform.parent = Canvas.transform;
				dialogGo.transform.localPosition = Vector3.zero;
				RectTransform rectTrs = (dialogGo.transform as RectTransform);
			
				rectTrs.offsetMax = Vector2.zero;
				rectTrs.offsetMin = Vector2.zero;
			}

		
			public void OnButtonClick(Button button)
			{
				if (button.name == "BtnReplay")
				{
					Game.Instance.Replay();
				}
				else if (button.name == "BtnMenu")
				{

				}
			
				core.Dbg.Log("UI.OnButtonClick() " + button.name);
			}
		}
	}
}
