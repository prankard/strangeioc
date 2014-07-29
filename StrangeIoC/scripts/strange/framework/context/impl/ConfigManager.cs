using System;
using System.Collections.Generic;
using strange.framework.context.api;
using strange.context.api;

namespace strange.framework.context.impl
{
	public class ConfigManager
	{
		public delegate void ProcessMatch(object obj);

		private bool _initialized = false;

		public IContext context;
		private List<object> _configs = new List<object>();
		private List<object> _queue = new List<object>();

		public ObjectProcessor _objectProcessor = new ObjectProcessor();

		public ConfigManager (IContext context)
		{
			this.context = context;
			
			AddConfigHandler (new MatchTypeIConfig (), HandleIConfigType);
			AddConfigHandler (new MatchIConfig (), HandleIConfigObject);
		}

		public void AddConfig<T>() where T : class
		{
			AddConfig (typeof(T));
		}

		public void AddConfig(object config)
		{
			UnityEngine.Debug.Log("Added config: " + config);

			if (!_configs.Contains(config))
			{
				_configs.Add(config);
				_objectProcessor.ProcessObject(config);
			}
		}
		
		public void AddConfigHandler(IMatcher matcher, Action<object> process)
		{
			_objectProcessor.AddObjectHandler(matcher, process);
		}

		public void Destroy()
		{
			_objectProcessor.RemoveAllHandlers();
			_configs.Clear();
		}
		
		public void Initialize()
		{
			if (!_initialized)
			{
				_initialized = true;
				ProcessQueue();
			}
		}

		private void ProcessQueue()
		{
			foreach (object config in _queue)
			{
				if (config is Type)
					ProcessIConfigType(config);
				else
					ProcessIConfigObject(config);
			}
			_queue.Clear();
		}

		private void HandleIConfigType(object config)
		{
			if (_initialized)
				ProcessIConfigType(config);
			else
				_queue.Add(config);
		}

		private void HandleIConfigObject(object config)
		{
			if (_initialized)
				ProcessIConfigObject(config);
			else
				_queue.Add(config);
		}

		private void ProcessIConfigType(object config)
		{
			Type type = config as Type;
			
			context.injectionBinder.Bind<IConfig>().To(type);
			IConfig typedConfig = context.injectionBinder.GetInstance<IConfig> ();
			context.injectionBinder.Unbind<IConfig> ();

			if (typedConfig != null) typedConfig.Configure ();
		}

		private void ProcessIConfigObject(object config)
		{
			//TODO: This injection binder trick doesn't work.
			// We will need to re-process the injection so new injection rules will be handled for 
			// objects that have already been instatiated.

			IConfig typedConfig = config as IConfig;
			context.injectionBinder.Bind<IConfig> ().ToValue(typedConfig);
			typedConfig = context.injectionBinder.GetInstance<IConfig> ();
			context.injectionBinder.Unbind<IConfig> ();

			if (typedConfig != null) typedConfig.Configure ();
		}
	}
	
	public class MatchTypeIConfig : IMatcher
	{
		public bool Matches(object obj)
		{
			return (obj is Type && typeof(IConfig).IsAssignableFrom(obj as Type));
		}
	}
	
	public class MatchIConfig : IMatcher
	{
		public bool Matches(object obj)
		{
			return obj is IConfig;
		}
	}
}

