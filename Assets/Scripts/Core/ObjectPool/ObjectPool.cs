using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Core
{
	public class ObjectPool
	{
		private Dictionary<GameObject, List<GameObject>> Objects = new Dictionary<GameObject, List<GameObject>>();
		private Dictionary<GameObject, GameObject> TakenObjects = new Dictionary<GameObject, GameObject>();
		private Transform Parent;

		public void SetParent(Transform parent)
		{
			Parent = parent;
		}

		public void PrewarmPool(GameObject prefab, int count)
		{
			if (Objects.ContainsKey(prefab) || Parent == null || count == 0)
			{
				return;
			}

			Objects.Add(prefab, new List<GameObject>());
			GameObject tempGo;

			for(int i = 0; i < count; i++)
			{
				tempGo = GameObject.Instantiate(prefab);
				tempGo.SetActive(false);
				tempGo.transform.SetParent(Parent);
				tempGo.name = string.Format("{0}_{1}", prefab.name, i);

				Objects[prefab].Add(tempGo);
			}
		}

		public GameObject Instantiate(GameObject prefab)
		{
			if (!Objects.ContainsKey(prefab) || Objects[prefab].Count == 0)
			{
				return null;
			}

			var result = Objects[prefab][0];
			Objects[prefab].Remove(result);
			TakenObjects.Add(result, prefab);
			return result;
		}

		public void Destroy(GameObject obj)
		{
			if (TakenObjects.ContainsKey(obj))
			{
				obj.SetActive(false);
				obj.transform.SetParent(Parent);
				Objects[TakenObjects[obj]].Add(obj);
				TakenObjects.Remove(obj);
			}
			else
			{
				this.GetLog().LogWarning(LogChanel.ObjectPool,string.Format("Destroyed GO:{0} not from pool",obj.name));
				GameObject.Destroy(obj);
			}
		}

		public void UnloadObject(GameObject prefab)
		{
			if (!Objects.ContainsKey(prefab))
			{
				return;
			}

			var toDestroy = new List<GameObject>(Objects[prefab]);

			foreach (var item in TakenObjects)
			{
				if (item.Value == prefab)
				{
					toDestroy.Add(item.Key);
				}
			}

			Objects.Remove(prefab);

			foreach (var item in toDestroy)
			{
				if (TakenObjects.ContainsKey(item))
				{
					TakenObjects.Remove(item);
				}

				GameObject.Destroy(item);
			}
		}
	}
}
