using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Core
{
	public static class CollectionExtentions
	{
		public static T Random<T>(this List<T> collection)
		{
			return collection[UnityEngine.Random.Range(0, collection.Count)];
		}
	}
}
