using System;
using System.Collections.Generic;

namespace AdmissionEF.Models;

public partial class VApplicationFullInfo
{
    public int ApplicationId { get; set; }

    public string ApplicationNumber { get; set; } = null!;

    public int ApplicantId { get; set; }

    public string ApplicantName { get; set; } = null!;

    public string? Phone { get; set; }

    public decimal AverageScore { get; set; }

    public int SpecialtyId { get; set; }

    public string SpecialtyCode { get; set; } = null!;

    public string SpecialtyName { get; set; } = null!;

    public string FacultyName { get; set; } = null!;

    public string EducationForm { get; set; } = null!;

    public int Priority { get; set; }

    public DateTime SubmissionDateTime { get; set; }

    public string Status { get; set; } = null!;
}
