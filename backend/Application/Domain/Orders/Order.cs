namespace Application.Domain.Orders;

using Application.Common;
using Application.Domain.Products;
using Application.Domain.Users;

using System.Collections.Generic;

public class Order : IHasDomainEvent
{
    public int Id { get; set; }

    public decimal Vat { get; set; }

    public decimal? Tip { get; set; }

    public decimal TotalPrice { get; set; }

    public long EmployeeId { get; set; }

    public Employee Employee { get; set; } = default!;

    public List<Product> Products { get; set; } = [];

    public DateTimeOffset Timestamp { get; set; }

    public List<DomainEvent> DomainEvents { get; } = [];
}
