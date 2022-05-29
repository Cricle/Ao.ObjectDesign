using Ao.ObjectDesign.Sources;

namespace ObjectDesign.Projecting
{
    public class DataManager
    {
        public DataManager(DataProviderGroup group)
        {
            Group = group;
        }

        public DataProviderGroup Group { get; }

        public object DataObject => Group.GetData();
    }
}
