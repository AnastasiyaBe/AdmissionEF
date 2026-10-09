using System;
using System.Collections.Generic;

namespace AdmissionEF.Models;

public partial class OrderEnrollment
{
    public int EnrollmentId { get; set; }

    public int OrderId { get; set; }

    public int ApplicationId { get; set; }

    public string FundingForm { get; set; } = null!;

    public virtual Application Application { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;
}
