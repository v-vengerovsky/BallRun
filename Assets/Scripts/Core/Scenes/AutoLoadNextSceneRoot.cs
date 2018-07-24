using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Core
{
	public class AutoLoadNextSceneRoot:SceneRoot
	{
		[SerializeField]
		private float Delay;
		[SerializeField]
		private AppState NextState;

		protected override void Init()
		{
			base.Init();

			this.DelayedCall(Delay, () => AppStateMachine.Fire(AppState.SceneLoading));
		}
	}
}
