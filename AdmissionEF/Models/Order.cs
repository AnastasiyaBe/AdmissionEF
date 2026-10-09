using System;
using System.Collections.Generic;

namespace AdmissionEF.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public string OrderNumber { get; set; } = null!;

    public DateOnly OrderDate { get; set; }

    public virtual ICollection<OrderEnrollment> OrderEnrollments { get; set; } = new List<OrderEnrollment>();
}
