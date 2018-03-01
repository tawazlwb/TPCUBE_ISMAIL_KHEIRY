using ikheiry.FileWriter;
using System;
using System.Collections.Generic;
using static ikheiry.FileWriter.Bitmap;

namespace ikheiry.Entities
{
    class Cercle : Point
    {
        protected int _rayon;
        public int Rayon { get => _rayon; set => _rayon = value; }

        public Cercle() : base()
        {
            Rayon = 0;
        }

        public Cercle(int x, int y, int z) : base(x, y, z)
        {
            Rayon = 0;
        }

        public Cercle(int x, int y, int z, int rayon) : base(x, y, z)
        {
            Rayon = rayon;
        }

        public Cercle(Cercle c) : base(c)
        {
            Rayon = c.Rayon;
        }

        public override Point Homothetie(Point p, double rapport)
        {
            Cercle cercle = new Cercle((int)(rapport * X + (1 - rapport) * p.X), (int)(rapport * Y + (1 - rapport) * p.Y), (int)(rapport * Z + (1 - rapport) * p.Z))
            {
                Rayon = (int)(rapport * ((Cercle)(p)).Rayon)
            };
            return cercle;
        }
        // Détecte les points appartenant au cercle (contour)
         public List<Point> MyBorder()
        {
            int marge = Rayon / 2;
            List<Point> border = new List<Point>();
            for(int j=Y-Rayon-marge; j<Y+Rayon+marge; ++j)
            {
                for(int i=X-Rayon-marge; i<Y+Rayon+marge; ++i)
                {
                    if ( (int) (Math.Sqrt(Math.Pow(i-X,2)+ Math.Pow(j-Y, 2))) == Rayon || (int)(Math.Sqrt(Math.Pow(i - X, 2) + Math.Pow(j - Y, 2))) == Rayon)
                    {
                        border.Add(new Point(i, j, 0));
                    }
                }
            }
            return border;
        }

        // Afficher
        public override void Afficher(Bitmap img, Color pen)
        {
            List<Point> points = MyBorder();
            foreach(Point p in points)
            {
                p.Afficher(img, pen);
            }
        }
    }
}
