using Ao.ObjectDesign.Designing.Annotations;

using ObjectDesign.Brock.Controls.BindingCreators;
using System.Windows.Controls.Primitives;

namespace ObjectDesign.Brock.Controls
{
    
    
    public class TextBoxBaseSetting : ControlSetting
    {
        private bool acceptsReturn;

        public bool AcceptsReturn
        {
            get => acceptsReturn;
            set => Set(ref acceptsReturn,value);
        }
        public override void SetDefault()
        {
            base.SetDefault();
            AcceptsReturn = false;
        }
    }
}
