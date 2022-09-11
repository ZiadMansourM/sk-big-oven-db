using FluentMigrator;

namespace Database.Seeds
{
    [Migration(1003)]
    public class _1003_SeedRecipe : Migration
    {
        public override void Up()
        {
            var seeds = new List<(string, List<string>, List<string>, List<Guid>)> {
                (
                    "Koshari",
                    new List<string> {
                        "Rice.",
                        "Lentils.",
                        "Macaroni.",
                        "Tomato sauce.",
                        "Vegetable oil.",
                        "Onions."
                    },
                    new List<string> {
                        "Sprinkle the onion rings with salt, then toss them in the flour to coat. Shake off excess.",
                        "heat 1 tablespoon cooking oil. Add the grated onion, cook on medium-high until the onion turns a translucent gold.",
                        "Stir in the distilled white vinegar, and turn the heat to low. Cover and keep warm until ready to serve."
                    },
                    new List<Guid> {
                        new Guid("0254c085-f091-4f77-85df-bd9802ae8119"),
                        new Guid("3d6ba989-00dd-479a-b9f5-3d54686bc35c")
                    }
                ),
                (
                    "Paella",
                    new List<string> {
                        "0.5 kg rice.",
                        "400g King Arthur Sir Lancelot flour.",
                        "10g agave syrup / honey / sugar / salt."
                    },
                    new List<string> {
                        "dissolve the sugar into the water and combine with dry ingredients by hand until the mixture forms a shaggy dough.",
                        "divide the dough into 3 portions (should be slightly less than 230g each) and fold each into a ball.",
                        "you can knead the dough a little, but it doesn't need much."
                    },
                    new List<Guid> {
                        new Guid("67a1bba0-c2a8-4490-84f3-93507d445e2b"),
                        new Guid("37e11423-6e1e-414b-8447-6065275d2acb")
                    }
                ),
                (
                    "Pizza",
                    new List<string> {
                        "400g King Arthur Sir Lancelot flour.",
                        "260g water (~65%).",
                        "5 to 10g salt.",
                        "10g agave syrup / honey / sugar.",
                        "5g SAF instant yeast."
                    },
                    new List<string> {
                        "combine flour salt & yeast in a large bowl.",
                        "dissolve the sugar into the water and combine with dry ingredients by hand until the mixture forms a shaggy dough.",
                        "sit covered for 20 minutes to hydrate the dough.",
                        "you can knead the dough a little, but it doesn't need much.",
                        "divide the dough into 3 portions (should be slightly less than 230g each) and fold each into a ball.",
                        "refrigerate for 24-72 hours."
                    },
                    new List<Guid> {
                        new Guid("70d97553-1383-4ad1-84f7-e38fd288c5a9")
                    }
                ),

            };

            foreach(var recipe in seeds)
            {
                var recipeGuid = Guid.NewGuid();
                // [1]: recipe
                Insert.IntoTable(tableName: Migrations.Tables.Recipes).Row(new
                {
                    id = recipeGuid,
                    title = recipe.Item1
                });
                // [2]: ingredients
                foreach(string ing in recipe.Item2) {
                    Insert.IntoTable(tableName: Migrations.Tables.Ingredients).Row(new
                    {
                        id = Guid.NewGuid(),
                        recipe_id = recipeGuid,
                        ingredient = ing
                    });
                }
                // [3]: instructions
                foreach(string ins in recipe.Item3) {
                    Insert.IntoTable(tableName: Migrations.Tables.Instructions).Row(new
                    {
                        id = Guid.NewGuid(),
                        recipe_id = recipeGuid,
                        instruction = ins
                    });
                }
                // [4]: categories
                foreach(Guid guid in recipe.Item4) {
                    Insert.IntoTable(tableName: Migrations.Tables.RecipesCategories).Row(new
                    {
                        id = Guid.NewGuid(),
                        recipe_id = recipeGuid,
                        category_id = guid
                    });
                }
            }
        }

        public override void Down() { }
    }
}