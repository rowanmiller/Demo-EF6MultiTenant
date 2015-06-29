using System.Collections.Generic;

namespace MultiTenantDemo
{
    public class List
    {
        public int ListId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<ListColumn> Columns { get; set; }
    }

    public class ListColumn
    {
        public int ListColumnId { get; set; }
        public string Heading { get; set; }

        public int ListId { get; set; }
        public List List { get; set; }
    }
}
