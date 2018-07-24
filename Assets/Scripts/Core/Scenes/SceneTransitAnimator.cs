using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Core.DI;

namespace Core
{
	public class SceneTransitAnimator:MonoBehaviour
	{
		protected SceneTransitionStateMachine SceneTransitionStateMachine
		{
			get
			{
				return DependencyManager.ResolveDependency<SceneTransitionStateMachine>();
			}
		}

		private void Awake()
		{
			SceneTransitionStateMachine.Subscribe(SceneTransitionState.TransitionFrom, TransitFrom);
			SceneTransitionStateMachine.Subscribe(SceneTransitionState.TransitionTo, TransitTo);
		}

		private void TransitFrom()
		{
			this.DelayedCall(0.1f, () => SceneTransitionStateMachine.Fire(SceneTransitionState.Loading));
			//SceneTransitionStateMachine.Fire(SceneTransitionState.Loading);
		}

		private void TransitTo()
		{
			this.DelayedCall(0.1f, () => SceneTransitionStateMachine.Fire(SceneTransitionState.Idle));
			//SceneTransitionStateMachine.Fire(SceneTransitionState.Idle);
		}
	}
}
