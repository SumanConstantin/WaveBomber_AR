using UnityEngine;
using System;
using System.Collections.Generic;

namespace AssemblyCSharp
{
	// This is a FIFO (FirstIn-FirstOut) Actions Queue
	
	public class ActionQueue
	{
		private List<ActionAbstract> actions = new List<ActionAbstract>();
		public void Add(ActionAbstract action)
		{
			actions.Add(action);
		}

		public void Remove(ActionAbstract action)
		{
			actions.Remove(action);
		}

		public ActionAbstract PopFirst()
		{
			ActionAbstract result = actions[0];
			Remove(result);
			return result;
		}

		public int Count
		{
			get{return actions.Count;}
		}

		public void Destroy()
		{
			foreach (ActionAbstract action in actions)
			{
				action.Destroy();
			}

			actions = null;
		}
	}
}

