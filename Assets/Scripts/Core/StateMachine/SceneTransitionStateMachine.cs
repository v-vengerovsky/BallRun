using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
	public class SceneTransitionStateMachine : StateMachine<SceneTransitionState>
	{
		protected override Dictionary<SceneTransitionState, List<SceneTransitionState>> Transitions
		{
			get
			{
				return SceneTransitionStates.Transitions;
			}
		}

		public SceneTransitionStateMachine(SceneTransitionState state) : base(state)
		{
		}

		public SceneTransitionStateMachine() : base(SceneTransitionState.Idle)
		{
		}
	}
}
