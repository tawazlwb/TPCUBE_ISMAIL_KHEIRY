using ikheiry.Entities;
using ikheiry.FileWriter;
using ikheiry.Test;
using System;
using System.Linq;
using System.Collections.Generic;
using static ikheiry.FileWriter.Bitmap;
using static ikheiry.Entities.Cube;

namespace ikheiry
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            Console.Write("Donner le nombre de point que vous vouler afficher : ");
            int nbre = int.Parse(Console.ReadLine());
            Console.WriteLine(nbre);
            */
            //int nbre = 105;

            // Couleurs
            Color pen = new Color(255, 0, 0);
            Color greenPen = new Color(0, 255, 0);
            Color bluePen = new Color(0, 0, 255);
            Color yellowPen = new Color(255, 255, 0);
            Color background = new Color(255, 255, 255);
            // Couleur du centre du repère
            Color couleurCentreImage = new Color(0, 0, 0);

            // Images
            Bitmap img = new Bitmap(400, 400);
            img.Fill(background);
            
            // Centre du repere
            img.SetPixel(0 + img.Width / 2, img.Height / 2 - 0, couleurCentreImage);

            /* 
             * Debut Affichage des figures
            */
            // Affichages des points
            //Drawer.DrawPoints(nbre, img, pen);

            // Affichages des cercles
            //Drawer.DrawCercles(img, pen);

            // Affichage des segments
            //Drawer.DrawSegments(img, pen);

            // Affichage des Facettes en fil de fer
            //Drawer.DrawFacettesFilDeFer(img, pen);

            // Affichage des Facette
            //Drawer.DrawFacette(img, pen);

            // Affichage d'un cube en fil de fer
            //Drawer.DrawCubeFilDeFer(img, pen);

            // Affichage des cubes en fil de fer
            //Cube c = Drawer.DrawCube();
            //Drawer.DrawCubeFilDeFer(c, img, greenPen, pen);

            // Affichage des cubes en complet
            Drawer.DrawCubeFaces(img);


            PPMWriter.WriteBitmapToPPM("IKheiryPPM.ppm", img);
            Console.WriteLine("Finish");
            Console.ReadLine();
        }
    }
}
