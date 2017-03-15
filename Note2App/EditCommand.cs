namespace Note2App {
    using System;
    using System.Windows.Input;

    /// <summary>
    /// EditCommand class.
    /// </summary>
    public class EditCommand : ICommand {
        #region Fields

        /// <summary>
        /// PageDataContext field.
        /// </summary>
        private PageDataContext pdc;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EditCommand" /> class.
        /// </summary>
        /// <param name="pdc">The PageDataContext this command belongs to.</param>
        public EditCommand(PageDataContext pdc) {
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
        /// Verifies if this command can be executed.
        /// </summary>
        /// <param name="parameter">Command parameters</param>
        /// <returns>True if the command can be executed.</returns>
        public bool CanExecute(object parameter) {
            return pdc.IsReadOnly;
        }

        /// <summary>
        /// Executes this command
        /// </summary>
        /// <param name="parameter">Command parameters</param>
        public void Execute(object parameter) {
            pdc.IsReadOnly = false;
        }

        /// <summary>
        /// Triggers the CanExecuteChange event.
        /// </summary>
        public void FireCanExecuteChanged() {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        #endregion Methods
    }
}