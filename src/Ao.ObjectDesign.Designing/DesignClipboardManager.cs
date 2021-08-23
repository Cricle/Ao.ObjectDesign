using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Designing
{
    public abstract class DesignClipboardManager<TDesignObject>
    {
        private IReadOnlyList<TDesignObject> copiedObjects;
        private IReadOnlyList<TDesignObject> originObjects;

        public IReadOnlyList<TDesignObject> OriginObjects => originObjects;

        public IReadOnlyList<TDesignObject> CopiedObjects => copiedObjects;

        public event EventHandler<DesignClipboardCopiedResetEventArgs<TDesignObject>> CopiedObjectChanged;

        public void SetCopiedObject(IReadOnlyList<TDesignObject> objects, bool copy)
        {
            var oldCopiedObject = copiedObjects;
            var oldOriginObject = originObjects;
            originObjects = objects;
            if (objects is null)
            {
                copiedObjects = null;
            }
            else if (copy)
            {
                copiedObjects = objects.Select(x => Clone(x)).ToList();
            }
            else
            {
                copiedObjects = objects;
            }
            SetToClipboard(objects);
            var e = new DesignClipboardCopiedResetEventArgs<TDesignObject>(oldCopiedObject,
                oldOriginObject,
                copiedObjects,
                objects);
            CopiedObjectChanged?.Invoke(this, e);
            OnCopiedObjectChanged(e);
        }

        public void UpdateFromClipboard(bool canNull, bool copy)
        {
            var val = GetFromClipboard();
            if (val != null || canNull)
            {
                SetCopiedObject(val, copy);
            }
        }

        protected abstract void SetToClipboard(IReadOnlyList<TDesignObject> @object);

        protected abstract IReadOnlyList<TDesignObject> GetFromClipboard();

        public virtual TDesignObject Clone(TDesignObject @object)
        {
            if (@object == null)
            {
                return default;
            }
            return (TDesignObject)ReflectionHelper.Clone(@object.GetType(), @object);
        }

        protected virtual void OnCopiedObjectChanged(DesignClipboardCopiedResetEventArgs<TDesignObject> e)
        {

        }
    }
}
