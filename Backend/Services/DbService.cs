using System;
using System.Net;

using Orm.DatabaseSpecific;
using Orm.EntityClasses;
using Orm.Linq;
using SD.LLBLGen.Pro.DQE.PostgreSql;
using SD.LLBLGen.Pro.LinqSupportClasses;
using SD.LLBLGen.Pro.ORMSupportClasses;

namespace Backend.Services;

public class DbService
{
    public DbService()
    {
        RuntimeConfiguration.ConfigureDQE<PostgreSqlDQEConfiguration>(c => c.AddDbProviderFactory(typeof(Npgsql.NpgsqlFactory)));
    }

    public List<Models.User> ListUsers()
    {
        using (var adapter = new DataAccessAdapter(Program.config["ConnectionStr"]))
        {
            var metaData = new LinqMetaData(adapter);
            return metaData.User.OrderBy(u => u.Username).Select(
                u => new Models.User(
                    u.Id, u.Username, u.PasswordHash, u.RefreshToken,
                    u.TokenCreated ?? new DateTime(),
                    u.TokenExpires ?? new DateTime()
                )
            ).ToList();
        }
    }

    public List<Models.Category> ListCategories()
    {
        using (var adapter = new DataAccessAdapter(Program.config["ConnectionStr"]))
        {
            var metaData = new LinqMetaData(adapter);
            return metaData.Category.OrderBy(c => c.Name).Select(
                c => new Models.Category(c.Id, c.Name)
            ).ToList();
        }
    }

    public List<Models.Recipe> ListRecipes()
    {
        using (var adapter = new DataAccessAdapter(Program.config["ConnectionStr"]))
        {
            var metaData = new LinqMetaData(adapter);
            List<Models.Recipe> recipes = new();
            foreach (var r in metaData.Recipe.OrderBy(r => r.Title).ToList())
            {
                List<string> ingredients = metaData.Ingredient
                    .Where(ing => ing.RecipeId == r.Id)
                    .Select(ing => ing.Ingredient)
                    .ToList();
                List<string> instructions = metaData.Instruction
                    .Where(ins => ins.RecipeId == r.Id)
                    .Select(ins => ins.Instruction)
                    .ToList();
                List<Guid> guids = metaData.RecipesCategory
                    .Where(g => g.RecipeId == r.Id)
                    .Select(g => g.CategoryId)
                    .ToList();
                recipes.Add(
                    new Models.Recipe(r.Id, r.Title, ingredients, instructions, guids)
                );
            }
            return recipes;
        }
    }

    // CREATE
    async public Task<bool> CreateCategory(string name)
    {
        using (var adapter = new DataAccessAdapter(Program.config["ConnectionStr"]))
        {
            CategoryEntity newCategory = new CategoryEntity
            {
                Id = Guid.NewGuid(),
                Name = name
            };
            return await adapter.SaveEntityAsync(newCategory);
        }
    }

    async public Task<Models.Recipe> CreateRecipe(string name, List<string> ingredients, List<string> instructions, List<Guid> categoriesIds)
    {
        using (var adapter = new DataAccessAdapter(Program.config["ConnectionStr"]))
        {
            Models.Recipe recipe = new Models.Recipe(name, ingredients, instructions, categoriesIds);
            RecipeEntity newRecipe = new RecipeEntity
            {
                Id = recipe.Id,
                Title = recipe.Name
            };
            await adapter.SaveEntityAsync(newRecipe);
            foreach(string ing in recipe.Ingredients)
            {
                IngredientEntity newIngredient = new IngredientEntity
                {
                    Id = Guid.NewGuid(),
                    RecipeId = recipe.Id,
                    Ingredient = ing
                };
                await adapter.SaveEntityAsync(newIngredient);
            }
            foreach (string ins in recipe.Instructions)
            {
                InstructionEntity newInstruction = new InstructionEntity
                {
                    Id = Guid.NewGuid(),
                    RecipeId = recipe.Id,
                    Instruction = ins
                };
                await adapter.SaveEntityAsync(newInstruction);
            }
            foreach (Guid guid in recipe.CategoriesIds)
            {
                RecipesCategoryEntity newIdGuid = new RecipesCategoryEntity
                {
                    Id = Guid.NewGuid(),
                    RecipeId = recipe.Id,
                    CategoryId = guid
                };
                await adapter.SaveEntityAsync(newIdGuid);
            }
            return recipe;
        }
    }

