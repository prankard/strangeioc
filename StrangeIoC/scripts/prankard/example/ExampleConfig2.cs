using System;
using strange.framework.context.api;

namespace prankard.example
{
	public class ExampleConfig2 : IConfig
	{
		public void Configure()
		{
			Console.WriteLine ("Configured config 2");
		}
	}
}

