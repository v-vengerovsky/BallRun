using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.DI;
using Core;

public class AppRoot : MonoBehaviour
{

	void Start ()
	{
		DependencyManager.AddDependency<AppStateMachine>(new AppStateMachine());
		DependencyManager.ResolveDependency<AppStateMachine>().Fire(AppState.Menu);
	}
	
	void Update ()
	{
		
	}
}
