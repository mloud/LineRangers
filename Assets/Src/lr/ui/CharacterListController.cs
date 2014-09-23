using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

namespace lr 
{
	namespace ui
	{
		public class CharacterListController : UnityEngine.MonoBehaviour
		{

			[SerializeField]
			Button btnShoot;

			[SerializeField]
			List<Button> btnCharacters;

			[SerializeField]
			Button btnMissile;

			public void Init(data.Player player)
			{

				// characters
				for (int i = 0; i < btnCharacters.Count; ++i)
				{
					CharacterController charController = btnCharacters[i].gameObject.GetComponent<CharacterController>();

					if (i < player.Characters.Count)
					{
						// set button for character controller
						btnCharacters[i].name = player.Characters[i].Name;
						btnCharacters[i].GetComponentInChildren<Text>().text = player.Characters[i].Name;
						charController.Init(player.Characters[i]);
					}
					else
					{
						btnCharacters[i].gameObject.SetActive(false);
						charController.scrollbar.gameObject.SetActive(false);
					}
				}


				MissileController missileController = btnMissile.gameObject.GetComponent<MissileController>();
				// missiles
				if (player.Missiles.Count > 0)
				{
					// set button for character controller
					missileController.Init(player.Missiles[0]);
				}
				else
				{
					missileController.gameObject.SetActive(false);
				}
			}


			public void OnCharacterClick(CharacterController charController)
			{
				charController.OnClick ();
			}

			public void OnMissileClick(MissileController missileController)
			{
				missileController.OnClick ();
			}



		}
	}
}
