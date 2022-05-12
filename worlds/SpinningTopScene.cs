//============================================================================
// SpinningTopScene.cs  Main code for the Spinning Top scene.
//============================================================================
using Godot;
using Sim;
using System;

public class SpinningTopScene : Node
{
    private SpinningTop b;            // simulation of the SpinningTop
    

    // Initial conditions 
    [Export]
    float q0IC=0.0f;
    [Export]
    float q1IC=0.0f;
    [Export]
    float q2IC=0.0f;
    [Export]
    float q3IC=0.0f;

    // Initial components of angular velocity about body axes
    [Export]
    float omega1_IC = 0f;
    [Export]
    float omega2_IC = 0f;
    [Export]
    float omega3_IC = 0.0f;

    // Principal moments of inertia about each of the principal axes
    float IG1;
    float IG10;
    float IG2;
    float IG3;
    float IG30; 

    Quat bQuat;

    private SpinningTopModel bmodel;
    private SpheriCam sCam;        // reference to the sphericam

    //------------------------------------------------------------------------
    // _Ready: Called when the node enters the scene tree for the first time.
    //------------------------------------------------------------------------
    public override void _Ready()
    {

        IG1 = 0.18f;
        IG10 = 1.46f;
        IG2 = 0.36f;
        IG3 = 0.18f;
        IG30 = 1.46f;
        
        float pivotHeight = 0.5f;


        Vector3 pivotLoc = new Vector3(0.0f,pivotHeight,0.0f);
        float longitudeDeg = 30.0f;   // sphericam angles
        float latitudeDeg = 15.0f;
        
        // Set up the the geometric/kinematic model
        bmodel = GetNode<SpinningTopModel>("SpinningTopModel");
        bmodel.setCGLoc(pivotLoc);

        // Set up the SpheriCam
        sCam = GetNode<SpheriCam>("SpheriCam");
        sCam.Translation = pivotLoc;
        sCam.LongitudeDeg = longitudeDeg;
        sCam.LatitudeDeg = latitudeDeg;

        // Set up simulation
        b = new SpinningTop();
        b.IG1 = (double)IG1;
        b.IG10 = (double)IG1;
        b.IG2 = (double)IG2;
        b.IG3 = (double)IG3;
        b.IG30 = (double)IG30;

        b.q0 = (double)q0IC;
        b.q1 = (double)q1IC;
        b.q2 = (double)q2IC;
        b.q3 = (double)q3IC;

        b.omega1 = (double)omega1_IC;
        b.omega2 = (double)omega2_IC;
        b.omega3 = (double)omega3_IC;

        bQuat = new Quat();
        bQuat = Quat.Identity;
        bmodel.setOrientation(bQuat);
    }


    //------------------------------------------------------------------------
    // _PhysicsProcess:
    //------------------------------------------------------------------------
    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);

        b.step((double)delta);

        bQuat.w = (float)b.q0;
        bQuat.x = (float)b.q1;
        bQuat.y = (float)b.q2;
        bQuat.z = (float)b.q3;

        bmodel.setOrientation(bQuat);
        
    }
}
