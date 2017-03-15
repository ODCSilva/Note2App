namespace Note2App {
    using System;
    using System.Windows.Input;

    #pragma warning disable CS0067
    /// <summary>
    /// AddCommand class.
    /// </summary>
    public class AddCommand : ICommand {
        #region Fields

        /// <summary>
        /// PageDataContext field.
        /// </summary>
        private PageDataContext pdc;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AddCommand"/> class.
        /// </summary>
        /// <param name="pdc">The PageDataContext this command belongs to.</param>
        public AddCommand(PageDataContext pdc) {
            this.pdc = pdc;
        }

        #endregion Constructors

        #region Events

        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        #endregion Events

        #region Methods

        /// <summary>
        /// Checks if the command can be executed.
        /// </summary>
        /// <param name="parameter">Command parameter</param>
        /// <returns>True if the command can be executed.</returns>
        public bool CanExecute(object parameter) {
            return true;
        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="parameter">Command parameters</param>
        public void Execute(object parameter) {
            pdc.SelectedNote = null;
        }

        #endregion Methods
    }
}