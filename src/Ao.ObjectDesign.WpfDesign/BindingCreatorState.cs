﻿using System;
using System.Collections;

namespace Ao.ObjectDesign.WpfDesign
{
    public class BindingCreatorState : IBindingCreatorState
    {
        public IServiceProvider Provider { get; set; }

        public IDictionary Features { get; set; }

        public DesignRuntimeTypes RuntimeType { get; set; }

        public object GetService(Type serviceType)
        {
            return Provider?.GetService(serviceType);
        }
    }
}
