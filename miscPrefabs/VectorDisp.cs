using Godot;
using System;

public class VectorDisp : Spatial
{
    private float mag;      // magnitude of vector

    private float sc; // multiplicative scale which converts val to length 

    private MeshInstance stem;     // mesh for the vector stem
    private CylinderMesh stemMesh;  // reference to the vector stem mesh  
    private Vector3 vecOffset;    

    //------------------------------------------------------------------------
    // Called when the node enters the scene tree for the first time.
    //------------------------------------------------------------------------
    public override void _Ready()
    {
        stem = GetNode<MeshInstance>("Stem");
        stemMesh = (CylinderMesh)stem.Mesh;
        stemMesh.TopRadius = 0.04f;
        stemMesh.BottomRadius = 0.04f;

        vecOffset = new Vector3(0.0f, 0.0f, 0.0f);
        calcVecLen(0.02f);

        sc = 1.0f;
    }

    //------------------------------------------------------------------------
    // calcVecLen:
    //------------------------------------------------------------------------
    private void calcVecLen(float val)
    {
        vecOffset.x = 0.5f * val;
        stem.Translation = vecOffset;
        stemMesh.Height = Mathf.Abs(val);
    }

    //------------------------------------------------------------------------
    // Setter for val, the magnitude of the vector displayed
    //------------------------------------------------------------------------
    public float val
    {
        set{
            mag = sc*value;
            calcVecLen(mag);
        }
    }

    public float scale
    {
        set{
            sc = value;
        }
    }
}
