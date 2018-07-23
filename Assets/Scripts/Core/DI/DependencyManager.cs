using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.DI
{
	public class DependencyManager
	{
		private static Dictionary<Type, object> Dependencies;

		public static void AddDependency<T>(object dependency)
		{
			AddDependency(typeof(T), dependency);
		}

		public static void AddDependency(Type type, object dependency)
		{
			if (Dependencies == null)
			{
				Dependencies = new Dictionary<Type, object>();
			}

			if (Dependencies.ContainsKey(type))
			{
				return;
			}

			Dependencies.Add(type, dependency);
		}

		public static T ResolveDependency<T>()
		{
			if (Dependencies != null && Dependencies.ContainsKey(typeof(T)))
			{
				return (T)Dependencies[typeof(T)];
			}

			return default(T);
		}
	}
}
