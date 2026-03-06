using Microsoft.EntityFrameworkCore;
using YogaStudio.Core.Entities;

namespace YogaStudio.Infrastructure;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<StudentProfile> StudentProfiles => Set<StudentProfile>();
    public DbSet<InstructorProfile> InstructorProfiles => Set<InstructorProfile>();
    public DbSet<YogaClass> YogaClasses => Set<YogaClass>();
    public DbSet<ClassSession> ClassSessions => Set<ClassSession>();
    public DbSet<Room> Rooms => Set<Room>();
    public DbSet<Booking> Bookings => Set<Booking>(); protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.CoreEventId.PossibleIncorrectRequiredNavigationWithQueryFilterInteractionWarning));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply all configurations defined in the Infrastructure assembly
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        // --- Data Seeding ---

        // 1. Users
        var adminId = Guid.Parse("11111111-1111-1111-1111-111111111111");
        var instructorId = Guid.Parse("22222222-2222-2222-2222-222222222222");
        var studentId1 = Guid.Parse("33333333-3333-3333-3333-333333333333");
        var studentId2 = Guid.Parse("44444444-4444-4444-4444-444444444444");

        modelBuilder.Entity<User>().HasData(
            new User { Id = adminId, FirstName = "Admin", LastName = "User", Email = "admin@yogastudio.com" },
            new User { Id = instructorId, FirstName = "Sarah", LastName = "Connor", Email = "sarah.connor@yogastudio.com" },
            new User { Id = studentId1, FirstName = "Mario", LastName = "Rossi", Email = "mario.rossi@example.com" },
            new User { Id = studentId2, FirstName = "Giulia", LastName = "Bianchi", Email = "giulia.bianchi@example.com" }
        );

        // 2. Profiles
        modelBuilder.Entity<InstructorProfile>().HasData(
            new InstructorProfile { UserId = instructorId, Bio = "Expert in Vinyasa and Ashtanga with 10 years of experience.", Specialties = "Vinyasa, Ashtanga" }
        );

        modelBuilder.Entity<StudentProfile>().HasData(
            new StudentProfile { UserId = studentId1, Notes = "Beginner", EmergencyContact = "Luigi Rossi - 123456789" },
            new StudentProfile { UserId = studentId2, Notes = "Intermediate", EmergencyContact = "Marco Bianchi - 987654321" }
        );

        // 3. Rooms
        var room1Id = Guid.NewGuid();
        var room2Id = Guid.NewGuid();
        modelBuilder.Entity<Room>().HasData(
            new Room { Id = room1Id, Name = "Zen Room", Capacity = 20, Address = "Piano Terra, Sala 1" },
            new Room { Id = room2Id, Name = "Lotus Room", Capacity = 10, Address = "Primo Piano, Sala 2" }
        );

        // 4. Yoga Classes
        var vinyasaClassId = Guid.NewGuid();
        var hathaClassId = Guid.NewGuid();
        modelBuilder.Entity<YogaClass>().HasData(
            new YogaClass { Id = vinyasaClassId, Name = "Vinyasa Flow", Description = "Flow dinamico per tonificare corpo e mente.", DifficultyLevel = YogaStudio.Core.Enums.DifficultyLevel.Intermediate },
            new YogaClass { Id = hathaClassId, Name = "Hatha Yoga Base", Description = "Lezione morbida focalizzata su respiro e posture statiche.", DifficultyLevel = YogaStudio.Core.Enums.DifficultyLevel.Beginner }
        );

        // 5. Class Sessions
        var sessionId1 = Guid.NewGuid();
        var sessionId2 = Guid.NewGuid();
        modelBuilder.Entity<ClassSession>().HasData(
            new ClassSession { Id = sessionId1, YogaClassId = vinyasaClassId, RoomId = room1Id, InstructorUserId = instructorId, StartTime = DateTimeOffset.UtcNow.AddDays(1).AddHours(18), DurationMinutes = 60, MaxCapacity = 20, DeliveryMode = YogaStudio.Core.Enums.DeliveryMode.InStudio },
            new ClassSession { Id = sessionId2, YogaClassId = hathaClassId, RoomId = room2Id, InstructorUserId = instructorId, StartTime = DateTimeOffset.UtcNow.AddDays(2).AddHours(10), DurationMinutes = 90, MaxCapacity = 10, DeliveryMode = YogaStudio.Core.Enums.DeliveryMode.InStudio }
        );

        // 6. Bookings
        modelBuilder.Entity<Booking>().HasData(
            new Booking { Id = Guid.NewGuid(), SessionId = sessionId1, StudentUserId = studentId1, Status = YogaStudio.Core.Enums.BookingStatus.Confirmed },
            new Booking { Id = Guid.NewGuid(), SessionId = sessionId1, StudentUserId = studentId2, Status = YogaStudio.Core.Enums.BookingStatus.Confirmed },
            new Booking { Id = Guid.NewGuid(), SessionId = sessionId2, StudentUserId = studentId1, Status = YogaStudio.Core.Enums.BookingStatus.Waitlisted }
        );
    }
}
