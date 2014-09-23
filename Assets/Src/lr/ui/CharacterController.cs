using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

namespace lr 
{
	namespace ui
	{
		public class CharacterController : UnityEngine.MonoBehaviour
		{
	
			[SerializeField]
			public Scrollbar scrollbar;

			
			[SerializeField]
			public Image charImage;

			[SerializeField]
			public Animator animator;

		
			Button Button { get; set; }

			private float TimerEnd { get; set; }
			private float TimerStart { get; set; }

			enum State
			{
				Inactive,
				Producing,
				Finished
			}

			private State CurrentState { get; set; }

			void Awake()
			{
				Button = GetComponent<Button> ();
			}

			public data.CharacterInstance CharInstance { get; private set; }

			public void Init(data.CharacterInstance charInstance)
			{
				CharInstance = charInstance;
				CurrentState = State.Inactive;

				SpriteRenderer sprren = CharInstance.BattleVisual.transform.Find("Visual").GetComponent<SpriteRenderer> ();
				charImage.sprite = sprren.sprite;
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

			public void Release()
			{
				core.Dbg.Assert (CurrentState == State.Finished);

				if (CurrentState == State.Finished)
				{

					CurrentState = State.Inactive;
				}
			}

			void Update()
			{
				if (CurrentState == State.Producing)
				{
					if (Time.time > TimerEnd)
					{
						CurrentState = State.Finished;

						animator.SetTrigger("ready");
					}
				}

				SetProgress ();
			}

			float GetProgress()
			{
				return (Time.time - TimerStart) / (TimerEnd - TimerStart);
			}

			void SetProgress()
			{
				if (CurrentState == State.Producing)
				{
					scrollbar.gameObject.SetActive(true);
					scrollbar.size = GetProgress();
					Button.interactable = false;

				}
				else if (CurrentState == State.Finished)
				{
					scrollbar.gameObject.SetActive(false);
					Button.interactable = true;
				}
				else
				{
					scrollbar.gameObject.SetActive(true);
					Button.interactable = true;
					scrollbar.size = 0;
				}

			}

			public void OnClick()
			{
				if (CurrentState == State.Finished)
				{
					Game.Instance.Battle.ReleaseCharacter (CharInstance, obj.Types.Side.Player);

					CurrentState = State.Inactive;
				
					animator.SetTrigger("idle");
				}
				else if (CurrentState == State.Inactive)
				{
					StartProducing();
				}
			}

		}
	}
}
