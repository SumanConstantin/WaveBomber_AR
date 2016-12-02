using System;
using System.Collections.Generic;
using UnityEngine;

namespace AssemblyCSharp
{
	public class ExplosionZone
	{
		private int[] pattern;
		public int[] Pattern
		{
			get{return pattern;}
		}

		public ExplosionZone(int[] pattern)
		{
			this.pattern = pattern;
		}

		public void Destroy()
		{
			pattern = null;
		}
	}
}

