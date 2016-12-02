using System;
using UnityEngine;
using System.Collections;

namespace AssemblyCSharp
{
	public class BombAbstractBehaviour : GameElementAbstractBehaviour, IExplodable, ITouchable {
		protected ExplosionZone explosionZone;

		// This is the scale of the object
		// It is set on Init()
		// It helps in calculating the distance between objects and setting the targetMove vector
		// Is is used because the marker used in Vuforia is of same scale as the projected object
		public static float unitScaleFactor = .2f;		

		override protected void Init()
		{
			base.Init();
			unitScaleFactor = gameObject.transform.localScale.x;
		}

		virtual public void CheckTouch()
		{
			foreach (Touch touch in Input.touches) {
				if (touch.phase == TouchPhase.Began) {
					var ray = Camera.main.ScreenPointToRay (touch.position);
					if (Physics.Raycast (ray)) {
						OnTouch();
					}
				}
			}
		}

		public void UpdateBomb()
		{
			base.UpdateGameElement();
		}

		override protected void OnBombExplode(GameObject bomb)
		{
			if(bomb == gameObject)
			{
				return;
			}

			base.OnBombExplode(bomb);

			if(willBeMovedByExplode)
			{
				actionQueue.Add(new ExplodeAction());
			}
		}

		virtual public void OnTouch()
		{
			Explode();
			GameEvent.PlayerMadeInput();
		}

		void OnMouseDown()
		{
			if(PlayerModel.PlayerMayMakeInput)
			{
				OnTouch();
			}
		}

		override protected void UpdateGameElement()
		{
			if(PlayerModel.PlayerMayMakeInput)
			{
				CheckTouch();
			}

			base.UpdateGameElement();
		}

		override protected void UpdateCurrentAction()
		{
			base.UpdateCurrentAction();

			if(currentAction != null)
			{
				if(currentAction is ExplodeAction)
				{
					Explode();
					ResetCurrentAction();
				}
			}
		}

		virtual public void Explode()
		{
			GameEvent.BombExploded(gameObject);
			GameEvent.BombDestroyed(gameObject);
		}

		public Vector3 GetExplosionVector(Vector3 affectedObjPosition)
		{
			Vector3 result = new Vector3(0, 0, 0);

			Vector3 pos = this.gameObject.transform.position;
			int deltaX = (int)Math.Round((affectedObjPosition.x - pos.x)/unitScaleFactor);
			int deltaZ = (int)Math.Round((affectedObjPosition.z - pos.z)/unitScaleFactor);


			int iMax = ExplosionZonePattern.I_MAX;
			int jMax = ExplosionZonePattern.J_MAX;

			for(int i=0; i<iMax; i++)
			{
				for(int j=0; j<jMax; j++)
				{
					int index = (i*jMax)+j;
					int patternCode = explosionZone.Pattern[index];

					switch (patternCode)
					{
						case ExplosionZonePattern.EXPLOSION_ORIGIN_CODE:
						// Check if target obj is affected by wave (target obj should move)
						int targJ = j + deltaX;
						int targI = i - deltaZ;
						if(targJ > -1 && targJ < iMax && targI > -1 && targI < jMax)
						{
							int targetObjIndex = targI * jMax + targJ;
							if(explosionZone.Pattern[targetObjIndex] == ExplosionZonePattern.WAVE_CODE)
							{
								float movePower = 1*unitScaleFactor;	// TODO: Refactor, put movePower somewhere else

								result.x += deltaX != 0 ? movePower * Math.Sign(deltaX) : 0;
								result.z += deltaZ != 0 ? movePower * Math.Sign(deltaZ) : 0;
							}
						}
						break;

						// TODO: Check if target obj is affected by explode (target obj should explode)

						default:
						break;
					}
				}
			}

			Debug.Log("GetExplosionVector() result:"+result);
			return result;
		}
	}
}
