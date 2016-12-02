using UnityEngine;
using System.Collections;
using System;

namespace AssemblyCSharp
{
	public class GameElementAbstractBehaviour : MonoBehaviour, IMovable {

		[SerializeField]
		protected bool movable = true;
		[SerializeField]
		protected bool explodable = true;

		protected ActionQueue actionQueue;
		protected Vector3 targetPosition = new Vector3();
		protected ActionAbstract currentAction;
		protected bool willBeMovedByExplode = false;

		protected float moveTime = 7f;
		protected float currentMoveTime;

		public int ActionQueueCount
		{
			get{return actionQueue.Count;}
		}

		void Awake()
		{
			Init();
		}

		virtual protected void Init()
		{
			actionQueue = new ActionQueue();
			InitEventListeners();
			ResetTargetPosition();
		}

		virtual protected void UpdateGameElement()
		{
			CheckAndSetCurrentAction();
			UpdateCurrentAction();
		}

		#region EVENTS

		protected void InitEventListeners()
		{
			GameEvent.onBombExploded += OnBombExplode;
		}

		protected void RemoveEventListeners()
		{
			GameEvent.onBombExploded -= OnBombExplode;
		}

		virtual protected void OnBombExplode(GameObject bomb)
		{
			BombAbstractBehaviour bombMB = bomb.GetComponent<BombAbstractBehaviour>();

			if(bombMB != null)
			{
				Vector3 explosionVector = bombMB.GetExplosionVector(gameObject.transform.position);
				willBeMovedByExplode = explosionVector.x != 0 || explosionVector.z != 0;
				if(willBeMovedByExplode)
				{
					targetPosition.x += explosionVector.x;
					targetPosition.z += explosionVector.z;

					if(movable)
					{
						actionQueue.Add(new MoveAction(targetPosition));
					}
				}
			}
		}

		#endregion // EVENTS

		#region ACTIONS

		public void Move()
		{
			currentMoveTime += Time.deltaTime;
			float t = currentMoveTime / moveTime;
			t = Mathf.Sin(t * Mathf.PI * .6f);
			transform.position = Vector3.Lerp(transform.position, targetPosition, t);

			// Interupt movement cycle
			if(t > 0.25f)
			{
				transform.position = targetPosition;
				currentMoveTime = 0f;
				ResetTargetPosition();
				ResetCurrentAction();
			}
		}

		protected void ResetTargetPosition()
		{
			targetPosition.x = transform.position.x;
			targetPosition.y = transform.position.y;
			targetPosition.z = transform.position.z;
		}

		protected void ResetCurrentAction()
		{
			currentAction = null;
		}

		protected void CheckAndSetCurrentAction()
		{
			if(currentAction == null)
			{
				// Check actionQueue
				if(actionQueue.Count > 0)
				{
					currentAction = actionQueue.PopFirst();
				}
			}
		}

		virtual protected void UpdateCurrentAction()
		{
			if(currentAction != null)
			{
				if(currentAction is MoveAction)
				{
					targetPosition = (currentAction as MoveAction).TargetPosition;
					Move();
				}
			}
		}

		#endregion // ACTIONS

		virtual public void Destroy()
		{
			gameObject.SetActive(false);

			RemoveEventListeners();

			actionQueue.Destroy();

			if(currentAction != null)
			{
				currentAction.Destroy();
			}
		}

		void OnDestroy()
		{
			Destroy();
		}
	}
}