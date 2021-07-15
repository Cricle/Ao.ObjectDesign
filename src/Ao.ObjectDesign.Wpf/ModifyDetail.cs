namespace Ao.ObjectDesign.Wpf
{
    public class ModifyDetail : IModifyDetail
    {
        public ModifyDetail(object instance, string propertyName, object from, object to)
        {
            Instance = instance;
            PropertyName = propertyName;
            From = from;
            To = to;
        }

        public object Instance { get; }

        public string PropertyName { get; }

        public object From { get; }

        public object To { get; }

        public ModifyDetail Reverse()
        {
            return new ModifyDetail(Instance, PropertyName, To, From);
        }
    }
}
