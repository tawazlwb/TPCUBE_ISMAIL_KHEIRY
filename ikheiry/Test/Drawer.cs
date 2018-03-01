using ikheiry.Entities;
using ikheiry.FileWriter;
using System;
using System.Collections.Generic;
using System.Linq;
using static ikheiry.FileWriter.Bitmap;

namespace ikheiry.Test
{
    class Drawer
    {
        public static void DrawPoints(int nbre, Bitmap img, Color pen)
        {
            List<Point> points = new List<Point>();
            Random random = new Random();
            for (int i = 0; i < nbre; ++i)
            {
                points.Add(new Point(random.Next(-img.Width / 2, img.Width / 2), random.Next(-img.Height / 2, img.Height / 2), random.Next(-(img.Width + img.Height) / 2, (img.Width + img.Height) / 2)));
                Console.WriteLine("X :" + points.Last().X + ", Y :" + points.Last().Y + ", Z :" + points.Last().Z);
                points.Last().Afficher(img, pen);
            }
        }
        
        public static void DrawCercles(Bitmap img, Color pen)
        {
            Cercle c = new Cercle(0, 0, 0, 50);
            c.Afficher(img, pen);
            Cercle c1 = new Cercle(10, 0, 0, 30);
            c1.Afficher(img, pen);
            Cercle c2 = new Cercle(10, 10, 0, 60);
            c2.Afficher(img, pen);
            Cercle c3 = new Cercle(15, 15, 0, 70);
            c3.Afficher(img, pen);
        }

        public static void DrawSegments(Bitmap img, Color pen)
        {
            Segment segment = new Segment(new Point(1, 1, 1), new Point(100, 100, 100));
            segment.Afficher(img, pen);
            Segment segment1 = segment.Translation(10, 30, 0);
            segment1.Afficher(img, pen);
            Segment segment2 = segment.RotationZ(90.0);
            Console.WriteLine(segment2.Type);
            //segment2.Afficher(img, pen);
            Segment segment3 = segment2.Homothetie(new Point(0, 0, 0), 0.25);
            segment3.Afficher(img, pen);
        }

        public static void DrawFacettesFilDeFer(Bitmap img, Color pen)
        {
            Color bluePen = new Color(0, 0, 255);
            Facette facette = new Facette(
                new Point(-50, 100, 0),
                new Point(50, 100, 0),
                new Point(50, -100, 0),
                new Point(-50, -100, 0));
            facette.TraceFilDeFer(img, bluePen);

            Facette f = facette.Translation(15, 15, 0);
            f.TraceFilDeFer(img, pen);

            Facette f2 = facette.RotationZ(tetaZ: 45);
            f2.TraceFilDeFer(img, pen);

            Facette f3 = facette.Homothetie(new Point(), 0.25);
            f3.TraceFilDeFer(img, pen);
        }

        public static void DrawFacettesFilDeFer(Facette facette, Bitmap img, Color pen)
        {
            facette.TraceFilDeFer(img, pen);
        }

        public static void DrawFacette(Bitmap img, Color pen)
        {
            Facette facette = new Facette(
               new Point(50, -20, 0),
               new Point(100, 100, 0),
               new Point(-50, 20, 0),
               new Point(-100, -100, 0));
            facette = facette.RotationZ(45);
            facette = facette.Homothetie(new Point(0, 0, 0), 0.4);
            facette = facette.Translation(50, 50, 0);
            facette.TraceFilDeFer(img, pen);
            facette.RemplissageFacette(new List<Point>() { facette.GermeDeDEpart() }, new List<Point>(), img, pen, 0);
        }
        public static void DrawFacette(Facette facette, Bitmap img, Color pen)
        {
            facette.TraceFilDeFer(img, pen);
            facette.RemplissageFacette(new List<Point>() { facette.GermeDeDEpart() }, new List<Point>(), img, pen, 0);
        }

