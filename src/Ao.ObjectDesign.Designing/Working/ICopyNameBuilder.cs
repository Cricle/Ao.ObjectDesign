namespace Ao.ObjectDesign.Designing.Working
{
    public interface ICopyNameBuilder
    {
        string CreateCopyName(string originName, int index);

        string GetOrigin(string name);

        int? GetIndex(string name);
    }
}
