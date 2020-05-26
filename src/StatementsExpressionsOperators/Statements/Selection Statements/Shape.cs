using System;

namespace Statements.Selection_Statements
{
    public abstract class Shape
    {
        public abstract double Area { get; }
        public abstract double Circumference { get; }
    }

    public class Rectangle : Shape
    {
        public Rectangle(double length, double width)
        {
            Length = length;
            Width = width;
        }

        public double Length { get; set; }
        public double Width { get; set; }

        public override double Area
        {
            get { return Math.Round(Length * Width,2); }
        }

        public override double Circumference
        {
            get { return (Length + Width) * 2; }
        }
    }

    public class Square : Rectangle
    {
        public Square(double side) : base(side, side)
        {
            Side = side;
        }

        public double Side { get; set; }
    }

    public class Circle : Shape
    {
        public Circle(double radius)
        {
            Radius = radius;
        }

        public double Radius { get; set; }

        public override double Circumference
        {
            get { return 2 * Math.PI * Radius; }
        }

        public override double Area
        {
            get { return Math.PI * Math.Pow(Radius, 2); }
        }
    }
}