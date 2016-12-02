using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AssemblyCSharp
{
	public class GameElementsModel {

		private List<GameObject> bombGOs;
		private List<BombAbstractBehaviour> bombMBs;	// Used for caching

		private List<GameObject> playerCharGOs;
		private List<CharBehaviour> playerCharMBs;	// Used for caching

		private List<GameObject> targetZoneGOs;
		private List<TargetZoneBehaviour> targetZoneMBs;	// Used for caching

		private int playerCharCount = 0;
		public int PlayerCharCount
		{
			get{return playerCharCount;}
			set{
				playerCharCount = value;
				GameEvent.PlayerCharCountChanged(playerCharCount);
			}
		}

		public GameElementsModel()
		{
			Init();
		}

		private void Init()
		{
			// Bombs
			bombGOs = new List<GameObject>();
			bombMBs = new List<BombAbstractBehaviour>();
			GameObject[] bombGOsArr = GameObject.FindGameObjectsWithTag("Bomb");

			foreach (GameObject bombObj in bombGOsArr)
			{
				bombGOs.Add(bombObj);
				bombMBs.Add(bombObj.GetComponent<BombAbstractBehaviour>());
			}

			// Chars
			playerCharGOs = new List<GameObject>(); 
			playerCharMBs = new List<CharBehaviour>();
			GameObject[] playerCharGOsArr = GameObject.FindGameObjectsWithTag("PlayerChar");

			foreach (GameObject charObj in playerCharGOsArr)
			{
				playerCharGOs.Add(charObj);
				playerCharMBs.Add(charObj.GetComponent<CharBehaviour>());

				playerCharCount++;
			}

			// TargetZones
			targetZoneGOs = new List<GameObject>();
			targetZoneMBs = new List<TargetZoneBehaviour>();
			GameObject[] targetZoneGOsArr = GameObject.FindGameObjectsWithTag("TargetZone");

			foreach (GameObject targetZoneObj in targetZoneGOsArr)
			{
				targetZoneGOs.Add(targetZoneObj);
				targetZoneMBs.Add(targetZoneObj.GetComponent<TargetZoneBehaviour>());
			}

			InitEventListeners();
		}

		private void InitEventListeners()
		{
			GameEvent.onBombDestroyed += OnBombDestroy;
			GameEvent.onPlayerCharReachedTargetZone += OnPlayerCharReachTargetZone;
		}

		private void RemoveEventListeners()
		{
			GameEvent.onBombDestroyed -= OnBombDestroy;
			GameEvent.onPlayerCharReachedTargetZone -= OnPlayerCharReachTargetZone;
		}

		private void OnBombDestroy(GameObject bomb)
		{
			RemoveBomb(bomb);
		}

		private void OnPlayerCharReachTargetZone(GameObject charGO)
		{
			RemovePlayerChar(charGO);
		}

		private void RemoveBomb(GameObject bombGO)
		{
			bombMBs.RemoveAt(bombMBs.IndexOf(bombGO.GetComponent<BombAbstractBehaviour>()));
			bombGOs.RemoveAt(bombGOs.IndexOf(bombGO));
			MonoBehaviour.Destroy(bombGO);
		}

		private void RemovePlayerChar(GameObject charGO)
		{
			playerCharMBs.RemoveAt(playerCharMBs.IndexOf(charGO.GetComponent<CharBehaviour>()));
			playerCharGOs.RemoveAt(playerCharGOs.IndexOf(charGO));
			MonoBehaviour.Destroy(charGO);

			PlayerCharCount--;
		}

		public void Update()
		{
			int queuedActionsCount = 0;

			// Bombs
			for (int i = bombMBs.Count-1; i>-1; i--)
			{
				BombAbstractBehaviour bombMB = bombMBs[i];
				bombMB.UpdateBomb();
				queuedActionsCount += bombMB.ActionQueueCount;
			}

			// Chars
			for (int i = playerCharMBs.Count-1; i>-1; i--)
			{
				CharBehaviour charMB = playerCharMBs[i];
				charMB.UpdateChar();
				queuedActionsCount += charMB.ActionQueueCount;

				// Check reach target zone
				if(charMB.DoCheckReachTargetZone)
				{
					foreach (GameObject zoneGO in targetZoneGOs)
					{
						charMB.CheckReachTargetZone(zoneGO.transform.position);
					}
				}
			}

			if(queuedActionsCount == 0 && !PlayerModel.PlayerMayMakeInput && doubleCheckedQueuedActionsCount == 0)
			{
				// Game Lost
				GameEvent.NoActionsNoMovesLeft();
			}


		}

		private int doubleCheckedQueuedActionsCount
		{
			get{int result = 0;

			// Bombs
			for (int i = bombMBs.Count-1; i>-1; i--)
			{
				BombAbstractBehaviour bombMB = bombMBs[i];
				result += bombMB.ActionQueueCount;
			}

			// Chars
			for (int i = playerCharMBs.Count-1; i>-1; i--)
			{
				CharBehaviour charMB = playerCharMBs[i];
				result += charMB.ActionQueueCount;
			}

				return result;}
		}

		public void Destroy()
		{
			RemoveEventListeners();
		}
	}
}
