using System;
using System.Diagnostics;

namespace Ao.ObjectDesign.WpfDesign
{
    public class DelegateInstanceFactory : InstanceFactory
    {
        public DelegateInstanceFactory(Type targetType, Func<object> creator)
            : base(targetType)
        {
            Creator = creator ?? throw new ArgumentNullException(nameof(creator));
        }

        public Func<object> Creator { get; }

        public override object Create()
        {
            Debug.Assert(Creator != null);
            return Creator();
        }
    }
}
