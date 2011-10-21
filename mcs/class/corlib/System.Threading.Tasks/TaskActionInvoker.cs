//
// TaskActionInvoker.cs
//
// Authors:
//    Marek Safar  <marek.safar@gmail.com>
//
// Copyright 2011 Xamarin Inc.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//
//

#if NET_4_0 || MOBILE

namespace System.Threading.Tasks
{
	abstract class TaskActionInvoker
	{
		sealed class ActionInvoke : TaskActionInvoker
		{
			readonly Action action;

			public ActionInvoke (Action action)
			{
				this.action = action;
			}

			public override Delegate Action {
				get {
					return action;
				}
			}

			public override void Invoke (Task owner, object state, Task context)
			{
				action ();
			}
		}

		sealed class ActionObjectInvoke : TaskActionInvoker
		{
			readonly Action<object> action;

			public ActionObjectInvoke (Action<object> action)
			{
				this.action = action;
			}

			public override Delegate Action {
				get {
					return action;
				}
			}

			public override void Invoke (Task owner, object state, Task context)
			{
				action (state);
			}
		}

		sealed class ActionTaskInvoke : TaskActionInvoker
		{
			readonly Action<Task> action;

			public ActionTaskInvoke (Action<Task> action)
			{
				this.action = action;
			}

			public override Delegate Action {
				get {
					return action;
				}
			}

			public override void Invoke (Task owner, object state, Task context)
			{
				action (owner);
			}
		}

		sealed class ActionTaskObjectInvoke : TaskActionInvoker
		{
			readonly Action<Task, object> action;

			public ActionTaskObjectInvoke (Action<Task, object> action)
			{
				this.action = action;
			}

			public override Delegate Action {
				get {
					return action;
				}
			}

			public override void Invoke (Task owner, object state, Task context)
			{
				action (owner, state);
			}
		}

		sealed class ActionTaskObjectInvoke<TResult> : TaskActionInvoker
		{
			readonly Action<Task<TResult>, object> action;

			public ActionTaskObjectInvoke (Action<Task<TResult>, object> action)
			{
				this.action = action;
			}

			public override Delegate Action {
				get {
					return action;
				}
			}

			public override void Invoke (Task owner, object state, Task context)
			{
				action ((Task<TResult>) owner, state);
			}
		}

		sealed class ActionTaskInvoke<TResult> : TaskActionInvoker
		{
			readonly Action<Task<TResult>> action;

			public ActionTaskInvoke (Action<Task<TResult>> action)
			{
				this.action = action;
			}

			public override Delegate Action {
				get {
					return action;
				}
			}

			public override void Invoke (Task owner, object state, Task context)
			{
				action ((Task<TResult>) owner);
			}
		}

		sealed class FuncInvoke<TResult> : TaskActionInvoker
		{
			readonly Func<TResult> action;

			public FuncInvoke (Func<TResult> action)
			{
				this.action = action;
			}

			public override Delegate Action {
				get {
					return action;
				}
			}

			public override void Invoke (Task owner, object state, Task context)
			{
				((Task<TResult>) context).Result = action ();
			}
		}

		sealed class FuncTaskInvoke<TResult> : TaskActionInvoker
		{
			readonly Func<Task, TResult> action;

			public FuncTaskInvoke (Func<Task, TResult> action)
			{
				this.action = action;
			}

			public override Delegate Action {
				get {
					return action;
				}
			}

			public override void Invoke (Task owner, object state, Task context)
			{
				((Task<TResult>) context).Result = action (owner);
			}
		}

		sealed class FuncTaskInvoke<TResult, TNewResult> : TaskActionInvoker
		{
			readonly Func<Task<TResult>, TNewResult> action;

			public FuncTaskInvoke (Func<Task<TResult>, TNewResult> action)
			{
				this.action = action;
			}

			public override Delegate Action {
				get {
					return action;
				}
			}

			public override void Invoke (Task owner, object state, Task context)
			{
				((Task<TNewResult>) context).Result = action ((Task<TResult>) owner);
			}
		}

		sealed class FuncObjectInvoke<TResult> : TaskActionInvoker
		{
			readonly Func<object, TResult> action;

			public FuncObjectInvoke (Func<object, TResult> action)
			{
				this.action = action;
			}

			public override Delegate Action {
				get {
					return action;
				}
			}

			public override void Invoke (Task owner, object state, Task context)
			{
				((Task<TResult>) context).Result = action (state);
			}
		}

		sealed class FuncTaskObjectInvoke<TResult> : TaskActionInvoker
		{
			readonly Func<Task, object, TResult> action;

			public FuncTaskObjectInvoke (Func<Task, object, TResult> action)
			{
				this.action = action;
			}

			public override Delegate Action {
				get {
					return action;
				}
			}

			public override void Invoke (Task owner, object state, Task context)
			{
				((Task<TResult>) context).Result = action (owner, state);
			}
		}

		sealed class FuncTaskObjectInvoke<TResult, TNewResult> : TaskActionInvoker
		{
			readonly Func<Task<TResult>, object, TNewResult> action;

			public FuncTaskObjectInvoke (Func<Task<TResult>, object, TNewResult> action)
			{
				this.action = action;
			}

			public override Delegate Action {
				get {
					return action;
				}
			}

			public override void Invoke (Task owner, object state, Task context)
			{
				((Task<TNewResult>) context).Result = action ((Task<TResult>) owner, state);
			}
		}

		public static TaskActionInvoker Create (Action action)
		{
			return new ActionInvoke (action);
		}

		public static TaskActionInvoker Create (Action<object> action)
		{
			return new ActionObjectInvoke (action);
		}

		public static TaskActionInvoker Create (Action<Task> action)
		{
			return new ActionTaskInvoke (action);
		}

		public static TaskActionInvoker Create (Action<Task, object> action)
		{
			return new ActionTaskObjectInvoke (action);
		}

		public static TaskActionInvoker Create<TResult> (Action<Task<TResult>> action)
		{
			return new ActionTaskInvoke<TResult> (action);
		}

		public static TaskActionInvoker Create<TResult> (Action<Task<TResult>, object> action)
		{
			return new ActionTaskObjectInvoke<TResult> (action);
		}

		public static TaskActionInvoker Create<TResult> (Func<TResult> action)
		{
			return new FuncInvoke<TResult> (action);
		}

		public static TaskActionInvoker Create<TResult> (Func<object, TResult> action)
		{
			return new FuncObjectInvoke<TResult> (action);
		}

		public static TaskActionInvoker Create<TResult> (Func<Task, TResult> action)
		{
			return new FuncTaskInvoke<TResult> (action);
		}

		public static TaskActionInvoker Create<TResult> (Func<Task, object, TResult> action)
		{
			return new FuncTaskObjectInvoke<TResult> (action);
		}

		public static TaskActionInvoker Create<TResult, TNewResult> (Func<Task<TResult>, TNewResult> action)
		{
			return new FuncTaskInvoke<TResult, TNewResult> (action);
		}

		public static TaskActionInvoker Create<TResult, TNewResult> (Func<Task<TResult>, object, TNewResult> action)
		{
			return new FuncTaskObjectInvoke<TResult, TNewResult> (action);
		}

		public abstract Delegate Action { get; }
		public abstract void Invoke (Task owner, object state, Task context);
	}
}
#endif
