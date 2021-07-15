﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Wpf.Annotations
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class TransferOriginAttribute : Attribute
    {
        public TransferOriginAttribute(Type origin)
        {
            Origin = origin;
        }

        public Type Origin { get; set; }
    }
}