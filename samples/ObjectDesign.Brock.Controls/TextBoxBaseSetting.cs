using Ao.ObjectDesign.Designing.Annotations;
using System.Windows.Controls.Primitives;

namespace ObjectDesign.Brock.Controls
{
    [MappingFor(typeof(TextBoxBase))]
    public class TextBoxBaseSetting : ControlSetting
    {
        private bool acceptsReturn;

        public bool AcceptsReturn
        {
            get => acceptsReturn;
            set => Set(ref acceptsReturn, value);
        }
        public override void SetDefault()
        {
            base.SetDefault();
            AcceptsReturn = false;
        }
    }
}
