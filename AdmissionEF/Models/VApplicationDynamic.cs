using System;
using System.Collections.Generic;

namespace AdmissionEF.Models;

public partial class VApplicationDynamic
{
    public DateOnly? SubmissionDate { get; set; }

    public int SpecialtyId { get; set; }

    public string SpecialtyCode { get; set; } = null!;

    public string SpecialtyName { get; set; } = null!;

    public string FacultyName { get; set; } = null!;

    public string EducationForm { get; set; } = null!;

    public int? ApplicationsCount { get; set; }

    public int? TotalPlan { get; set; }

    public decimal? CompetitionRatio { get; set; }
}
