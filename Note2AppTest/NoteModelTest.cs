using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Note2App;
using System;
using Windows.System.Threading;

namespace Note2AppTest {
    [TestClass]
    public class NoteModelTest {

        [TestMethod]
        public void NoteModelInitializesProperly() {
            var note = new NoteModel(1, "The Title", "Content");

            Assert.AreEqual((uint)1, note.Id);
            Assert.AreEqual("The Title", note.Title);
            Assert.AreEqual("Content", note.Contents);
        }

        [TestMethod]
        public void NotesAreEqualIfPropertiesAreTheSame() {
            var note1 = new NoteModel(1, "The Title", "Content");
            var note2 = new NoteModel(1, "The Title", "Content");
            var note3 = new NoteModel(3, "Hello", "Hi");

            Assert.IsTrue(note2.Equals(note1));
            Assert.IsFalse(note2.Equals(note3));
            Assert.IsFalse(note2.Equals(null));
        }

        [TestMethod]
        public void NoteContentIsDirtyIfPassedContentIsNotSame() {
            var note1 = new NoteModel(1, "The Title", "Content");

            Assert.IsTrue(note1.IsDirty("New Content"));
            note1.Contents = "New Content";
            Assert.IsFalse(note1.IsDirty("New Content"));
        }

        [TestMethod]
        public void NoteModifiedAtIsUpdatedWhenContentsAreChange() {
            var note1 = new NoteModel(1, "The Title", "Content");
            note1.Contents = "A";
            DateTime stamp = note1.ModifiedAt;

            TimeSpan seconds = TimeSpan.FromSeconds(2);

            ThreadPoolTimer timer = ThreadPoolTimer.CreatePeriodicTimer((t) => {
                note1.Contents = "B";
                Assert.AreNotEqual(stamp, note1.ModifiedAt);
            }, seconds);
        }
    }
}
