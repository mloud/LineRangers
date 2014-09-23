using UnityEngine;
using System.Collections;

namespace lr 
{
	public class Game : MonoBehaviour
	{
		public static Game Instance { get; private set; }

		public ui.UI UI { get; private set; }

		public battle.IBattle Battle { get; set; }
		public data.Player Player { get; private set; }


		void Awake()
		{
			//DontDestroyOnLoad (this);
			Instance = this;

			GameObject playerGo = GameObject.Instantiate (Resources.Load ("prefabs/__Player__") as GameObject) as GameObject;
			playerGo.transform.parent = transform;
			Player = playerGo.GetComponent<data.Player> ();
			Player.Init ();
		
			GameObject uiGo = GameObject.Instantiate (Resources.Load ("prefabs/__UI__") as GameObject) as GameObject;
			uiGo.transform.parent = transform;
			UI = uiGo.GetComponent<ui.UI> ();
		
		}


		void Start()
		{
			StartNewBattle ();
		}

		public void StartNewBattle()
		{
			lr.battle.Battle battle = GameObject.FindObjectOfType<lr.battle.Battle> ();
			if (battle == null)
			{
				GameObject battleGo = new GameObject ("__Battle__");
				battleGo.transform.parent = transform;
				Battle = battleGo.AddComponent <lr.battle.Battle> ();
			}


			Battle.Init ();

			UI.InitForBattle ();
		}

		public void Replay()
		{
			Application.LoadLevel (Application.loadedLevel);
		}

	}

}
