using HotelsApplication.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelsApplication.Infrastructure.DataSource
{
    public class DataContext : DbContext
    {        

        public DbSet<Hotel> Hotel { get; set; }
        public DbSet<Room> Room { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<Reservation> Reservation { get; set; }
        public DbSet<Guest> Guest { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder is null)
            {
                return;
            }

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);

            modelBuilder.Entity<Hotel>();
            modelBuilder.Entity<Room>();
            modelBuilder.Entity<Contact>();
            modelBuilder.Entity<Reservation>();
            modelBuilder.Entity<Guest>();


            modelBuilder.Entity<Room>().Property(e => e.Features).HasConversion(
            v => string.Join(',', v),
            v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList());

            // Configuración de la entidad Reservation
            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.HasKey(r => r.ReservationId);                
                entity.Ignore(e => e.Id);

                entity.Property(r => r.CheckInDate).IsRequired();
                entity.Property(r => r.CheckOutDate).IsRequired();
                entity.Property(r => r.RoomId).IsRequired();
                
                entity.HasOne(r => r.Room)
                      .WithMany()
                      .HasForeignKey(r => r.RoomId);
                
                entity.HasMany(r => r.Guests)
                      .WithOne(g => g.Reservation)
                      .HasForeignKey(g => g.ReservationId);
                
                entity.HasOne(r => r.EmergencyContact)
                      .WithOne()
                      .HasForeignKey<Reservation>(r => r.ContactId);                
            });


            // Configuración de la entidad Guest
            modelBuilder.Entity<Guest>(entity =>
            {
                entity.HasKey(g => g.GuestId);
                entity.Ignore(e => e.Id);

                entity.Property(g => g.FullName).IsRequired().HasMaxLength(200);
                entity.Property(g => g.DateOfBirth).IsRequired();
                entity.Property(g => g.Gender).IsRequired().HasMaxLength(50);
                entity.Property(g => g.DocumentType).IsRequired().HasMaxLength(50);
                entity.Property(g => g.DocumentNumber).IsRequired().HasMaxLength(50);
                entity.Property(g => g.Email).IsRequired().HasMaxLength(100);
                entity.Property(g => g.PhoneNumber).IsRequired().HasMaxLength(50);                
                entity.Property(g => g.ReservationId).IsRequired();                
            });

            // Configuración de la entidad Contact
            modelBuilder.Entity<Contact>(entity =>
            {
                entity.HasKey(c => c.ContactId);
                entity.Ignore(e => e.Id);

                entity.Property(c => c.FullName).IsRequired().HasMaxLength(200);
                entity.Property(c => c.PhoneNumber).IsRequired().HasMaxLength(50);                
            });


            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var t = entityType.ClrType;
                if (typeof(DomainEntity).IsAssignableFrom(t))
                {
                    modelBuilder.Entity(entityType.Name).Property<DateTime>("CreatedOn");
                    modelBuilder.Entity(entityType.Name).Property<DateTime>("LastModifiedOn");
                }
            }
            base.OnModelCreating(modelBuilder);
        }
    }
}
