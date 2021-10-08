namespace Cfg.Configuration
{
    /// <summary>
    /// Comparison result between two <see cref="Cfg.Configuration.Parameter" />s with identical the id's.
    /// </summary>
    public class ComparedParameter : Parameter
    {
        /// <summary>
        /// Holds change status between two <see cref="Cfg.Configuration.Parameter" />s with identical the id's.
        /// </summary>
        public ComparisonStatus Status { get; }

        /// <summary>
        /// Holds changed value in case <see cref="Cfg.Configuration.ComparedParameter.Status" />
        /// is equal to <see cref="Cfg.Configuration.ComparisonStatus.Modified" />, otherwise it should be null.
        /// </summary>
        public string? ChangedValue { get; }

        public ComparedParameter(ComparisonStatus status, string id, string value, string? changedValue) : base(id, value)
        {
            (Status, ChangedValue) = (status, changedValue);
        }
    }
}
