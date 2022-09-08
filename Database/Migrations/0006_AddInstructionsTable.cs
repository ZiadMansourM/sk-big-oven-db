using FluentMigrator;
namespace Database.Migrations;

[Migration(6)]
public class _0006_AddInstructionsTable : Migration
{
    public override void Up()
    {
        Create.Table(Tables.Instructions)
                .WithColumn("id").AsGuid().PrimaryKey()
                .WithColumn("recipe_id").AsGuid().NotNullable().ForeignKey(Tables.Recipes, "id")
                .WithColumn("instruction").AsString(1000).NotNullable()
                .WithColumn("is_active").AsBoolean().NotNullable().WithDefaultValue(true);
    }

    public override void Down()
    {
        Delete.Table(Tables.Instructions);
    }
}