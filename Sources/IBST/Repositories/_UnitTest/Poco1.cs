using System.Collections.Generic;

namespace BND.Services.IbanStore.RepositoryTest
{
    internal class Poco1
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Poco2> Poco2s { get; set; }

        public Poco1()
        {
            Poco2s = new HashSet<Poco2>();
        }
    }
}
