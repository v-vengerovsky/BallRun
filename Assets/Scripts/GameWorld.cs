using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace BallRun
{
	public class GameWorld:MonoBehaviour
	{
		[SerializeField]
		private List<BallController> BallPrefabs;
		[SerializeField]
		private int BallCount;
		[SerializeField]
		private List<GameObject> PlatformPrefabs;
		[SerializeField]
		private Transform StartPos;

		private List<GameObject> Platforms;
		private List<BallController> Balls;

		private void Awake()
		{
			Platforms = new List<GameObject>();
			Balls = new List<BallController>();
		}
	}
}
