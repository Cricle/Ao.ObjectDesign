using System;

namespace Ao.ObjectDesign
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
