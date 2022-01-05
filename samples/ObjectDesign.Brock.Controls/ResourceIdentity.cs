using Ao.ObjectDesign.Designing;
using System;
using System.IO;
using System.IO.Abstractions;

namespace ObjectDesign.Brock.Controls
{
    public class ResourceIdentity : NotifyableObject, ICloneable
    {
        private string resourceGroupName;
        private string resourceName;
        private string uri;
        private ResourceTypes type;

        public string Uri
        {
            get => uri;
            set => Set(ref uri, value);
        }

        public ResourceTypes Type
        {
            get => type;
            set=> Set(ref type, value);
        }

        public string ResourceName
        {
            get => resourceName;
            set => Set(ref resourceName, value);
        }

        public string ResourceGroupName
        {
            get => resourceGroupName;
            set => Set(ref resourceGroupName, value);
        }
        public ResourceIdentity Clone()
        {
            return new ResourceIdentity
            {
                resourceName = resourceName,
                resourceGroupName = resourceGroupName,
                uri=uri,
                type = type
            };
        }
        object ICloneable.Clone()
        {
            return Clone();
        }

        public override bool Equals(object obj)
        {
            if (obj is ResourceIdentity identity)
            {
                if (identity.type== ResourceTypes.Path)
                {
                    return identity.resourceName == resourceName &&
                        identity.resourceGroupName == resourceGroupName &&
                        identity.type == type;
                }
                else if (identity.type== ResourceTypes.Uri)
                {
                    return identity.type == type &&
                           identity.uri == uri;
                }
                else
                {
                    return identity.type == type;
                }
            }
            return false;
        }
        public override int GetHashCode()
        {
            unchecked
            {
                if (type == ResourceTypes.Path)
                {
                    var s = 0;
                    if (resourceGroupName != null)
                    {
                        s = resourceGroupName.GetHashCode();
                    }
                    var h = 31 * 7 + s;
                    if (resourceName != null)
                    {
                        s = resourceName.GetHashCode();
                    }
                    h = h * 31 + s;
                    h = h * 31 + (int)type;
                    return h;
                }
                else if (type== ResourceTypes.Uri)
                {
                    var s = 0;
                    if (uri != null)
                    {
                        s = uri.GetHashCode();
                    }
                    var h = 31 * 7 + s;
                    h = h * 7 + (int)type;
                    return h;
                }
                return 0;
            }
        }
        public override string ToString()
        {
            if (Type== ResourceTypes.Path)
            {
                return $"{{Type:{type}, Group: {resourceGroupName}, Name: {resourceName}}}";
            }
            else if (type== ResourceTypes.Uri)
            {
                return $"{{Type:{type}, Uri: {uri}}}";
            }
            else
            {
                return "{{Type: Unknow}}";
            }
        }
        public string GetPath(IFileSystem fs,IDirectoryInfo dir)
        {
            if (string.IsNullOrEmpty(ResourceName))
            {
                return null;
            }
            var path = ResourceName;
            if (!string.IsNullOrEmpty(ResourceGroupName))
            {
                path = Path.Combine(ResourceGroupName, ResourceName);
            }
            if (dir != null)
            {
                path = Path.Combine(dir.FullName, path);
            }
            if (dir != null)
            {
                if (!dir.FileSystem.File.Exists(path))
                {
                    return null;
                }
            }
            else if (!fs.File.Exists(path))
            {
                return null;
            }
            return path;
        }
    }
}
