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
			if (context.injectionBinder.GetBinding<ICommandBinder>() == null)
			{
				context.injectionBinder.Bind<ICommandBinder>().To<EventCommandBinder>().ToSingleton();
			}
		}
	}
}

