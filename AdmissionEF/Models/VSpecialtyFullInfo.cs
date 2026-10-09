using System;
using System.Collections.Generic;

namespace AdmissionEF.Models;

public partial class VSpecialtyFullInfo
{
    public int SpecialtyId { get; set; }

    public string SpecialtyCode { get; set; } = null!;

    public string SpecialtyName { get; set; } = null!;

    public string FacultyName { get; set; } = null!;

    public string EducationForm { get; set; } = null!;

    public decimal DurationYears { get; set; }

    public int BudgetPlan { get; set; }

    public int PaidPlan { get; set; }
}
