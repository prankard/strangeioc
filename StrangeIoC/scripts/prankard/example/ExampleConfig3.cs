using System;
using strange.framework.context.api;
using strange.extensions.dispatcher.eventdispatcher.api;

namespace prankard.example
{
	public class ExampleConfig3 : IConfig
	{
		
		[Inject(ContextKeys.CONTEXT_DISPATCHER)] 
		public IEventDispatcher eventDispatcher { get; set; }
		
		public void Configure()
		{
			Console.WriteLine ("Configured config 3");
			
			eventDispatcher.Dispatch ("Poop");
		}
	}
}

