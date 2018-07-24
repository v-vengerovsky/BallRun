using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
	public class AppStateTransitions
	{
		public static Dictionary<AppState, List<AppState>> Transitions = new Dictionary<AppState, List<AppState>>()
		{
			{ AppState.Splash ,new List<AppState> { AppState.SceneLoading } },
			{ AppState.Preloader ,new List<AppState> { AppState.SceneLoading } },
			{ AppState.Menu ,new List<AppState> { AppState.SceneLoading, AppState.Exit} },
			{ AppState.Game ,new List<AppState> { AppState.Pause, AppState.GameOver} },
			{ AppState.GameOver ,new List<AppState> { AppState.SceneLoading } },
			{ AppState.Pause ,new List<AppState> { AppState.SceneLoading, AppState.Exit} },
			{ AppState.SceneLoading ,new List<AppState> { AppState.Menu, AppState.Game, AppState.Preloader} },
		};
	}
}
