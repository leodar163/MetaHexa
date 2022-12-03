using System;
using System.Collections.Generic;
using Graphics;
using UnityEngine;

namespace Tuile.Graphics.Data
{
    [CreateAssetMenu(fileName = "nvTuileMeshData", menuName = "Datas/Tuie Mesh Data")]
    public class TuileMeshData : ScriptableObject
    {
        [SerializeField] private List<MeshData> _meshDatasTuileHexa = new List<MeshData>();

        [SerializeField] private Mesh _meshTrituile;

        public Mesh MeshDataTrituile
        {
            get
            {
                if (!_meshTrituile)
                {
                    _meshTrituile = MeshsTuiles.GenerateTrituileMesh();
                    Debug.Log("un nouveau mesh trituile a été généré");
                    
                    #if UNITY_EDITOR
                    UnityEditor.AssetDatabase.CreateAsset(_meshTrituile, "Assets/Graphisme/Meshs/Trituile.asset");
                    #endif
                    
                    return _meshTrituile;
                }
                return _meshTrituile;
            }
        }

        public Mesh RecupMeshTuileHexa(int index)
        {
            if (index < 0) throw  new ArgumentOutOfRangeException("l'argument \"index\" ne doit pas " +
                                                                  "être inférieur à 0");
            
            if (index >= _meshDatasTuileHexa.Count)
            {
                _meshDatasTuileHexa.AddRange(new MeshData[index + 1 - _meshDatasTuileHexa.Count]);
            }

            if (!_meshDatasTuileHexa[index].aEteInit)
            {
                Mesh meshRetour = MeshsTuiles.GenererMeshTuileHexa();
                _meshDatasTuileHexa[index] = new MeshData(meshRetour);
                return meshRetour;
            }
            return _meshDatasTuileHexa[index].ConvertirEnMesh();
        }

        public void SauvegarderMeshTuileHexa(Mesh meshASauvegarder, int index)
        {
            if (index < 0) throw  new ArgumentOutOfRangeException("l'argument \"index\" ne doit pas " +
                                                                  "être inférieur à 0");
            
            if (index >= _meshDatasTuileHexa.Count)
            {
                _meshDatasTuileHexa.AddRange(new MeshData[index + 1 - _meshDatasTuileHexa.Count]);
            }

            _meshDatasTuileHexa[index] = new MeshData(meshASauvegarder);
        }
    }
}