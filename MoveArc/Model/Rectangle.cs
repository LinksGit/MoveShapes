namespace MoveArc.Model
{
    public class Rectangle: Shape
    {
        public override string TypeName => "Rectangle";
        public Rectangle(double width, double height)
        {
            Width = width;
            Height = height;
        }
    }
}
