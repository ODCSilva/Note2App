using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Note2App;
using System;
using System.Collections.ObjectModel;
using Windows.System.Threading;

namespace Note2AppTest {
    [TestClass]
    public class PageDataContextTest {

        [TestMethod]
        public void DuplicateNoteTitlesAreTested() {
            PageDataContext pdc = new PageDataContext();
            pdc.Notes = new ObservableCollection<NoteModel>();
            pdc.Notes.Add(new NoteModel(1, "TestTitle1", ""));
            Assert.IsTrue(pdc.CheckForDuplicateNoteTitles("TestTitle1"));
            Assert.IsFalse(pdc.CheckForDuplicateNoteTitles("TestTitle2"));
        }

        [TestMethod]
        public void TitleUpdatesWhenCurrentNoteIsSet() {
            PageDataContext pdc = new PageDataContext();

            string oldTitle = pdc.Title;

            pdc.SelectedNote = new NoteModel(1, "This is a new title");

            Assert.AreNotEqual(oldTitle, pdc.Title);
            Assert.IsTrue(pdc.Title.Equals("This is a new title"));
        }

        [TestMethod]
        public void TestNoteIsDirtyIfContentsHaveChanged() {
            PageDataContext pdc = new PageDataContext();
            string originalContents = "Original contents";
            pdc.SelectedNote = new NoteModel(1, "A note", originalContents);

            Assert.IsFalse(pdc.CanSave);

            pdc.Contents = "New Contents";

            Assert.IsTrue(pdc.CanSave);
        }

        [TestMethod]
        public void FilteringNotesReturnFilteredList() {
            ObservableCollection<NoteModel> filtered = new ObservableCollection<NoteModel>();
            PageDataContext pdc = new PageDataContext();

            TimeSpan seconds = TimeSpan.FromSeconds(3);

            ThreadPoolTimer timer = ThreadPoolTimer.CreatePeriodicTimer((t) => {
                pdc.Notes.Add(new NoteModel(1, "Filter Test One"));
                pdc.Notes.Add(new NoteModel(2, "Filter Test Two"));
                pdc.Notes.Add(new NoteModel(3, "Filter Test Three"));

                pdc.Filter = "Test Three";

                Assert.IsNotNull(filtered);

                int collectionSize = filtered.Count;
                string noteTitle = filtered[0].Title;

                Assert.AreEqual(1, collectionSize);
                Assert.IsTrue(noteTitle.Equals("Filter Test Three"));
            }, seconds);

        }
    }
}
