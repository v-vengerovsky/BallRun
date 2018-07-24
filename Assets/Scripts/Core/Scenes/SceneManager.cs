using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Core.DI;

namespace Core
{
	[Serializable]
	public class SceneData
	{
		public string SceneName;
		public UnityEngine.Object SceneRef;
	}

	public class SceneManager: MonoBehaviour
	{
		[SerializeField]
		private AppStateListScenesDictionary SceneTransitionsInitial;

		private Dictionary<AppState, SceneData> SceneTransitions;

		protected SceneTransitionStateMachine SceneTransitionStateMachine
		{
			get
			{
				return DependencyManager.ResolveDependency<SceneTransitionStateMachine>();
			}
		}

		protected AppStateMachine AppStateMachine
		{
			get
			{
				return DependencyManager.ResolveDependency<AppStateMachine>();
			}
		}

		private void OnValidate()
		{
			var scenes = SceneTransitionsInitial.GetDictionary();

			foreach (var item in scenes)
			{
				if (item.Value.SceneRef != null)
				{
					item.Value.SceneName = item.Value.SceneRef.name;
				}
			}
		}

		private void Awake()
		{
			DontDestroyOnLoad(gameObject);
			DependencyManager.AddDependency<SceneManager>(this);
			SceneTransitions = SceneTransitionsInitial.GetDictionary();

			AppStateMachine.Subscribe(AppState.SceneLoading, AppStateChange);

			SceneTransitionStateMachine.Subscribe(SceneTransitionState.Loading, TransitScene);
		}

		private void AppStateChange()
		{
			var state = AppStateMachine.OldState;

			if (SceneTransitions.ContainsKey(state) && SceneTransitions[state] != null && !string.IsNullOrEmpty(SceneTransitions[state].SceneName))
			{
				SceneTransitionStateMachine.Fire(SceneTransitionState.TransitionFrom);
			}
		}

		private void TransitScene()
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene(SceneTransitions[AppStateMachine.OldState].SceneName,UnityEngine.SceneManagement.LoadSceneMode.Single);
		}
	}
}
