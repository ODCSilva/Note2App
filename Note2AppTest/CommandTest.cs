using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Note2App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note2AppTest {
    [TestClass]
    public class CommandTest {

        [TestMethod]
        public void AddCommandCanExecuteReturnTrue() {
            PageDataContext pdc = new PageDataContext();
            AddCommand add = new AddCommand(pdc);

            Assert.IsTrue(add.CanExecute(null));
        }

        [TestMethod]
        public void SaveCommandCanOnlyExecuteWhenNoteIsDirty() {
            PageDataContext pdc = new PageDataContext();
            string originalContents = "Original contents";
            pdc.SelectedNote = new NoteModel(1, "A note", originalContents);
            SaveCommand save = new SaveCommand(pdc);
            Assert.IsFalse(save.CanExecute(null));
            pdc.Contents = "New Contents";
            Assert.IsTrue(save.CanExecute(null));
        }

        [TestMethod]
        public void EditCommandCanOnlyExecuteIfFileIsReadOnly() {
            PageDataContext pdc = new PageDataContext();
            EditCommand edit = new EditCommand(pdc);
            pdc.IsReadOnly = true;
            Assert.IsTrue(edit.CanExecute(null));
            pdc.IsReadOnly = false;
            Assert.IsFalse(edit.CanExecute(null));
        }

        [TestMethod]
        public void DeleteCommandCantExecuteIsSelectedNoteIsNull() {
            PageDataContext pdc = new PageDataContext();
            DeleteCommand delete = new DeleteCommand(pdc);
            Assert.IsFalse(delete.CanExecute(null));
            pdc.SelectedNote = new NoteModel(30, "Some note");
            Assert.IsTrue(delete.CanExecute(null));
        }
    }
}
