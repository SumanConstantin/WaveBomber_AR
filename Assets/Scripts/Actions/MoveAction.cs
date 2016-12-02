using System;
using UnityEngine;

namespace AssemblyCSharp
{
	public class MoveAction : ActionAbstract
	{
		private Vector3 targetPosition;
		public Vector3 TargetPosition
		{
			get{return targetPosition;}
			set{targetPosition = value;}
		}
		
		public MoveAction (Vector3 targetPos)
		{
			targetPosition = targetPos;
		}
	}
}

