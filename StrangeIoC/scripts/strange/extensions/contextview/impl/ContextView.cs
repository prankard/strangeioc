using System;
using strange.extensions.contextview.api;

namespace strange.extensions.contextview.impl
{
	public class ContextView : IContextView
	{
		private object _view;

		public ContextView (object view)
		{
			_view = view;
		}

		public object view
		{
			get
			{
				return _view;
			}
		}
	}
}

