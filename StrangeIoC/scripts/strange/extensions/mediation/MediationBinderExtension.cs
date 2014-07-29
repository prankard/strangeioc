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
using strange.extensions.mediation.api;
using strange.extensions.mediation.impl;
using strange.extensions.injector.api;
using strange.extensions.viewManager.api;


namespace strange.extensions.mediation
{
	public class MediationBinderExtension : IExtension
	{
		private IInjectionBinder _injectionBinder;
		
		private IMediationBinder _mediationBinder;
		
		private IViewManager _viewManager;

		public void Extend (IContext context)
		{
			
			UnityEngine.Debug.Log("-----> MediationBinderExtension");
			// Store reference for functions below
			_injectionBinder = context.injectionBinder;

			// Bind Mediation Binder
			_injectionBinder.Bind<IMediationBinder>().To<MediationBinder>().ToSingleton();

			context.AddPreInitializedCallback(BeforeInitializing);
			context.AddPreDestroyCallback(BeforeDestroying);
		}

		private void BeforeInitializing()
		{
			// Before initializing
			_mediationBinder = _injectionBinder.GetInstance<IMediationBinder>();

			//TODO: Add child injectors to injection binder, and then check it's not a child injector
			_viewManager = _injectionBinder.GetInstance<IViewManager>();
			_viewManager.AddViewHandler(_mediationBinder);

			UnityEngine.Debug.Log("Added view handler");
		}

		private void BeforeDestroying()
		{
			_viewManager.RemoveViewHandler(_mediationBinder);
		}

		private void WhenDestroying()
		{
			_injectionBinder.Unbind(_injectionBinder.GetBinding<IMediationBinder>());
		}
	}
}

