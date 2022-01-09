﻿using System;
using System.IO;

namespace Ao.ObjectDesign.Abstract.Store
{
    public interface IDesignStreamInterop
    {
        void SerializeToStream(object val, Type type, Stream stream);

        object DeserializeByStream(Stream stream, Type type);
    }
}
