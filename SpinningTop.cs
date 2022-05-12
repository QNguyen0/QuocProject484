using System;
namespace Sim
{    
    public class SpinningTop : Integrator
    {

        private double L=0.8; // length
        private double g=9.81; // gravity
        private double m=2.0; //mass
        //private double R=0.6; //Radius
        private double I1=0.18;   // Principal moments of inertia
        private double I10=1.46; 
        private double I2=0.36;
        private double I3=0.18;
        private double I30=1.46; 

        //--------------------------------------------------------------------
        // constructor
        //--------------------------------------------------------------------
        public SpinningTop() : base(7)
        {
            
            
            x[0] = 0.92388;    // q0
            //x[0] = 1;    // q0
            x[1] = 0.0;    // q1
            x[2] = 0.0;    // q2
            x[3] = Math.Sqrt(1-(x[0]*x[0]));    // q3
            //x[3] = 0.0;    // q3
            x[4] = 0.0;    // u1   body angular velocity
            x[5] = 35;   // u2   body angular velocity
            x[6] = 0.0;    // u3   body angular velocity
            
            integratorInit(rhsFunc);
        }

        //--------------------------------------------------------------------
        // rhsFunc: function which calculates the right side of the
        //          differential equation.
        //--------------------------------------------------------------------
        public void rhsFunc(double[] st, double[] ff)
        {
            double Q0 = st[0];
            double Q1 = st[1];
            double Q2 = st[2];
            double Q3 = st[3];

            double u1 = st[4];
            double u2 = st[5];
            double u3 = st[6];

            // kinematic differential equations
            ff[0] = 0.5*(-Q1*u1 - Q2*u2 - Q3*u3);
            ff[1] = 0.5*( Q0*u1 - Q3*u2 + Q2*u3);
            ff[2] = 0.5*( Q3*u1 + Q0*u2 - Q1*u3);
            ff[3] = 0.5*(-Q2*u1 + Q1*u2 + Q0*u3);
    
            // kinetics
            ff[4] = ((I2 - I30)*u2*u3+2*m*g*L*(Q0*Q1-Q2*Q3))/I10;
            ff[5] = (I30 - I10)*u1*u3/I2;
            ff[6] = ((I10 - I2)*u1*u2+2*m*g*L*(Q0*Q3+Q1*Q2))/I30;

        }

        //--------------------------------------------------------------------
        // getters/setters
        //--------------------------------------------------------------------
        public double q0
        {
            get{ return x[0]; }
            set{ x[0] = value; }
        }

        public double q1
        {
            get{ return x[1]; }
            set{ x[1] = value; }
        }
        public double q2
        {
            get{ return x[2]; }
            set{ x[2] = value; }
        }
        public double q3
        {
            get{ return x[3]; }
            set{ x[3] = value; }
        }
        public double omega1
        {
            get{ return x[4]; }

            set{ x[4] = value; }
        }

        public double omega2
        {
            get{ return x[5]; }

            set{ x[5] = value; }
        }

        public double omega3
        {
            get{ return x[6]; }

            set{ x[6] = value; }
        }
        public double IG1
        {
            get{ return I1; }

            set
            {
                if(value >= 0.1)
                    I1 = value;
            }
        }

        public double IG10
        {
            get{ return I10; }

            set
            {
                if(value >= 0.1)
                    I10 = value;
            }
        }
        public double IG2
        {
            get{ return I2; }

            set
            {
                if(value >= 0.1)
                    I2 = value;
            }
        }
        public double IG3
        {
            get{ return I3; }

            set
            {
                if(value >= 0.1)
                    I3 = value;
            }
        }

        public double IG30
        {
            get{ return I30; }

            set
            {
                if(value >= 0.1)
                    I30 = value;
            }
        }
    }

}
