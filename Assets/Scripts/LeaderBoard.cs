using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Core.DI;
using TMPro;

namespace BallRun
{
	public class LeaderBoard:MonoBehaviour
	{
		public class LeaderPositionInfo
		{
			public GameObject Title;
			public Sequence Tween;
			public int position;
		}

		[SerializeField]
		private GameObject ItemsPrefab;
		[SerializeField]
		private Image Crown;
		[SerializeField]
		private Vector3 MovingDirection = new Vector3(1f, 0f, 0f);
		[SerializeField]
		private Transform Origin;
		[SerializeField]
		private Vector3 ItemOffset;
		[SerializeField]
		private float TransitionTime = 1f;

		private Dictionary<int, LeaderPositionInfo> Items = new Dictionary<int, LeaderPositionInfo>();

		private void Awake()
		{
			DependencyManager.AddDependency<LeaderBoard>(this);
		}

		public void UpdatePositions(List<BallController> balls)
		{
			var sortedBalls = new List<BallController>(balls);
			sortedBalls.Sort(CompareBalls);

			for (int i = 0; i < balls.Count; i++)
			{
				if (!Items.ContainsKey(i))
				{
					Items.Add(i, new LeaderPositionInfo());
					Items[i].Title = Instantiate(ItemsPrefab);
					Items[i].Title.transform.SetParent(transform);
					Items[i].position = i;
					var text = Items[i].Title.GetComponentInChildren<TextMeshProUGUI>();
					text.text = balls[i].Name;
				}

				var sortedIndex = sortedBalls.IndexOf(balls[i]);
				var index = i;

				if (sortedIndex != Items[i].position)
				{
					Items[i].Tween.Kill();
					Items[i].Tween = DOTween.Sequence();
					Items[i].Tween.Append(Items[index].Title.transform.DOMove(GetPosition(sortedIndex), TransitionTime));
					Items[i].position = sortedIndex;

					if (sortedIndex == 0)
					{
						Crown.gameObject.SetActive(false);
						Crown.color = Color.clear;
						Items[i].Tween.AppendCallback(() => Crown.gameObject.SetActive(true));
						Items[i].Tween.Append(Crown.DOColor(Color.white, 0.5f));
					}
				}
			}
		}

		private Vector3 GetPosition(int index)
		{
			return Origin.position + index * ItemOffset;
		}

		private int CompareBalls(BallController a, BallController b)
		{
			var aPos = a.transform.position;
			aPos.Scale(MovingDirection);
			var bPos = b.transform.position;
			bPos.Scale(MovingDirection);

			return Math.Sign(aPos.sqrMagnitude - bPos.sqrMagnitude);
		}
	}
}
