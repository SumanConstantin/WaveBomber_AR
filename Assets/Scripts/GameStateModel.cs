using UnityEngine;
using System.Collections;

public class GameStateModel {
	public const string GAME_STATE_INIT = "gameStateInit";
	public const string GAME_STATE_PLAYING = "gameStatePlaying";
	public const string GAME_STATE_STOPPED = "gameStateStopped";

	private static string gameState = GAME_STATE_INIT;
	public static string GameState
	{
		get { return gameState; }
	}

	private int playerCharCount = 1;

	public GameStateModel()
	{
		Init();
	}

	private void Init()
	{
		InitEventListeners();
	}

	private void InitEventListeners()
	{
		GameEvent.onPlayerCharCountChanged += OnPlayerCharCountChanged;
		GameEvent.onNoActionsNoMovesLeft += OnNoActionsNoMovesLeft;
		GameEvent.onGameInitialized += OnGameInitialized;
	}

	private void RemoveEventListeners()
	{
		GameEvent.onPlayerCharCountChanged -= OnPlayerCharCountChanged;
		GameEvent.onNoActionsNoMovesLeft -= OnNoActionsNoMovesLeft;
		GameEvent.onGameInitialized -= OnGameInitialized;
	}

	private void OnPlayerCharCountChanged( int count)
	{
		playerCharCount = count;

		if(count == 0)
		{
			gameState = GAME_STATE_STOPPED;
			GameEvent.GameStateChanged(gameState, true);
		}
	}

	private void OnNoActionsNoMovesLeft()
	{
		if(playerCharCount > 0)
		{
			gameState = GAME_STATE_STOPPED;
			GameEvent.GameStateChanged(gameState, false);
		}
	}

	private void OnGameInitialized()
	{
		gameState = GAME_STATE_PLAYING;
	}

	public void Destroy()
	{
		RemoveEventListeners();
	}
}
