//============================================================================
// SpinningTopModel.cs  Code for the graphical kinematic model of the SpinningTop.
//============================================================================
using Godot;
using System;

public class SpinningTopModel : Spatial
{
    private Transform tr;     // reference to a Transform object
    private Vector3 cgLoc;    // location of the center of mass
    private MeshInstance plate;     // mesh for the plate
    private CylinderMesh plateMesh;  // reference to the plate mesh
    private Vector3 PlateLoc;

    private MeshInstance rod;     // mesh for the rod
    private CylinderMesh rodMesh;  // reference to the rod mesh
    private Vector3 RodLoc;

    private float Radius=1.0f;   //Radius of the spinning top
    private float Length=2.0f;   
    private float HLength;       
    

    //------------------------------------------------------------------------
    // _Ready: Called when the node enters the scene tree for the first time.
    //------------------------------------------------------------------------


    public override void _Ready()
    {

        HLength= (float)Length/2;
        PlateLoc = new Vector3(0.0f, HLength, 0.0f);

        plate = GetNode<MeshInstance>("Plate");
        plate.Translation = PlateLoc;
        plateMesh = (CylinderMesh)plate.Mesh;
        plateMesh.TopRadius = Radius;
        plateMesh.BottomRadius = Radius;
        

        RodLoc = new Vector3(0.0f, HLength, 0.0f);

        rod = GetNode<MeshInstance>("Rod");
        rod.Translation = RodLoc;
        rodMesh = (CylinderMesh)rod.Mesh;
        rodMesh.Height= Length;
        
        tr = new Transform();
        cgLoc = new Vector3(0.0f,0.0f,0.0f);
        tr.basis = Basis.Identity;
        tr.origin = cgLoc;


    }

    //------------------------------------------------------------------------
    // setCGLoc: Set location of the CG
    //------------------------------------------------------------------------
    public void setCGLoc(Vector3 org)
    {
        cgLoc.x = org.x;
        cgLoc.y = org.y;
        cgLoc.z = org.z;

        tr.origin = cgLoc;
        Transform = tr;
    }

    //------------------------------------------------------------------------
    // setOrientation:
    //------------------------------------------------------------------------
    public void setOrientation(Quat q)
    {
        tr.basis = new Basis(q);
        //tr.origin = cgLoc;
        Transform  = tr;

        //Translation = new Vector3(0.0f,0.0f,0.0f);
    }


    //public float R
    //{
        //get{ return Radius; }
        //set{ Radius = value; }
    //}


}
