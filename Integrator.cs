//============================================================================
// Integrator.cs   Class for performing RK4 integration of differential
//                 equations.
//============================================================================

using System;

namespace Sim
{
    public class Integrator
    {
        protected int n;     // number of differential equations    
        protected double[] x;          // array of states
        protected double[] xi;         // array of intermediate states
        protected double[][] f;        // rhs of ode evaluated

        private Action<double[], double[]> rhs;

        bool initialized;              // whether integrator is ready to sim

        //--------------------------------------------------------------------
        // constructor
        //--------------------------------------------------------------------
        public Integrator(int nn)
        {
            n = nn;

            x = new double[n];
            xi = new double[n];
            f = new double[4][];
            f[0] = new double[n];
            f[1] = new double[n];
            f[2] = new double[n];
            f[3] = new double[n];

            rhs = dumFunc;
            initialized = false;
        }
    
        //--------------------------------------------------------------------
        // IntegratorInit: 
        //--------------------------------------------------------------------
        protected void integratorInit(Action<double[], double[]> rhhs)
        {
            rhs = rhhs;
            initialized = true;
        }

        //--------------------------------------------------------------------
        // step: takes one RK4 step
        //--------------------------------------------------------------------
        public void step(double dt)
        {
            if(!initialized){
                //################# Need to throw exception here
            }

            int i;
            double dtByTwo = 0.5*dt;
            double dtBySix = dt/6.0;

            // take first RK4 substep
            rhs(x,f[0]);
            for(i=0;i<n;++i){
                xi[i] = x[i] + f[0][i]*dtByTwo;
            }

            // take second RK4 substep
            rhs(xi,f[1]);
            for(i=0;i<n;++i){
                xi[i] = x[i] + f[1][i]*dtByTwo;
            }

            // take third RK4 substep
            rhs(xi,f[2]);
            for(i=0;i<n;++i){
                xi[i] = x[i] + f[2][i]*dt;
            }

            // take final RK4 substep
            rhs(xi,f[3]);
            for(i=0;i<n;++i){
                x[i] = x[i] + (f[0][i] + 2.0*f[1][i] + 2.0*f[2][i] +
                    f[3][i])*dtBySix;
            }
        }

        //--------------------------------------------------------------------
        // initialize rhs to this to get rid of warning
        //--------------------------------------------------------------------
        private void dumFunc(double[] a, double[] b)
        {

        }
    }
}