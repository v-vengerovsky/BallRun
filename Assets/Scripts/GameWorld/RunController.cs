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
	public class RunController:MonoBehaviour
	{
		[SerializeField]
		private List<BallController> BallPrefabs;
		[SerializeField]
		private int BallCount;
		[SerializeField]
		private Transform StartPos;
		[SerializeField]
		private float minSpeed = 10f;
		[SerializeField]
		private float maxSpeed = 15f;
		[SerializeField]
		private float minSpeedTweenTime = 0.5f;
		[SerializeField]
		private float maxSpeedTweenTime = 1f;
		[SerializeField]
		private float minSpeedTweenDelay = 2f;
		[SerializeField]
		private float maxSpeedTweenDelay = 5f;
		[SerializeField]
		private float rotationSpeedMultiplier = 5f;

		private List<BallController> Balls;

		private PlatformMoveController PlatformMoveController
		{
			get
			{
				return DependencyManager.ResolveDependency<PlatformMoveController>();
			}
		}

		public float MinSpeed
		{
			get
			{
				return minSpeed;
			}
		}

		public float MaxSpeed
		{
			get
			{
				return maxSpeed;
			}
		}

		public float MinSpeedTweenTime
		{
			get
			{
				return minSpeedTweenTime;
			}
		}

		public float MaxSpeedTweenTime
		{
			get
			{
				return maxSpeedTweenTime;
			}
		}

		public float MinSpeedTweenDelay
		{
			get
			{
				return minSpeedTweenDelay;
			}
		}

		public float MaxSpeedTweenDelay
		{
			get
			{
				return maxSpeedTweenDelay;
			}
		}

		public float RotationSpeedMultiplier
		{
			get
			{
				return rotationSpeedMultiplier;
			}
		}

		private void Awake()
		{
			DependencyManager.AddDependency<RunController>(this);
			Balls = new List<BallController>();
			var platformSizeHorizontal = PlatformMoveController.PlatformSize;
			platformSizeHorizontal.Scale(Vector3.forward);
			var verticalOffset = PlatformMoveController.PlatformSize;
			verticalOffset.Scale(Vector3.up / 2f);
			BallController ball;

			for (int i = 0; i < BallCount; i++)
			{
				ball = Instantiate(BallPrefabs.Random());
				ball.transform.SetParent(transform);
				ball.transform.position = StartPos.position + platformSizeHorizontal * ((float)(1 + i) / (BallCount + 1) - 0.5f) + verticalOffset + Vector3.up * ball.Radius;
				ball.Name = (i + 1).ToString();
				Balls.Add(ball);
			}
		}

		private void Update()
		{
			float averageSpeed = 0f;

			foreach (var item in Balls)
			{
				averageSpeed += item.Speed;
			}

			PlatformMoveController.Speed = averageSpeed/Balls.Count;
		}

		public void StartRun()
		{
			foreach (var item in Balls)
			{
				item.StartRun();
			}
		}
	}
}
