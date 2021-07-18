namespace Ao.ObjectDesign.Wpf
{
    public interface IUndoRedo
    {
        void CleanAllRecords();
        void Redo(bool pushUndo);
        void Undo(bool pushRedo);
    }
}