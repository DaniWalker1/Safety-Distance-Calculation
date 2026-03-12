using System;

namespace Safety_Distance_Calculation.Exceptions
{
    
    /// Exception thrown when input values violate ISO 13855 or ISO 13857 normative limits.
    
    public class SafetyDistanceException : Exception
    {
        public SafetyDistanceException(string message) : base(message) { }
    }
}