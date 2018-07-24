using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
	public class AppStateMachine:StateMachine<AppState>
	{
		protected override Dictionary<AppState, List<AppState>> Transitions
		{
			get
			{
				return AppStateTransitions.Transitions;
			}
		}

		public AppStateMachine(AppState state) : base(state)
		{
		}

		public AppStateMachine() : base(AppState.Splash)
		{
		}
	}
}
