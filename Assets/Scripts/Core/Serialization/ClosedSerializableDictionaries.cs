using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
	[Serializable]
	public class AppStateListScenesPair : SerializableKeyValuePair<AppState, SceneData>
	{
		public AppStateListScenesPair(AppState key, SceneData value) : base(key, value)
		{
		}
	}

	[Serializable]
	public class AppStateListScenesDictionary:SerializableDictionary<AppState, SceneData, AppStateListScenesPair>
	{
	}
}
