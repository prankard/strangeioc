using System;
using strange.framework.impl;
using strange.extensions.injector.api;
using strange.extensions.injector.impl;
using strange.framework.context.api;

namespace strange.framework.context.impl
{
	public class Context : IContext // : Binder // Do we have to extend this? I think it's unnessisary
	{
		private bool _initialized = false;

		private ConfigManager _configManager;
		private ExtensionInstaller _extensionInstaller;
		public IInjectionBinder _injectionBinder;

		public Context ()
		{
			_extensionInstaller = new ExtensionInstaller (this);
			_configManager = new ConfigManager (this);
			_injectionBinder = new InjectionBinder ();
			_injectionBinder.Bind<IInjectionBinder> ().ToValue (_injectionBinder);
			Console.WriteLine ("Init Context");
		}

		public IInjectionBinder injectionBinder
		{
			get { return _injectionBinder; }
		}

		public bool initialized
		{
			get { return _initialized; }
		}

		public IContext Initialize()
		{
			_configManager.ConfigureAll ();
			_initialized = true;
			Console.WriteLine ("Initalize");
			// Dispatch
			return this;
		}

		public IContext Install<T>() where T : IExtension
		{
			_extensionInstaller.Install<T>();
			return this;
		}

		public IContext Configure<T>() where T : IConfig
		{
			_configManager.AddConfig<T>();
			return this;
		}
	}
}

