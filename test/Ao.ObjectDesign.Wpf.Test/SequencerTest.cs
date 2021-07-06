using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var seq = new Sequencer();
            var student = new Student();
            seq.Attack(student);
            student.Name = "hello";
            seq.Undo(false);
            Assert.IsNull(student.Name);
        }
        [TestMethod]
        public void Changed_UndoAfterRedo_PropertyMustResetOrigin()
        {
            var seq = new Sequencer();
            var student = new Student();
            seq.Attack(student);
            student.Name = "hello";
            seq.Undo(true);
            seq.Redo(true);
            Assert.AreEqual("hello",student.Name);
        }

    }
}
