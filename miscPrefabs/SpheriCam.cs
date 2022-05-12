//============================================================================
// SpheriCam.cs  Prefab for placing and orienting a camera using spherical
//               coordinates
//============================================================================
using Godot;
using System;

public class SpheriCam : Spatial
{
    private float radius = 3.0f;   // radius of the sphere
    private float longitude = 0.0f;  // longitude angle in radians
    private float latitude = 0.0f;  // latitude angle in radians
    private Vector3 sphAngles;      // container for the longit. & lat angles
    private Camera cam;             // reference to the camera; why?
    private Spatial target;         // target axes

    //------------------------------------------------------------------------
    // _Ready: Called when the node enters the scene tree for the first time.
    //------------------------------------------------------------------------
    public override void _Ready()
    {
        // GD.Print("Inside _Ready");
        
        target = GetNode<Spatial>("Target");
        targetHide();

        sphAngles = new Vector3(-latitude, longitude, 0.0f);
        setSphericalAngles();
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

    //------------------------------------------------------------------------
    // setSphericalAngles: sets the longitude and latitude angles for the 
    //                     camera.
    //------------------------------------------------------------------------
    private void setSphericalAngles()
    {
        Rotation = sphAngles;
    }

    //------------------------------------------------------------------------
    // targetShow/Hide
    //------------------------------------------------------------------------
    public void targetShow()
    {
        target.Visible = true;
    }

    public void targetHide()
    {
        target.Visible = false;
    }

    //------------------------------------------------------------------------
    // setter for longitude, but in degrees
    //------------------------------------------------------------------------
    public float LongitudeDeg
    {
        set
        {
            longitude = Mathf.Deg2Rad(value);
            sphAngles.y = longitude;
            setSphericalAngles();
        }
    }

    //------------------------------------------------------------------------
    // setter for latitude, but in degrees
    //------------------------------------------------------------------------
    public float LatitudeDeg
    {
        get{ return(Mathf.Rad2Deg(latitude)); }

        set
        {
            float v = value;
            if(v > 90.0f)
                v = 90.0f;
            else if(v < -90.0f)
                v = -90.0f;
            
            latitude = Mathf.Deg2Rad(v);
            sphAngles.x = -latitude;
            setSphericalAngles();
        }
    }
}