    // UPDATE
    async public Task<Models.Category> UpdateCategory(Guid id, string name)
    {
        using (var adapter = new DataAccessAdapter(Program.config["ConnectionStr"]))
        {
            var metaData = new LinqMetaData(adapter);
            CategoryEntity category = await metaData.Category.FirstOrDefaultAsync(c => c.Id == id);
            category.Name = name;
            await adapter.SaveEntityAsync(category);
            return new Models.Category(id, name);
        }
    }

    async public Task<Models.Recipe> UpdateRecipe(Guid id, string name, List<string> ingredients, List<string> instructions, List<Guid> categoriesIds)
    {
        using (var adapter = new DataAccessAdapter(Program.config["ConnectionStr"]))
        {
            var metaData = new LinqMetaData(adapter);
            // [1] Recipe
            RecipeEntity newRecipe = await metaData.Recipe.FirstOrDefaultAsync(r => r.Id == id);
            newRecipe.Title = name;
            await adapter.SaveEntityAsync(newRecipe);
            // [2] Ingredients DELETE then CREATE
            foreach (IngredientEntity ingredient in metaData.Ingredient)
            {
                if (ingredient.RecipeId == id)
                    await adapter.DeleteEntityAsync(ingredient);
            }
            foreach (string ing in ingredients)
            {
                IngredientEntity newIngredient = new IngredientEntity
                {
                    Id = Guid.NewGuid(),
                    RecipeId = id,
                    Ingredient = ing
                };
                await adapter.SaveEntityAsync(newIngredient);
            }
            // [3] Instructions
            foreach (InstructionEntity instruction in metaData.Instruction)
            {
                if (instruction.RecipeId == id)
                    await adapter.DeleteEntityAsync(instruction);
            }
            foreach (string ins in instructions)
            {
                InstructionEntity newInstruction = new InstructionEntity
                {
                    Id = Guid.NewGuid(),
                    RecipeId = id,
                    Instruction = ins
                };
                await adapter.SaveEntityAsync(newInstruction);
            }
            // [4] Guids
            foreach (RecipesCategoryEntity recipeCategory in metaData.RecipesCategory)
            {
                if (recipeCategory.RecipeId == id)
                    await adapter.DeleteEntityAsync(recipeCategory);
            }
            foreach (Guid guid in categoriesIds)
            {
                RecipesCategoryEntity recipesCategory = new RecipesCategoryEntity
                {
                    Id = Guid.NewGuid(),
                    RecipeId = id,
                    CategoryId = guid
                };
                await adapter.SaveEntityAsync(recipesCategory);
            }
            return new Models.Recipe(id, name, ingredients, instructions, categoriesIds);
        }
    }

    // DELETE
    async public Task DeleteCategory(Guid id)
    {
        using (var adapter = new DataAccessAdapter(Program.config["ConnectionStr"]))
        {
            var metaData = new LinqMetaData(adapter);
            foreach (RecipesCategoryEntity recipeCategory in metaData.RecipesCategory)
            {
                if (recipeCategory.CategoryId == id)
                {
                    await adapter.DeleteEntityAsync(recipeCategory);
                }
            }
            CategoryEntity category = await metaData.Category.FirstOrDefaultAsync(c => c.Id == id);
            await adapter.DeleteEntityAsync(category);
        }
    }

    async public Task DeleteRecipe(Guid id)
    {
        using (var adapter = new DataAccessAdapter(Program.config["ConnectionStr"]))
        {
            var metaData = new LinqMetaData(adapter);
            foreach (RecipesCategoryEntity recipeCategory in metaData.RecipesCategory)
            {
                if (recipeCategory.RecipeId == id)
                    await adapter.DeleteEntityAsync(recipeCategory);
            }
            foreach (InstructionEntity instruction in metaData.Instruction)
            {
                if (instruction.RecipeId == id)
                    await adapter.DeleteEntityAsync(instruction);
            }
            foreach (IngredientEntity ingredient in metaData.Ingredient)
            {
                if (ingredient.RecipeId == id)
                    await adapter.DeleteEntityAsync(ingredient);
            }
            RecipeEntity recipe = await metaData.Recipe.FirstOrDefaultAsync(r => r.Id == id);
            await adapter.DeleteEntityAsync(recipe);
        }
    }
}

