using Unity.VisualScripting;
using UnityEngine;

namespace MetaHexa.PlaneteProcedurale
{
    // ref : https://fr.wikipedia.org/wiki/Coordonn%C3%A9es_sph%C3%A9riques#:~:text=On%20appelle%20coordonn%C3%A9es%20sph%C3%A9riques%20divers,p%C3%B4le)%20et%20par%20deux%20angles.
    public struct Coordonnee
    {
        private Vector3 coor;

        public Vector3 VersPosition()
        {
            return new Vector3
            {
                x = coor.x * Mathf.Cos(coor.z) * Mathf.Cos(coor.y),
                y = coor.x * Mathf.Sign(coor.z),
                z = coor.x * Mathf.Cos(coor.z) * Mathf.Sign(coor.y)
            };
        }
        
        public float h => coor.x;
        public float lonRad => coor.y;
        public float latRad => coor.z;

        public float lonDeg => coor.y * Mathf.Rad2Deg;
        public float latDeg => coor.z * Mathf.Rad2Deg;
        
        public Coordonnee(Vector3 position, float hauteur = 1)
        {
            coor = new Vector3
            {
                x = hauteur,
                z = Mathf.Acos(position.z / position.x),
                y = Mathf.Atan(position.y / hauteur) - Mathf.PI / 2
            };
        }

        
        //ref : https://fr.wikipedia.org/wiki/Formule_de_haversine#:~:text=La%20loi%20des%20haversines,-Un%20triangle%20sph%C3%A9rique&text=Dans%20ce%20cas%2C%20a%20et,la%20formule%20de%20haversine%20suit.
        public static float Distance(Coordonnee a, Coordonnee b, float rayonSphere = 1)
        {
            return 2 * rayonSphere * Mathf.Asin(
                Mathf.Sqrt(Mathf.Pow(Mathf.Sin((b.latRad - a.latRad) / 2),2))
                + Mathf.Cos(a.latRad) * Mathf.Cos(a.latRad)
                * Mathf.Pow(Mathf.Sin((b.lonRad - b.lonRad)) ,2));
        }
    }
}