using FluentMigrator;
namespace Database.Migrations;

[Migration(2)]
public class _0002_AddUserTable : Migration
{
    public override void Up()
    {
        Create.Table(Tables.Users)
            .WithColumn("id").AsGuid().PrimaryKey()
            .WithColumn("username").AsString()
            .WithColumn("password_hash").AsString()
            .WithColumn("refresh_token").AsString().Nullable()
            .WithColumn("token_created").AsDateTime().Nullable()
            .WithColumn("token_expires").AsDateTime().Nullable()
            .WithColumn("is_active").AsBoolean().NotNullable().WithDefaultValue(true);
    }

    public override void Down()
    {
        Delete.Table(Tables.Users);
    }
}