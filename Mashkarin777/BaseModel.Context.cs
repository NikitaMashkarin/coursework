namespace Mashkarin777
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    public partial class mashkarin777Entities : DbContext
    {
        public mashkarin777Entities()
            : base("name=mashkarin777Entities")
        {
        }

        public static mashkarin777Entities GetContext()
        {
            return new mashkarin777Entities();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }

        public virtual DbSet<Personal_data> Personal_data { get; set; }
        public virtual DbSet<Post> Post { get; set; }
        public virtual DbSet<Project> Project { get; set; }
        public virtual DbSet<Project_objectives> Project_objectives { get; set; }
        public virtual DbSet<Report> Report { get; set; }
        public virtual DbSet<Social_worker> Social_worker { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Task> Task { get; set; }
    }
}
