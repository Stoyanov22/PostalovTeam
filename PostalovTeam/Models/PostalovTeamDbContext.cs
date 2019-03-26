using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PostalovTeam.Models
{
    public class PostalovTeamDbContext : IdentityDbContext<AppUser>
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<PostalovTeamDbContext>(null);
            base.OnModelCreating(modelBuilder);
        }

        public PostalovTeamDbContext()
            : base("DefaultConnection")
        {
#if DEBUG
            Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
#endif

            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }
    }
}