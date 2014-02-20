using System;
using strange.framework.context.api;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.framework.context.api;

namespace prankard.example
{
	public class ExampleConfig1 : IConfig
	{
		[Inject(ContextKeys.CONTEXT_DISPATCHER)]
		public IEventDispatcher dispatcher { get; set; }

		public void Configure ()
		{
			Console.WriteLine ("Configured config 1");
			//exampleService.
			dispatcher.Dispatch(new ExampleEvent(ExampleEvent.Type.EVENT_A, "Hello MVC"));
		}
	}
}

