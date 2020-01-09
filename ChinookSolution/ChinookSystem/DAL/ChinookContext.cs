using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using System.Data.Entity;
using ChinookSystem.Data.Entities;
#endregion

namespace ChinookSystem.DAL
{
    internal class ChinookContext:DbContext
    {
        //Reminder - Constructor is used to pass the connection string name
        public ChinookContext() : base("ChinookDB")
        {

        }

        //Set up the properties for the DbSet<>  used 
        // to access the sql data
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Album> Albums { get; set; }

        //DTO and POCOs DO NOT get DbSet<> properties they dont represent entities only entities
       // get DbSet<>
    }
}
