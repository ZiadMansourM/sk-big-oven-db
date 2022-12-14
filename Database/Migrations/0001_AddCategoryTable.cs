using FluentMigrator;
namespace Database.Migrations;

[Migration(1)]
public class _0001_AddCategoryTable : Migration
{
    public override void Up()
    {
        Create.Table(Tables.Categories)
            .WithColumn("id").AsGuid().PrimaryKey()
            .WithColumn("name").AsString()
            .WithColumn("is_active").AsBoolean().NotNullable().WithDefaultValue(true);
    }

    public override void Down()
    {
        Delete.Table(Tables.Categories);
    }
}