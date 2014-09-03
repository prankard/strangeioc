using System;
using robotlegs.bender.framework.api;

namespace robotlegs.bender.bundles
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

