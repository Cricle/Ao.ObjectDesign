using System;

namespace Ao.ObjectDesign.Designing
{
    //https://docs.microsoft.com/en-us/dotnet/api/system.windows.media.effects.effect?view=windowsdesktop-5.0
    [Flags]
    public enum EffectTypes
    {
        None = 0,
        BlurEffect = 1,
        DropShadowEffect = 2
    }
}
