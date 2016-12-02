using System;
using UnityEngine;
using System.Collections.Generic;

namespace AssemblyCSharp
{
	// The pattern defines the way the wave from explosion is propagated within the 2D space
	// This pattern is a 2D 5*5 array, aplicable to the explodable objects
	// Each cell respresents one UnityUnit
	// Value of 2 - explosion epicenter (objects get damaged)
	// Value of 1 - explosion wave, no explosion (objects get pushed/moved)
	// Value of 0 - no explosion, no wave (objects are not affected)
	// Downward of the array X increases;
	// Leftward of the array Z increases;
	public class ExplosionZonePattern
	{
		// Limists of the two-dimentional array used to describe the explosion wave
		public const int I_MAX = 5;
		public const int J_MAX = 5;
		public const int EXPLOSION_ORIGIN_CODE = 3;
		public const int EXPLOSION_CODE = 2;
		public const int WAVE_CODE = 1;
		public const int WAVE_MOVE_POWER = 1;

		public static readonly int[] PATTERN_DEFAULT = new int[] {
			0, 0, 0, 0, 0,
			0, 0, 0, 0, 0,
			0, 0, 0, 0, 0,
			0, 0, 0, 0, 0,
			0, 0, 0, 0, 0
		};

		public static readonly int[] PATTERN_SPOT = new int[]{
			0, 0, 0, 0, 0,
			0, 1, 1, 1, 0,
			0, 1, 3, 1, 0,
			0, 1, 1, 1, 0,
			0, 0, 0, 0, 0
		};
	}
}

