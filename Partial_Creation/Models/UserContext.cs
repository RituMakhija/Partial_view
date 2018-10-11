using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Partial_Creation.Models
{
    public class UserContext : DbContext
    {        
        public DbSet<Users> Users { get; set; }
    }
}
