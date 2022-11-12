using System;
using UnityEngine;

namespace Tuile.Graphics
{
    public static class MeshsTuiles 
    {
        #region Tuile Hexa

        private static float decalageRad = Mathf.PI / 2;
        private static float cranRad = 2 * Mathf.PI / 6;

        private static Vector3 centre = new Vector3(0, 0, 0);
        private static Vector3 bas = new Vector3(0, 0, -1);
        private static Vector3 basExt = new Vector3(0, 0, -2);
        private static Vector3 haut = new Vector3(0, 0, 1);
        private static Vector3 hautExt = new Vector3(0, 0, 2);
        private static Vector3 hautDroite = new Vector3(Mathf.Cos(decalageRad - cranRad), 0, Mathf.Sin(decalageRad -cranRad));
        private static Vector3 hautDroiteExt = new Vector3(Mathf.Cos(decalageRad - cranRad) * 2, 0, Mathf.Sin(decalageRad -cranRad) * 2);
        private static Vector3 hautGauche = new Vector3(Mathf.Cos(cranRad + decalageRad), 0, Mathf.Sin(cranRad + decalageRad));
        private static Vector3 hautGaucheExt = new Vector3(Mathf.Cos(cranRad + decalageRad) * 2, 0, Mathf.Sin(cranRad + decalageRad) * 2);
        private static Vector3 basDroite = new Vector3(Mathf.Cos(cranRad * 4 + decalageRad), 0, Mathf.Sin(cranRad * 4 + decalageRad));
        private static Vector3 basDroiteExt = new Vector3(Mathf.Cos(cranRad * 4 + decalageRad) * 2, 0, Mathf.Sin(cranRad * 4 + decalageRad) *2);
        private static Vector3 basGauche = new Vector3(Mathf.Cos(cranRad * 2 + decalageRad), 0, Mathf.Sin(cranRad * 2 + decalageRad));
        private static Vector3 basGaucheExt = new Vector3(Mathf.Cos(cranRad * 2 + decalageRad) * 2, 0, Mathf.Sin(cranRad * 2 + decalageRad) * 2);
        private static Vector3 droiteExt = Vector3.Lerp(basDroiteExt, hautDroiteExt, 0.5f);
        private static Vector3 gaucheExt = Vector3.Lerp(basGaucheExt, hautGaucheExt, 0.5f);
        private static Vector3 midHautDroiteExt = Vector3.Lerp(hautExt, hautDroiteExt, 0.5f);
        private static Vector3 midHautGaucheExt = Vector3.Lerp(hautExt, hautGaucheExt, 0.5f);
        private static Vector3 midBasDroiteExt = Vector3.Lerp(basExt, basDroiteExt, 0.5f);
        private static Vector3 midbasGaucheExt = Vector3.Lerp(basExt, basGaucheExt, 0.5f);

        public static Mesh tuileMetaHexa => new Mesh
        {
            name = "Tuile de Triangles",
            vertices = new Vector3[]
            {
                //18 vertices de l'hexa central
                centre, hautDroite, basDroite,
                centre, haut, hautDroite, 
                centre,  hautGauche, haut,
                centre, basGauche, hautGauche, 
                centre, bas, basGauche, 
                centre, basDroite, bas,
                
                //18 à 26 
                basDroite, droiteExt, basDroiteExt,
                basDroite, hautDroite, droiteExt,
                hautDroite, hautDroiteExt, droiteExt,
                
                //27 - 35
                hautDroite, midHautDroiteExt, hautDroiteExt,
                hautDroite, haut, midHautDroiteExt,
                haut, hautExt, midHautDroiteExt,
                
                //36 - 44
                haut, midHautGaucheExt, hautExt,
                haut, hautGauche, midHautGaucheExt,
                hautGauche, hautGaucheExt, midHautGaucheExt,
                
                //45 - 53
                hautGauche, gaucheExt, hautGaucheExt,
                hautGauche, basGauche, gaucheExt,
                basGauche, basGaucheExt, gaucheExt,
                
                //54 - 62
                basGauche, midbasGaucheExt, basGaucheExt,
                basGauche, bas, midbasGaucheExt,
                bas, basExt, midbasGaucheExt,
                
                //63 - 71
                bas, midBasDroiteExt, basExt,
                bas, basDroite, midBasDroiteExt,
                basDroite, basDroiteExt, midBasDroiteExt,
                
                //72 - 83
                basDroiteExt, droiteExt, hautDroiteExt, midHautDroiteExt, hautExt, midHautGaucheExt, hautGaucheExt, 
                gaucheExt, basGaucheExt, midbasGaucheExt, basExt, midBasDroiteExt
            },
            triangles = new int[]
            {
                0,1,2, 
                3,4,5, 
                6,7,8, 
                9,10,11, 
                12,13,14,
                15,16,17,
             
                18,19,20,
                21,22,23,
                24,25,26,
                
                27,28,29,
                30,31,32,
                33,34,35,
                
                36,37,38,
                39,40,41,
                42,43,44,
                
                45,46,47,
                48,49,50,
                51,52,53,
                
                54,55,56,
                57,58,59,
                60,61,62,
                
                63,64,65,
                66,67,68,
                69,70,71,
                
                //Liaisons entre les cases
                0,3,5,
                5,1,0,
                
                3,6,8,
                8,4,3,
                
                6,9,11,
                11,7,6,
                
                9,12,14,
                14,10,9,
                
                12,15,17,
                17,13,12,
                
                15,0,2,
                2,16,15,
                
                2,22,21,
                2,1,22,
                
                5,31,30,
                5,4,31,
                
                8,40,39,
                8,7,40,
                
                11,49,48,
                11,10,49,
                
                14,58,57,
                14,13,58,
                
                17,67,66,
                17,16,67,
                
                21,19,18,
                19,21,23,
                
                22,24,26,
                23,22,26,
                
                24,27,29,
                25,24,29,
                
                27,30,32,
                28,27,32,
                
                31,33,35,
                32,31,35,
                
                33,36,38,
                34,33,38,
                
                36,39,41,
                37,36,41,
                
                40,42,44,
                41,40,44,
                
                42,45,47,
                43,42,47,
                
                45,48,50,
                46,45,50,
                
                49,51,53,
                50,49,53,
                
                51,54,56,
                52,51,56,
                
                54,57,59,
                55,54,59,
                
                59,58,60,
                59,60,62,
                
                60,63,65,
                61,60,65,
                
                63,66,68,
                64,63,68,
                
                67,69,71,
                68,67,71,
                
                69,18,20,
                70,69,20,
                
                //bords extérieurs
                20,73,72,
                19,73,20,
                
                26,74,73,
                25,74,26,
                
                29,75,74,
                28,75,29,
                
                35,76,75,
                34,76,35,
                
                38,77,76,
                37,77,38,
                
                44,78,77,
                43,78,44,
                
                47,79,78,
                46,79,47,
                
                53,80,79,
                52,80,53,
                
                56,81,80,
                55,81,56,
                
                62,82,81,
                61,82,62,
                
                65,83,82,
                64,83,65,
                
                71,72,83,
                70,72,71
            }
        };

