using System;
using strange.extensions.injector.api;
using strange.framework.context.api;

namespace prankard.example
{
	public class ExampleExtension2 : IExtension
	{
		[Inject]
		public IInjectionBinder injectionBinder { get; set; }

		public void Extend (IContext context)
		{
			Console.WriteLine (injectionBinder.GetInstance<string>("test"));
			context.Install<ExampleExtension3> ();
			Console.WriteLine ("Extension 2 Installed");
			context.Configure<ExampleConfig2> ();
		}	
	}
}

