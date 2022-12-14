using FluentValidation;
using System.Text.Json.Serialization;

namespace Frontend.Models;

public class Category
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public Category(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
    }

    [JsonConstructor]
    public Category(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public List<Category> ToList()
    {
        return new List<Category> { this };
    }
}

public class CategoryValidator : AbstractValidator<Category>
{
    public CategoryValidator()
    {
        RuleFor(category => category.Name).NotEmpty();
    }
}