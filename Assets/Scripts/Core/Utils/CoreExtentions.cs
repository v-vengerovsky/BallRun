using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.DI;
using Core;

namespace Core
{
	public static class CoreExtentions
	{
		public static ILog GetLog(this object anyObject)
		{
			return DependencyManager.ResolveDependency<ILog>();
		}

		public static void DelayedCall(this object anyObject, float delay, Action call)
		{
			DependencyManager.ResolveDependency<DelayedCallService>().DelayedCall(delay, call);
		}
	}
}
