using Ao.ObjectDesign.Wpf.Annotations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ao.ObjectDesign.Wpf.Designing
{
    [DesignFor(typeof(Cursor))]
    public class CursorDesigner : NotifyableObject
    {
        public static IReadOnlyDictionary<string, Cursor> cursorMap;
        public static IReadOnlyDictionary<Cursor,string> cursorRevMap;

        static CursorDesigner()
        {
            var cursorType = typeof(Cursors).GetProperties(BindingFlags.Static | BindingFlags.Public);
            cursorMap = cursorType.ToDictionary(x => x.Name, x => (Cursor)x.GetValue(null));
            cursorRevMap = cursorMap.ToDictionary(x => x.Value, x => x.Key);
        }

        private string name;
        private CursorTypes cursorType;
        private Stream cursorStream;
        private string cursorFile;

        public virtual Stream CursorStream
        {
            get => cursorStream;
            set
            {
                Set(ref cursorStream, value);
                RaiseCursorChanged();
            }
        }

        public virtual string CursorFile
        {
            get => cursorFile;
            set
            {
                Set(ref cursorFile, value);
                RaiseCursorChanged();
            }
        }

        public CursorTypes CursorType
        {
            get => cursorType;
            set
            {
                Set(ref cursorType, value);
                RaiseCursorChanged();
            }
        }

        public virtual string Name
        {
            get => name;
            set
            {
                Set(ref name, value);
                RaiseCursorChanged();
            }
        }
        private Cursor cursor;
        [PlatformTargetProperty]
        public virtual Cursor Cursor
        {
            get
            {
                cursor?.Dispose();
                if (CursorType == CursorTypes.None)
                {
                    return null;
                }
                else if (CursorType == CursorTypes.SystemCursorName)
                {
                    if (!string.IsNullOrEmpty(name) && cursorMap.TryGetValue(name, out var cur))
                    {
                        return cur;
                    }
                    return null;
                }
                else if (CursorType == CursorTypes.FilePath)
                {
                    if (string.IsNullOrEmpty(CursorFile))
                    {
                        return null;
                    }
                    return cursor = new Cursor(CursorFile);
                }
                else if (CursorType == CursorTypes.Stream)
                {
                    if (CursorStream is null)
                    {
                        return null;
                    }
                    return cursor = new Cursor(CursorStream);
                }
                return null;
            }
            set
            {
                if (value is null)
                {
                    CursorType = CursorTypes.None;
                }
                else if (cursorRevMap.TryGetValue(value,out var v))
                {
                    Name = v;
                    CursorType = CursorTypes.SystemCursorName;
                }
                else
                {
                    throw new NotSupportedException("Not support not set null or system cursor!");
                }
            }
        }

        protected void RaiseCursorChanged()
        {
            RaisePropertyChanged(nameof(Cursor));
        }
    }
}
