using System;
using strange.extensions.command.impl;
using strange.extensions.dispatcher.eventdispatcher.api;

namespace prankard.example
{
	public class ExampleCommand : Command
	{
		[Inject]
		public IEvent e { get; set; }

		public override void Execute ()
		{
			//Console.WriteLine ("Example Command");
			ExampleEvent evt = e as ExampleEvent;
			Console.WriteLine ("Command executed with message " + evt.message);
		}
	}
}

