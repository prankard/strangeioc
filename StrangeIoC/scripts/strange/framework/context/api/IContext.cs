using System;
using strange.extensions.injector.api;

namespace strange.framework.context.api
{
	public interface IContext
	{
		IInjectionBinder injectionBinder { get; }
		bool initialized { get; }
		IContext Initialize();
		IContext Install<T>() where T : IExtension;
		IContext Configure<T>() where T : IConfig;
	}
}

