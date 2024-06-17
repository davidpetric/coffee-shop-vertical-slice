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
        List<ProductType> productTypes = new(ProductTypeEnum.List.Count);

        foreach (ProductTypeEnum @enum in ProductTypeEnum.List)
        {
            if (context.ProductTypes.Any(a => a.Id == @enum.Value))
            {
                continue;
            }

            ProductType prd = new() { Name = @enum.Name };
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
                      { "Black", ProductTypeEnum.Coffee.Value },
                      { "Latte",ProductTypeEnum.Coffee.Value },
                      { "Cappuccino", ProductTypeEnum.Coffee.Value },
                      { "Americano", ProductTypeEnum.Coffee.Value },
                      { "Espresso", ProductTypeEnum.Coffee.Value },
                      { "Doppio", ProductTypeEnum.Coffee.Value },
                      { "Cortado", ProductTypeEnum.Coffee.Value },
                      { "Red Eye",ProductTypeEnum.Coffee.Value  },
                      { "Galao", ProductTypeEnum.Coffee.Value },
                      { "Lungo", ProductTypeEnum.Coffee.Value },
                      { "Macchiato", ProductTypeEnum.Coffee.Value },
                      { "Mocha", ProductTypeEnum.Coffee.Value },
                      { "Ristretto", ProductTypeEnum.Coffee.Value },
                      { "Flat White",ProductTypeEnum.Coffee.Value  },
                      { "Affogato", ProductTypeEnum.Coffee.Value },
                      { "Cafe Au Lait", ProductTypeEnum.Coffee.Value },
                      { "Irish",ProductTypeEnum.Coffee.Value },
                      { "CocaCola",ProductTypeEnum.Coffee.Value },
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