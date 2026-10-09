using System;
using System.Collections.Generic;

namespace AdmissionEF.Models;

public partial class EntranceTest
{
    public int TestId { get; set; }

    public string Name { get; set; } = null!;

    public string Form { get; set; } = null!;

    public decimal MinScore { get; set; }

    public virtual ICollection<SpecialtyTest> SpecialtyTests { get; set; } = new List<SpecialtyTest>();

    public virtual ICollection<TestResult> TestResults { get; set; } = new List<TestResult>();
}
