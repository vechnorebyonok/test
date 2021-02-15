namespace Kursach.DataBase
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<Class> Class { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<Contract> Contract { get; set; }
        public virtual DbSet<Description_DTP> Description_DTP { get; set; }
        public virtual DbSet<DTP> DTP { get; set; }
        public virtual DbSet<Info_client> Info_client { get; set; }
        public virtual DbSet<License> License { get; set; }
        public virtual DbSet<marka> marka { get; set; }
        public virtual DbSet<Model> Model { get; set; }
        public virtual DbSet<Pay> Pay { get; set; }
        public virtual DbSet<PriceList> PriceList { get; set; }
        public virtual DbSet<Return_contract> Return_contract { get; set; }
        public virtual DbSet<Type_body> Type_body { get; set; }
        public virtual DbSet<Vehicle> Vehicle { get; set; }
        public virtual DbSet<Check_sost> Check_sost { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Class>()
                .HasMany(e => e.PriceList)
                .WithRequired(e => e.Class1)
                .HasForeignKey(e => e.Class)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Class>()
                .HasMany(e => e.Vehicle)
                .WithRequired(e => e.Class)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Client>()
                .HasMany(e => e.Contract)
                .WithRequired(e => e.Client)
                .HasForeignKey(e => e.client_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Client>()
                .HasOptional(e => e.Info_client)
                .WithRequired(e => e.Client);

            modelBuilder.Entity<Contract>()
                .HasMany(e => e.DTP)
                .WithOptional(e => e.Contract)
                .HasForeignKey(e => e.contract_id);

            modelBuilder.Entity<Contract>()
                .HasMany(e => e.Pay)
                .WithRequired(e => e.Contract1)
                .HasForeignKey(e => e.Contract)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Contract>()
                .HasMany(e => e.Return_contract)
                .WithRequired(e => e.Contract)
                .HasForeignKey(e => e.contract_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DTP>()
                .HasOptional(e => e.Description_DTP)
                .WithRequired(e => e.DTP);

            modelBuilder.Entity<License>()
                .HasMany(e => e.Info_client)
                .WithRequired(e => e.License)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<marka>()
                .HasMany(e => e.Model)
                .WithRequired(e => e.marka)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Model>()
                .HasMany(e => e.Vehicle)
                .WithRequired(e => e.Model)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Return_contract>()
                .HasMany(e => e.Check_sost)
                .WithOptional(e => e.Return_contract)
                .HasForeignKey(e => e.return_id);

            modelBuilder.Entity<Type_body>()
                .HasMany(e => e.Vehicle)
                .WithRequired(e => e.Type_body)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Vehicle>()
                .HasMany(e => e.Contract)
                .WithRequired(e => e.Vehicle)
                .HasForeignKey(e => e.veh_id)
                .WillCascadeOnDelete(false);
        }
    }
}
