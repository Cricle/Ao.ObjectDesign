using Ao.ObjectDesign.Controls;
using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Wpf.Xaml;
using System.Collections.Generic;
using System.Windows;

namespace ObjectDesign.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            var lst = new List<NotifyableObject>();
            var btn=new ButtonSetting();
            btn.SetDefault();
            lst.Add(btn);
            var tbx=new TextBlockSetting();
            tbx.SetDefault();
            lst.Add(tbx);

            //var str=JsonConvert.SerializeObject(lst,new JsonSerializerSettings { TypeNameHandling= TypeNameHandling.Auto});
            //var obj= JsonConvert.DeserializeObject<List<UIElementSetting>>(str);

            //var str = DesignMessagePackHelper.Serialize(typeof(List<NotifyableObject>), lst);
            //var obj = DesignMessagePackHelper.Deserialize(typeof(List<NotifyableObject>), str);

            var str = DeisgnXamlSerializer.Serialize(lst);
            var obj = DeisgnXamlSerializer.Deserialize(str);

            //var str = DeisgnYamlSerializer.Serialize(lst);
            //var obj = DeisgnYamlSerializer.Deserializer(str,typeof(List<NotifyableObject>));
        }
    }
}
