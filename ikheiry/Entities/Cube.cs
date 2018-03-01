using ikheiry.FileWriter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ikheiry.FileWriter.Bitmap;

namespace ikheiry.Entities
{
    class Cube
    {
        //struct
        public class TabProfondeur
        {
            protected int _indice;
            protected double _moyenne;

            public int Indice { get => _indice; set => _indice = value; }
            public double Moyenne { get => _moyenne; set => _moyenne = value; }
            
            public TabProfondeur(int indice, double moyenne)
            {
                Indice = indice;
                Moyenne = moyenne;
            }
        }

        protected Facette _face1;
        protected Facette _face2;
        protected Facette _face3;
        protected Facette _face4;
        protected Facette _face5;
        protected Facette _face6;

        public Facette Face1 { get => _face1; set => _face1 = value; }
        public Facette Face2 { get => _face2; set => _face2 = value; }
        public Facette Face3 { get => _face3; set => _face3 = value; }
        public Facette Face4 { get => _face4; set => _face4 = value; }
        public Facette Face5 { get => _face5; set => _face5 = value; }
        public Facette Face6 { get => _face6; set => _face6 = value; }

        public Cube()
        {
            Face1 = new Facette();
            Face2 = new Facette();
            Face3 = new Facette();
            Face4 = new Facette();
            Face5 = new Facette();
            Face6 = new Facette();
        }

        public Cube(Facette face1, Facette face2, Facette face3, Facette face4, Facette face5, Facette face6)
        {
            Face1 = new Facette(face1);
            Face2 = new Facette(face2);
            Face3 = new Facette(face3);
            Face4 = new Facette(face4);
            Face5 = new Facette(face5);
            Face6 = new Facette(face6);
        }

        public Cube(Cube cube)
        {
            Face1 = new Facette(cube.Face1);
            Face2 = new Facette(cube.Face2);
            Face3 = new Facette(cube.Face3);
            Face4 = new Facette(cube.Face4);
            Face5 = new Facette(cube.Face5);
            Face6 = new Facette(cube.Face6);
        }

        // Translation Cube
        public Cube Translation(int x, int y, int z)
        {
            Facette F1 = Face1.Translation(x, y, z);
            Facette F2 = Face2.Translation(x, y, z);
            Facette F3 = Face3.Translation(x, y, z);
            Facette F4 = Face4.Translation(x, y, z);
            Facette F5 = Face5.Translation(x, y, z);
            Facette F6 = Face6.Translation(x, y, z);
            return new Cube(F1, F2, F3, F4, F5, F6);
        }

        // Rotation Cube
        public Cube RotationX(double tetaX)
        {
            Facette F1 = Face1.RotationX(tetaX);
            Facette F2 = Face2.RotationX(tetaX);
            Facette F3 = Face3.RotationX(tetaX);
            Facette F4 = Face4.RotationX(tetaX);
            Facette F5 = Face5.RotationX(tetaX);
            Facette F6 = Face6.RotationX(tetaX);
            return new Cube(F1, F2, F3, F4, F5, F6);
        }
        public Cube RotationY(double tetaY)
        {
            Facette F1 = Face1.RotationY(tetaY);
            Facette F2 = Face2.RotationY(tetaY);
            Facette F3 = Face3.RotationY(tetaY);
            Facette F4 = Face4.RotationY(tetaY);
            Facette F5 = Face5.RotationY(tetaY);
            Facette F6 = Face6.RotationY(tetaY);
            return new Cube(F1, F2, F3, F4, F5, F6);
        }
        public Cube RotationZ(double tetaZ)
        {
            Facette F1 = Face1.RotationZ(tetaZ);
            Facette F2 = Face2.RotationZ(tetaZ);
            Facette F3 = Face3.RotationZ(tetaZ);
            Facette F4 = Face4.RotationZ(tetaZ);
            Facette F5 = Face5.RotationZ(tetaZ);
            Facette F6 = Face6.RotationZ(tetaZ);
            return new Cube(F1, F2, F3, F4, F5, F6);
        }
        public Cube Rotation(double tetaX, double tetaY, double tetaZ)
        {
            Cube c1 = RotationX(tetaX);
            Cube c2 = c1.RotationY(tetaY);
            return c2.RotationZ(tetaZ);
        }

