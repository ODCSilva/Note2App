namespace Note2App {
    using System;

    /// <summary>
    /// NoteModel class.
    /// </summary>
    public class NoteModel : IEquatable<NoteModel> {
        #region Fields

        /// <summary>
        /// Contents field.
        /// </summary>
        private string contents;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NoteModel"/> class.
        /// </summary>
        /// <param name="id">The id of this NoteModel instance.</param>
        /// <param name="title">The title of this NoteModel instance.</param>
        public NoteModel(uint id, string title) {
            Id = id;
            Title = title;
            Contents = string.Empty;
            CreatedAt = DateTime.Now;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NoteModel"/> class.
        /// </summary>
        public NoteModel() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="NoteModel"/> class.
        /// </summary>
        /// <param name="id">The id of this NoteModel instance.</param>
        /// <param name="title">The title of this NoteModel instance.</param>
        /// <param name="content">The contents of this NoteModel instance.</param>
        public NoteModel(uint id, string title, string content) {
            Id = id;
            Title = title;
            Contents = content;
            CreatedAt = DateTime.Now;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the contents of this Note.
        /// </summary>
        public string Contents {
            get
            {
                return contents;
            }

            set
            {
                contents = value;
                ModifiedAt = DateTime.Now;
            }
        }

        /// <summary>
        /// Gets the created date of this note.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the Id of this note.
        /// </summary>
        public uint Id { get; set; }

        /// <summary>
        /// Gets or sets the Modified date of this note.
        /// </summary>
        public DateTime ModifiedAt { get; set; }

        /// <summary>
        /// Gets or sets the Title of this note.
        /// </summary>
        public string Title { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Check if notes are equal by comparing their ids.
        /// </summary>
        /// <param name="other">The note to compare with.</param>
        /// <returns>True if the ids are equal, false otherwise.</returns>
        public bool Equals(NoteModel other) {
            if (other == null) {
                return false;
            }
            else if (Id.Equals(other.Id) 
                && Title.Equals(other.Title) 
                && Contents.Equals(other.Contents)) {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Checks if note's contents are dirty by comparing it with the contents of the <see paramref="comp"/> parameter.
        /// </summary>
        /// <param name="comp">The contents to compare with.</param>
        /// <returns>True if contents are different, false otherwise.</returns>
        public bool IsDirty(string comp) {
            return Contents != comp;
        }

        #endregion Methods
    }
}