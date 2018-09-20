using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Media;
using MoveArc.Annotations;
using MoveArc.Model;

namespace MoveArc.ViewModel
{
    public class MainViewModel:INotifyPropertyChanged
    {
        Random rnd = new Random();
        double _widthForm = 800;
        double _heightForm = 500;
        public ObservableCollection<Shape> ListShapes { get; set; } = new ObservableCollection<Shape>();
        public List<Task> Tasks { get; set; } = new List<Task>();
        public double WidthForm
        {
            get => _widthForm;
            set
            {
                if (value.Equals(_widthForm)) return;
                _widthForm = value;
                OnPropertyChanged();
            }
        }

        public double HeightForm
        {
            get => _heightForm;
            set
            {
                if (value.Equals(_heightForm)) return;
                _heightForm = value;
                OnPropertyChanged();
            }
        }
        
        public MainViewModel()
        {
            CreateRandomShape(typeof(Circle), 450, 80, 25);
            CreateRandomShape(typeof(Rectangle), 450, 80, 25);
            
            SetRandomDirection();
            SetRandomPosition();
            SetRandomColor();
            Tasks.Add(new Task(()=>MoveShapes()));
            foreach (var task in Tasks)
            {
                task.Start();
            }
        }

        public void CreateRandomShape(Type typeShape, int count, int maxRadius, int maxSpeed)
        {
            for (int i = 0; i < count; i++)
            {
                Shape shape = null;
                switch (typeShape)
                {
                    case Type _ when typeShape == typeof(Circle):
                        shape = new Circle(rnd.Next(10, maxRadius));
                        break;
                    case Type _ when typeShape == typeof(Rectangle):
                        shape = new Rectangle(rnd.Next(10, maxRadius), rnd.Next(10, maxRadius));
                        break;
                }
                shape.Speed = rnd.Next(1, maxSpeed);
                ListShapes.Add(shape);
               // Tasks.Add(new Task(() => MoveShape(shape)));
            }
        }
       
        public void SetRandomDirection()
        {
            foreach (var shape in ListShapes)
            {
                int index = rnd.Next(2) == 0 ? rnd.Next(3, 5) : rnd.Next(6, 8);
                shape.Direction = (Direction)Enum.GetValues(typeof(Direction)).GetValue(index);
            }
        }

        public void SetRandomColor()
        {
            foreach (var shape in ListShapes)
            {
                 shape.Color = new SolidColorBrush(Color.FromRgb((byte)rnd.Next(255), (byte)rnd.Next(255), (byte)rnd.Next(255))); ;
            }
        }
        public void SetRandomPosition()
        {
            foreach (var shape in ListShapes)
            {
                shape.X = rnd.Next((int)shape.Width, (int)WidthForm - (int)shape.Width);
                shape.Y = rnd.Next((int)shape.Height, (int)HeightForm - (int)shape.Height);
            }
        }

        async void MoveShapes()
        {
            while (true)
            {
                foreach (Shape shape in ListShapes)
                {
                    MoveShape(shape);
                }
                await Task.Delay(1);
            }
        }
        void MoveShape(Shape shape)
        {
            CheckCollision(shape);

            if ((shape.Direction & Direction.Down) != 0)
                shape.Y += shape.Speed;
            if ((shape.Direction & Direction.Up) != 0)
                shape.Y -= shape.Speed;
            if ((shape.Direction & Direction.Left) != 0)
                shape.X -= shape.Speed;
            if ((shape.Direction & Direction.Right) != 0)
                shape.X += shape.Speed;
        }

        void CheckCollision(Shape shape)
        {
            if (shape.X + shape.Speed + shape.Width * 1.15 >= WidthForm)
            {
                shape.Direction &= ~Direction.Right;
                shape.Direction |= Direction.Left;
            }
            if (shape.X - shape.Speed <= 0)
            {
                shape.Direction &= ~Direction.Left;
                shape.Direction |= Direction.Right;
            }
            if (shape.Y + shape.Speed + shape.Height * 1.35 >= HeightForm)
            {
                shape.Direction &= ~Direction.Down;
                shape.Direction |= Direction.Up;
            }
            if (shape.Y - shape.Speed <= 0)
            {
                shape.Direction &= ~Direction.Up;
                shape.Direction |= Direction.Down;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
