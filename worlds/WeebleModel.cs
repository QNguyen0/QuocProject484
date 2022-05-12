//============================================================================
// PendModel.cs   Geometric model for the Weeble Wooble
//============================================================================
using Godot;
using System;

public class WeebleModel : Spatial
{
    private float angle = 0.0f;  // Weeble angle
    private Vector3 pivotEulerAngles; // angle for the pivot


    //------------------------------------------------------------------------
    // _Ready: Called when the node enters the scene tree for the first time.
    //------------------------------------------------------------------------
    public override void _Ready()
    {
        pivotEulerAngles = new Vector3(0.0f, 0.0f, angle);
        Rotation = pivotEulerAngles;

    }


    //------------------------------------------------------------------------
    // Setter for theta, Weeble angle
    //------------------------------------------------------------------------
    public float theta
    {
        get {return angle;}
        set { 
            angle = value;
            pivotEulerAngles.z = angle;
            Rotation = pivotEulerAngles;
        }
    }


}
