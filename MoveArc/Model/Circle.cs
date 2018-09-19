namespace MoveArc.Model
{
    public class Circle: Shape
    {
        private int _radius;

        public int Radius   
        {
            get => _radius;
            set
            {
                if (value == _radius) return;
                _radius = value;
                OnPropertyChanged();
            }
        }

        public Circle(int radius)
        {
            Radius = radius;
            Width = Height = radius;
        }
    }
}
