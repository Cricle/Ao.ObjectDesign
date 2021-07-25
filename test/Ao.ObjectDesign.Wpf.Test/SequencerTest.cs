using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ao.ObjectDesign.Wpf.Test
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
        [TestMethod]
        public void Changed_Undo_PropertyMustUndo()
        {
            PropertySequencer seq = new PropertySequencer();
            Student student = new Student();
            seq.Attack(student);
            student.Name = "hello";
            seq.Undo(false);
            Assert.IsNull(student.Name);
        }
        [TestMethod]
        public void Changed_UndoAfterRedo_PropertyMustResetOrigin()
        {
            PropertySequencer seq = new PropertySequencer();
            Student student = new Student();
            seq.Attack(student);
            student.Name = "hello";
            seq.Undo(true);
            seq.Redo(true);
            Assert.AreEqual("hello", student.Name);
        }

    }
}
