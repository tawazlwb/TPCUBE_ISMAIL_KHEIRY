using ikheiry.FileWriter;
using System;
using System.Collections.Generic;
using System.Linq;
using static ikheiry.FileWriter.Bitmap;

namespace ikheiry.Entities
{
    class Segment
    {
        protected Point _p1;
        protected Point _p2;
        protected int _type;

        public Point P1 { get => _p1; set => _p1 = value; }
        public Point P2 { get => _p2; set => _p2 = value; }
        public int Type { get => _type; set => _type = value; }

        public Segment()
        {
            P1 = new Point();
            P2 = new Point();
            DetermineType();
        }
        public Segment(Point p1, Point p2)
        {
            P1 = new Point(p1.X, p1.Y, p1.Z);
            P2 = new Point(p2.X, p2.Y, p2.Z);
            DetermineType();
        }
        public Segment(Segment segment)
        {
            P1 = new Point(segment.P1.X, segment.P1.Y, segment.P1.Z);
            P2 = new Point(segment.P2.X, segment.P2.Y, segment.P2.Z);
            DetermineType();
        }

        // type du segment
        public void DetermineType()
        {
            if(P1.X == P2.X && P1.Y == P2.Y)
            {   
                // segment Point
                Type = 0;
            }
            else if (P1.X != P2.X && P1.Y == P2.Y)
            {
                // segment Horizontal
                Type = 1;
            }
            else if (P1.X == P2.X && P1.Y != P2.Y)
            {
                // segment Vertical
                Type = 2;
            }
            else
            {
                // segment Oblique
                Type = 3;
            }
        }

        // Translation segment
        public Segment Translation(int x, int y, int z) => new Segment(P1.Translation(x, y, z), P2.Translation(x, y, z));

        // Rotation segment
        public Segment RotationX(double tetaX) => new Segment(P1.RotationX(tetaX), P2.RotationX(tetaX));
        public Segment RotationY(double tetaY) => new Segment(P1.RotationY(tetaY), P2.RotationY(tetaY));
        public Segment RotationZ(double tetaZ) => new Segment(P1.RotationZ(tetaZ), P2.RotationZ(tetaZ));
        public Segment Rotation(double tetaX, double tetaY, double tetaZ) => new Segment(P1.Rotation(tetaX, tetaY, tetaZ), P2.Rotation(tetaX, tetaY, tetaZ));

        // Homothetie segment
        public Segment Homothetie(Point p, double rapport) => new Segment(P1.Homothetie(p, rapport), P2.Homothetie(p, rapport));

        // Coefficient de la droite du segment
        public double Coefficient()
        {
            return ((double)P2.Y - P1.Y) / ((double)P2.X - P1.X);
        }

        // Constante de la droite du segment 
        public double Constante()
        {
            return P1.Y - P1.X * Coefficient();
        }

        // Point appertient à la droite
        public bool IsFromSegment(int x, int y, double coefficient, double constante)
        {
            // *************************************************************************************************************
            if (y == (int)(coefficient * x + constante))
                return true;
            return false;
        }

        // Ordre de la boucle de l'affichage 
        public int OrdreDeBoucleDAffichage()
        {
            if (P1.Y < P2.Y && P1.X < P2.X)
                return 1;
            else if (P1.Y < P2.Y && P1.X > P2.X)
                return 2;
            else if (P1.Y > P2.Y && P1.X < P2.X)
                return 3;
            else
                return 4;
        }

        // Détecte les points appartenant au segment oblique
        public List<Point> MyBorderHorizontal()
        {
            List<Point> points = new List<Point>();
            if(P1.X < P2.X)
            {
                for(int i=P1.X; i<=P2.X; ++i)
                {
                    points.Add(new Point(i, P1.Y, (P1.Z + P2.Z)/2));
                }
            }
            else
            {
                for (int i = P2.X; i <= P1.X; ++i)
                {
                    points.Add(new Point(i, P2.Y, (P1.Z + P2.Z)/2));
                }
            }
            return points;
        }

