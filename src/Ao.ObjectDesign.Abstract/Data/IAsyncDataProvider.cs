﻿using System.Threading.Tasks;

namespace Ao.ObjectDesign.Data
{
    public interface IAsyncDataProvider<T>
    {
        Task<T> GetAsDataAsync();
    }
    public interface IAsyncDataProvider
    {
        Task<object> GetDataAsync();
    }
}
