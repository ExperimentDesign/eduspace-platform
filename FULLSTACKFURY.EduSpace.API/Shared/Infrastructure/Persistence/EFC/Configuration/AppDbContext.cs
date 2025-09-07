using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using FULLSTACKFURY.EduSpace.API.EventsScheduling.Domain.Model.Aggregates;
using FULLSTACKFURY.EduSpace.API.EventsScheduling.Domain.Model.ValueObjects;
using FULLSTACKFURY.EduSpace.API.IAM.Domain.Model.Aggregates;
using FULLSTACKFURY.EduSpace.API.PayrollManagement.Domain.Model.Aggregates;
using FULLSTACKFURY.EduSpace.API.Profiles.Domain.Model.Aggregates;
using FULLSTACKFURY.EduSpace.API.ReservationScheduling.Domain.Model.Aggregates;
using FULLSTACKFURY.EduSpace.API.ReservationScheduling.Domain.Model.Entities;
using FULLSTACKFURY.EduSpace.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using FULLSTACKFURY.EduSpace.API.SpacesAndResourceManagement.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.X509.Qualified;
using TeacherId = FULLSTACKFURY.EduSpace.API.ReservationScheduling.Domain.Model.ValueObjects.TeacherId;

namespace FULLSTACKFURY.EduSpace.API.Shared.Infrastructure.Persistence.EFC.Configuration;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.AddCreatedUpdatedInterceptor();
        base.OnConfiguring(builder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
 

        //Teacher Profiles Context
        builder.Entity<TeacherProfile>().HasKey(tp => tp.Id);
        builder.Entity<TeacherProfile>().Property(tp => tp.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<TeacherProfile>().Property(tp => tp.AdministratorId).IsRequired();
        builder.Entity<TeacherProfile>().OwnsOne(tp => tp.ProfileName,
            pn =>
            {
                pn.WithOwner().HasForeignKey("Id");
                pn.Property(tp => tp.FirstName).HasColumnName("FirstName");
                pn.Property(tp => tp.LastName).HasColumnName("LastName");
            });

        builder.Entity<TeacherProfile>().OwnsOne(tp => tp.ProfilePrivateInformation,
            pi =>
            {
                pi.WithOwner().HasForeignKey("Id");
                pi.Property(tp => tp.Email).HasColumnName("Email");
                pi.Property(tp => tp.Dni).HasColumnName("Dni");
                pi.Property(tp => tp.Address).HasColumnName("Address");
                pi.Property(tp => tp.Phone).HasColumnName("Phone");

            });
        
        // Administrators Profile Context

        builder.Entity<AdminProfile>().HasKey(ap => ap.Id);
        builder.Entity<AdminProfile>().Property(ap => ap.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<AdminProfile>().OwnsOne(ap => ap.ProfileName,
            pn =>
            {
                pn.WithOwner().HasForeignKey("Id");
                pn.Property(ap => ap.FirstName).HasColumnName("FirstName");
                pn.Property(ap => ap.LastName).HasColumnName("LastName");
            });
        builder.Entity<AdminProfile>().OwnsOne(ap => ap.ProfilePrivateInformation,
            pi =>
            {
                pi.WithOwner().HasForeignKey("Id");
                pi.Property(tp => tp.Email).HasColumnName("Email");
                pi.Property(tp => tp.Dni).HasColumnName("Dni");
                pi.Property(tp => tp.Address).HasColumnName("Address");
                pi.Property(tp => tp.Phone).HasColumnName("Phone");
            });
        
        //IAM CONTEXT

        builder.Entity<Account>().HasKey(a => a.Id);
        builder.Entity<Account>().Property(a => a.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Account>().Property(a => a.Username).IsRequired();
        builder.Entity<Account>().Property(a => a.PasswordHash).IsRequired();
        builder.Entity<Account>().Property(a => a.Role).IsRequired();
        
        
        //Reservations Context

        builder.Entity<Reservation>().HasKey(r => r.Id);
        builder.Entity<Reservation>().Property(r => r.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Reservation>().Property(r => r.Title).IsRequired();
        builder.Entity<Reservation>().OwnsOne(r => r.ReservationDate,
            rd =>
            {
                rd.WithOwner().HasForeignKey("Id");
                rd.Property(r => r.Start).HasColumnName("Start");
                rd.Property(r => r.End).HasColumnName("End");
            });
        builder.Entity<Reservation>().OwnsOne(r => r.AreaId,
            ai =>
            {
                ai.WithOwner().HasForeignKey("Id");
                ai.Property(r => r.Identifier).HasColumnName("AreaId");
            });
        builder.Entity<Reservation>().OwnsOne(r => r.TeacherId,
            ti =>
            {
                ti.WithOwner().HasForeignKey("Id");
                ti.Property(r => r.TeacherIdentifier).HasColumnName("TeacherId");
            });
        
        // Payroll Context
        builder.Entity<Payroll>().HasKey(p => p.Id);
        builder.Entity<Payroll>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Payroll>().Property(p => p.TeacherId).IsRequired();
        builder.Entity<Payroll>().Property(p => p.SalaryNet).IsRequired();

        // Configuración de SalaryAmount como Owned Type
        builder.Entity<Payroll>().OwnsOne(p => p.SalaryAmount, sa =>
            {
                sa.WithOwner().HasForeignKey("Id");
                sa.Property(p => p.Value).HasColumnName("SalaryAmount").IsRequired();
            });

        // Configuración de PayrollAdjustment como Owned Type
        builder.Entity<Payroll>().OwnsOne(p => p.PayrollAdjustment, pa =>
            {
                pa.WithOwner().HasForeignKey("Id");
                pa.Property(p => p.PensionContribution).HasColumnName("PensionContribution").IsRequired();
                pa.Property(p => p.SalaryBonus).HasColumnName("SalaryBonus").IsRequired();
            });
        
        

        
        builder.Entity<Classroom>().HasKey(c => c.Id);
        builder.Entity<Classroom>().Property(c => c.Name).IsRequired();
        builder.Entity<Classroom>().Property(c => c.Description).IsRequired();
        builder.Entity<Classroom>().OwnsOne(r => r.TeacherId,
            ti =>
            {
                ti.WithOwner().HasForeignKey("Id");
                ti.Property(r => r.TeacherIdentifier).HasColumnName("TeacherId");
            });
        
        builder.Entity<Resource>().HasKey(r => r.Id);
        builder.Entity<Resource>().Property(r => r.Name).IsRequired();
        builder.Entity<Resource>().Property(r => r.KindOfResource).IsRequired();
        builder.Entity<Resource>()
            .HasOne(r => r.Classroom)
            .WithMany(c => c.Resources)
            .HasForeignKey(r => r.ClassroomId)
            .OnDelete(DeleteBehavior.Cascade);

                
        builder.Entity<SharedArea>().HasKey(sa => sa.Id);
        builder.Entity<SharedArea>().Property(sa => sa.Name).IsRequired();
        builder.Entity<SharedArea>().Property(sa => sa.Capacity).IsRequired();
        builder.Entity<SharedArea>().Property(sa => sa.Description).IsRequired();
        
        
        //RESERVATION SCHEDULING BC 
        
        builder.Entity<Meeting>().HasKey(m => m.Id);
        builder.Entity<Meeting>().Property(m => m.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Meeting>().Property(m => m.Title).IsRequired();
        builder.Entity<Meeting>().Property(m => m.Description).IsRequired();
        builder.Entity<Meeting>().Property(m => m.Date).IsRequired();
        //Date conversion to fit the values from the db
        builder.Entity<Meeting>().Property(m => m.Date)
            .HasConversion(v => v.ToDateTime(TimeOnly.MinValue),
                v => DateOnly.FromDateTime(v));
        
        builder.Entity<Meeting>()
            .Property(m => m.EndTime)
            .HasConversion(
                v => v.ToTimeSpan(),                 // Convert TimeOnly to TimeSpan for the database
                v => TimeOnly.FromTimeSpan(v));  
        
        builder.Entity<Meeting>()
            .Property(m => m.StartTime)
            .HasConversion(
                v => v.ToTimeSpan(),                 // Convert TimeOnly to TimeSpan for the database
                v => TimeOnly.FromTimeSpan(v));  
        
        
        builder.Entity<Meeting>().Property(m => m.StartTime).IsRequired();
        builder.Entity<Meeting>().Property(m => m.EndTime).IsRequired();
        
        builder.Entity<Meeting>().OwnsOne(m => m.AdministratorId,
           ai =>
           {
               ai.WithOwner().HasForeignKey("Id");
               ai.Property(r => r.AdministratorIdentifier).HasColumnName("TeacherId");
           });
        
        
        builder.Entity<Meeting>().OwnsOne(m => m.ClassroomId,
    ci =>
    {
        ci.WithOwner().HasForeignKey("Id");
        ci.Property(r => r.ClassroomIdentifier).HasColumnName("ClassroomId");
    });
        


        builder.Entity<MeetingSession>()
            .HasKey(ms => new { ms.TeacherId, ms.MeetingId });
        

        builder.Entity<Meeting>().HasMany(m => m.MeetingParticipants);
        
        
        base.OnModelCreating(builder);
        
        //#TODO Add configurations here
        
        builder.UseSnakeCaseNamingConvention();
    }
}