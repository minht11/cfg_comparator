
namespace Web.Models
{
    public class CompareQuery
    {
        public string? IdStartsWith { get; set; }
            
        public bool? Unchanged { get; set; }

        public bool? Modified { get; set; }

        public bool? Added { get; set; }

        public bool? Removed { get; set; }
    }
}