        // Détecte les points appartenant au segment oblique
        public List<Point> MyBorderVertical()
        {
            List<Point> points = new List<Point>();
            if (P1.Y < P2.Y)
            {
                for (int i = P1.Y; i <= P2.Y; ++i)
                {
                    points.Add(new Point(P1.X, i, (P1.Z + P2.Z)/2));
                }
            }
            else
            {
                for (int i = P2.Y; i <= P1.Y; ++i)
                {
                    points.Add(new Point(P1.X, i, (P1.Z + P2.Z)/2));
                }
            }
            return points;
        }

        // Détecte les points appartenant au segment oblique
        public List<Point> MyBorderOblique()
        {
            double coefficient = Coefficient();
            double constante = Constante();
            int action = OrdreDeBoucleDAffichage();
            List<Point> points = new List<Point>();

            //Console.WriteLine(coefficient + " , " + constante);

            switch (action)
            {
                case 1 :
                    for(int j=P1.Y; j<=P2.Y; ++j)
                    {
                        for (int i = P1.X; i<=P2.X; ++i)
                        {
                            if (IsFromSegment(i, j, coefficient, constante))
                                points.Add(new Point(i, j, (i + j)/2));
                        }
                    }
                    break;
                case 2:
                    for (int j = P1.Y; j <= P2.Y; ++j)
                    {
                        for (int i = P2.X; i <= P1.X; ++i)
                        {
                            if (IsFromSegment(i, j, coefficient, constante))
                                points.Add(new Point(i, j, (i + j)/2));
                        }
                    }
                    break;
                case 3:
                    for (int j = P2.Y; j <= P1.Y; ++j)
                    {
                        for (int i = P1.X; i <= P2.X; ++i)
                        {
                            if (IsFromSegment(i, j, coefficient, constante))
                                points.Add(new Point(i, j, (i + j)/2));
                        }
                    }
                    break;
                default:
                    for (int j = P2.Y; j <= P1.Y; ++j)
                    {
                        for (int i = P2.X; i <= P1.X; ++i)
                        {
                            if (IsFromSegment(i, j, coefficient, constante))
                                points.Add(new Point(i, j, (i + j)/2));
                        }
                    }
                    break;
            }
            return points;
        }
        // Détecte les points appartenant au segment oblique
        public List<Point> MyBorder()
        {
            List<Point> points;
            switch (Type)
            {
                case 0:
                    points = new List<Point>() {
                    new Point(P1.X,P1.Y,P1.Z)};
                    break;
                case 1:
                    points = MyBorderHorizontal();
                    break;
                case 2:
                    points = MyBorderVertical();
                    break;
                default:
                    points = MyBorderOblique();
                    break;
            }
            return points;
        }

        public List<Point> RemplirTrousY(List<Point> points)
        {
            if (points.Count > 2)
            {
                List<Point> resultats = new List<Point>();
                resultats.AddRange(points);
                Point depart = points.ElementAt(0);
                for (int i = 1; i < points.Count; ++i)
                {
                    if((depart.Y - points.ElementAt(i).Y) > 1)
                    {
                        for(int j=0; j<(depart.Y - points.ElementAt(i).Y); ++j)
                        {
                            int nb = -1 - j;
                            resultats.Add(new Point(depart.X, depart.Y +nb, (depart.Z + points.ElementAt(i).Z) / 2));
                        }
                    }
                    else if ((depart.Y - points.ElementAt(i).Y) < -1)
                    {
                        for (int j = 0; j <(points.ElementAt(i).Y - depart.Y); ++j)
                        {
                            int nb = 1 + j;
                            resultats.Add(new Point(depart.X, depart.Y + nb, (depart.Z + points.ElementAt(i).Z) / 2));
                        }
                    }
                    depart = points.ElementAt(i);
                }
                return resultats;
            }
            return points;
        }

        // Afficher
        public void Afficher(Bitmap img, Color pen)
        {
            List<Point> points = MyBorder();
            
            // remplir les trous importannnnnnnnnnnnnnnnnnnnnnnnnnnt
            points = RemplirTrousY(points);

            foreach (Point p in points)
            {
                p.Afficher(img, pen);
            }
        }
    }
}
