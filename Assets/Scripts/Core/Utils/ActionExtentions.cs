using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
	public static class ActionExtentions
	{
		public static void InvokeSafe(this Action action)
		{
			if (action != null)
			{
				var delegates = action.GetInvocationList();

				foreach (var item in delegates)
				{
					if (item.Target == null)
					{
						return;
					}
				}

				action.Invoke();
			}
		}

		public static void InvokeSafe<T>(this Action<T> action, T arg)
		{
			if (action != null)
			{
				var delegates = action.GetInvocationList();

				foreach (var item in delegates)
				{
					if (item.Target == null)
					{
						return;
					}
				}

				action.Invoke(arg);
			}
		}

		public static void InvokeSafe<T1,T2>(this Action<T1, T2> action, T1 arg1, T2 arg2)
		{
			if (action != null)
			{
				var delegates = action.GetInvocationList();

				foreach (var item in delegates)
				{
					if (item.Target == null)
					{
						return;
					}
				}

				action.Invoke(arg1, arg2);
			}
		}

		public static void InvokeSafe<T1, T2, T3>(this Action<T1, T2, T3> action, T1 arg1, T2 arg2, T3 arg3)
		{
			if (action != null)
			{
				var delegates = action.GetInvocationList();

				foreach (var item in delegates)
				{
					if (item.Target == null)
					{
						return;
					}
				}

				action.Invoke(arg1, arg2, arg3);
			}
		}

		public static void InvokeSafe<T1, T2, T3, T4>(this Action<T1, T2, T3, T4> action, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
		{
			if (action != null)
			{
				var delegates = action.GetInvocationList();

				foreach (var item in delegates)
				{
					if (item.Target == null)
					{
						return;
					}
				}

				action.Invoke(arg1, arg2, arg3, arg4);
			}
		}
	}
}
