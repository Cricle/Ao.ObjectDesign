using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Ao.ObjectDesign.Designing.Test
{
    class Student : NotifyableObject
    {
        private string name;

        public string Name
        {
            get => name;
            set => Set(ref name, value);
        }
    }
    [TestClass]
    public class SequencerTest
    {
        private void CoreChanged_Undo_PropertyMustUndo(Func<PropertySequencer> func)
        {
            PropertySequencer seq = func();
            Student student = new Student();
            seq.Attack(student);
            student.Name = "hello";
            seq.Undo(false);
            Assert.IsNull(student.Name);
        }
        private void CoreChanged_UndoAfterRedo_PropertyMustResetOrigin(Func<PropertySequencer> func)
        {
            PropertySequencer seq = func();
            Student student = new Student();
            seq.Attack(student);
            student.Name = "hello";
            seq.Undo(true);
            seq.Redo(true);
            Assert.AreEqual("hello", student.Name);
        }
        [TestMethod]
        public void Changed_Undo_PropertyMustUndo()
        {
            CoreChanged_Undo_PropertyMustUndo(() => new PropertySequencer());
        }
        [TestMethod]
        public void Changed_UndoAfterRedo_PropertyMustResetOrigin()
        {
            CoreChanged_UndoAfterRedo_PropertyMustResetOrigin(() => new PropertySequencer());
        }
        [TestMethod]
        public void Changed_Undo_CompiledPropertyMustUndo()
        {
            CoreChanged_Undo_PropertyMustUndo(() => new CompiledPropertySequencer());
        }
        [TestMethod]
        public void Changed_UndoAfterRedo_CompiledPropertyMustResetOrigin()
        {
            CoreChanged_UndoAfterRedo_PropertyMustResetOrigin(() => new CompiledPropertySequencer());
        }

    }
}
