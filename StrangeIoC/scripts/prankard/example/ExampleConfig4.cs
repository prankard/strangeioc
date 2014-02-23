using System;
using strange.framework.context.api;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.contextview.api;

namespace prankard.example
{
	public class ExampleConfig4 : IConfig
	{
		[Inject(ContextKeys.CONTEXT_DISPATCHER)] 
		public IEventDispatcher eventDispatcher { get; set; }

		[Inject(ContextKeys.CONTEXT_VIEW)]
		public IContextView contextView { get; set; }

		public void Configure()
		{
			Console.WriteLine ("Configured config 4");

			Console.WriteLine ("Found context view");
			Console.WriteLine (contextView);

			//eventDispatcher.Dispatch ("Poop");
		}
	}
}

