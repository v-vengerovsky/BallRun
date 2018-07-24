using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Core
{
	[Serializable]
	public class SerializableDictionary<TKey, TValue, TKeyValuePair> where TKeyValuePair:SerializableKeyValuePair<TKey,TValue>
	{
		[SerializeField]
		private List<TKeyValuePair> Pairs;

		public Dictionary<TKey, TValue> GetDictionary()
		{
			var result = new Dictionary<TKey, TValue>();

			foreach (var item in Pairs)
			{
				if (!result.ContainsKey(item.Key))
				{
					result.Add(item.Key, item.Value);
				}
				else
				{
					this.GetLog().LogWarning(LogChanel.Serialization, string.Format("Duplicate key: {0}", item.Key));
				}
			}

			return result;
		}
	}
}
