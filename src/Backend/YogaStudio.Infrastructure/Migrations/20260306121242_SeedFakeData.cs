using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace YogaStudio.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedFakeData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "Address", "Capacity", "Name" },
                values: new object[,]
                {
                    { new Guid("cf427c08-a297-4b72-b374-2700c5b346ff"), "Primo Piano, Sala 2", 10, "Lotus Room" },
                    { new Guid("d3d6d675-ed57-44ce-8fb9-fc5a40acc283"), "Piano Terra, Sala 1", 20, "Zen Room" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "Email", "FirstName", "IsActive", "LastName", "Phone", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), new DateTimeOffset(new DateTime(2026, 3, 6, 12, 12, 41, 149, DateTimeKind.Unspecified).AddTicks(8255), new TimeSpan(0, 0, 0, 0, 0)), null, "admin@yogastudio.com", "Admin", true, "User", null, new DateTimeOffset(new DateTime(2026, 3, 6, 12, 12, 41, 149, DateTimeKind.Unspecified).AddTicks(8260), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new DateTimeOffset(new DateTime(2026, 3, 6, 12, 12, 41, 149, DateTimeKind.Unspecified).AddTicks(9147), new TimeSpan(0, 0, 0, 0, 0)), null, "sarah.connor@yogastudio.com", "Sarah", true, "Connor", null, new DateTimeOffset(new DateTime(2026, 3, 6, 12, 12, 41, 149, DateTimeKind.Unspecified).AddTicks(9147), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("33333333-3333-3333-3333-333333333333"), new DateTimeOffset(new DateTime(2026, 3, 6, 12, 12, 41, 149, DateTimeKind.Unspecified).AddTicks(9149), new TimeSpan(0, 0, 0, 0, 0)), null, "mario.rossi@example.com", "Mario", true, "Rossi", null, new DateTimeOffset(new DateTime(2026, 3, 6, 12, 12, 41, 149, DateTimeKind.Unspecified).AddTicks(9150), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("44444444-4444-4444-4444-444444444444"), new DateTimeOffset(new DateTime(2026, 3, 6, 12, 12, 41, 149, DateTimeKind.Unspecified).AddTicks(9166), new TimeSpan(0, 0, 0, 0, 0)), null, "giulia.bianchi@example.com", "Giulia", true, "Bianchi", null, new DateTimeOffset(new DateTime(2026, 3, 6, 12, 12, 41, 149, DateTimeKind.Unspecified).AddTicks(9167), new TimeSpan(0, 0, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "YogaClasses",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "Description", "DifficultyLevel", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("1eddc719-0a8c-422e-b1af-4711f31c27cd"), new DateTimeOffset(new DateTime(2026, 3, 6, 12, 12, 41, 150, DateTimeKind.Unspecified).AddTicks(9146), new TimeSpan(0, 0, 0, 0, 0)), null, "Flow dinamico per tonificare corpo e mente.", 1, "Vinyasa Flow", new DateTimeOffset(new DateTime(2026, 3, 6, 12, 12, 41, 150, DateTimeKind.Unspecified).AddTicks(9149), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("7a4c0d7a-eebd-4cae-991e-7b66c84583b2"), new DateTimeOffset(new DateTime(2026, 3, 6, 12, 12, 41, 150, DateTimeKind.Unspecified).AddTicks(9885), new TimeSpan(0, 0, 0, 0, 0)), null, "Lezione morbida focalizzata su respiro e posture statiche.", 0, "Hatha Yoga Base", new DateTimeOffset(new DateTime(2026, 3, 6, 12, 12, 41, 150, DateTimeKind.Unspecified).AddTicks(9886), new TimeSpan(0, 0, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "ClassSessions",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "DeliveryMode", "DurationMinutes", "InstructorUserId", "MaxCapacity", "MeetingUrl", "RoomId", "StartTime", "UpdatedAt", "YogaClassId" },
                values: new object[,]
                {
                    { new Guid("39364a0a-8d99-499b-a7cc-9db06b0f05dd"), new DateTimeOffset(new DateTime(2026, 3, 6, 12, 12, 41, 151, DateTimeKind.Unspecified).AddTicks(250), new TimeSpan(0, 0, 0, 0, 0)), null, 0, 60, new Guid("22222222-2222-2222-2222-222222222222"), 20, null, new Guid("d3d6d675-ed57-44ce-8fb9-fc5a40acc283"), new DateTimeOffset(new DateTime(2026, 3, 8, 6, 12, 41, 151, DateTimeKind.Unspecified).AddTicks(1495), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 3, 6, 12, 12, 41, 151, DateTimeKind.Unspecified).AddTicks(251), new TimeSpan(0, 0, 0, 0, 0)), new Guid("1eddc719-0a8c-422e-b1af-4711f31c27cd") },
                    { new Guid("cb1cc0e5-e01c-45d7-a206-959cd09c8bdd"), new DateTimeOffset(new DateTime(2026, 3, 6, 12, 12, 41, 151, DateTimeKind.Unspecified).AddTicks(3441), new TimeSpan(0, 0, 0, 0, 0)), null, 0, 90, new Guid("22222222-2222-2222-2222-222222222222"), 10, null, new Guid("cf427c08-a297-4b72-b374-2700c5b346ff"), new DateTimeOffset(new DateTime(2026, 3, 8, 22, 12, 41, 151, DateTimeKind.Unspecified).AddTicks(3445), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 3, 6, 12, 12, 41, 151, DateTimeKind.Unspecified).AddTicks(3442), new TimeSpan(0, 0, 0, 0, 0)), new Guid("7a4c0d7a-eebd-4cae-991e-7b66c84583b2") }
                });

            migrationBuilder.InsertData(
                table: "InstructorProfiles",
                columns: new[] { "UserId", "Bio", "Specialties" },
                values: new object[] { new Guid("22222222-2222-2222-2222-222222222222"), "Expert in Vinyasa and Ashtanga with 10 years of experience.", "Vinyasa, Ashtanga" });

            migrationBuilder.InsertData(
                table: "StudentProfiles",
                columns: new[] { "UserId", "EmergencyContact", "Notes" },
                values: new object[,]
                {
                    { new Guid("33333333-3333-3333-3333-333333333333"), "Luigi Rossi - 123456789", "Beginner" },
                    { new Guid("44444444-4444-4444-4444-444444444444"), "Marco Bianchi - 987654321", "Intermediate" }
                });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "Id", "BookedAt", "CancelledAt", "CheckedInAt", "SessionId", "Status", "StudentUserId" },
                values: new object[,]
                {
                    { new Guid("177e659f-542d-4a85-8412-48b822b5448f"), new DateTimeOffset(new DateTime(2026, 3, 6, 12, 12, 41, 151, DateTimeKind.Unspecified).AddTicks(5360), new TimeSpan(0, 0, 0, 0, 0)), null, null, new Guid("cb1cc0e5-e01c-45d7-a206-959cd09c8bdd"), 3, new Guid("33333333-3333-3333-3333-333333333333") },
                    { new Guid("217b96cf-8a42-442c-82f1-9621814531be"), new DateTimeOffset(new DateTime(2026, 3, 6, 12, 12, 41, 151, DateTimeKind.Unspecified).AddTicks(4352), new TimeSpan(0, 0, 0, 0, 0)), null, null, new Guid("39364a0a-8d99-499b-a7cc-9db06b0f05dd"), 0, new Guid("33333333-3333-3333-3333-333333333333") },
                    { new Guid("b6828123-de06-4ed5-9cf6-242f07147d39"), new DateTimeOffset(new DateTime(2026, 3, 6, 12, 12, 41, 151, DateTimeKind.Unspecified).AddTicks(5354), new TimeSpan(0, 0, 0, 0, 0)), null, null, new Guid("39364a0a-8d99-499b-a7cc-9db06b0f05dd"), 0, new Guid("44444444-4444-4444-4444-444444444444") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: new Guid("177e659f-542d-4a85-8412-48b822b5448f"));

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: new Guid("217b96cf-8a42-442c-82f1-9621814531be"));

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: new Guid("b6828123-de06-4ed5-9cf6-242f07147d39"));

            migrationBuilder.DeleteData(
                table: "InstructorProfiles",
                keyColumn: "UserId",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "StudentProfiles",
                keyColumn: "UserId",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"));

            migrationBuilder.DeleteData(
                table: "StudentProfiles",
                keyColumn: "UserId",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "ClassSessions",
                keyColumn: "Id",
                keyValue: new Guid("39364a0a-8d99-499b-a7cc-9db06b0f05dd"));

            migrationBuilder.DeleteData(
                table: "ClassSessions",
                keyColumn: "Id",
                keyValue: new Guid("cb1cc0e5-e01c-45d7-a206-959cd09c8bdd"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"));

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("cf427c08-a297-4b72-b374-2700c5b346ff"));

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("d3d6d675-ed57-44ce-8fb9-fc5a40acc283"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "YogaClasses",
                keyColumn: "Id",
                keyValue: new Guid("1eddc719-0a8c-422e-b1af-4711f31c27cd"));

            migrationBuilder.DeleteData(
                table: "YogaClasses",
                keyColumn: "Id",
                keyValue: new Guid("7a4c0d7a-eebd-4cae-991e-7b66c84583b2"));
        }
    }
}
