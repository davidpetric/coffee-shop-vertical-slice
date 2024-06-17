namespace Application.Infrastructure.Persistence;

using Application.Common.ValueObjects;
using Application.Domain.Products;
using Application.Domain.Products.ValueObjects;
using Application.Domain.Users;
using Application.Domain.Users.ValueObjects;

using System.Collections.Generic;

public static class DbSeed
{
    public static void Seed(CoffeeShopDbContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        context.Database.EnsureCreated();

        SeedUsers(context);
        SeedProductTypes(context);

        SeedProducts(context);

        SeedRoles(context);
    }

    private static void SeedRoles(CoffeeShopDbContext context)
    {
        if (context.Roles.Any())
        {
            return;
        }

        List<Role> roles = [];
        foreach (AppRole role in AppRole.List)
        {
            roles.Add(new Role(role.Value) { Name = role.Name, });
        }

        if (context.Roles.Count() != roles.Count)
        {
            context.Roles.AddRange(roles);
        }

        context.SaveChanges();
    }

    private static void SeedUsers(CoffeeShopDbContext context)
    {
        if (!context.Roles.Any())
        {
            List<Role> roles = new(AppRole.List.Count);
            foreach (AppRole appRole in AppRole.List)
            {
                if (context.Roles.Any(x => x.Id == appRole.Value))
                {
                    continue;
                }

                Role rol = new()
                {
                    Name = appRole.Name,
                };

                rol.SetId(appRole.Value);

                roles.Add(rol);
            }

            context.Roles.AddRange(roles);

            context.SaveChanges();
        }

        User? ronUser = context.Users.FirstOrDefault(x => x.Id == AppRole.Ron.Value);

        if (ronUser is null)
        {
            ronUser = new()
            {
                FirstName = AppRole.Ron.Name,
                LastName = string.Empty,
                MiddleName = string.Empty,
                CanLogin = false,
                Employee = null,
                EmployeeId = null,
                IsActive = false,
                LastLogin = null,
            };

            EmailAddress emailAddress = new();

            _ = emailAddress.Create("ron@coffee-shop.db");

            ronUser.EmailAddress = emailAddress;

            Role ronRole = context.Roles.First(x => x.Id == AppRole.Ron.Value);
            ronUser.Roles.Add(ronRole);

            context.Users.Add(ronUser);
            context.SaveChanges();
        }
    }

    private static void SeedProductTypes(CoffeeShopDbContext context)
    {
        List<Domain.Products.ProductType> productTypes = new(Domain.Products.ValueObjects.ProductType.List.Count);

        foreach (Domain.Products.ValueObjects.ProductType @enum in Domain.Products.ValueObjects.ProductType.List)
        {
            if (context.ProductTypes.Any(a => a.Id == @enum.Value))
            {
                continue;
            }

            Domain.Products.ProductType prd = new() { Name = @enum.Name };
            prd.SetId(@enum.Value);
            productTypes.Add(prd);
        }

        context.ProductTypes.AddRange(productTypes);

        context.SaveChanges();
    }

    private static void SeedProducts(CoffeeShopDbContext context)
    {
        List<Product> products = [];

        if (!context.Products.Any())
        {
            Dictionary<string, long> productNames =
                new(StringComparer.Ordinal)
                {
                      { "Black", Domain.Products.ValueObjects.ProductType.Coffee.Value },
                      { "Latte", Domain.Products.ValueObjects.ProductType.Coffee.Value },
                      { "Cappuccino", Domain.Products.ValueObjects.ProductType.Coffee.Value },
                      { "Americano", Domain.Products.ValueObjects.ProductType.Coffee.Value },
                      { "Espresso", Domain.Products.ValueObjects.ProductType.Coffee.Value },
                      { "Doppio", Domain.Products.ValueObjects.ProductType.Coffee.Value },
                      { "Cortado", Domain.Products.ValueObjects.ProductType.Coffee.Value },
                      { "Red Eye", Domain.Products.ValueObjects.ProductType.Coffee.Value  },
                      { "Galao", Domain.Products.ValueObjects.ProductType.Coffee.Value },
                      { "Lungo", Domain.Products.ValueObjects.ProductType.Coffee.Value },
                      { "Macchiato", Domain.Products.ValueObjects.ProductType.Coffee.Value },
                      { "Mocha", Domain.Products.ValueObjects.ProductType.Coffee.Value },
                      { "Ristretto", Domain.Products.ValueObjects.ProductType.Coffee.Value },
                      { "Flat White", Domain.Products.ValueObjects.ProductType.Coffee.Value  },
                      { "Affogato", Domain.Products.ValueObjects.ProductType.Coffee.Value },
                      { "Cafe Au Lait", Domain.Products.ValueObjects.ProductType.Coffee.Value },
                      { "Irish", Domain.Products.ValueObjects.ProductType.Coffee.Value },
                      { "CocaCola", Domain.Products.ValueObjects.ProductType.Coffee.Value },
                };

            List<Product> coffees =
              productNames.Select(name => new Product()
              {
                  Name = name.Key,
                  ProductTypeId = name.Value,
              }).ToList();

            products.AddRange(coffees);
        }

        context.Products.AddRange(products);

        context.SaveChanges();
    }
}
