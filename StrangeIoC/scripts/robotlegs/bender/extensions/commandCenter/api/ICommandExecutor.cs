namespace robotlegs.bender.extensions.commandCenter.api
{
	/// <summary>
	/// Optional Command interface.
	///
	/// <p>Note, you do not need to implement this interface,
	/// any class with an execute method can be used.</p>
	/// </summary>
	public interface ICommand
	{
		/// <summary>
		/// The execute method
		/// </summary>
		void Execute();
	}
}