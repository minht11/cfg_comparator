namespace CfgComparator.Configuration
{
    /// <summary>
    /// Class used for storing the results after two configuration id and value pairs are compared.
    /// </summary>
    public class ComparedParameter : Parameter
    {
        /// <summary>
        /// Change status between two configuration id value pairs.
        /// </summary>
        public ComparisonStatus Status { get; }

        /// <summary>
        /// Holds changed in case <see cref="CfgComparator.Configuration.ComparedParameter.ChangedValue" />
        /// is equal to <see cref="CfgComparator.Configuration.ComparisonStatus.Modified" />, otherwise it should be null.
        /// </summary>
        public string? ChangedValue { get; }

        public ComparedParameter(ComparisonStatus status, string id, string value, string? changedValue) : base(id, value)
        {
            (Status, ChangedValue) = (status, changedValue);
        }
    }
}
