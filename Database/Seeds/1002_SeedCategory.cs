using FluentMigrator;

namespace Database.Seeds
{
    [Migration(1002)]
    public class _1002_SeedCategory : Migration
    {
        public override void Up()
        {
            var seeds = new List<(Guid, string)> {
                (new Guid("0254c085-f091-4f77-85df-bd9802ae8119"), "Egyptian"),
                (new Guid("37e11423-6e1e-414b-8447-6065275d2acb"), "Spanish"),
                (new Guid("67a1bba0-c2a8-4490-84f3-93507d445e2b"), "Italian"),
                (new Guid("70d97553-1383-4ad1-84f7-e38fd288c5a9"), "Syrian"),
                (new Guid("0a9ba5d6-3e52-412b-9e56-fb2a2adf8fe2"), "Broiled Double-Thick Lamb Rib Chops With SlickedUp Store-Bought Mint Jelly Sauce"),
                (new Guid("3d6ba989-00dd-479a-b9f5-3d54686bc35c"), "African")
            };

            foreach(var cat in seeds)
            {
                Insert.IntoTable(tableName: Migrations.Tables.Categories).Row(new
                {
                    id = cat.Item1,
                    name = cat.Item2
                });
            }
        }

        public override void Down() { }
    }
}