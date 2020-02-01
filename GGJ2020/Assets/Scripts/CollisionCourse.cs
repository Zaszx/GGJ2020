using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCourse
{
    // Start is called before the first frame update

    public static Vector3 CalculateInterceptionPoint(Vector3 RunnerPosition, Vector3 ChaserPosition, Vector3 RunnerVelocity, float ChaserSpeed)
    {
        Vector3 vectorFromRunner = ChaserPosition - RunnerPosition;
        float distanceToRunner = vectorFromRunner.magnitude;
        double m_timeToInterception;
        Vector3 m_interceptionPosition;
        //Vector3 m_chaserVelocity;

        // Now set up the quadratic formula coefficients
        float runnerSpeed = RunnerVelocity.magnitude;
        double a = ChaserSpeed * ChaserSpeed - runnerSpeed * runnerSpeed;
        double b = 2 * Vector3.Dot(vectorFromRunner,RunnerVelocity);
        double c = -distanceToRunner * distanceToRunner;

        double t1, t2;
        if (!SolveQuadratic(a, b, c, out t1, out t2))
        {
            // No real-valued solution, so no interception possible
            Debug.Log("Quadratic Solution not found in CollisionCourse case 1");
            return new Vector3(0, 0, 0);
        }

        if (t1 < 0 && t2 < 0)
        {
            // Both values for t are negative, so the interception would have to have
            // occured in the past
            Debug.Log("Quadratic Solution not found in CollisionCourse case 2");
            return new Vector3(0, 0, 0);
        }

        if (t1 > 0 && t2 > 0) // Both are positive, take the smaller one
            m_timeToInterception = System.Math.Min(t1, t2);
        else // One has to be negative, so take the larger one
            m_timeToInterception = System.Math.Max(t1, t2);

        m_interceptionPosition = RunnerPosition + RunnerVelocity * (float)m_timeToInterception;
        return m_interceptionPosition;

        // Calculate the resulting velocity based on the time and intercept position
        /*m_chaserVelocity = (m_interceptionPosition - ChaserPosition) * (float)(1/m_timeToInterception);
        return m_chaserVelocity;*/
    }



    public static bool SolveQuadratic(double a, double b, double c, out double x1, out double x2)
    {
        //Quadratic Formula: x = (-b +- sqrt(b^2 - 4ac)) / 2a

        //Calculate the inside of the square root
        double insideSquareRoot = (b * b) - 4 * a * c;

        if (insideSquareRoot < 0)
        {
            //There is no solution
            x1 = double.NaN;
            x2 = double.NaN;
            return false;
        }
        else
        {
            //Compute the value of each x
            //if there is only one solution, both x's will be the same
            double sqrt = System.Math.Sqrt(insideSquareRoot);
            x1 = (-b + sqrt) / (2 * a);
            x2 = (-b - sqrt) / (2 * a);
            return true;
        }
    }
}
