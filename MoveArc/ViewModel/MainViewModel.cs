using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
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
            Circle circle1 = new Circle(100) {Speed = 5};
            Circle circle2 = new Circle(50) { Speed = 8 };
            Circle circle3 = new Circle(25) { Speed = 11 };
            Circle circle4 = new Circle(40) { Speed = 3 };
            ListShapes.Add(circle1);
            ListShapes.Add(circle2);
            ListShapes.Add(circle3);
            ListShapes.Add(circle4);
            Tasks.Add(new Task(()=> MoveShape(circle1)));
            Tasks.Add(new Task(()=> MoveShape(circle2)));
            Tasks.Add(new Task(()=> MoveShape(circle3)));
            Tasks.Add(new Task(()=> MoveShape(circle4)));

            CreateRandomCircle(35, 200, 20);
            SetRandomDirection();
            SetRandomPosition();

            foreach (var task in Tasks)
            {
                task.Start();
            }
        }

        public void CreateRandomCircle(int count, int maxRadius, int maxSpeed)
        {
            for (int i = 0; i < count; i++)
            {
                Circle circle = new Circle(rnd.Next(10, maxRadius))
                {
                    Speed = rnd.Next(1, maxSpeed)
                };
                ListShapes.Add(circle);
                Tasks.Add(new Task(() => MoveShape(circle)));

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
        public void SetRandomPosition()
        {
            foreach (var shape in ListShapes)
            {
                shape.X = rnd.Next((int)shape.Width, (int)WidthForm - (int)shape.Width);
                shape.Y = rnd.Next((int)shape.Height, (int)HeightForm - (int)shape.Height);
            }
        }
        async void MoveShape(Shape shape)
        {
            while (true)
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
                await Task.Delay(1);
            }
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
