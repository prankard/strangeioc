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
using strange.extensions.viewManager.api;
using strange.extensions.mediatorMap.api;
using System.Collections.Generic;


namespace strange.extensions.mediatorMap.impl
{
	public class MediatorViewHandler : IViewHandler
	{
		/*============================================================================*/
		/* Private Properties                                                         */
		/*============================================================================*/
		
		private List<IMediatorMapping> _mappings = new List<IMediatorMapping>();
		
		private Dictionary<Type, List<IMediatorMapping>> _knownMappings = new Dictionary<Type, List<IMediatorMapping>>();
		
		private MediatorFactory _factory;

		/*============================================================================*/
		/* Constructor                                                                */
		/*============================================================================*/

		public MediatorViewHandler (MediatorFactory factory)
		{
			_factory = factory;
		}
		
		/*============================================================================*/
		/* Public Functions                                                           */
		/*============================================================================*/

		public void AddMapping(IMediatorMapping mapping)
		{
			if (_mappings.Contains(mapping))
				return;

			_mappings.Add(mapping);
			FlushCache();
		}

		public void RemoveMapping(IMediatorMapping mapping)
		{
			int index = _mappings.IndexOf(mapping);
			if (index == -1)
				return;

			_mappings.RemoveAt(index);
			FlushCache();
		}
		
		public void HandleView(object view, Type type)
		{
			List<IMediatorMapping> interestedMappings = GetInterestedMappingsFor(view, type);
			UnityEngine.Debug.Log("Handle view; " + interestedMappings);
			if (interestedMappings != null)
				_factory.CreateMediators(view, type, interestedMappings);
		}

		/*============================================================================*/
		/* Private Functions                                                          */
		/*============================================================================*/

		private void FlushCache()
		{
			_knownMappings.Clear();
		}
		
		private List<IMediatorMapping> GetInterestedMappingsFor(object item, Type type)
		{
			if (!_knownMappings.ContainsKey(type))
			{
				_knownMappings[type] = new List<IMediatorMapping>();

				foreach (IMediatorMapping mapping in _mappings)
				{
					if (mapping.Matcher.Matches(item))
					{
						_knownMappings[type].Add(mapping);
					}
				}

				if (_knownMappings[type].Count == 0)
					_knownMappings[type] = null;
			}

			return _knownMappings[type];
		}
	}
}