        // Homothetie Cube
        public Cube Homothetie(Point p, double rapport)
        {
            Facette F1 = Face1.Homothetie(p, rapport);
            Facette F2 = Face2.Homothetie(p, rapport);
            Facette F3 = Face3.Homothetie(p, rapport);
            Facette F4 = Face4.Homothetie(p, rapport);
            Facette F5 = Face5.Homothetie(p, rapport);
            Facette F6 = Face6.Homothetie(p, rapport);
            return new Cube(F1, F2, F3, F4, F5, F6);
        }

        // Tracé en fil de fer
        public void TraceFilDeFer(Bitmap img, Color pen)
        {
            Face1.TraceFilDeFer(img, pen);
            Face2.TraceFilDeFer(img, pen);
            Face3.TraceFilDeFer(img, pen);
            Face4.TraceFilDeFer(img, pen);
            Face5.TraceFilDeFer(img, pen);
            Face6.TraceFilDeFer(img, pen);
        }

        public TabProfondeur MinZ(TabProfondeur[] tab)
        {
            TabProfondeur minProfondeur = tab[0];
            for (int i=1; i<tab.Length; ++i)
            {
                if (minProfondeur.Moyenne > tab[i].Moyenne)
                    minProfondeur = tab[i];
            }
            return minProfondeur;
        }

        public TabProfondeur[] EnleverElement(TabProfondeur element, TabProfondeur[] tab)
        {
            TabProfondeur[] profondeur = new TabProfondeur[tab.Length-1];
            for (int i = 0, j=0; i < tab.Length; ++i)
            {
                if (element.Indice != tab[i].Indice)
                {
                    profondeur[j] = tab[i];
                    ++j;
                }
            }
            return profondeur;
        }

        public TabProfondeur[] TriFaces()
        {
            TabProfondeur[] profondeurTemp = new TabProfondeur[6];
            TabProfondeur[] prof = new TabProfondeur[6];
            profondeurTemp[0] = new TabProfondeur(1, Face1.DistanceMoyenneEnZ());
            profondeurTemp[1] = new TabProfondeur(2, Face2.DistanceMoyenneEnZ());
            profondeurTemp[2] = new TabProfondeur(3, Face3.DistanceMoyenneEnZ());
            profondeurTemp[3] = new TabProfondeur(4, Face4.DistanceMoyenneEnZ());
            profondeurTemp[4] = new TabProfondeur(5, Face5.DistanceMoyenneEnZ());
            profondeurTemp[5] = new TabProfondeur(6, Face6.DistanceMoyenneEnZ());
                       
            TabProfondeur minProfondeur;
            int j = 0;
            while(profondeurTemp.Length >= 1)
            {
                minProfondeur = MinZ(profondeurTemp);
                prof[j] = minProfondeur;
                profondeurTemp = EnleverElement(minProfondeur, profondeurTemp);
                ++j;
            }
            return prof;
        }

        public virtual void Dessiner(Bitmap img, Color[] pen)
        {
            TabProfondeur[] faces = TriFaces();
            for(int i=0; i<faces.Length; ++i)
            {
                switch (faces[i].Indice)
                {
                    case 1:
                        Face1.TraceFilDeFer(img, pen[i]);
                        Face1.RemplissageFacette(new List<Point>() { Face1.GermeDeDEpart() }, new List<Point>(), img, pen[i], 0);
                        break;
                    case 2:
                        Face2.TraceFilDeFer(img, pen[i]);
                        Face2.RemplissageFacette(new List<Point>() { Face2.GermeDeDEpart() }, new List<Point>(), img, pen[i], 0);
                        break;
                    case 3:
                        Face3.TraceFilDeFer(img, pen[i]);
                        Face3.RemplissageFacette(new List<Point>() { Face3.GermeDeDEpart() }, new List<Point>(), img, pen[i], 0);
                        break;
                    case 4:
                        Face4.TraceFilDeFer(img, pen[i]);
                        Face4.RemplissageFacette(new List<Point>() { Face4.GermeDeDEpart() }, new List<Point>(), img, pen[i], 0);
                        break;
                    case 5:
                        Face5.TraceFilDeFer(img, pen[i]);
                        Face5.RemplissageFacette(new List<Point>() { Face5.GermeDeDEpart() }, new List<Point>(), img, pen[i], 0);
                        break;
                    case 6:
                        Face6.TraceFilDeFer(img, pen[i]);
                        Face6.RemplissageFacette(new List<Point>() { Face6.GermeDeDEpart() }, new List<Point>(), img, pen[i], 0);
                        break;

                }
            }
        }
    }
}
