namespace Application.Domain.Products.ValueObjects;

using Ardalis.SmartEnum;

using System.Runtime.CompilerServices;

public sealed class ProductType(long value, [CallerMemberName] string name = default!)
    : SmartEnum<ProductType, long>(name, value)
{
    public static readonly ProductType Unknown = new(1);

    public static readonly ProductType Coffee = new(2);

    public static readonly ProductType SoftDrink = new(3);

    public static readonly ProductType Tea = new(4);
}
