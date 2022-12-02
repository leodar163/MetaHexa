using System.Collections.Generic;
using UnityEngine;

namespace Tuile.Graphics.Data
{
    [CreateAssetMenu(fileName = "nvTuileMeshData", menuName = "Datas/Tuie Mesh Data")]
    public class TuileMeshData : ScriptableObject
    {
        [SerializeField] private List<Mesh> _meshesTuileHexa;

        [SerializeField] private Mesh _meshTrituile;

        public Mesh meshTrituile
        {
            get
            {
                if (!_meshTrituile) _meshTrituile = MeshsTuiles.GenerateTrituileMesh();
                return _meshTrituile;
            }
        }
        
        public Mesh RecupMeshTuileHexa(int index)
        {
            if (index >= _meshesTuileHexa.Count)
            {
                _meshesTuileHexa.AddRange(new Mesh[index + 1 - _meshesTuileHexa.Count]);
            }

            if (!_meshesTuileHexa[index])
            {
                _meshesTuileHexa[index] = MeshsTuiles.GenererMeshTuileHexa();
            }
            return _meshesTuileHexa[index];
        }
    }
}