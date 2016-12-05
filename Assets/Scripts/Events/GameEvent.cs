using UnityEngine;
using System.Collections;

public class GameEvent : MonoBehaviour {
	
	// Bomb Event
	public delegate void BombDelegate (GameObject bombGO);
	public static event BombDelegate onBombExploded;

	public static void BombExploded(GameObject bomb)
	{
		if(onBombExploded != null)
		{
			onBombExploded( bomb );
		}
	}

	public static event BombDelegate onBombDestroyed;
	public static void BombDestroyed(GameObject bomb)
	{
		if(onBombDestroyed != null)
		{
			onBombDestroyed( bomb );
		}
	}

	// Char Event
	public delegate void PlayerCharReachedTargetZoneDelegate (GameObject charGO);
	public static event PlayerCharReachedTargetZoneDelegate onPlayerCharReachedTargetZone;

	public static void PlayerCharReachedTargetZone(GameObject charGO)
	{
		if(onPlayerCharReachedTargetZone != null)
		{
			onPlayerCharReachedTargetZone( charGO );
		}
	}

	// Player Char Count Event
	public delegate void PlayerCharCountDelegate ( int count );
	public static event PlayerCharCountDelegate onPlayerCharCountChanged;

	public static void PlayerCharCountChanged( int count)
	{
		if(onPlayerCharCountChanged != null)
		{
			onPlayerCharCountChanged( count );
		}
	}

	// Player Event
	public delegate void PlayerMadeInputDelegate ();
	public static event PlayerMadeInputDelegate onPlayerMadeInput;

	public static void PlayerMadeInput()
	{
		if(onPlayerMadeInput != null)
		{
			onPlayerMadeInput();
		}
	}

	// Game State Event
	public delegate void GameStateChangeDelegate ( string gameState, bool isWin = false );
	public static event GameStateChangeDelegate onGameStateChanged;

	public static void GameStateChanged( string gameState, bool isWin = false)
	{
		if(onGameStateChanged != null)
		{
			onGameStateChanged( gameState, isWin );
		}
	}

	// On no queued actions and no player moves Event
	public delegate void NoActionsNoMovesLeftDelegate ();
	public static event NoActionsNoMovesLeftDelegate onNoActionsNoMovesLeft;

	public static void NoActionsNoMovesLeft()
	{
		if(onNoActionsNoMovesLeft != null)
		{
			onNoActionsNoMovesLeft();
		}
	}

	// Game Initialized Event
	public delegate void GameInitializedDelegate ();
	public static event GameInitializedDelegate onGameInitialized;

	public static void GameInitialized()
	{
		if(onGameInitialized != null)
		{
			onGameInitialized();
		}
	}

	// Image Target Initialized Event
	public delegate void ImageTargetInitializedDelegate (GameObject imageTarget);
	public static event ImageTargetInitializedDelegate onImageTargetInitialized;

	public static void ImageTargetInitialized(GameObject imageTarget)
	{
		if(onImageTargetInitialized != null)
		{
			onImageTargetInitialized(imageTarget);
		}
	}
}
