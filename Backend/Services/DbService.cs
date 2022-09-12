using System;
using System.Net;
using System.Text.Json;

using Orm.DatabaseSpecific;
using Orm.Linq;
using SD.LLBLGen.Pro.DQE.PostgreSql;
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
}

