using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Core;
using Core.DI;

namespace BallRun
{
	public class MenuController: MonoBehaviour
	{
		[SerializeField]
		private Button StartButton;

		private void Awake()
		{
			StartButton.onClick.AddListener(StartGame);
		}

		private void StartGame()
		{
			DependencyManager.ResolveDependency<AppStateMachine>().Fire(AppState.SceneLoading);
		}

		private void Reset()
		{
			ResetSerializedData();
		}

		[ContextMenu("Reset serialized data")]
		private void ResetSerializedData()
		{
			StartButton = GetComponentInChildren<Button>();
		} 
	}
}