        private static int[] coordonnesTriangles =
        {
            51, 54, 57, 60, 63, 
            45, 48, 9, 12, 15, 66, 69,
            42, 39, 6, 3, 0, 21, 18,
            36, 33, 30, 27, 24
        };

        /// <summary>
        /// Change la hauteurs des tuiles triangulaires
        /// </summary>
        /// <param name="mappeHauteur">tableau de 24 entrées représentant la hauteur des tuiles triangulaires</param>
        /// <param name="meshTuile">Mesh de la tuile que l'on veut modifier</param>
        public static void AppliquerMappeHauteur(float[] mappeHauteur, Mesh meshTuile)
        {
            Vector3[] vertices = meshTuile.vertices;
            
            for (int i = 0; i < (mappeHauteur.Length > 24 ? 24 : mappeHauteur.Length); i++)
            {
                int indexVertex = coordonnesTriangles[i];

                vertices[indexVertex].y = mappeHauteur[i];
                vertices[indexVertex + 1].y = mappeHauteur[i];
                vertices[indexVertex + 2].y = mappeHauteur[i];
            }
            meshTuile.vertices = vertices;
            meshTuile.RecalculateBounds();
            meshTuile.RecalculateNormals();
        }

        #endregion

        #region Tuile Triangulaire

        private static float cranRadTT = 2 * Mathf.PI / 3;

        private static Vector3 hauteur = Vector3.up * 0.001f;
        private static float rayon = 0.5f / Mathf.Cos(Mathf.Deg2Rad * 30); 
        
        
        private static Vector3 zeroRad = new Vector3(rayon, 0, 0);
        private static Vector3 deuxRad = new Vector3(Mathf.Cos(cranRadTT) * rayon, 0f, Mathf.Sin(cranRadTT) * rayon);
        private static Vector3  unRad =  new Vector3(Mathf.Cos(cranRadTT * 2) * rayon,0,Mathf.Sin(cranRadTT * 2) * rayon);
        
        
        public static readonly Mesh TriTuile = new Mesh
        {
            name = "TriTuile",

            vertices = new []
            {
             zeroRad + hauteur,
             unRad + hauteur,
             deuxRad + hauteur,

             zeroRad - hauteur,
             deuxRad - hauteur,
             unRad - hauteur,
             
             zeroRad + hauteur,
             zeroRad - hauteur,
             deuxRad + hauteur,
             deuxRad - hauteur,
             
             zeroRad + hauteur,
             zeroRad - hauteur,
             unRad + hauteur,
             unRad - hauteur,
             
             unRad + hauteur,
             unRad - hauteur,
             deuxRad + hauteur,
             deuxRad - hauteur,
            },
            triangles = new []
            {
                0,1,2,
                3,4,5,
                
                7,6,8,
                9,7,8,
                
                11,12,10,
                11,13,12,
                
                15,16,14,
                15,17,16
                
            }
        };

        #endregion
    }
}
