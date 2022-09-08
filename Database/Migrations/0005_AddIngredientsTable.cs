using FluentMigrator;
namespace Database.Migrations;

[Migration(5)]
public class _0005_AddIngredientsTable : Migration
{
    public override void Up()
    {
        Create.Table(Tables.Ingredients)
                .WithColumn("id").AsGuid().PrimaryKey()
                .WithColumn("recipe_id").AsGuid().NotNullable().ForeignKey(Tables.Recipes, "id")
                .WithColumn("ingredient").AsString(1000).NotNullable()
                .WithColumn("is_active").AsBoolean().NotNullable().WithDefaultValue(true);
    }

    public override void Down()
    {
        Delete.Table(Tables.Ingredients);
    }
}