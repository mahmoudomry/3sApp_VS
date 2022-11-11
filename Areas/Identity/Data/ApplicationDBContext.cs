using _3sApp.Areas.Identity.Data;
using _3sApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace _3sApp.Areas.Identity.Data;

public class ApplicationDBContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
        builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());
        builder.Entity<About>().ToTable("About");
        builder.Entity<Client>().ToTable("Client");
       // builder.Entity<ContactInfo>().ToTable("ContactInfo");
        builder.Entity<ContactUsMessage>().ToTable("ContactUsMessage");
        builder.Entity<Industry>().ToTable("Industry");
        builder.Entity<Media>().ToTable("Media");
        builder.Entity<OurValue>().ToTable("OurValue");
        builder.Entity<Partner>().ToTable("Partner");
        builder.Entity<Project>().ToTable("Project");
        builder.Entity<Service>().ToTable("Service");
        builder.Entity<SiteSetting>().ToTable("SiteSetting");
        builder.Entity<Slider>().ToTable("Slider");
        builder.Entity<Solution>().ToTable("Solution");
        builder.Entity<SubSolution>().ToTable("SubSolution");
        builder.Entity<Contactitem>().ToTable("Contactitem");
        builder.Entity<Member>().ToTable("Member");
        builder.Entity<SocialMedia>().ToTable("SocialMedia");
        builder.Entity<Career>().ToTable("Career");
        builder.Entity<CareerApplication>().ToTable("CareerApplication");
        builder.Entity<News>().ToTable("News");
    }
    public DbSet<About> Abouts { get; set; }
    public DbSet<Client> Clients { get; set; }
   // public DbSet<ContactInfo> ContactInfos { get; set; }
    public DbSet<ContactUsMessage> ContactUsMessages { get; set; }
    public DbSet<Industry> Industries { get; set; }
    public DbSet<Media> Medias { get; set; }
    public DbSet<OurValue> OurValues { get; set; }
    public DbSet<Partner> Partners { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<SiteSetting> SiteSettings { get; set; }
    public DbSet<Slider> Sliders { get; set; }
    public DbSet<Solution> Solutions { get; set; }
    public DbSet<SubSolution> SubSolutions { get; set; }
    public DbSet<_3sApp.Models.Contactitem> Contactitems { get; set; }
    public DbSet<_3sApp.Models.Member> Members { get; set; }
    public DbSet<_3sApp.Models.SocialMedia> SocialMedias { get; set; }
    public DbSet<Career> Careers { get; set; }
    public DbSet<CareerApplication> CareerApplications { get; set; }
    public DbSet<News> News { get; set; }
}

internal class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(x => x.FullName).HasMaxLength(250);
            
    }
}