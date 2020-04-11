using QLector.Domain.Core;

namespace QLector.Domain
{
    public class Document : Entity
    {
        public string Name { get; set; }
        public string Location { get; set; }
    }
}