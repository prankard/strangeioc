using System;
using strange.framework.context.api;
using strange.framework.context.impl;
using strange.bundle;
using prankard.example;

namespace prankard.example
{
	public class ContextExample
	{
		public IContext context;

		public ContextExample ()
		{
			context = new Context()
				.Install<MVCSBundle>()
				.Install<ExampleExtension1>()
				.Install<ExampleExtension2>()
				.Install<ExampleExtension3>()
				.Configure<ExampleConfig4>();
			context.Initialize();
		}
	}
}

