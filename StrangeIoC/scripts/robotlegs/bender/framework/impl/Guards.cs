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
using strange.extensions.injector.api;
using System.Reflection;


namespace robotlegs.bender.framework.impl
{
	public class Guards
	{
		public static bool Approve(object[] guards, IInjectionBinder injector) 
		{
			object guardInstance;

			foreach (object guard in guards)
			{
				if (guard is Func<bool>)
				{
					if ((guard as Func<bool>)())
						continue;
					return false;
				}
				if (guard is Type)
				{
					if (injector != null)
						guardInstance = injector.InstantiateUnmapped(guard as Type);
					else
						guardInstance = Activator.CreateInstance(guard as Type);
				}
				else
					guardInstance = guard;

				MethodInfo approveMethod = guardInstance.GetType().GetMethod("Approve");
				if (approveMethod != null)
				{
					if ((bool)approveMethod.Invoke(guardInstance, null) == false)
						return false;
				}
			}
			return true;
		}
	}
}
