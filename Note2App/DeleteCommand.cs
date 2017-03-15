namespace Note2App {
    using System;
    using System.Windows.Input;
    using Windows.UI.Xaml.Controls;

    /// <summary>
    /// DeleteCommand class.
    /// </summary>
    public class DeleteCommand : ICommand {
        #region Fields

        /// <summary>
        /// PageDataContext field.
        /// </summary>
        private PageDataContext pdc;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteCommand" /> class.
        /// </summary>
        /// <param name="pdc">The PageDataContext this command belongs to.</param>
        public DeleteCommand(PageDataContext pdc) {
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
            return pdc.SelectedNote != null;
        }

        /// <summary>
        /// Executes this command
        /// </summary>
        /// <param name="parameter">Command parameters</param>
        public void Execute(object parameter) {
            ShowDeleteConfirmationDialog();
        }

        /// <summary>
        /// Triggers the CanExecuteChange event.
        /// </summary>
        public void FireCanExecuteChanged() {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Displays a dialog prompting for confirmation of note deletion.
        /// </summary>
        private async void ShowDeleteConfirmationDialog() {
            ContentDialog deleteFileDialog = new ContentDialog() {
                Title = "Delete file permanently?",
                Content = "If you delete this file, you won't be able to recover it. Do you want to delete it?",
                PrimaryButtonText = "Delete",
                SecondaryButtonText = "Cancel"
            };

            ContentDialogResult result = await deleteFileDialog.ShowAsync();

            if (result == ContentDialogResult.Primary) {
                pdc.Notes.Remove(pdc.SelectedNote);
                pdc.SaveNotes();
            }
        }

        #endregion Methods
    }
}