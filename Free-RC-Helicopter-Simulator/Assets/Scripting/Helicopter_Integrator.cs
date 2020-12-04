// ############################################################################
// Free RC helicopter Simulator
// 20.01.2020 
// Copyright (c) zulu
// Source: https://joinerda.github.io/Solving-ODEs-in-Unity/
//
// Unity c# code
// ############################################################################

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Helicopter_Integrator is an abstract class for integrating a system of ODEs
/// </summary>
abstract public class Helicopter_Integrator  {

	int nEquations;
	double [] store;
	double [] k1;
	double [] k2;
	double [] k3;
	double [] k4;
	double [] ym1;
	double [] ym2;
	double [] ym3;
	double [] P;
	double [] dm1;
	double [] dm2;
	double [] dm3;
	double [] dp1;
	int abmSteps =0;
	double abmRms2;

	public Helicopter_Integrator() {
		Init (1);
	}

	/// <summary>
	/// Allocate memory for all storage arrays and set number of equations
	/// </summary>
	/// <param name="nEquations">N equations.</param>
	public void Init (int nEquations) {
		// set up temp arrays
		this.nEquations = nEquations;
		store = new double[nEquations];
		k1 = new double[nEquations];
		k2 = new double[nEquations];
		k3 = new double[nEquations];
		k4 = new double[nEquations];
		ym1 = new double[nEquations];
		ym2 = new double[nEquations];
		ym3 = new double[nEquations];
		P = new double[nEquations];
		dm1 = new double[nEquations];
		dm2 = new double[nEquations];
		dm3 = new double[nEquations];
		dp1 = new double[nEquations];
		abmSteps = 0;
	}

	/// <summary>
	/// Abstract void, override this method to set the ODEs to be
	/// integrated.
	/// </summary>
	/// <param name="x">The values being integrated.</param>
	/// <param name="xdot">The derivatives being calculated.</param>
	abstract public void ODE(int integrator_function_call_number, double[] x, double[] u, double[] xdot, double t , double dt);

	/// <summary>
	/// Step forward using Euler's method
	/// </summary>
	/// <param name="x">The values being integrated.</param>
	/// <param name="h">The time step.</param>
	public void EulerStep(double [] x, double[] u, double t, double h) {
        int integrator_function_call_number = 0;
        ODE(integrator_function_call_number++, x, u, k1, t, h);
		for(int i=0;i<nEquations;i++) {
			x[i] += k1[i]*h;
		}
	}

	/// <summary>
	/// Step forward using 4th order Runge Kutta method
	/// </summary>
	/// <param name="x">The values being integrated.</param>
	/// <param name="h">The time step.</param>
	public double RK4Step(double [] x, double[] u, double t, double h) {
        int integrator_function_call_number = 0;
        ODE (integrator_function_call_number++, x ,u, k1, t , h);
		for (int i = 0; i < nEquations; i++) {
			store [i] = x [i] + k1 [i] * h / 2.0;
		}
        ODE(integrator_function_call_number++, store, u, k2, t, h);
		for (int i = 0; i < nEquations; i++) {
			store [i] = x [i] + k2 [i] * h / 2.0;
		}
        ODE(integrator_function_call_number++, store, u, k3, t, h);
		for (int i = 0; i < nEquations; i++) {
			store [i] = x [i] + k3 [i] * h;
		}
        ODE(integrator_function_call_number++, store, u, k4, t, h);
		for (int i = 0; i < nEquations; i++) {
			x [i] = x [i] + (k1[i] +2.0*k2[i]+ 2.0*k3 [i]+k4[i]) * h/6.0;
		}
		return t + h;
	}


	/**
	 * Calculates a single step using Adams Bashforth Moulton,
	 * 
	 * @param x Array of values being integrated.
	 * @param t Time at which step begins
	 * @param h Duration of step
	 * @return Error prediction at end of step
	 */
	public double abmStep(double [] x, double[] u, double t, double h) {
		abmRms2 = 0.0;
        int integrator_function_call_number=0;
		if(abmSteps==0) {
			for(int i=0;i<x.Length;i++) {
				ym3[i] = x[i];
				ym2[i] = x[i];
			}
			ODE(integrator_function_call_number++, dm3, u, ym3,t,h);
			t = RK4Step(ym2, u, t, h);
			ODE(integrator_function_call_number++, dm2, u, ym2, t,h);
			for(int i=0;i<x.Length;i++) {
				x[i] = ym2[i];
			}
			abmSteps+=1;
			return 1.0;
		} else if(abmSteps==1) {
			for(int i=0;i<x.Length;i++) {
				ym1[i] = ym2[i];
			}
			t = RK4Step(ym1, u, t,  h);
			ODE(integrator_function_call_number++, dm1, u, ym1, t, h);
			for(int i=0;i<x.Length;i++) {
				x[i] = ym1[i];
			}
			abmSteps +=1;
			return 1.0;
		} else {
			ODE(integrator_function_call_number++, k1, u, x, t, h);
			for(int i=0;i<x.Length;i++) {
				P[i] = x[i] + (h/24.0)*
					(55.0*k1[i]-59.0*dm1[i]+37.0*dm2[i]-9.0*dm3[i]);
			}
			ODE(integrator_function_call_number++, dp1, u, P, t+h, h);
			abmRms2 = 0.0;
			for(int i=0;i<x.Length;i++) {
				store[i] = x[i];
				x[i] += (h/24.0)*(9*dp1[i]+19.0*k1[i]-5.0*dm1[i]+dm2[i]);
				dm3[i] = dm2[i];
				dm2[i] = dm1[i];
				dm1[i] = k1[i];
				ym3[i] = ym2[i];
				ym2[i] = ym1[i];
				ym1[i] = store[i];
				abmRms2 += (x[i]-P[i])*(x[i]-P[i])/(x[i]+P[i])/(x[i]+P[i]);
			}
			abmRms2 /= x.Length;
			if(abmSteps<5) abmSteps += 1;
			return t+h;
		}
	}

	public double abmError() {
		return abmRms2;
	}

}