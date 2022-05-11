using System;
using System.Collections.Generic;
using System.Text;

namespace Ao.ObjectDesign.Data
{
    public interface IDataProvider<T>
    {
        T GetAsData();
    }
    public interface IDataProvider
    {
        object GetData();
    }
}
