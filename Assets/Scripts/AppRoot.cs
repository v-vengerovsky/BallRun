using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.DI;
using Core;
using Core.Log;

public class AppRoot : MonoBehaviour
{

	private void Awake ()
	{
		DependencyManager.AddDependency<AppStateMachine>(new AppStateMachine());
		DependencyManager.AddDependency<SceneTransitionStateMachine>(new SceneTransitionStateMachine());
		DependencyManager.AddDependency<ILog>(new DebugLog());
		DependencyManager.AddDependency<ObjectPool>(new ObjectPool());
	}
}
