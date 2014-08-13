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
using strange.extensions.mediatorMap.api;
using strange.extensions.viewManager.api;
using System.Collections.Generic;
using strange.extensions.mediatorMap.dsl;
using strange.framework.context.api;
using strange.extensions.matching;

namespace strange.extensions.mediatorMap.impl
{
	public class MediatorMap : IMediatorMap, IViewHandler
	{
		/*============================================================================*/
		/* Private Properties                                                         */
		/*============================================================================*/

		private Dictionary<string, MediatorMapper> _mappers = new Dictionary<string, MediatorMapper>();

		private ILogger _logger;

		private MediatorFactory _factory;

		private MediatorViewHandler _viewHandler;

		private static readonly IMediatorUnmapper NULL_UNMAPPER = new NullMediatorUnmapper();
		
		/*============================================================================*/
		/* Constructor                                                                */
		/*============================================================================*/

		public MediatorMap (IContext context)
		{
			_logger = context.GetLogger(this);
			_factory = new MediatorFactory(context.injectionBinder, null);
			_viewHandler = new MediatorViewHandler(_factory);
		}

		/*============================================================================*/
		/* Public Functions                                                           */
		/*============================================================================*/
		
		public IMediatorMapper MapMatcher(ITypeMatcher matcher)
		{
			string descriptor = matcher.CreateTypeFilter().Descriptor;
			if (!_mappers.ContainsKey(descriptor))
				_mappers[descriptor] = CreateMapper(matcher);

			return _mappers[descriptor];
		}

		public IMediatorMapper Map(Type type)
		{
			return MapMatcher(new TypeMatcher().AllOf(type));
		}

		public IMediatorUnmapper UnmapMatcher(ITypeMatcher matcher)
		{
			string descriptor = matcher.CreateTypeFilter().Descriptor;
			return _mappers.ContainsKey(descriptor) ? _mappers[descriptor] : NULL_UNMAPPER;
		}

		public IMediatorUnmapper Unmap(Type type)
		{
			return UnmapMatcher(new TypeMatcher().AllOf(type));
		}

		public void HandleView(object view, Type type)
		{
			_viewHandler.HandleView(view, type);
		}

		public void Mediate(object item)
		{
			_viewHandler.HandleView(item, item.GetType());
		}

		public void Unmediate(object item)
		{
			_factory.RemoveMediators(item);
		}

		public void UnmediateAll()
		{
			_factory.RemoveAllMediators();
		}

		/*============================================================================*/
		/* Private Functions                                                          */
		/*============================================================================*/

		private MediatorMapper CreateMapper(ITypeMatcher matcher)
		{
			return new MediatorMapper(matcher.CreateTypeFilter(), _viewHandler, _logger);
		}
	}
}

