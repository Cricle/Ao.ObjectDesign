﻿using Ao.ObjectDesign.Wpf.Conditions;
using System.Windows;

namespace ObjectDesign.Brock.Views
{
    public class TextWrappingCondition : TemplateCondition<TextWrapping>
    {
        protected override string GetResourceKey()
        {
            return "ObjectDesign.TextWrapping";
        }
    }
}
