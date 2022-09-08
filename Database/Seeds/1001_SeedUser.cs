using FluentMigrator;

namespace Database.Seeds
{
    [Migration(1001)]
    public class _1001_SeedUser : Migration
    {
        public record User
        {
            public Guid Id { get; set; } = Guid.NewGuid();
            public string Username { get; set; } = string.Empty;
            public string PasswordHash { get; set; } = string.Empty;
            public string RefreshToken { get; set; } = string.Empty;
            public DateTime TokenCreated { get; set; } = new DateTime();
            public DateTime TokenExpires { get; set; } = new DateTime();
        }
        static Microsoft.AspNetCore.Identity.PasswordHasher<User> hasher = new();

        public override void Up()
        {
            var seeds = new List<(string, string)> {
                ("ziad_m_404", "testing321"), 
                ("mando_saad", "testing546"),
                ("string", "string"), 
                ("maged", "maged546")
            };

            foreach((string, string) user in seeds)
            {
                Insert.IntoTable(tableName: Migrations.Tables.Users).Row(new
                {
                    id = Guid.NewGuid(),
                    username = user.Item1,
                    password_hash = hasher.HashPassword(new User(), user.Item2)
                });
            }
        }

        public override void Down() { }
    }
}