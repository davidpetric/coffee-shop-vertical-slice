namespace Application.Domain.Users.ValueObjects;

using Ardalis.SmartEnum;

using System.Runtime.CompilerServices;

/// <summary>
/// Roles used in application. <br/> <br/>
/// Value is roleId.
/// </summary>
/// <param name="roleId"></param>
/// <param name="name"></param>
public sealed class AppRole(long roleId, [CallerMemberName] string name = default!) : SmartEnum<AppRole, long>(name, roleId)
{
    public static readonly AppRole Ron = new(1);

    public static readonly AppRole Barista = new(2);
}
