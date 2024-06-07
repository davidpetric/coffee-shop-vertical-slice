namespace Application.Domain.Products.ValueObjects;

using Ardalis.SmartEnum;

using System.Runtime.CompilerServices;

public sealed class ProductTypeEnum(long value, [CallerMemberName] string name = default!)
    : SmartEnum<ProductTypeEnum, long>(name, value)
{
    public static readonly ProductTypeEnum Unknown = new(1);

    public static readonly ProductTypeEnum Coffee = new(2);

    public static readonly ProductTypeEnum SoftDrink = new(3);

    public static readonly ProductTypeEnum Tea = new(4);
}
