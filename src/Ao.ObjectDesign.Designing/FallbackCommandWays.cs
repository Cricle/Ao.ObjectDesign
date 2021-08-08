using System.Diagnostics;

namespace Ao.ObjectDesign.Designing
{
    [DebuggerDisplay("Count = {Count}")]
    public class FallbackCommandWays<T> : CommandWays<T>, IUndoRedo
        where T : IFallbackable
    {
        public void CleanAllRecords()
        {
            Clear(true);
        }
        public void Redo()
        {
            foreach (var item in this)
            {
                item.Reverse().Fallback();
            }
        }
        void IUndoRedo.Redo(bool pushUndo)
        {
            Redo();
        }
        public void Undo()
        {
            foreach (var item in this)
            {
                item.Fallback();
            }
        }

        void IUndoRedo.Undo(bool pushRedo)
        {
            Undo();
        }
    }
}
