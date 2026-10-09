using System;
using System.Collections.Generic;

namespace AdmissionEF.Models;

public partial class VAdmissionPlanReport
{
    public int FacultyId { get; set; }

    public string FacultyName { get; set; } = null!;

    public string EducationForm { get; set; } = null!;

    public int SpecialtyId { get; set; }

    public string SpecialtyCode { get; set; } = null!;

    public string SpecialtyName { get; set; } = null!;

    public int BudgetPlan { get; set; }

    public int PaidPlan { get; set; }

    public int BudgetEnrolled { get; set; }

    public int PaidEnrolled { get; set; }
}
