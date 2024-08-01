using Core.Entities.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Reflection;

namespace DataAccess.Concrete.EntityFramework.Contexts
{
    /// <summary>
    /// Because this context is followed by migration for more than one provider
    /// works on PostGreSql db by default. If you want to pass sql
    /// When adding AddDbContext, use MsDbContext derived from it.
    /// </summary>
    public class ProjectDbContext : DbContext
    {
        /// <summary>
        /// in constructor we get IConfiguration, parallel to more than one db
        /// we can create migration.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="configuration"></param>
        public ProjectDbContext(DbContextOptions<ProjectDbContext> options, IConfiguration configuration)
            : base(options)
        {
            Configuration = configuration;
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
        }

        /// <summary>
        /// Let's also implement the general version.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="configuration"></param>
        protected ProjectDbContext(DbContextOptions options, IConfiguration configuration)
            : base(options)
        {
            Configuration = configuration;

        }

        public ProjectDbContext()
        {
        }

        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserClaim> UserClaims { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<GroupClaim> GroupClaims { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<MobileLogin> MobileLogins { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Translate> Translates { get; set; }
        public DbSet<Bolum> Bolum { get; set; }
        public DbSet<Ogrenci> Ogrenci { get; set; }
        public DbSet<Bolum> Bolums { get; set; }
        public DbSet<Ogrenci> Ogrencis { get; set; }
        public DbSet<Derslik> Derslik { get; set; }
        public DbSet<DersHavuzu> DersHavuzu { get; set; }
        public DbSet<Mufredat> Mufredat { get; set; }
        public DbSet<DersAcma> DersAcma { get; set; }
        public DbSet<DersAlma> DersAlma { get; set; }
        public DbSet<Sinav> Sinav { get; set; }
        public DbSet<Degerlendirme> Degerlendirme { get; set; }
        public DbSet<DersProgrami> DersProgrami { get; set; }
        public DbSet<ST_AkademikDonem> ST_AkademikDonem { get; set; }
        public DbSet<ST_AkademikYil> ST_AkademikYil { get; set; }
        public DbSet<ST_DersAlmaDurumu> ST_DersAlmaDurumu { get; set; }
        public DbSet<ST_DersDili> ST_DersDili { get; set; }
        public DbSet<ST_DersGunu> ST_DersGunu { get; set; }
        public DbSet<ST_DerslikTuru> ST_DerslikTuru { get; set; }
        public DbSet<ST_DersSeviyesi> ST_DersSeviyesi { get; set; }
        public DbSet<ST_DersTuru> ST_DersTuru { get; set; }
        public DbSet<ST_OgrenciDurum> ST_OgrenciDurum { get; set; }
        public DbSet<ST_OgretimDili> ST_OgretimDili { get; set; }
        public DbSet<ST_OgretimTuru> ST_OgretimTuru { get; set; }
        public DbSet<ST_ProgramTuru> ST_ProgramTuru { get; set; }
        public DbSet<ST_SinavTuru> ST_SinavTuru { get; set; }
        public DbSet<Danismanlik> Danismanlik { get; set; }
        public DbSet<Danismanlik> Danismanliks { get; set; }
        public DbSet<ST_ProgramTuru> ST_ProgramTurus { get; set; }
        public DbSet<ST_SinavTuru> ST_SinavTurus { get; set; }
        public DbSet<ST_OgretimTuru> ST_OgretimTurus { get; set; }
        public DbSet<ST_OgretimDili> ST_OgretimDilis { get; set; }
        public DbSet<ST_OgrenciDurum> ST_OgrenciDurums { get; set; }
        public DbSet<ST_DersTuru> ST_DersTurus { get; set; }
        public DbSet<ST_DersSeviyesi> ST_DersSeviyesis { get; set; }
        public DbSet<ST_DerslikTuru> ST_DerslikTurus { get; set; }
        public DbSet<ST_DersGunu> ST_DersGunus { get; set; }
        public DbSet<ST_DersDili> ST_DersDilis { get; set; }
        public DbSet<ST_DersAlmaDurumu> ST_DersAlmaDurumus { get; set; }
        public DbSet<ST_AkademikYil> ST_AkademikYils { get; set; }
        public DbSet<ST_AkademikDonem> ST_AkademikDonems { get; set; }
        public DbSet<Sinav> Sinavs { get; set; }
        public DbSet<OgretimElemani> OgretimElemani { get; set; }
        public DbSet<Mufredat> Mufredats { get; set; }
        public DbSet<DersProgrami> DersProgramis { get; set; }
        public DbSet<Derslik> Dersliks { get; set; }
        public DbSet<DersHavuzu> DersHavuzus { get; set; }
        public DbSet<DersAlma> DersAlmas { get; set; }
        public DbSet<DersAcma> DersAcmas { get; set; }
        public DbSet<Degerlendirme> Degerlendirmes { get; set; }
      
       




        protected IConfiguration Configuration { get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            if (!optionsBuilder.IsConfigured)
            {
                base.OnConfiguring(optionsBuilder.UseNpgsql(Configuration.GetConnectionString("DArchPgContext"))
                    .EnableSensitiveDataLogging());
            }
        }
    }
}
