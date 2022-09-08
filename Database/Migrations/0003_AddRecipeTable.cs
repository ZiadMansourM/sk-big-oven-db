using FluentMigrator;
namespace Database.Migrations;

[Migration(3)]
public class _0003_AddRecipeTable : Migration
{
    public override void Up()
    {
        Create.Table(Tables.Recipes)
            .WithColumn("id").AsGuid().PrimaryKey()
            .WithColumn("title").AsString()
            .WithColumn("is_active").AsBoolean().NotNullable().WithDefaultValue(true);
    }

    public override void Down()
    {
        Delete.Table(Tables.Recipes);
    }
}