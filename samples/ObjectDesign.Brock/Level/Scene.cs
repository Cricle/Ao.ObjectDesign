using Ao.ObjectDesign.Designing.Level;
using ObjectDesign.Brock.Components;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ObjectDesign.Brock.Level
{
    public class Scene : FrameworkElementSetting, IObservableDesignScene<UIElementSetting>
    {
        public Scene()
        {
        }
        public Scene(IEnumerable<UIElementSetting> objects)
        {
            if (objects is null)
            {
                throw new ArgumentNullException(nameof(objects));
            }

            DesigningObjects = new SilentObservableCollection<UIElementSetting>(objects);
        }
        private SilentObservableCollection<UIElementSetting> designingObjects;

        public SilentObservableCollection<UIElementSetting> DesigningObjects
        {
            get
            {
                if (designingObjects == null)
                {
                    designingObjects = new SilentObservableCollection<UIElementSetting>();
                }
                return designingObjects;
            }
            set
            {
                if (designingObjects != null)
                {
                    throw new InvalidOperationException("Property DesigningObjects is once set property");
                }
                designingObjects = value;
            }
        }

        IList<UIElementSetting> IDesignScene<UIElementSetting>.DesigningObjects => DesigningObjects;
    }
}
