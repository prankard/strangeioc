using System;
using strange.extensions.injector.api;
using strange.framework.context.api;
using strange.extensions.command.api;

namespace prankard.example
{
	public class ExampleExtension1 : IExtension
	{
		[Inject]
		public IInjectionBinder injectionBinder { get; set; }

		[Inject]
		public ICommandBinder commandBinder {get;set;}

		public void Extend (IContext context)
		{
			context.Install<ExampleExtension3> ();

			injectionBinder.Bind<string>().To ("string").ToName("test");
			
			
			commandBinder.Bind (ExampleEvent.Type.EVENT_A).To<ExampleCommand>();
			Console.WriteLine ("Extension 1 Installed");
			context.Configure<ExampleConfig1> ();
		}
	}
}

