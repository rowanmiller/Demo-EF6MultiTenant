using System.Collections.Generic;
using System.Data.Entity;

namespace MultiTenantDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer<ListContext>(null);

            TenantInitializer.EnsureTenantInitialized("john");
            using (var db = new ListContext("john"))
            {
                db.Lists.Add(new List
                {
                    Name = "My List",
                    Description = "My latest list of stuff",
                    Columns = new List<ListColumn>
                    {
                        new ListColumn { Heading = "Thing" },
                        new ListColumn { Heading = "About the thing" }
                    }
                });
                db.SaveChanges();
            }

            TenantInitializer.EnsureTenantInitialized("jane");
            using (var db = new ListContext("jane"))
            {
                db.Lists.Add(new List
                {
                    Name = "My List",
                    Description = "Jad's latest list of stuff",
                    Columns = new List<ListColumn>
                    {
                        new ListColumn { Heading = "Name" },
                        new ListColumn { Heading = "Description" }
                    }
                });
                db.SaveChanges();
            }
        }
    }
}
