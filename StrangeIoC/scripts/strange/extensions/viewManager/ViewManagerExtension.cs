//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18449
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using strange.framework.context.api;
using strange.extensions.viewManager.impl;
using strange.extensions.viewManager.api;


namespace strange.extensions.viewManager
{
	public class ViewManagerExtension : IExtension
	{
		private IViewManager _viewManager;

		public void Extend (IContext context)
		{
			context.injectionBinder.Bind<IViewManager>().To<ViewManager>().ToSingleton();
		}
	}
}

