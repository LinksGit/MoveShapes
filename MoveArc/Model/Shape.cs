using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using MoveArc.Annotations;

namespace MoveArc.Model
{
    public class Shape: INotifyPropertyChanged
    {
        private double _x;
        private double _y;
        private Direction _direction;
        private Thickness _location;
        private double _width;
        private double _height;
        private double _speed;

        public virtual string TypeName { get; }
        public double Speed
        {
            get => _speed;
            set
            {
                if (value == _speed) return;
                _speed = value;
                OnPropertyChanged();
            }
        }

        public double X
        {
            get => _x;
            set
            {
                if (value.Equals(_x)) return;
                _x = value;
                SetCurrentLocation();
                OnPropertyChanged();
            }
        }

        public double Y
        {
            get => _y;
            set
            {
                if (value.Equals(_y)) return;
                _y = value;
                SetCurrentLocation();
                OnPropertyChanged();
            }
        }

        public double Width
        {
            get => _width;
            set
            {
                if (value == _width) return;
                _width = value;
                SetCurrentLocation();
                OnPropertyChanged();
            }
        }

        public double Height
        {
            get => _height;
            set
            {
                if (value == _height) return;
                _height = value;
                SetCurrentLocation();
                OnPropertyChanged();
            }
        }

        public Direction Direction
        {
            get => _direction;
            set
            {
                if (value == _direction) return;
                _direction = value;
                OnPropertyChanged();
            }
        }

        public Thickness Location
        {
            get => _location;
            set
            {
                if (value.Equals(_location)) return;
                _location = value;
                OnPropertyChanged();
            }
        }

        void SetCurrentLocation()
        {
            Location = new Thickness(X, Y, X + Width, Y + Height);
        }
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
