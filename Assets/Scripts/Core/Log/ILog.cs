using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
	public interface ILog
	{
		void Log(LogChanel chanel, string message);
		void LogWarning(LogChanel chanel, string message);
		void LogError(LogChanel chanel, string message);
	}
}
