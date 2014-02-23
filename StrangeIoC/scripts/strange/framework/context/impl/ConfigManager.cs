using System;
using System.Collections.Generic;
using strange.framework.context.api;
using strange.context.api;

namespace strange.framework.context.impl
{
	public class ConfigManager
	{
		public delegate void ProcessMatch(object obj);

		public IContext context;
		public List<object> configs;

		public List<MatchHandler> matchHandlers;

		public ConfigManager (IContext context)
		{
			this.context = context;
			configs = new List<object> ();
			matchHandlers = new List<MatchHandler> ();
			
			AddHandler (new MatchType (), ProcessIConfigType);
			AddHandler (new MatchIConfig (), ProcessIConfig);
		}

		public void AddConfig<T>() where T : class
		{
			AddConfig (typeof(T));
		}

		public void AddConfig(object obj)
		{
			configs.Add (obj);

			if (context.initialized)
				Configure (obj);
		}

		public void ConfigureAll ()
		{
			foreach (object config in configs) 
			{
				Configure(config);
			}
		}

		private void Configure(object config)
		{
			foreach (MatchHandler matchHandler in matchHandlers) 
			{
				if (matchHandler.matcher.Matches(config))
					matchHandler.process(config);
			}
		}

		public void AddHandler(IMatcher matcher, ProcessMatch process)
		{
			MatchHandler matchHandler;
			matchHandler.matcher = matcher;
			matchHandler.process = process;
			matchHandlers.Add (matchHandler);
		}

		public struct MatchHandler
		{
			public IMatcher matcher;
			public ProcessMatch process;
		}

		public void ProcessIConfigType(object obj)
		{
			Type type = obj as Type;
			
			context.injectionBinder.Bind<IConfig>().To(type);
			IConfig config = context.injectionBinder.GetInstance<IConfig> ();
			context.injectionBinder.Unbind<IConfig> ();

			if (config != null) config.Configure ();
		}

		public void ProcessIConfig(object obj)
		{
			// This injection binder trick doesn't work.
			// We will need to re-process the injection so new injection rules will be handled for 
			// objects that have already been instatiated.

			IConfig config = obj as IConfig;
			context.injectionBinder.Bind<IConfig> ().ToValue(config);
			config = context.injectionBinder.GetInstance<IConfig> ();
			context.injectionBinder.Unbind<IConfig> ();

			if (config != null) config.Configure ();
		}
	}
	
	public class MatchType : IMatcher
	{
		public bool Matches(object obj)
		{
			return obj is Type;
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

