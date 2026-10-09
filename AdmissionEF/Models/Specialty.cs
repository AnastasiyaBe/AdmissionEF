using System;
using System.Collections.Generic;

namespace AdmissionEF.Models;

public partial class Specialty
{
    public int SpecialtyId { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int FacultyId { get; set; }

    public string EducationForm { get; set; } = null!;

    public decimal DurationYears { get; set; }

    public int BudgetPlan { get; set; }

    public int PaidPlan { get; set; }

    public virtual ICollection<Application> Applications { get; set; } = new List<Application>();

    public virtual Faculty Faculty { get; set; } = null!;

    public virtual ICollection<SpecialtyTest> SpecialtyTests { get; set; } = new List<SpecialtyTest>();
}
