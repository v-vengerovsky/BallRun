using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Utils;

namespace Core
{
	public class StateMachine<E> where E:struct
	{
		protected E CurrentState;
		private Dictionary<E, List<Action>> Subscriptions;

		public void Fire(E state)
		{
			if (Subscriptions != null && Subscriptions.ContainsKey(state))
			{
				foreach (var item in Subscriptions[state])
				{
					item.Invoke();
				}
			}
		}

		public void Subscribe(E state, Action callback, bool duplicateCheck = true)
		{
			if (callback == null)
			{
				return;
			}

			if (Subscriptions == null)
			{
				Subscriptions = new Dictionary<E, List<Action>>();
			}

			if (!Subscriptions.ContainsKey(state))
			{
				Subscriptions.Add(state, new List<Action>());
			}

			if (!duplicateCheck || !Subscriptions[state].Contains(callback))
			{
				Subscriptions[state].Add(callback);
			}
		}

		public void Unsubscribe(object target)
		{
			if (Subscriptions == null)
			{
				return;
			}

			foreach (var item in Subscriptions)
			{
				Unsubscribe(item.Key, target);
			}
		}

		public void Unsubscribe(E state, object target)
		{
			if (Subscriptions == null || !Subscriptions.ContainsKey(state))
			{
				return;
			}

			Delegate[] delegates;

			for (int i = Subscriptions[state].Count - 1; i >= 0; i--)
			{
				if (Subscriptions[state][i].Target == target)
				{
					Subscriptions[state].RemoveAt(i);
				}
				else
				{
					delegates = Subscriptions[state][i].GetInvocationList();

					foreach (var item in delegates)
					{
						if (item.Target == target)
						{
							Subscriptions[state].RemoveAt(i);
							break;
						}
					}
				}
			}
		}
	}
}
