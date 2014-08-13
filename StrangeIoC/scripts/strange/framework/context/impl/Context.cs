using System;
using strange.framework.impl;
using strange.extensions.injector.api;
using strange.extensions.injector.impl;
using strange.framework.context.api;
using strange.context.api;

namespace strange.framework.context.impl
{
	public class Context : IContext // : Binder // Do we have to extend this? I think it's unnessisary
	{
		private bool _initialized = false;

		private ConfigManager _configManager;
		private ExtensionInstaller _extensionInstaller;
		public IInjectionBinder _injectionBinder;
		
		private ContextStateCallback _preInitilizeCallback;
		private ContextStateCallback _postInitializeCallback;
		private ContextStateCallback _preDestroyCallback;
		private ContextStateCallback _postDestroyCallback;

		public Context ()
		{
			_extensionInstaller = new ExtensionInstaller (this);
			_configManager = new ConfigManager (this);

			_injectionBinder = new InjectionBinder ();
			_injectionBinder.Bind<IInjectionBinder> ().ToValue (_injectionBinder);

			_preInitilizeCallback = new ContextStateCallback ();
			_postInitializeCallback = new ContextStateCallback ();
			_preDestroyCallback = new ContextStateCallback ();
			_postDestroyCallback = new ContextStateCallback ();

			UnityEngine.Debug.Log ("Init Context");
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
			PreInitialized();

			_configManager.Initialize ();
			_initialized = true;
//			UnityEngine.Debug.Log ("Initalize");
			
			PostInitialized ();
			return this;
		}

		public IContext Install<T>() where T : IExtension
		{
			_extensionInstaller.Install<T>();
			return this;
		}

		public IContext Configure<T>() where T : class
		{
			_configManager.AddConfig<T>();
			return this;
		}

		public IContext Configure(params object[] objects)
		{
			foreach (Object obj in objects)
				_configManager.AddConfig(obj);
			return this;
		}

		// Handle this process match from the config
		public IContext AddConfigHandler(IMatcher matcher, Action<object> handler)
		{
			_configManager.AddConfigHandler (matcher, handler);
			return this;
		}


		// The states the context goes through in order

		// New Context uninitialized
		// User installs and runs Extensions
		// User adds configs

		// Context gets initialized either by user or a config
		// Context fires pre-initilized callbacks
		// Context processes configs
		// Initialized flag set
		// Context fires post-initilized callbacks
		
		// Context gets destroyed either by user or a config
		// Pre Destroyed
		// Destroyed


		private void PreInitialized()
		{
			_preInitilizeCallback.ProcessCallbacks ();
		}

		private void PostInitialized()
		{
			_postInitializeCallback.ProcessCallbacks ();
		}
		
		public IContext AddPreInitializedCallback(ContextStateCallback.CallbackDelegate callback)
		{
			_preInitilizeCallback.AddCallback (callback);
			return this;
		}
		
		public IContext AddPostInitializedCallback(ContextStateCallback.CallbackDelegate callback)
		{
			_postInitializeCallback.AddCallback (callback);
			return this;
		}
		
		public IContext AddPreDestroyCallback(ContextStateCallback.CallbackDelegate callback)
		{
			_preDestroyCallback.AddCallback (callback);
			return this;
		}
		
		public IContext AddPostDestroyCallback(ContextStateCallback.CallbackDelegate callback)
		{
			_postDestroyCallback.AddCallback (callback);
			return this;
		}
	}
}

