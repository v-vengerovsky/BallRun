using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Core;
using Core.DI;

namespace BallRun
{
	public class PlatformMoveController:MonoBehaviour
	{
		[SerializeField]
		private List<GameObject> PlatformPrefabs;
		[SerializeField]
		private int PlatformCount = 3;
		[SerializeField]
		private Vector3 MovingDirection = new Vector3(1f, 0f, 0f);
		[SerializeField]
		private Transform StartPos;
		[SerializeField]
		private float speed = 10f;

		private List<GameObject> MovingPlatforms = new List<GameObject>();

		protected ObjectPool Pool
		{
			get
			{
				return DependencyManager.ResolveDependency<ObjectPool>();
			}
		}

		public float Speed
		{
			get
			{
				return speed;
			}
			set
			{
				speed = value;
			}
		}

		public Vector3 PlatformSize
		{
			get
			{
				Vector3 result = Vector3.zero;

				foreach (var item in PlatformPrefabs)
				{
					result = Vector3.Max(result, item.transform.localScale);
				}

				return result;
			}
		}

		private void Awake()
		{
			DependencyManager.AddDependency<PlatformMoveController>(this);

			foreach (var item in PlatformPrefabs)
			{
				Pool.PrewarmPool(item, PlatformCount);
			}

			for (int i = 0; i < PlatformCount; i++)
			{
				AddPlatform();
				UpdatePlatformPosition(i);
			}
		}

		private void AddPlatform()
		{
			if (MovingPlatforms.Count > PlatformCount)
			{ 
				return;
			}

			var platform = Pool.Instantiate(PlatformPrefabs.Random());
			platform.transform.SetParent(transform);
			platform.SetActive(true);
			MovingPlatforms.Add(platform);
		}

		private void RemovePlatform()
		{
			if (MovingPlatforms.Count != PlatformCount)
			{
				return;
			}

			Pool.Destroy(MovingPlatforms[0]);
			MovingPlatforms.RemoveAt(0);
		}

		private void UpdatePlatformPosition(int platformIndex)
		{
			if (platformIndex == 0)
			{
				SetStartPositionForPlatform();
				return;
			}

			var previousPlatform = MovingPlatforms[platformIndex - 1];
			var updatingPlatform = MovingPlatforms[platformIndex];

			var offset = previousPlatform.transform.localScale / 2f;
			offset += updatingPlatform.transform.localScale / 2f;
			offset.Scale(MovingDirection);

			updatingPlatform.transform.localPosition = previousPlatform.transform.localPosition + offset;
		}

		private void SetStartPositionForPlatform()
		{
			if (MovingPlatforms.Count != 1)
			{
				return;
			}

			var hOffset = MovingPlatforms[0].transform.localScale;
			hOffset.Scale(-MovingDirection);
			MovingPlatforms[0].transform.position = StartPos.position + hOffset;
		}

		private void Update()
		{
			if (!FirstPlatformVisible())
			{
				RemovePlatform();
				AddPlatform();
				UpdatePlatformPosition(MovingPlatforms.Count - 1);
			}

			foreach (var item in MovingPlatforms)
			{
				item.transform.localPosition += Speed * Time.deltaTime * -MovingDirection;
			}
		}

		private bool FirstPlatformVisible()
		{
			return MovingPlatforms[0].GetComponent<Renderer>().isVisible;
		}
	}
}
