using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace MetaHexa.Debug
{
    public class StructTests : MonoBehaviour
    {
        [SerializeField] private bool _returnValue;

        [SerializeField] private bool _refresh;
        
        private enum ComparisonType
        {
            ReferenceEquale,
            ValueEquale
        }

        private struct MyStruct
        {
            public Code code;
        }

        private class Code
        {
            public Vector3[] traits = new Vector3[40];
        }
        
        private void OnValidate()
        {
            if (_refresh)
            {
                _refresh = false;
                
                Tests();
            }
            
            
        }

        private void Tests()
        {
            MyStruct structTest = new MyStruct();
            print(Marshal.SizeOf(structTest));
        }

        private void ModifieAllVectors(Vector3[] vectors)
        {
            for (int i = 0; i < vectors.Length; i++)
            {
                vectors[i].x = 3;
            }
        }
    }
}