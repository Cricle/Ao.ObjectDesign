using Ao.ObjectDesign.Sources.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Ao.ObjectDesign.Sources.Types
{
    public class SourceInfo
    {
        public SourceInfo(IReadOnlyList<SourceColumn> primaryKeys, IReadOnlyList<SourceColumn> columns)
        {
            PrimaryKeys = primaryKeys ?? throw new ArgumentNullException(nameof(primaryKeys));
            Columns = columns ?? throw new ArgumentNullException(nameof(columns));
        }

        public IReadOnlyList<SourceColumn> PrimaryKeys { get; }

        public IReadOnlyList<SourceColumn> Columns { get; }
    }
}
