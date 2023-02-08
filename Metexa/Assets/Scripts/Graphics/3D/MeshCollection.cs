using System.Collections.Generic;
using UnityEngine;

namespace Graphics
{
    public static class MeshCollection
    {
        public static Mesh GenererIcosahedre()
        {
            
            RecupIcosahedreInfos(out Vector3[] vertices, out int[] triangles);
            Mesh ico = new Mesh
            {
                vertices = vertices,
                triangles = triangles
            };
            
            ico.RecalculateNormals();
            ico.name = "Icosahedre";
            return ico;
        }

        public static Mesh GenererSphere(int definition = 1)
        {
            RecupSphereInfos(out Vector3[] vertices, out int[] triangles, definition);
            Mesh sphere = new Mesh
            {
                vertices = vertices,
                triangles = triangles
            };
            
            sphere.RecalculateNormals();
            sphere.name = "Sphere:" + definition;
            return sphere;
        }
        
        public static void RecupIcosahedreInfos(out Vector3[] vertices, out int[] triangles)
        {
            float t = (1.0f + Mathf.Sqrt(5.0f)) / 2.0f;

            vertices = new[]
            {
                new Vector3(-1, t, 0).normalized,
                new Vector3(1, t, 0).normalized,
                new Vector3(-1, -t, 0).normalized,
                new Vector3(1, -t, 0).normalized,
                new Vector3(0, -1, t).normalized,
                new Vector3(0, 1, t).normalized,
                new Vector3(0, -1, -t).normalized,
                new Vector3(0, 1, -t).normalized,
                new Vector3(t, 0, -1).normalized,
                new Vector3(t, 0, 1).normalized,
                new Vector3(-t, 0, -1).normalized,
                new Vector3(-t, 0, 1).normalized
            };

            triangles = new[]
            {
                0, 11, 5,
                0, 5, 1,
                0, 1, 7,
                0, 7, 10,
                0, 10, 11,
                1, 5, 9,
                5, 11, 4,
                11, 10, 2,
                10, 7, 6,
                7, 1, 8,
                3, 9, 4,
                3, 4, 2,
                3, 2, 6,
                3, 6, 8,
                3, 8, 9,
                4, 9, 5,
                2, 4, 11,
                6, 2, 10,
                8, 6, 7,
                9, 8, 1
            };
        }

        public static void RecupSphereInfos(out Vector3[] vertices, out int[] triangles, int definition)
        {
            RecupIcosahedreInfos(out vertices, out triangles);
            SubdiviserSphere(ref vertices, ref triangles, definition);
        }

        private static void SubdiviserSphere(ref Vector3[] vertices, ref int[] triangles, int definition)
        {
            var midPointCache = new Dictionary<int, int> ();

            for (int i = 0; i < definition; i++)
            {
                List<int> newTriangles = new List<int>();
                List<Vector3> newVertices = new List<Vector3>(vertices);

                for (int j = 0; j < triangles.Length ; j+=3)
                {
                    int indexA = triangles[j];
                    int indexB = triangles[j + 1];
                    int indexC = triangles[j + 2];

                    int keyAB = (Mathf.Min(indexA, indexB) << 16) + Mathf.Max(indexA, indexB);
                    int keyBC = (Mathf.Min(indexC, indexB) << 16) + Mathf.Max(indexC, indexB);
                    int keyCA = (Mathf.Min(indexA, indexC) << 16) + Mathf.Max(indexA, indexC);

                    if (!midPointCache.TryGetValue(keyAB, out int indexAB))
                    {
                        indexAB = newVertices.Count;
                        midPointCache.Add(keyAB,indexAB);
                        newVertices.Add(Vector3.Lerp(vertices[indexA], vertices[indexB], 0.5f).normalized);
                    }
                    if (!midPointCache.TryGetValue(keyBC, out int indexBC))
                    {
                        indexBC = newVertices.Count;
                        midPointCache.Add(keyBC,indexBC);
                        newVertices.Add(Vector3.Lerp(vertices[indexB], vertices[indexC], 0.5f).normalized);
                    }
                    if (!midPointCache.TryGetValue(keyCA, out int indexCA))
                    {
                        indexCA = newVertices.Count;
                        midPointCache.Add(keyCA,indexCA);
                        newVertices.Add(Vector3.Lerp(vertices[indexC], vertices[indexA], 0.5f).normalized);
                    }

                    newTriangles.AddRange(new []
                    {
                        indexA, indexAB, indexCA,
                        indexB, indexBC, indexAB,
                        indexC, indexCA, indexBC,
                        indexAB, indexBC, indexCA
                    });
                }

                vertices = newVertices.ToArray();
                triangles = newTriangles.ToArray();
            }
        }
    }
}