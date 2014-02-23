using System;
using strange.framework.context.api;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.dispatcher.api;
using strange.extensions.sequencer.api;

namespace strange.extensions.sequencer
{
	public class SequencerDispatchConfig : IConfig
	{
		[Inject(ContextKeys.CONTEXT_DISPATCHER)]
		public IEventDispatcher dispatcher { get;set; }
		
		[Inject]
		public ISequencer sequencer { get;set; }
		
		public void Configure()
		{
			if (sequencer is ITriggerable && dispatcher is ITriggerProvider) 
			{
				(dispatcher as ITriggerProvider).AddTriggerable(sequencer as ITriggerable);
			}
		}
	}
}

