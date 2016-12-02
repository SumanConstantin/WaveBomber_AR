using System;
using UnityEngine;
using System.Collections;

namespace AssemblyCSharp
{
	public class BombSpotBehaviour : BombAbstractBehaviour  {

		override protected void Init()
		{
			base.Init();
			explosionZone = new ExplosionZone(ExplosionZonePattern.PATTERN_SPOT);
		}

		override public void Destroy()
		{
			base.Destroy();
			explosionZone.Destroy();
		}
	}
}
