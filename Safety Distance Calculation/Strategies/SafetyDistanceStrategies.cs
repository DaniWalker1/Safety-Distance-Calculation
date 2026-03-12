using Safety_Distance_Calculation.Exceptions;
using System;

namespace Safety_Distance_Calculation.Strategies
{
    
    /// Defines the strategy for calculating the minimum safety distance (S).
    
    public interface ISafetyDistanceStrategy
    {
        
        /// Calculates the safety distance (S) based on response time, resolution, and other factors.
        
        /// <param name="t">Overall system stopping performance (T) in seconds.</param>
        /// <param name="d">Detection capability (resolution) in mm.</param>
        /// <param name="additionalParameter">Height (H) for parallel, or Angle for angular approaches.</param>
        /// <returns>Safety distance (S) in mm.</returns>
        double Calculate(double t, double d, double additionalParameter = 0);
    }

    
    /// ISO 13855 Section 6.2: Perpendicular approach to the detection zone.
    
    public class PerpendicularApproachStrategy : ISafetyDistanceStrategy
    {
        public double Calculate(double t, double d, double additionalParameter = 0)
        {
            if (t <= 0 || d <= 0)
                throw new SafetyDistanceException("Time (T) and Resolution (d) must be greater than zero.");

            double k = 2000.0; // Initial approach speed (mm/s)
            double c = d <= 40 ? Math.Max(0, 8 * (d - 14)) : 850.0;

            double s = (k * t) + c;

            // ISO 13855 logic: If S > 500mm with K=2000, recalculate with K=1600
            if (d <= 40 && s > 500)
            {
                k = 1600.0;
                s = (k * t) + c;
                s = Math.Max(500, s); // S cannot be less than 500mm when recalculated
            }

            return s;
        }
    }

    
    /// ISO 13855 Section 6.3: Parallel approach to the detection zone.
    
    public class ParallelApproachStrategy : ISafetyDistanceStrategy
    {
        public double Calculate(double t, double d, double h)
        {
            if (t <= 0)
                throw new SafetyDistanceException("Time (T) must be greater than zero.");

            if (h < 0 || h > 1000)
                throw new SafetyDistanceException("Height (H) must be between 0 and 1000 mm according to ISO 13855.");

            if (h > 300)
            {
                // In a real application, this should log a warning or flag a UI indicator.
                Console.WriteLine("WARNING: H > 300 mm. There is a risk of crawling under the detection zone (ISO 13855). Additional measures required.");
            }

            double k = 1600.0; // Approach speed for parallel is generally 1600 mm/s
            double c = 1200 - (0.4 * h);

            c = Math.Max(850, c); // Minimum acceptable C is 850 mm

            return (k * t) + c;
        }
    }

    
    /// ISO 13855 Section 6.4: Angled approach to the detection zone.
    
    public class AngularApproachStrategy : ISafetyDistanceStrategy
    {
        private readonly ISafetyDistanceStrategy _perpendicular = new PerpendicularApproachStrategy();
        private readonly ISafetyDistanceStrategy _parallel = new ParallelApproachStrategy();

        public double Calculate(double t, double d, double angleDegrees)
        {
            if (angleDegrees > 30)
            {
                return _perpendicular.Calculate(t, d);
            }
            else if (angleDegrees < 5)
            {
                // Assuming H is derived elsewhere, defaulting to 0 for strict parallel boundary test
                return _parallel.Calculate(t, d, 0);
            }
            else
            {
                // Intermediate angles treated as perpendicular as requested
                return _perpendicular.Calculate(t, d);
            }
        }
    }
}
