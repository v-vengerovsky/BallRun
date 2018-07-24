using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.DI;

namespace Core
{
	public class StateMachine<TState> where TState:struct
	{
		protected TState currentState;
		protected TState oldState;
		private Dictionary<TState, List<Action>> Subscriptions;

		public TState CurrentState
		{
			get
			{
				return currentState;
			}
			protected set
			{
				oldState = currentState;
				currentState = value;
			}
		}

		public TState OldState
		{
			get
			{
				return oldState;
			}
		}

		public StateMachine(TState state)
		{
			CurrentState = state;
		}

		protected virtual Dictionary<TState, List<TState>> Transitions
		{
			get
			{
				return null;
			}
		}

		public void Fire(TState state)
		{
			if (Transitions == null)
			{
				this.GetLog().LogError(LogChanel.StateMachine, "No transitions");
			}
			else
			if (Transitions[CurrentState] == null)
			{
				this.GetLog().LogError(LogChanel.StateMachine, string.Format("No transition from {0}", CurrentState));
			}
			else
			if (!Transitions[CurrentState].Contains(state))
			{
				this.GetLog().LogError(LogChanel.StateMachine, string.Format("No transition from {0} to {1}", CurrentState, state));
			}
			else
			{
				this.GetLog().Log(LogChanel.StateMachine, string.Format("Transition from {0} to {1}", CurrentState, state));
				CurrentState = state;
				InternalFire(state);
			}
		}

		protected void InternalFire(TState state)
		{
			if (Subscriptions != null && Subscriptions.ContainsKey(state))
			{
				foreach (var item in Subscriptions[state])
				{
					item.Invoke();
				}
			}
		}

		public void Subscribe(TState state, Action callback, bool duplicateCheck = true)
		{
			if (callback == null)
			{
				return;
			}

			if (Subscriptions == null)
			{
				Subscriptions = new Dictionary<TState, List<Action>>();
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

		public void Unsubscribe(TState state, object target)
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
