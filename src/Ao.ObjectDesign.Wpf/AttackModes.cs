using System;

namespace Ao.ObjectDesign.Wpf
{
    [Flags]
    public enum AttackModes
    {
        None = 0,
        PropertyVisitor = 1,
        NativeProperty = PropertyVisitor << 1,
        DeclareObject = PropertyVisitor << 2,
        VisitorAndDeclared = PropertyVisitor | DeclareObject,
        NativeAndDeclared = NativeProperty | DeclareObject,
        All = PropertyVisitor | NativeProperty | DeclareObject
    }
}
