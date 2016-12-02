using System;
using UnityEngine;
using System.Collections;

namespace AssemblyCSharp
{
	public class CharBehaviour : GameElementAbstractBehaviour
	{
		private bool doCheckReachTargetZone = false;
		public bool DoCheckReachTargetZone
		{
			get{return doCheckReachTargetZone;}
		}

		override protected void OnBombExplode(GameObject bomb)
		{
			base.OnBombExplode(bomb);

			if(willBeMovedByExplode)
			{
				actionQueue.Add(new CheckReachTargetZoneAction());
			}

			willBeMovedByExplode = false;
		}

		public void UpdateChar()
		{
			base.UpdateGameElement();
		}

		override protected void UpdateCurrentAction()
		{
			base.UpdateCurrentAction();
			
			if(currentAction != null)
			{
				if(currentAction is CheckReachTargetZoneAction)
				{
					doCheckReachTargetZone = true;
				}
			}
		}

		public void CheckReachTargetZone(Vector3 targetZonePosition)
		{
			float epsilon = 0.01f;	// The max error
			bool posXIsOk = Math.Abs(targetZonePosition.x - transform.position.x) < epsilon;
			bool posZIsOk = Math.Abs(targetZonePosition.z - transform.position.z) < epsilon;

			if(posXIsOk && posZIsOk)
			{
				GameEvent.PlayerCharReachedTargetZone(gameObject);
			}

			doCheckReachTargetZone = false;
			ResetCurrentAction();
		}

		override public void Destroy()
		{
			base.Destroy();
		}
	}
}

