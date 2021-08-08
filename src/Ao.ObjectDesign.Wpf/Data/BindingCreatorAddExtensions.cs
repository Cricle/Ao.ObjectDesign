using System;
using System.Windows;
using System.Windows.Data;

namespace Ao.ObjectDesign.Wpf.Data
{
    public static class BindingCreatorAddExtensions
    {
        public static IBindingCreator AddSetConfig(this IBindingCreator creator, BindingMode mode, UpdateSourceTrigger trigger)
        {
            if (creator is null)
            {
                throw new ArgumentNullException(nameof(creator));
            }

            return creator.Add(x =>
            {
                x.Mode = mode;
                x.UpdateSourceTrigger = trigger;
            });
        }
        public static IBindingCreator AddSetPath(this IBindingCreator creator, string path)
        {
            if (creator is null)
            {
                throw new ArgumentNullException(nameof(creator));
            }

            return creator.Add(x =>
            {
                x.Path = new PropertyPath(path);
            });
        }

    }

}
