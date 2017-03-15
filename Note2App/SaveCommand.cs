namespace Note2App {
    using System;
    using System.Windows.Input;
    using Windows.UI.Xaml.Controls;

    /// <summary>
    /// SaveCommand class
    /// </summary>
    public class SaveCommand : ICommand {
        #region Fields

        /// <summary>
        /// PageDataContext field.
        /// </summary>
        private PageDataContext pdc;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveCommand" /> class.
        /// </summary>
        /// <param name="pdc">The PageDataContext this command belongs to.</param>
        public SaveCommand(PageDataContext pdc) {
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
            return pdc.CanSave;
        }

        /// <summary>
        /// Executes this command
        /// </summary>
        /// <param name="parameter">Command parameters</param>
        public void Execute(object parameter) {
            if (pdc.SelectedNote != null) {
                pdc.SelectedNote.Contents = pdc.Contents;
                pdc.Title = pdc.SelectedNote.Title;
            }
            else {
                ShowNewNoteTitleDialog();
            }

            FireCanExecuteChanged();
        }

        /// <summary>
        /// Triggers the CanExecuteChange event.
        /// </summary>
        public void FireCanExecuteChanged() {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Shows a dialog prompting user to enter the title of the new note.
        /// </summary>
        public async void ShowNewNoteTitleDialog() {
            NoteTitleDialog newNoteTitleDialog = new NoteTitleDialog() {
                Title = "Name your new note",
                PrimaryButtonText = "Save",
                SecondaryButtonText = "Cancel"
            };

            ContentDialogResult result = await newNoteTitleDialog.ShowAsync();

            if (result == ContentDialogResult.Primary) {
                var noteTitle = newNoteTitleDialog.NoteTitle;

                if (pdc.CheckForDuplicateNoteTitles(noteTitle)) {
                    ContentDialog noDuplicateTitlesDialog = new ContentDialog() {
                        Title = "Duplicate notes names",
                        Content = "A note with this title already exists. Please try again.",
                        PrimaryButtonText = "Okay"
                    };

                    await noDuplicateTitlesDialog.ShowAsync();
                }
                else {
                    NoteModel note = new NoteModel((uint)pdc.Notes.Count + 1, noteTitle, pdc.Contents);
                    pdc.Notes.Add(note);
                    pdc.SelectedNote = note;
                    pdc.SaveNotes();
                }
            }
        }

        #endregion Methods
    }
}