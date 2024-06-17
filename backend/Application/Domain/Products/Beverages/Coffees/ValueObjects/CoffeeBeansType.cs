namespace Application.Domain.Products.Beverages.Coffees.ValueObjects;

using Ardalis.SmartEnum;

using System.Runtime.CompilerServices;

public sealed class CoffeeBeansType(long value, [CallerMemberName] string name = default!)
    : SmartEnum<CoffeeBeansType, long>(name, value)
{
    public static readonly CoffeeBeansType Unknown = new(1);

    public static readonly CoffeeBeansType Arabica = new(2);

    public static readonly CoffeeBeansType Robusta = new(3);

    public static readonly CoffeeBeansType Bourbon = new(4);

    public static readonly CoffeeBeansType Catimor = new(5);

    public static readonly CoffeeBeansType Caturra = new(6);

    public static readonly CoffeeBeansType Geisha = new(7);

    public static readonly CoffeeBeansType Harar = new(8);

    public static readonly CoffeeBeansType Icatu = new(9);

    public static readonly CoffeeBeansType Jackson = new(10);

    public static readonly CoffeeBeansType Jamaican = new(11);

    public static readonly CoffeeBeansType Java = new(12);

    public static readonly CoffeeBeansType Jember = new(13);

    public static readonly CoffeeBeansType Kent = new(14);

    public static readonly CoffeeBeansType Kona = new(15);

    public static readonly CoffeeBeansType Liberica = new(16);

    public static readonly CoffeeBeansType Maragatura = new(17);

    public static readonly CoffeeBeansType Maragogype = new(18);

    public static readonly CoffeeBeansType Mocha = new(19);

    public static readonly CoffeeBeansType MundoNovo = new(20);

    public static readonly CoffeeBeansType Pacamara = new(21);

    public static readonly CoffeeBeansType Pacas = new(22);

    public static readonly CoffeeBeansType Ruiru = new(23);

    public static readonly CoffeeBeansType Sagada = new(24);

    public static readonly CoffeeBeansType TanzaniaPeaberry = new(25);

    public static readonly CoffeeBeansType Timor = new(26);

    public static readonly CoffeeBeansType Typica = new(27);
}
