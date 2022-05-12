//============================================================================
// Weeble.cs  
// Class for defining the dynamics of the weeble wooble
//============================================================================

using System;

namespace Sim
{    
    public class Weeble : Integrator
    {
        private double g;   // gravitational field strength
        private double m;   // mass
        private double IG;  // moment of inertia
        private double d;   // Center of mass location
        private double R;   // Radius

        //--------------------------------------------------------------------
        // constructor
        //--------------------------------------------------------------------
        public Weeble() : base(2)
        {
            // Default values of parameters
            g = 9.81; 
            R=1;
            m=1;
            d=(4*R)/(3*(Math.PI)); //Center of mass location for half circle
            IG=(1/4)*((Math.PI)*(R*R*R*R)); //Moment of inertia of a half circle

            // set initial conditions
            x[0] = 1.0;     // theta
            x[1] = 0.0;     // thetaDot

            integratorInit(rhsFunc);
        }

        //--------------------------------------------------------------------
        // rhsFunc: function which calculates the right side of the
        //          differential equation.
        //--------------------------------------------------------------------
        public void rhsFunc(double[] st, double[] ff)
        {
            ff[0] = st[1];
            ff[1] = (-(d*Math.Cos(st[0]))*(d*Math.Sin(st[0]))*(st[1]*st[1])*m
            -m*g*d*Math.Sin(st[0])-(R-(d*Math.Cos(st[0])))*(d*Math.Sin(st[0]))*(st[1]*st[1])*m)
            /(IG+d*Math.Sin(st[0])*d*Math.Sin(st[0])*m+(R-d*Math.Cos(st[0]))*(R-d*Math.Cos(st[0]))*m);
        }

        //--------------------------------------------------------------------
        // getters & setters
        //--------------------------------------------------------------------

        public double Grav
        {
            get { return g; }
            set 
            {
                if( value >= 0.0) 
                    g = value; 
            }
        }

        public double theta
        {
            get { return x[0]; }
            set { x[0] = value; }
        }

        public double thetaDot
        {
            get { return x[1]; }
            set { x[1] = value; }
        }

        public double Radius
        {
            get { return R; }
            set { R = value; }
        }

        public double Mass
        {
            get { return m; }
            set { m = value; }
        }
    }
}
