//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using strange.framework.context.api;
using strange.extensions.debugLogging.impl;


namespace strange.extensions.debugLogging
{
	public class DebugLoggingExtension : IExtension
	{
		public void Extend (IContext context)
		{
			context.AddLogTarget(new DebugLogTarget(context));
		}
	}
}

