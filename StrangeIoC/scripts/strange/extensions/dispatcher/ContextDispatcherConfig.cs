using System;
using strange.framework.context.api;
using strange.extensions.injector.api;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.dispatcher.eventdispatcher.impl;

namespace stange.extensions.dispatcher
{
	public class ContextDispatcherConfig : IConfig
	{
		[Inject]
		public IInjectionBinder injectionBinder { get; set; }

		public void Configure()
		{
			UnityEngine.Debug.Log ("Configure context dispatcher");
			injectionBinder.Bind<IEventDispatcher>().To<EventDispatcher>().ToSingleton().ToName(ContextKeys.CONTEXT_DISPATCHER);
		}
	}
}

