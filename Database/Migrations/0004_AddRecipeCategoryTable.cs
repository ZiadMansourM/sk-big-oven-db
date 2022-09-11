using FluentMigrator;
namespace Database.Migrations;

[Migration(4)]
public class _0004_AddRecipeCategoryTable : Migration
{
    public override void Up()
    {
        Create.Table(Tables.RecipesCategories)
            .WithColumn("id").AsGuid().PrimaryKey()
            .WithColumn("recipe_id").AsGuid().NotNullable().ForeignKey(Tables.Recipes, "id")
            .WithColumn("category_id").AsGuid().NotNullable().ForeignKey(Tables.Categories, "id");
    }

    public override void Down()
    {
        Delete.Table(Tables.RecipesCategories);
    }
}