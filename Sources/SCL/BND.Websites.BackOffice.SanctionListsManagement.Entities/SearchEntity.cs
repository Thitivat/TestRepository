using System;

namespace BND.Websites.BackOffice.SanctionListsManagement.Entities
{
    public class SearchEntity
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime? BirthDate { get; set; }
        public ListType SanctionListType { get; set; }
    }
}
