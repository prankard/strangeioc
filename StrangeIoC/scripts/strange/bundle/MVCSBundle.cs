using System;
using strange.framework.context.api;

namespace strange.bundle
{
	public class MVCSBundle : IExtension
	{
		public void Extend(IContext context)
		{
#if UNITY
			context.Install<MVCSUnityBundle>();
#else
			context.Install<MVCSUnityBundle>();
#endif
		}
	}
}

