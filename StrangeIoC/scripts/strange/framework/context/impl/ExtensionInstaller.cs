using System;
using System.Collections.Generic;
using strange.extensions.injector.api;
using strange.framework.context.api;

namespace strange.framework.context.impl
{
	public class ExtensionInstaller
	{
		public Context context;
		public Dictionary <Type, IExtension> dict;

		public ExtensionInstaller (Context context)
		{
			this.context = context;
			dict = new Dictionary<Type, IExtension> ();
		}

		public void Install<T>() where T : IExtension
		{
			Install (typeof(T));
		}

		public void Install(IExtension extension)
		{
			Type extensionType = extension.GetType();
			if (!dict.ContainsKey(extensionType)) 
			{
				dict.Add (extensionType, extension);
				extension.Extend (context);
			}
		}

		private void Install(object obj)
		{
			context.injectionBinder.Bind<IExtension>().To(obj);
			IExtension extension = context.injectionBinder.GetInstance<IExtension> ();
			context.injectionBinder.Unbind<IExtension> ();
			Install(extension);
		}
	}
}