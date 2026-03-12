using Safety_Distance_Calculation.Exceptions;
using System;

namespace Safety_Distance_Calculation.Validation
{
    
    /// Implements the logic from Table 2 of the ISO 13857:2019 standard (High Risk).
   
    public static class ReachOverValidator
    {
        // X-Axis: Height of the protective structure / guard (b) in mm
        private static readonly int[] GuardHeights = { 1000, 1200, 1400, 1600, 1800, 2000, 2200, 2400, 2500 };

        // Y-Axis: Height of the hazard zone (a) in mm
        private static readonly int[] HazardHeights = { 2700, 2600, 2400, 2200, 2000, 1800, 1600, 1400, 1200, 1000, 800, 600, 400, 200, 0 };

        // Matrix of required horizontal distances (c) in mm.
        // NOTE: I have populated the first few rows based on the official high-risk table.
        private static readonly int[,] DistanceMatrix = new int[,]
        {
            /* a \ b -> 1000, 1200, 1400, 1600, 1800, 2000, 2200, 2400, 2500 */
            /* 2700 */ { 0,    0,    0,    0,    0,    0,    0,    0,    0 },
            /* 2600 */ { 900,  800,  700,  600,  600,  500,  400,  300,  0 },
            /* 2400 */ { 1100, 1000, 900,  800,  700,  600,  400,  300,  0 },
            /* 2200 */ { 1300, 1200, 1000, 900,  800,  600,  400,  300,  0 },
            /* 2000 */ { 1400, 1300, 1100, 900,  800,  600,  400,  0,    0 },
            /* 1800 */ { 1500, 1400, 1100, 900,  800,  600,  0,    0,    0 },
            /* 1600 */ { 1500, 1400, 1100, 900,  800,  500,  0,    0,    0 },
            /* 1400 */ { 1500, 1400, 1100, 900,  800,  0,    0,    0,    0 },
            /* 1200 */ { 1500, 1400, 1100, 900,  700,  0,    0,    0,    0 },
            /* 1000 */ { 1500, 1400, 1000, 800,  0,    0,    0,    0,    0 },
            /* 800  */ { 1500, 1300, 900,  600,  0,    0,    0,    0,    0 },
            /* 600  */ { 1400, 1300, 800,  0,    0,    0,    0,    0,    0 },
            /* 400  */ { 1400, 1200, 400,  0,    0,    0,    0,    0,    0 },
            /* 200  */ { 1200, 900,  0,    0,    0,    0,    0,    0,    0 },
            /* 0    */ { 1100, 500,  0,    0,    0,    0,    0,    0,    0 }
        };

        /// Gets the minimum horizontal distance (c) required by ISO 13857.
        /// <param name="hazardHeight">Height of the hazard zone in mm (a).</param>
        /// <param name="guardHeight">Height of the protective structure in mm (b).</param>
        /// <returns>The required horizontal safety distance in mm.</returns>
        /// <exception cref="SafetyDistanceException">Thrown when the guard height is below the normative scope.</exception>
        public static int GetRequiredHorizontalDistance(int hazardHeight, int guardHeight)
        {
            // 1. Conservative rule for the Guard: Round DOWN to the nearest value in the table.
            int safeGuardHeight = GuardHeights.Where(h => h <= guardHeight).DefaultIfEmpty(-1).Max();

            if (safeGuardHeight == -1)
                throw new SafetyDistanceException("The guard height is less than 1000 mm. Table 2 of ISO 13857 does not apply for these heights (refer to C-type standards).");

            int colIndex = Array.IndexOf(GuardHeights, safeGuardHeight);

            // 2. Conservative rule for the Hazard: Evaluate adjacent rows if there is no exact match.
            var applicableRowIndices = new List<int>();

            for (int i = 0; i < HazardHeights.Length; i++)
            {
                if (HazardHeights[i] == hazardHeight)
                {
                    applicableRowIndices.Add(i);
                    break; 
                }

                // If the hazard height falls between two table values, we store both indices
                if (i < HazardHeights.Length - 1 && hazardHeight < HazardHeights[i] && hazardHeight > HazardHeights[i + 1])
                {
                    applicableRowIndices.Add(i);
                    applicableRowIndices.Add(i + 1);
                    break;
                }
            }

            // Handling out-of-bounds values (greater than 2700 or less than 0)
            if (!applicableRowIndices.Any())
            {
                if (hazardHeight > HazardHeights[0]) applicableRowIndices.Add(0);
                else applicableRowIndices.Add(HazardHeights.Length - 1);
            }

            // 3. From the applicable rows, extract the LARGEST required distance (Worst-case scenario)
            int requiredDistance = 0;
            foreach (int rowIndex in applicableRowIndices)
            {
                int dist = DistanceMatrix[rowIndex, colIndex];
                if (dist > requiredDistance)
                {
                    requiredDistance = dist;
                }
            }

            return requiredDistance;
        }


        /// Validates whether the safety distance (S) calculated by ISO 13855 meets the ISO 13857 requirement to prevent reaching over.

        /// <param name="hazardHeight">Height of the hazard zone in mm (a).</param>
        /// <param name="guardHeight">Height of the protective structure in mm (b).</param>
        /// <param name="calculatedSafetyDistance">The S value calculated from ISO 13855 in mm.</param>
        /// <returns>True if the calculated distance is safe from reaching over; otherwise, false.</returns>
        public static bool IsSafeFromReachOver(int hazardHeight, int guardHeight, double calculatedSafetyDistance)
        {
            int requiredDistance = GetRequiredHorizontalDistance(hazardHeight, guardHeight);

            // If the calculated distance (S) is greater than or equal to the required distance (c), the system is safe.
            return calculatedSafetyDistance >= requiredDistance;
        }
    }
}