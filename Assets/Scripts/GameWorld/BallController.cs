using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Core.DI;
using TMPro;
using DG.Tweening;

namespace BallRun
{
	public class BallController:MonoBehaviour
	{
		[SerializeField]
		private float speed;
		[SerializeField]
		private TextMeshPro Text;
		[SerializeField]
		private SphereCollider Collider;
		[SerializeField]
		private Renderer Renderer;

		private Sequence tween;
		private bool correctingSpeed;

		public float Speed
		{
			get
			{
				return speed + PlatformMoveController.Speed;
			}
			set
			{
				speed = value - PlatformMoveController.Speed;
			}
		}

		public string Name
		{
			get
			{
				return Text.text;
			}
			set
			{
				Text.text = value;
			}
		}

		public float Radius
		{
			get
			{
				return Collider.radius * transform.localScale.y;
			}
		}

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

		public void StartRun()
		{
			tween = DOTween.Sequence();
			tween.Append(DOTween.To(() => Speed, (spd) => Speed = spd, UnityEngine.Random.Range(RunController.MinSpeed, RunController.MaxSpeed), RunController.MaxSpeedTweenTime));
			tween.AppendInterval(UnityEngine.Random.Range(RunController.MinSpeedTweenDelay, RunController.MaxSpeedTweenDelay));
			tween.OnComplete(ChangeSpeed);
		}

		public void ChangeSpeed()
		{
			tween = DOTween.Sequence();

			tween.Append(DOTween.To(() => Speed, (spd) => Speed = spd, UnityEngine.Random.Range(RunController.MinSpeed, RunController.MaxSpeed), UnityEngine.Random.Range(RunController.MinSpeedTweenTime, RunController.MaxSpeedTweenTime)));

			tween.AppendInterval(UnityEngine.Random.Range(RunController.MinSpeedTweenDelay, RunController.MaxSpeedTweenDelay));
			tween.OnComplete(ChangeSpeed);
		}

		public void Update()
		{
			if (!Renderer.isVisible && !correctingSpeed)
			{
				tween.Kill();
				tween = DOTween.Sequence();
				correctingSpeed = true;
				if ((transform.position - RunController.transform.position).x < 0)
				{
					tween.Append(DOTween.To(() => Speed, (spd) => Speed = spd, RunController.MaxSpeed, UnityEngine.Random.Range(RunController.MinSpeedTweenTime, RunController.MaxSpeedTweenTime)));
				}
				else
				{
					tween.Append(DOTween.To(() => Speed, (spd) => Speed = spd, RunController.MinSpeed, UnityEngine.Random.Range(RunController.MinSpeedTweenTime, RunController.MaxSpeedTweenTime)));
				}

				tween.AppendInterval(UnityEngine.Random.Range(RunController.MinSpeedTweenDelay, RunController.MaxSpeedTweenDelay));
				tween.AppendCallback(() => correctingSpeed = false);
				tween.OnComplete(ChangeSpeed);
			}

			Collider.transform.rotation = Quaternion.Euler(0, 0, (float)(Speed * Time.deltaTime * RunController.RotationSpeedMultiplier / Radius));
			transform.position += speed * Time.deltaTime * Vector3.right;
		}

		private void Reset()
		{
			ResetSerializedData();
		}

		[ContextMenu("Reset serialized data")]
		private void ResetSerializedData()
		{
			Text = GetComponentInChildren<TextMeshPro>();
			Collider = GetComponentInChildren<SphereCollider>();
			Renderer = Collider.GetComponentInChildren<Renderer>();
		}
	}
}
