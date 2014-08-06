using System;
using strange.framework.context.api;
using strange.extensions.injector.api;
using strange.extensions.contextview.impl;
using strange.extensions.contextview.api;
using strange.context.api;
using strange.extensions.matching;

namespace stange.extensions.contextview
{
	public class ContextViewExtension : IExtension
	{
		private IInjectionBinder _injectionBinder;

		public ContextViewExtension ()
		{

		}

		public void Extend(IContext context)
		{
			_injectionBinder = context.injectionBinder;
			UnityEngine.Debug.Log ("Context view extension");
			context.AddPostInitializedCallback (CheckInjection);
			context.AddConfigHandler(new InstanceOfMatcher (typeof(ContextView)), AddContextView);
		}
		
		private bool HasContextBinding()
		{
			return _injectionBinder.GetBinding<ContextView> () != null;
		}

		public void AddContextView(object contextView)
		{
			if (!HasContextBinding ()) 
			{
				UnityEngine.Debug.Log("Adding Context View");
				_injectionBinder.Bind<ContextView> ().To (contextView);
				_injectionBinder.Bind<IContextView> ().To (contextView);
			}
			else
				UnityEngine.Debug.Log ("You already have a context bound, please only use one contextview per context");
		}

		private void CheckInjection()
		{
			if (!HasContextBinding ()) 
			{
				UnityEngine.Debug.Log("Warning, you initilaized the context without a context view when the context view extension was installed");
			}
		}
	}
}