        public static Cube DrawCube()
        {
            // Faces de dessous
            Facette facette1 = new Facette(
               new Point(-20, -20, 20),
               new Point(0, 0, -20),
               new Point(40, 0, -20),
               new Point(20, -20, 20));
            Facette facette2 = new Facette(
               new Point(0, 0, -20),
               new Point(0, 40, -20),
               new Point(40, 40, -20),
               new Point(40, 0, -20));
            Facette facette3 = new Facette(
               new Point(-20, -20, 20),
               new Point(-20, 20, 20),
               new Point(0, 40, -20),
               new Point(0, 0, -20));

            // Faces de dessus
            Facette facette4 = new Facette(
               new Point(20, -20, 20),
               new Point(20, 20, 20),
               new Point(-20, 20, 20),
               new Point(-20, -20, 20));
            Facette facette5 = new Facette(
               new Point(-20, 20, 20),
               new Point(20, 20, 20),
               new Point(40, 40, -20),
               new Point(0, 40, -20));
            Facette facette6 = new Facette(
               new Point(20, 20, 20),
               new Point(40, 40, -20),
               new Point(40, 0, -20),
               new Point(20, -20, 20));

            return new Cube(facette1, facette2, facette3, facette4, facette5, facette6);
        }

        public static void DrawCubeFilDeFer(Bitmap img, Color pen)
        {
            // Faces de dessous
            Facette facette1 = new Facette(
               new Point(-20, -20, 20),
               new Point(0, 0, -20),
               new Point(40, 0, -20),
               new Point(20, -20, 20));
            Facette facette2 = new Facette(
               new Point(0, 0, -20),
               new Point(0, 40, -20),
               new Point(40, 40, -20),
               new Point(40, 0, -20));
            Facette facette3 = new Facette(
               new Point(-20, -20, 20),
               new Point(-20, 20, 20),
               new Point(0, 40, -20),
               new Point(0, 0, -20));

            // Faces de dessus
            Facette facette4 = new Facette(
               new Point(20, -20, 20),
               new Point(20, 20, 20),
               new Point(-20, 20, 20),
               new Point(-20, -20, 20));
            Facette facette5 = new Facette(
               new Point(-20, 20, 20),
               new Point(20, 20, 20),
               new Point(40, 40, -20),
               new Point(0, 40, -20));
            Facette facette6 = new Facette(
               new Point(20, 20, 20),
               new Point(40, 40, -20),
               new Point(40, 0, -20),
               new Point(20, -20, 20));

            Cube c = new Cube(facette1, facette2, facette3, facette4, facette5, facette6);
            c.TraceFilDeFer(img, pen);
        }

        public static void DrawCubeFilDeFer(Cube c,Bitmap img, Color pen)
        {
            c.TraceFilDeFer(img, pen);
        }

        public static void DrawCubeFilDeFer(Cube c, Bitmap img, Color originPen, Color pen)
        {
            DrawCubeFilDeFer(c, img, originPen);
            Cube c1 = c.Translation(80, 80, 0);
            DrawCubeFilDeFer(c1, img, pen);
            c1 = c1.Translation(-160, 0, 0);
            // Rotation sur l'axe des x
            c1 = c1.RotationZ(10);
            DrawCubeFilDeFer(c1, img, pen);
            c1 = c1.Translation(0, -160, 0);
            // Rotation sur l'axe des Y
            c1 = c1.RotationX(-35);
            DrawCubeFilDeFer(c1, img, pen);
            Cube c2 = c.Translation(80, -80, 0);
            c2 = c2.Rotation(45, 0, 22.5);
            DrawCubeFilDeFer(c2, img, pen);
        }

        public static void DrawCubeFaces(Bitmap img)
        {
            Cube c = DrawCube();
            Color[] pens = new Color[]
            {
                new Color(0, 255, 0),
                new Color(0, 0, 255),
                new Color(255, 255, 0),
                new Color(255, 0, 0),
                new Color(255, 180, 255),
                new Color(255, 80, 0)
            };
            c.Dessiner(img, pens);
            Cube c1 = c.Translation(80, 80, 0);
            c1.Dessiner(img, pens);
            Cube c2 = c.RotationZ(45);
            c2 = c2.Translation(-80, 80, 0);
            c2.Dessiner(img, pens);
        }
    }
}
