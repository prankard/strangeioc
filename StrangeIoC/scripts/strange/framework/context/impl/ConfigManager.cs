using System;
using System.Collections.Generic;
using strange.framework.context.api;

namespace strange.framework.context.impl
{
	public class ConfigManager
	{
		public IContext context;
		public List<IConfig> configs;

		public ConfigManager (IContext context)
		{
			this.context = context;
			configs = new List<IConfig> ();
		}

		public void AddConfig<T>() where T : IConfig
		{
			AddConfig (typeof(T));
		}

		public void AddConfig(object obj)
		{
			context.injectionBinder.Bind<IConfig>().To(obj);
			IConfig config = context.injectionBinder.GetInstance<IConfig> ();
			context.injectionBinder.Unbind<IConfig> ();
			configs.Add (config);

			if (context.initialized)
				Configure (config);
		}

		public void ConfigureAll ()
		{
			foreach (IConfig config in configs) 
			{
				Configure(config);
			}
		}

		private void Configure(IConfig config)
		{
			config.Configure ();
		}
	}
}

