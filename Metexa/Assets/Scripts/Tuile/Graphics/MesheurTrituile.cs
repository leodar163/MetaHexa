using System;
using Graphics;
using UnityEngine;

namespace Tuile.Graphics
{
    public class MesheurTrituile : Mesheur
    {
        
        protected override Mesh MeshAMesher => MeshsTuiles.TriTuile;
        
    }
}