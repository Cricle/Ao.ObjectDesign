using System.ComponentModel;

namespace Ao.ObjectDesign.Test
{
    class Student
    {
        private string str;
        public string this[string str]
        {
            get => str;
            set { this.str = value; }
        }
        public string Name { get; set; }

        public int Age { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public int Address { get; set; }
    }
}
