using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Core.Log
{
	public class DebugLog : ILog
	{
		private List<LogChanel> Chanels = new List<LogChanel>() { LogChanel.StateMachine};

		public void Log(LogChanel chanel, string message)
		{
			if (Chanels.Contains(chanel))
			{
				Debug.LogFormat("[{0}] {1}", chanel, message);
			}
		}

		public void LogError(LogChanel chanel, string message)
		{
			if (Chanels.Contains(chanel))
			{
				Debug.LogWarningFormat("[{0}] {1}", chanel, message);
			}
		}

		public void LogWarning(LogChanel chanel, string message)
		{
			if (Chanels.Contains(chanel))
			{
				Debug.LogErrorFormat("[{0}] {1}", chanel, message);
			}
		}
	}
}
