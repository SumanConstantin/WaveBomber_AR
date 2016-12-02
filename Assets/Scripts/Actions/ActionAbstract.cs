using System;

namespace AssemblyCSharp
{
	public class ActionAbstract
	{
		private ActionType type;
		public ActionType Type
		{
			get
			{
				return type;
			}
			set
			{
				type = value;
			}
		}

		private ActionType finished;
		public ActionType Finished
		{
			get
			{
				return finished;
			}
			set
			{
				finished = value;
			}
		}

		public void Destroy()
		{
			
		}
	}
}

