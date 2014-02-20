using System;
using strange.extensions.dispatcher.eventdispatcher.impl;

namespace prankard.example
{
	public class ExampleEvent : TmEvent
	{
		public string message;
		public enum Type
		{
			EVENT_A,
			EVENT_B,
			EVENT_C
		}

		public ExampleEvent (object type, string message) : base (type, null, null)
		{
			this.message = message;
		}
	}
}

