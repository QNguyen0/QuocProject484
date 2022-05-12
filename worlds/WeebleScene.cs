//============================================================================
// WeebleScene.cs  Main code for the Weeble scene.
//============================================================================
using Godot;
using Sim;
using System;

public class WeebleScene : Node
{

    private Weeble w;
    private float thetaIC = 1.0f;    // init condition angle for Weeble Wooble
    private float thetaDotIC = 0.0f; // initial rotation rate
    private WeebleModel wmodel;      // reference to the Weeble Wooble model
    private SpheriCam sCam;        // reference to the sphericam

    //------------------------------------------------------------------------
    // _Ready: Called when the node enters the scene tree for the first time.
    //------------------------------------------------------------------------
    public override void _Ready()
    {
        // presumably, we would read these parameters from a file
        thetaIC = 1.0f;
        thetaDotIC = 0.0f;

        float longitudeDeg = 0.0f;   // sphericam angles
        float latitudeDeg = 10.0f;

        // Set up the the geometric/kinematic model
        wmodel = GetNode<WeebleModel>("WeebleModel");
        Vector3 pivotLoc = new Vector3(0.0f, 0.5f, 0.0f);
        wmodel.Translation = pivotLoc;


        wmodel.theta = thetaIC;

        // Set up the SpheriCam
        sCam = GetNode<SpheriCam>("SpheriCam");
        sCam.Translation = pivotLoc;
        sCam.LongitudeDeg = longitudeDeg;
        sCam.LatitudeDeg = latitudeDeg;

        // set up the Weeble Wooble simulation
        w = new Weeble();
        w.theta = (double)thetaIC;
        w.thetaDot = (double)thetaDotIC;

    }

    //------------------------------------------------------------------------
    // _PhysicsProcess: 
    //------------------------------------------------------------------------
    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);

        w.step((double)delta);

        wmodel.theta = (float)w.theta;

    }

}
