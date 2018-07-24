using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Core
{
	[Serializable]
	public class SerializableKeyValuePair<TKey, TValue>
	{
		[SerializeField]
		private TKey key;
		[SerializeField]
		private TValue value;

		public TKey Key
		{
			get
			{
				return key;
			}
		}

		public TValue Value
		{
			get
			{
				return value;
			}
		}

		public SerializableKeyValuePair(TKey key, TValue value)
		{
			this.key = key;
			this.value = value;
		}
	}
}
