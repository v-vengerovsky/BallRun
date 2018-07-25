using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using DG.Tweening;
using Core;
using Core.DI;

namespace BallRun
{
	public class GameWorld:MonoBehaviour
	{
		[SerializeField]
		private float RunStartDelay = 3f;

		private List<BallController> Balls;

		private PlatformMoveController PlatformMoveController
		{
			get
			{
				return DependencyManager.ResolveDependency<PlatformMoveController>();
			}
		}

		private RunController RunController
		{
			get
			{
				return DependencyManager.ResolveDependency<RunController>();
			}
		}

		private void Awake()
		{
			this.DelayedCall(RunStartDelay, StartRun);
		}

		private void StartRun()
		{
			RunController.StartRun();
		}
	}
}
