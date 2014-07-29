using System;
using strange.framework.context.api;
using strange.extensions.command.impl;
using strange.extensions.command.api;

namespace strange.extensions.command
{
	public class EventCommandBinderExtension : IExtension
	{
		public void Extend(IContext context)
		{
			UnityEngine.Debug.Log("EventCommandBinderExtension");
			if (context.injectionBinder.GetBinding<ICommandBinder>() == null)
			{
				UnityEngine.Debug.Log("Not null");
				context.injectionBinder.Bind<ICommandBinder>().To<EventCommandBinder>().ToSingleton();
			}
		}
	}
}

