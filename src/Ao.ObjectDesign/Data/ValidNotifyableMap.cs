using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Ao.ObjectDesign.Data
{
    [DebuggerDisplay("{" + nameof(ToString) + "(),nq}")]
    public class ValidNotifyableMap<TKey, TValue> : NotifyableMap<TKey, TValue>
    {
        public ValidNotifyableMap()
        {
            globalRules = new Lazy<IList<INotifyableSetValidater<TKey, TValue>>>(CreateGlobalRuleList);
            withKeyRules = new Lazy<IDictionary<TKey, IList<INotifyableSetValidater<TKey, TValue>>>>(CreateWithKeyRuleMap);
        }

        public ValidNotifyableMap(int concurrencyLevel, int capacity)
            : base(concurrencyLevel, capacity)
        {
            globalRules = new Lazy<IList<INotifyableSetValidater<TKey, TValue>>>(CreateGlobalRuleList);
            withKeyRules = new Lazy<IDictionary<TKey, IList<INotifyableSetValidater<TKey, TValue>>>>(CreateWithKeyRuleMap);
        }

        public ValidNotifyableMap(IDictionary<TKey, TValue> map)
            : base(map)
        {
            globalRules = new Lazy<IList<INotifyableSetValidater<TKey, TValue>>>(CreateGlobalRuleList);
            withKeyRules = new Lazy<IDictionary<TKey, IList<INotifyableSetValidater<TKey, TValue>>>>(CreateWithKeyRuleMap);

        }

        public ValidNotifyableMap(IEqualityComparer<TKey> comparer)
            : base(comparer)
        {
            globalRules = new Lazy<IList<INotifyableSetValidater<TKey, TValue>>>(CreateGlobalRuleList);
            withKeyRules = new Lazy<IDictionary<TKey, IList<INotifyableSetValidater<TKey, TValue>>>>(CreateWithKeyRuleMap);
        }

        private readonly Lazy<IList<INotifyableSetValidater<TKey, TValue>>> globalRules;
        private readonly Lazy<IDictionary<TKey, IList<INotifyableSetValidater<TKey, TValue>>>> withKeyRules;

        public IList<INotifyableSetValidater<TKey, TValue>> GlobalRules => globalRules.Value;

        public IDictionary<TKey, IList<INotifyableSetValidater<TKey, TValue>>> WithKeyRules => withKeyRules.Value;

        public bool IsGlobalRulesCreated => globalRules.IsValueCreated;

        public bool IsWithKeyRulesCreated => withKeyRules.IsValueCreated;

        protected virtual IList<INotifyableSetValidater<TKey, TValue>> CreateGlobalRuleList()
        {
            return new List<INotifyableSetValidater<TKey, TValue>>();
        }
        protected virtual IDictionary<TKey, IList<INotifyableSetValidater<TKey, TValue>>> CreateWithKeyRuleMap()
        {
            return new Dictionary<TKey, IList<INotifyableSetValidater<TKey, TValue>>>();
        }
        protected virtual IList<INotifyableSetValidater<TKey, TValue>> CreateWithKeyValidateList()
        {
            return new List<INotifyableSetValidater<TKey, TValue>>();
        }

        public void RegistGlobalRule(INotifyableSetValidater<TKey, TValue> validater)
        {
            if (validater is null)
            {
                throw new ArgumentNullException(nameof(validater));
            }

            GlobalRules.Add(validater);
        }
        public bool UnRegistGlobalRule(INotifyableSetValidater<TKey, TValue> validater)
        {
            if (validater is null)
            {
                throw new ArgumentNullException(nameof(validater));
            }

            if (IsGlobalRulesCreated)
            {
                return GlobalRules.Remove(validater);
            }
            return false;
        }
        public bool IsRegistGlobal(INotifyableSetValidater<TKey, TValue> validater)
        {
            if (validater is null)
            {
                throw new ArgumentNullException(nameof(validater));
            }

            if (IsGlobalRulesCreated)
            {
                return GlobalRules.Contains(validater);
            }
            return false;
        }

        public void RegistWithKeyRule(TKey key, INotifyableSetValidater<TKey, TValue> validater)
        {
            if (!WithKeyRules.TryGetValue(key, out var lst))
            {
                lst = CreateWithKeyValidateList();
                WithKeyRules.Add(key, lst);
            }

            if (validater is null)
            {
                throw new ArgumentNullException(nameof(validater));
            }

            lst.Add(validater);
        }
        public bool UnRegistWithKeyRule(TKey key, INotifyableSetValidater<TKey, TValue> validater)
        {
            if (validater is null)
            {
                throw new ArgumentNullException(nameof(validater));
            }

            if (IsWithKeyRulesCreated&&
                WithKeyRules.TryGetValue(key,out var lst))
            {
                return lst.Remove(validater);
            }

            return false;
        }
        public bool IsRegistdWithKeyRule(TKey key, INotifyableSetValidater<TKey, TValue> validater)
        {
            if (IsWithKeyRulesCreated &&
                WithKeyRules.TryGetValue(key, out var lst))
            {
                return lst.Contains(validater);
            }

            if (validater is null)
            {
                throw new ArgumentNullException(nameof(validater));
            }

            return false;
        }

        public bool ValidateData(TKey key, TValue value, out INotifyableSetValidater<TKey, TValue> failValidater)
        {
            var mode = ChangeModes.Change;
            if (!TryGetValue(key, out var old))
            {
                mode = ChangeModes.New;
            }
            var e = new DataChangedEventArgs<TKey, TValue>(key, old, value, mode);
            return ValidateData(e, out failValidater);
        }
        public bool ValidateData(DataChangedEventArgs<TKey, TValue> e, out INotifyableSetValidater<TKey, TValue> failValidater)
        {
            failValidater = null;
            var ctx = new NotifyableSetValidaterContext();
            if (globalRules.IsValueCreated)
            {
                var global = globalRules.Value;
                var len = global.Count;
                for (int i = 0; i < len; i++)
                {
                    var item = global[i];
                    if (ctx.IsStopValidate || ctx.IsSkipGlobalValidate)
                    {
                        return true;
                    }
                    if (!item.Validate(e, ref ctx))
                    {
                        failValidater = item;
                        return false;
                    }
                }
            }
            if (ctx.IsStopValidate || ctx.IsSkipWithKeyValidate)
            {
                return true;
            }
            if (withKeyRules.IsValueCreated && WithKeyRules.TryGetValue(e.Key, out var rules))
            {
                var len = rules.Count;
                for (int i = 0; i < len; i++)
                {
                    var item = rules[i];

                    if (!item.Validate(e, ref ctx))
                    {
                        failValidater = item;
                        return false;
                    }
                }
            }
            return true;
        }

        protected override void OnWritingData(DataChangedEventArgs<TKey, TValue> e)
        {
            var succeed = ValidateData(e, out var failtValidater);
            if (!succeed)
            {
                HandleValidateFail(failtValidater, e);
            }
        }
        protected virtual void HandleValidateFail(INotifyableSetValidater<TKey, TValue> validater, DataChangedEventArgs<TKey, TValue> e)
        {
            throw new InvalidOperationException($"Validater {validater} validate not accept, key {e.Key} from {e.Old} write to {e.New}");
        }
    }
}
