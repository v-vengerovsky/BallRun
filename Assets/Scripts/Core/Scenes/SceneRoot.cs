using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Core.DI;

namespace Core
{
	public class SceneRoot:MonoBehaviour
	{
		[SerializeField] protected AppState State;

		protected SceneTransitionStateMachine SceneTransitionStateMachine
		{
			get
			{
				return DependencyManager.ResolveDependency<SceneTransitionStateMachine>();
			}
		}

		protected AppStateMachine AppStateMachine
		{
			get
			{
				return DependencyManager.ResolveDependency<AppStateMachine>();
			}
		}

		private void Awake()
		{
			Init();
		}

		protected virtual void Init()
		{
			AppStateMachine.Fire(State);

			if (SceneTransitionStateMachine.CurrentState != SceneTransitionState.Idle)
			{
				SceneTransitionStateMachine.Fire(SceneTransitionState.TransitionTo);
			}
		}

		private void OnDestroy()
		{
			SceneTransitionStateMachine.Unsubscribe(this);
		}
	}
}
