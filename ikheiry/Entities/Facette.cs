using ikheiry.FileWriter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ikheiry.FileWriter.Bitmap;

namespace ikheiry.Entities
{
    class Facette
    {
        protected Point _p1;
        protected Point _p2;
        protected Point _p3;
        protected Point _p4;

        protected Segment _seg1;
        protected Segment _seg2;
        protected Segment _seg3;
        protected Segment _seg4;

        public Point P1 { get => _p1; set => _p1 = value; }
        public Point P2 { get => _p2; set => _p2 = value; }
        public Point P3 { get => _p3; set => _p3 = value; }
        public Point P4 { get => _p4; set => _p4 = value; }

        public Segment Seg1 { get => _seg1; set => _seg1 = value; }
        public Segment Seg2 { get => _seg2; set => _seg2 = value; }
        public Segment Seg3 { get => _seg3; set => _seg3 = value; }
        public Segment Seg4 { get => _seg4; set => _seg4 = value; }

        public Facette()
        {
            P1 = new Point();
            P2 = new Point();
            P3 = new Point();
            P4 = new Point();

            Seg1 = new Segment();
            Seg2 = new Segment();
            Seg3 = new Segment();
            Seg4 = new Segment();
        }

        // A revoir pour la généralisation
        public Facette(Point p1, Point p2, Point p3, Point p4)
        {
            P1 = new Point(p1);
            P2 = new Point(p2);
            P3 = new Point(p3);
            P4 = new Point(p4);

            Seg1 = new Segment(p1, p2);
            Seg2 = new Segment(p2, p3);
            Seg3 = new Segment(p3, p4);
            Seg4 = new Segment(p4, p1);
        }

        // A revoir pour la généralisation
        public Facette(Segment seg1, Segment seg2, Segment seg3, Segment seg4)
        {
            Seg1 = new Segment(seg1);
            Seg2 = new Segment(seg2);
            Seg3 = new Segment(seg3);
            Seg4 = new Segment(seg4);

            P1 = new Point(seg1.P1);
            P2 = new Point(seg2.P1);
            P3 = new Point(seg3.P1);
            P4 = new Point(seg4.P1);
        }

        // A revoir pour la généralisation
        public Facette(Facette facette)
        {
            Seg1 = new Segment(facette.Seg1);
            Seg2 = new Segment(facette.Seg2);
            Seg3 = new Segment(facette.Seg3);
            Seg4 = new Segment(facette.Seg4);

            P1 = new Point(facette.P1);
            P2 = new Point(facette.P2);
            P3 = new Point(facette.P3);
            P4 = new Point(facette.P4);
        }

        // Translation Facette
        public Facette Translation(int x, int y, int z)
        {
            Segment S1 = Seg1.Translation(x, y, z);
            Segment S2 = Seg2.Translation(x, y, z);
            Segment S3 = Seg3.Translation(x, y, z);
            Segment S4 = Seg4.Translation(x, y, z);
            return new Facette(S1, S2, S3, S4);
        }

        // Rotation Facette
        public Facette RotationX(double tetaX)
        {
            Segment S1 = Seg1.RotationX(tetaX);
            Segment S2 = Seg2.RotationX(tetaX);
            Segment S3 = Seg3.RotationX(tetaX);
            Segment S4 = Seg4.RotationX(tetaX);
            return new Facette(S1, S2, S3, S4);
        }
        public Facette RotationY(double tetaY)
        {
            Segment S1 = Seg1.RotationY(tetaY);
            Segment S2 = Seg2.RotationY(tetaY);
            Segment S3 = Seg3.RotationY(tetaY);
            Segment S4 = Seg4.RotationY(tetaY);
            return new Facette(S1, S2, S3, S4);
        }
        public Facette RotationZ(double tetaZ)
        {
            Segment S1 = Seg1.RotationZ(tetaZ);
            Segment S2 = Seg2.RotationZ(tetaZ);
            Segment S3 = Seg3.RotationZ(tetaZ);
            Segment S4 = Seg4.RotationZ(tetaZ);
            return new Facette(S1, S2, S3, S4);
        }
        public Facette Rotation(double tetaX, double tetaY, double tetaZ)
        {
            Facette f1 = RotationX(tetaX);
            Facette f2 = f1.RotationY(tetaY);
            return f2.RotationZ(tetaZ);
        }

        // Homothetie Facette
        public Facette Homothetie(Point p, double rapport)
        {
            Segment S1 = Seg1.Homothetie(p, rapport);
            Segment S2 = Seg2.Homothetie(p, rapport);
            Segment S3 = Seg3.Homothetie(p, rapport);
            Segment S4 = Seg4.Homothetie(p, rapport);
            return new Facette(S1, S2, S3, S4);
        }

        // Tracé en fil de fer
        public void TraceFilDeFer(Bitmap img, Color pen)
        {
            Seg1.Afficher(img, pen);
            Seg2.Afficher(img, pen);
            Seg3.Afficher(img, pen);
            Seg4.Afficher(img, pen);
        }

        // les Points en fil de fer
        public List<Point> PointEnFilDeFer()
        {
            List<Point> points = new List<Point>();
            points = Seg1.MyBorder();
            points.AddRange(Seg2.MyBorder());
            points.AddRange(Seg3.MyBorder());
            points.AddRange(Seg4.MyBorder());
            return points;
        }

        // Enlever les points déjà visités
        public List<Point> EnleverElements(List<Point> connexites, List<Point> dejaVisites)
        {
            for (int i = connexites.Count-1; i >= 0; --i)
            {
                if (connexites.ElementAt(i).IsThere2DXY(dejaVisites))
                {
                    connexites.RemoveAt(i);
                }
            }
            return connexites;
        }

        // Recuperation aléatoire
        public int GetIndexRandomky(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        // Remplissage
        public void RemplissageFacette(List<Point> aVisites, List<Point> dejaVisites, Bitmap img, Color pen, int nb)
        {
            if (nb < 3200)
            {
                if (aVisites.Count > 0)
                {
                    Point germeDepart = aVisites.ElementAt(GetIndexRandomky(0, aVisites.Count));
                    aVisites.Remove(germeDepart);

                    if (!germeDepart.ComparerCouleur(pen, img) && !germeDepart.IsThere2DXY(dejaVisites))
                    {
                        germeDepart.Afficher(img, pen);
                        dejaVisites.Add(germeDepart);

                        List<Point> connexites = germeDepart.Connexite();
                        // Enlever les points deja visités
                        connexites = EnleverElements(connexites, dejaVisites);
                        aVisites.AddRange(connexites);
                        ++nb;
                        //Console.WriteLine(nb);
                        RemplissageFacette(aVisites, dejaVisites, img, pen, nb);
                    }
                    else if (!germeDepart.IsThere2DXY(dejaVisites))
                    {
                        dejaVisites.Add(germeDepart);
                        RemplissageFacette(aVisites, dejaVisites, img, pen, nb);
                    }
                    else
                    {
                        RemplissageFacette(aVisites, dejaVisites, img, pen, nb);
                    }
                }
            }
        }

        public Point GermeDeDEpart()
        {
            double centreX = ((double)P1.X + P2.X + P3.X + P4.X) / 4;
            double centreY = ((double)P1.Y + P2.Y + P3.Y + P4.Y) / 4;
            double centreZ = ((double)P1.Z + P2.Z + P3.Z + P4.Z) / 4;
            return new Point((int)centreX, (int)centreY, (int)centreZ);
        }

        public double DistanceMoyenneEnZ()
        {
            return ((double)P1.Z + P2.Z + P3.Z + P4.Z)/4;
        }
    }
}
