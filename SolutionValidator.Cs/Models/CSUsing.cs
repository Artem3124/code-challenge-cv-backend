namespace SolutionValidator.Cs.Models
{
    public class CSUsing
    {
        public string Namespace { get; set; }

        public string ReferenceLocation { get; set; }

        public CSUsing(string name) : this(name, string.Empty)
        {

        }

        public CSUsing(string name, string referenceLocation)
        {
            Namespace = name;
            ReferenceLocation = referenceLocation;
        }
    }
}
