using GalaSoft.MvvmLight;

namespace Ao.ObjectDesign.Test.Data
{
    class Student : ViewModelBase
    {
        private string name;
        public string Name
        {
            get => name;
            set => Set(ref name, value);
        }
    }
}
