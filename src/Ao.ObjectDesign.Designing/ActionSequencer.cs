namespace Ao.ObjectDesign.Designing.Desiging
{
    public class ActionSequencer : Sequencer<IFallbackable>
    {
        protected override IFallbackable CreateFallback(object sender, PropertyChangeToEventArgs e)
        {
            return new CompiledModifyDetail(sender, e.PropertyName, e.From, e.To);
        }
    }
}
