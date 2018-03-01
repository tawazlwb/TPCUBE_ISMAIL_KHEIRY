using ikheiry.FileWriter;
using System;
using System.Collections.Generic;
using System.Linq;
using static ikheiry.FileWriter.Bitmap;

namespace ikheiry.Entities
{
#pragma warning disable CS0660 // Le type définit l'opérateur == ou l'opérateur != mais ne se substitue pas à Object.Equals(object o)
#pragma warning disable CS0661 // Le type définit l'opérateur == ou l'opérateur != mais ne se substitue pas à Object.GetHashCode()
    class Point
#pragma warning restore CS0661 // Le type définit l'opérateur == ou l'opérateur != mais ne se substitue pas à Object.GetHashCode()
#pragma warning restore CS0660 // Le type définit l'opérateur == ou l'opérateur != mais ne se substitue pas à Object.Equals(object o)
    {
        protected int _x;
        protected int _y;
        protected int _z;

        public int X { get => _x; set => _x = value; }
        public int Y { get => _y; set => _y = value; }
        public int Z { get => _z; set => _z = value; }

        public Point()
        {
            X = 0;
            Y = 0;
            Z = 0;
        }
        public Point(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public Point(Point p)
        {
            Point point = new Point(p.X, p.Y, p.Z);
            X = point.X;
            Y = point.Y;
            Z = point.Z;
        }

        /*
        public static Point operator +(Point p1, Point p2)
        {
            return new Point(p1.X + p2.X, p1.Y + p2.Y, p1.Z + p2.Z);
        }*/
        public static Point operator +(Point p1, Point p2) => new Point(p1.X + p2.X, p1.Y + p2.Y, p1.Z + p2.Z);
        public static bool operator ==(Point p1, Point p2)
        {
            if( p1.X == p2.X && p1.Y == p2.Y && p1.Z == p2.Z)
            {
                return true;
            }
            return false;
        }
        public static bool operator !=(Point p1, Point p2)
        {
            if (p1.X != p2.X || p1.Y != p2.Y || p1.Z != p2.Z)
            {
                return true;
            }
            return false;
        }
        // Translation
        public Point TranslationX(int x) => new Point(X + x, Y, Z);
        public Point TranslationY(int y) => new Point(X, Y + y, Z);
        public Point TranslationZ(int z) => new Point(X, Y, Z + z);
        public Point Translation(int x, int y, int z)
        {
            Point p = TranslationX(x);
            p = p.TranslationY(y);
            return p.TranslationZ(z);
        }
        // Rotation
        public Point RotationX(double tetaX)
        {
            double teta = Math.PI * tetaX / 180;
            return new Point(X, (int)(Math.Cos(teta) * Y) - (int)(Math.Sin(teta) * Z), (int)(Math.Sin(teta) * Y) + (int)(Math.Cos(teta) * Z));
        }
        public Point RotationY(double tetaY)
        {
            double teta = Math.PI * tetaY / 180;
            return new Point((int)(Math.Cos(teta) * X) + (int)(Math.Sin(teta) * Z), Y, (int)(-1 * Math.Sin(teta) * X) + (int)(Math.Cos(teta) * Z));
        }
        public Point RotationZ(double tetaZ)
        {
            double teta = Math.PI * tetaZ / 180.0;
            return new Point((int)(Math.Cos(teta) * X) - (int)(Math.Sin(teta) * Y), (int)(Math.Sin(teta) * X) + (int)(Math.Cos(teta) * Y), Z);
        }
        public Point Rotation(double tetaX, double tetaY, double tetaZ)
        {
            Point p = RotationX(tetaX);
            p = p.RotationY(tetaY);
            return p.RotationZ(tetaZ);
        }
        // homothetie
        public virtual Point Homothetie(Point p, double rapport) => new Point((int) (rapport * X + (1- rapport) * p.X), (int)(rapport * Y + (1 - rapport) * p.Y), (int)(rapport * Z + (1 - rapport) * p.Z));
        // les 4 connexités
        public Point ConnexiteXMoins1_Y() => new Point(X - 1, Y, Z);
        public Point ConnexiteXPlus1_Y() => new Point(X + 1, Y, Z);
        public Point ConnexiteX_YMoins1() => new Point(X, Y - 1, Z);
        public Point ConnexiteX_YPlus1() => new Point(X, Y + 1, Z);
        public List<Point> Connexite()
        {
            List<Point> points = new List<Point>
            {
                ConnexiteXMoins1_Y(),
                ConnexiteXPlus1_Y(),
                ConnexiteX_YMoins1(),
                ConnexiteX_YPlus1()
            };
            return points;
        }
        
        // Recuperer la couleur
        public Color GetPointColor(Bitmap img)
        {
            return img.GetPixel(X + img.Width / 2, img.Height / 2 - Y);
        }

        // verifier la couleur
        public bool ComparerCouleur(Point p, Bitmap img)
        {
            Color c1 = GetPointColor(img);
            Color c2 = p.GetPointColor(img);
            if (c1.Red == c2.Red && c1.Green == c2.Green && c1.Blue == c2.Blue)
                return true;
            return false;
        }
        public bool ComparerCouleur(Color color, Bitmap img)
        {
            Color c = GetPointColor(img);
            if (c.Red == color.Red && c.Green == color.Green && c.Blue == color.Blue)
                return true;
            return false;
        }

        // Verifier si le Point appartient à la liste 
        public bool IsThere3D(List<Point> points)
        {
            for(int i=0; i<points.Count; ++i)
            {
                if( X == points.ElementAt(i).X && Y == points.ElementAt(i).Y && Z == points.ElementAt(i).Z)
                {
                    return true;
                }
            }
            return false;
        }
        public bool IsThere2DXY(List<Point> points)
        {
            for (int i = 0; i < points.Count; ++i)
            {
                if (X == points.ElementAt(i).X && Y == points.ElementAt(i).Y)
                {
                    return true;
                }
            }
            return false;
        }

        // Afficher
        public virtual void Afficher(Bitmap img, Color pen)
        {
            img.SetPixel(X + img.Width / 2, img.Height / 2 - Y, pen);
        }

    }
}
