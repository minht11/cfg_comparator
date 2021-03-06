namespace Cfg.Config
{
    /// <summary>
    /// Individual configuration parameter storing it's id and value.
    /// </summary>
    public class Parameter
    { 
        public string ID { get; }

        public string Value { get; }

        public Parameter(string id, string value)
        {
            (ID, Value) = (id, value);
        }
    }
}
