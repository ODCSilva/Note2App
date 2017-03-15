namespace Note2App {
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;

    /// <summary>
    /// Data Context for this App's MainPage.
    /// </summary>
    public class PageDataContext : INotifyPropertyChanged {
        #region Fields

        /// <summary>
        /// isReadOnly field.
        /// </summary>
        private bool isReadOnly;

        /// <summary>
        /// Currently selected NoteModel
        /// </summary>
        private NoteModel selectedNote;

        /// <summary>
        /// Note contents.
        /// </summary>
        private string contents;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PageDataContext"/> class.
        /// </summary>
        public PageDataContext() {
            LoadNotes();
            AddCommand = new AddCommand(this);
            SaveCommand = new SaveCommand(this);
            DeleteCommand = new DeleteCommand(this);
            EditCommand = new EditCommand(this);
        }

        #endregion Constructors

        #region Events

        /// <summary>
        /// Event called when a property has changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Properties

        /// <summary>
        /// Gets the AddCommand
        /// </summary>
        public AddCommand AddCommand { get; }

        /// <summary>
        /// Gets a value indicating whether the note can be saved or not.
        /// </summary>
        public bool CanSave {
            get
            {
                if (selectedNote != null) {
                    var isDirty = selectedNote.IsDirty(contents);
                    Title = isDirty ? string.Format("{0}*", selectedNote.Title) : Title;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Title"));
                    return isDirty;
                }
                else {
                    return contents != string.Empty;
                }
            }
        }

        private string filter;
        /// <summary>
        /// 
        /// </summary>
        public string Filter
        {
            get { return filter; }
            set
            {
                filter = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Filter)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Notes)));
            }
        }

        /// <summary>
        /// Gets or sets the contents of the note.
        /// </summary>
        public string Contents {
            get {
                return contents;
            }

            set
            {
                contents = value;
                SaveCommand.FireCanExecuteChanged();
            }
        }

        /// <summary>
        /// Gets the DeleteCommand
        /// </summary>
        public DeleteCommand DeleteCommand { get; }

        /// <summary>
        /// Gets the EditCommand
        /// </summary>
        public EditCommand EditCommand { get; }

        /// <summary>
        /// Gets or sets a value indicating whether the main editor is read only or not.
        /// </summary>
        public bool IsReadOnly {
            get {
                return isReadOnly;
            }

            set
            {
                isReadOnly = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsReadOnly"));
                EditCommand.FireCanExecuteChanged();
            }
        }

        private ObservableCollection<NoteModel> notes;

        /// <summary>
        /// Gets or sets the Notes list.
        /// </summary>
        public ObservableCollection<NoteModel> Notes {
            get {
                if (!string.IsNullOrEmpty(Filter))
                {
                    string f = Filter.ToLowerInvariant().Trim();
                    return new ObservableCollection<NoteModel>(notes.Where(d => d.Title.ToLowerInvariant().
                    Contains(f)).ToList());
                }
                else
                {
                    return notes;
                }
            }
            set { notes = value; }
        }

        /// <summary>
        /// Gets the SaveCommand.
        /// </summary>
        public SaveCommand SaveCommand { get; }

        /// <summary>
        /// Gets or sets the currently selected Note.
        /// </summary>
        public NoteModel SelectedNote {
            get {
                return selectedNote;
            }

            set
            {
                selectedNote = value;
                Title = (value == null) ? "Untitled" : value.Title;
                Contents = (value == null) ? string.Empty : value.Contents;
                IsReadOnly = (value != null) ? true : false;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Title"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Contents"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedNote"));
                DeleteCommand.FireCanExecuteChanged();
                EditCommand.FireCanExecuteChanged();
            }
        }

        /// <summary>
        /// Gets or sets the note's Title.
        /// </summary>
        public string Title { get; set; } = "Untitled";

        #endregion Properties

        #region Methods

        /// <summary>
        /// Check for duplicate title names.
        /// </summary>
        /// <param name="title">The title to check duplicates for.</param>
        /// <returns>True if a duplicate title exists, false otherwise.</returns>
        public bool CheckForDuplicateNoteTitles(string title) {
            foreach (NoteModel note in Notes) {
                if (note.Title.Equals(title)) {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        public async void LoadNotes() {
            Notes = await NoteRepository.GetAllNotesAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        public void SaveNotes() {
            NoteRepository.StoreAllNotesAsync(Notes);
        }

        #endregion Methods
    }
}