using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace AssemblyCSharp
{
	public class GameBehaviour : MonoBehaviour {

		[SerializeField]
		private GameObject levelFinishPopup;

		private GameElementsController gameElementsModel;
		private PlayerModel playerModel;
		private GameStateModel gameStateModel;

		void Awake()
		{
			Init();
		}

		private void Init()
		{
			gameElementsModel = new GameElementsController();
			playerModel = new PlayerModel();
			gameStateModel = new GameStateModel();
			levelFinishPopup.SetActive(false);

			InitEventListeners();

			GameEvent.GameInitialized();
		}

		private void InitEventListeners()
		{
			GameEvent.onPlayerCharReachedTargetZone += OnPlayerCharReachTargetZone;
			GameEvent.onGameStateChanged += OnGameStateChanged;
		}

		private void RemoveEventListeners()
		{
			GameEvent.onPlayerCharReachedTargetZone -= OnPlayerCharReachTargetZone;
			GameEvent.onGameStateChanged -= OnGameStateChanged;
		}

		private void OnPlayerCharReachTargetZone(GameObject charGO)
		{
			// TODO: Update Score
		}

		private void OnGameStateChanged(string gameState, bool isWin)
		{
			if(gameState == GameStateModel.GAME_STATE_STOPPED)
			{
				if(isWin)
				{
					OnWin();
				}
				else
				{
					OnLose();
				}
			}
		}

		private void OnWin()
		{
			ShowWinMessage();
		}

		private void OnLose()
		{
			ShowLoseMessage();
		}

		private void ShowWinMessage()
		{
			levelFinishPopup.SetActive(true);
			levelFinishPopup.transform.Find("YouWonText").GetComponent<Text>().text = "You Won!";
			levelFinishPopup.transform.Find("RestartButton").GetComponent<Button>().onClick.AddListener(OnRestartClick);
		}

		private void ShowLoseMessage()
		{
			levelFinishPopup.SetActive(true);
			levelFinishPopup.transform.Find("YouWonText").GetComponent<Text>().text = "You Lost";
			levelFinishPopup.transform.Find("RestartButton").GetComponent<Button>().onClick.AddListener(OnRestartClick);
		}

		private void OnRestartClick()
		{
			Reset();
		}

		private void Reset()
		{
			gameElementsModel.Destroy();
			playerModel.Destroy();
			gameStateModel.Destroy();

			RemoveEventListeners();
			Application.LoadLevel("MainScene");
		}

		void Update ()
		{
			if(GameStateModel.GameState == GameStateModel.GAME_STATE_PLAYING)
			{
				gameElementsModel.Update();
			}
		}
	}
}
