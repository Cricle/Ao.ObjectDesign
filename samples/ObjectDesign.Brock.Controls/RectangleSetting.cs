namespace ObjectDesign.Brock.Controls
{


    public class RectangleSetting : ShapeSetting
    {
        private double radiusX;
        private double radiusY;

        public double RadiusX
        {
            get => radiusX;
            set
            {
                Set(ref radiusX, value);
            }
        }
        public double RadiusY
        {
            get => radiusY;
            set
            {
                Set(ref radiusY, value);
            }
        }
        public override void SetDefault()
        {
            base.SetDefault();
            RadiusX = 0;
            RadiusY = 0;
        }
    }
}
