using System;
using System.Collections.Generic;

namespace AdmissionEF.Models;

public partial class VSpecialtyTest
{
    public int SpecialtyId { get; set; }

    public string SpecialtyCode { get; set; } = null!;

    public string SpecialtyName { get; set; } = null!;

    public int TestId { get; set; }

    public string TestName { get; set; } = null!;

    public string TestForm { get; set; } = null!;

    public decimal MinScore { get; set; }

    public decimal Weight { get; set; }
}
