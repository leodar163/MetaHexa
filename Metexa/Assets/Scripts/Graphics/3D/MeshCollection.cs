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

        public static Mesh GenererSphere(uint definition = 1, bool tailleTriangleFixe = true)
        {
            RecupSphereInfos(out Vector3[] vertices, out int[] triangles, definition);
            Mesh sphere = new Mesh
            {
                vertices = vertices,
                triangles = triangles
            };

            if (tailleTriangleFixe)
            {
                sphere = GenererMeshAgrandi( sphere, Mathf.Pow(2,definition));
            }
            
            sphere.RecalculateNormals();
            sphere.name = "Sphere:" + definition;
            return sphere;
        }

        public static List<Mesh> GenererSphereATuilesExtrudables(uint definition = 1)
        {
            Mesh sphere = GenererSphere(definition);
            return DiviserEnTuilesTriangulairesExtrudables(sphere);
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

        public static void RecupSphereInfos(out Vector3[] vertices, out int[] triangles, uint definition)
        {
            RecupIcosahedreInfos(out vertices, out triangles);
            SubdiviserSphere(ref vertices, ref triangles, definition);
        }

        private static void SubdiviserSphere(ref Vector3[] vertices, ref int[] triangles, uint definition)
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
        
        /*
        private static Mesh CreerMeshTrianglesExtrudables(Mesh meshAExtruder)
        {
            List<int> nvTriangles = new List<int>();
            List<Vector3> nvVertices = new List<Vector3>();

            for (int i = 0; i < meshAExtruder.triangles.Length; i += 3)
            {
                Vector3 a = meshAExtruder.vertices[meshAExtruder.triangles[i]];
                Vector3 b = meshAExtruder.vertices[meshAExtruder.triangles[i + 1]];
                Vector3 c = meshAExtruder.vertices[meshAExtruder.triangles[i + 2]];
                
                nvVertices.AddRange(new []{a, b, c, a, b, c});
                
                nvTriangles.AddRange(new []
                {
                    i, i + 1, i + 2,                    
                    
                    i, i + 2, i + 3,
                    i + 2, i + 5, i,
                    
                    i + 1, i + 3, i + 4,
                    i, i + 3, i + 1,
                    
                    i + 2, i + 4, i + 5,
                    i + 1, i + 4, i + 2
                });
            }
            
            Mesh meshExtrudable = new Mesh()
            {
                vertices = nvVertices.ToArray(),
                triangles = nvTriangles.ToArray()
            };
                
            meshExtrudable.RecalculateNormals();
            return meshExtrudable;
        }
        */

        private static List<Mesh> DiviserEnTuilesTriangulairesExtrudables(Mesh meshAdiviser)
        {
            List<Mesh> tuiles = new List<Mesh>();

            for (int i = 0; i < meshAdiviser.triangles.Length; i += 3)
            {
                Mesh nvMesh = new Mesh
                {
                    vertices = new[]
                    {
                        meshAdiviser.vertices[meshAdiviser.triangles[i]],
                        meshAdiviser.vertices[meshAdiviser.triangles[i + 1]],
                        meshAdiviser.vertices[meshAdiviser.triangles[i + 2]],
                        
                        meshAdiviser.vertices[meshAdiviser.triangles[i + 1]],
                        meshAdiviser.vertices[meshAdiviser.triangles[i]],
                        Vector3.zero,
                        
                        meshAdiviser.vertices[meshAdiviser.triangles[i + 2]],
                        meshAdiviser.vertices[meshAdiviser.triangles[i + 1]],
                        Vector3.zero,
                        
                        meshAdiviser.vertices[meshAdiviser.triangles[i]],
                        meshAdiviser.vertices[meshAdiviser.triangles[i + 2]],
                        Vector3.zero
                    },
                    triangles = new[]
                    {
                        0, 1, 2,
                        3, 4, 5,
                        6, 7, 8,
                        9, 10, 11
                    },
                    name = $"tuile {i}"
                };

                nvMesh.RecalculateNormals();
                
                tuiles.Add(nvMesh);
            }

            return tuiles;
        }

        public static Mesh GenererMeshAgrandi(Mesh meshAAgrandir, float multiplicateur)
        {
            Vector3[] vertices = meshAAgrandir.vertices;
            int[] triangles = meshAAgrandir.triangles;
            
            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i] *= multiplicateur;
            }

            Mesh meshAgrandi = new Mesh
            {
                vertices = vertices,
                triangles = triangles,
                name = meshAAgrandir.name
            };
            
            meshAgrandi.RecalculateNormals();
            return meshAgrandi;
        }
    }
}