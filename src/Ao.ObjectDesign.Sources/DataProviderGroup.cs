using Ao.ObjectDesign.Data;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Sources
{
    public class DataProviderGroup : ObservableCollection<IDataAnyProvider<object>>, IDataAnyProvider<object>
    {
        public string Name { get; set; }

        public bool SupportRaiseDataChanged => true;

        public event EventHandler<DataProviderDataChangedEventArgs<object>> DataChanged;

        public DataProviderGroup()
        {
            CollectionChanged += OnCollectionChanged;
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (IDataProvider<object> item in e.NewItems)
                {
                    if (item.SupportRaiseDataChanged)
                    {
                        item.DataChanged += OnDataChanged;
                    }
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (IDataProvider<object> item in e.NewItems)
                {
                    item.DataChanged -= OnDataChanged;
                }
            }
        }

        private void OnDataChanged(object sender, DataProviderDataChangedEventArgs<object> e)
        {
            DataChanged?.Invoke(sender, e);
        }

        public object Get()
        {
            var dyn = new DynamicCombineObject();
            foreach (var item in this)
            {
                var val = item.Get();
                if (val != null)
                {
                    dyn.Combiner.Add(val);
                }
            }
            return dyn;
        }

        public async Task<object> GetAsync()
        {
            var dyn = new DynamicCombineObject();
            foreach (var item in this)
            {
                var val = await item.GetAsync();
                if (val!=null)
                {
                    dyn.Combiner.Add(val);
                }
            }
            return dyn;
        }

    }
}
