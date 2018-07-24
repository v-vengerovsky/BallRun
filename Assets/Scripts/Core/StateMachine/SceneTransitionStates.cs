using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
	public class SceneTransitionStates
	{
		public static Dictionary<SceneTransitionState, List<SceneTransitionState>> Transitions = new Dictionary<SceneTransitionState, List<SceneTransitionState>>()
		{
			{ SceneTransitionState.Idle ,new List<SceneTransitionState> { SceneTransitionState.TransitionFrom} },
			{ SceneTransitionState.TransitionFrom ,new List<SceneTransitionState> { SceneTransitionState.Loading} },
			{ SceneTransitionState.Loading ,new List<SceneTransitionState> { SceneTransitionState.TransitionTo} },
			{ SceneTransitionState.TransitionTo ,new List<SceneTransitionState> { SceneTransitionState.Idle} },
		};
	}
}
