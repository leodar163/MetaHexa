using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace MetaHexa.Debug
{
    public class TailleVariables : MonoBehaviour
    {
    
        void Update()
        {
            if (Input.GetKeyUp(KeyCode.A))
            {
                Vector3Int vector = new Vector3Int();
                List<int> list = new List<int>() {0,0,0};
            
                print("taille vecteur = " + Marshal.SizeOf(vector));
                print("taille List = " + Marshal.SizeOf(list));
            }
        }
    }
}
