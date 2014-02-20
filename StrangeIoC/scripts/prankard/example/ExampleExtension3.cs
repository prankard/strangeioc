using System;
using strange.framework.context.api;

namespace prankard.example
{
	public class ExampleExtension3 : IExtension
	{
		public void Extend (IContext context)
		{
			Console.WriteLine ("Example extension 3 installed");
			context.Configure<ExampleConfig3> ();
		}
	}
}

