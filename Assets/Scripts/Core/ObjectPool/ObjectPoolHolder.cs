using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Core.DI;

namespace Core
{
	public class ObjectPoolHolder:MonoBehaviour
	{
		private void Awake()
		{
			DependencyManager.ResolveDependency<ObjectPool>().SetParent(transform);
		}
	}
}
