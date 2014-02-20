using System;
using strange.framework.context.api;
using strange.extensions.command.api;
using strange.extensions.command.impl;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.dispatcher.eventdispatcher.impl;
using strange.extensions.sequencer.api;
using strange.extensions.sequencer.impl;
using strange.framework.context.api;
using strange.extensions.dispatcher.api;

namespace strange.bundle
{
	public class MVCSUnityBundle : IExtension
	{
		public void Extend(IContext context)
		{
			context.injectionBinder.Bind<IContext>().ToValue(context).ToName(ContextKeys.CONTEXT);
			context.injectionBinder.Bind<ICommandBinder>().To<EventCommandBinder>().ToSingleton();
			//This binding is for local dispatchers
			context.injectionBinder.Bind<IEventDispatcher>().To<EventDispatcher>();
			//This binding is for the common system bus
			context.injectionBinder.Bind<IEventDispatcher>().To<EventDispatcher>().ToSingleton().ToName(ContextKeys.CONTEXT_DISPATCHER);
			//context.injectionBinder.Bind<IMediationBinder>().To<MediationBinder>().ToSingleton();
			context.injectionBinder.Bind<ISequencer>().To<EventSequencer>().ToSingleton();
			//context.injectionBinder.Bind<IImplicitBinder>().To<ImplicitBinder>().ToSingleton();



			//injectionBinder.Bind<GameObject>().ToValue(contextView).ToName(ContextKeys.CONTEXT_VIEW);
			ICommandBinder commandBinder = context.injectionBinder.GetInstance<ICommandBinder>() as ICommandBinder;
			
			IEventDispatcher dispatcher = context.injectionBinder.GetInstance<IEventDispatcher>(ContextKeys.CONTEXT_DISPATCHER) as IEventDispatcher;
			//mediationBinder = injectionBinder.GetInstance<IMediationBinder>() as IMediationBinder;
			ISequencer sequencer = context.injectionBinder.GetInstance<ISequencer>() as ISequencer;
			//implicitBinder = injectionBinder.GetInstance<IImplicitBinder>() as IImplicitBinder;
			
			(dispatcher as ITriggerProvider).AddTriggerable(commandBinder as ITriggerable);
			(dispatcher as ITriggerProvider).AddTriggerable(sequencer as ITriggerable);
		}
	}
}

