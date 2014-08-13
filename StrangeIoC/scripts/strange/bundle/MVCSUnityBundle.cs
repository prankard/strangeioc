using System;
using strange.framework.context.api;
using strange.extensions.command.api;
using strange.extensions.command.impl;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.dispatcher.eventdispatcher.impl;
using strange.extensions.sequencer.api;
using strange.extensions.sequencer.impl;
using strange.extensions.dispatcher.api;
using stange.extensions.dispatcher;
using strange.extensions.dispatcher;
using strange.extensions.sequencer;
using strange.extensions.command;
using stange.extensions.contextview;
using strange.extensions.contextview;
using strange.extensions.viewManager;
using strange.extensions.mediatorMap;

namespace strange.bundle
{
	public class MVCSUnityBundle : IExtension
	{
		public void Extend(IContext context)
		{
			context.injectionBinder.Bind<IContext>().ToValue(context).ToName(ContextKeys.CONTEXT);
			context.Install<EventCommandBinderExtension>();

			context.Install<DispatcherExtension> ();
			context.Configure<ContextDispatcherConfig> ();

			//context.injectionBinder.Bind<IMediationBinder>().To<MediationBinder>().ToSingleton();
			context.Install<SequencerExtension>();
			//context.injectionBinder.Bind<IImplicitBinder>().To<ImplicitBinder>().ToSingleton();

			context.Install<ContextViewExtension> ();
			//injectionBinder.Bind<GameObject>().ToValue(contextView).ToName(ContextKeys.CONTEXT_VIEW);

			context.Install<ViewManagerExtension>();
			context.Install<MediatorMapExtension>();

			context.Configure<ContextViewListenerConfig>();
			context.Configure<CommandBinderDispatchConfig>();
			context.Configure<SequencerDispatchConfig>();
		}
	}
}

