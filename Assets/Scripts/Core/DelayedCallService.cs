using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Core.DI;

namespace Core
{
	public class DelayedCallService:MonoBehaviour
	{
		private void Awake()
		{
			DontDestroyOnLoad(gameObject);
			DependencyManager.AddDependency<DelayedCallService>(this);
		}

		public void DelayedCall(float delay, Action call)
		{
			if (delay == 0f)
			{
				call.InvokeSafe();
			}
			else
			{
				StartCoroutine(DelayedCallCoroutine(delay, call));
			}
		}

		private IEnumerator DelayedCallCoroutine(float delay, Action call)
		{
			yield return new WaitForSeconds(delay);

			call.InvokeSafe();
		}
	}
}
