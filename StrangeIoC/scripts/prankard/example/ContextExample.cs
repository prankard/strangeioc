using System;
using strange.framework.context.api;
using strange.framework.context.impl;
using strange.bundle;
using prankard.example;
using strange.extensions.contextview.impl;
using Gtk;

namespace prankard.example
{
	public class ContextExample
	{
		public IContext context;

		public ContextExample ()
		{
			context = new Context ()
				.Install<MVCSBundle> ()
				.Install<ExampleExtension1>()
				.Install<ExampleExtension2>()
				.Install<ExampleExtension3>()
				.Configure(new ExampleConfig4())
				.Configure(new ContextView(this));
			context.Initialize();
		}
	}
}

