using System;
using System.Collections.Generic;

namespace AdmissionEF.Models;

public partial class VEnrollmentFullInfo
{
    public int EnrollmentId { get; set; }

    public string OrderNumber { get; set; } = null!;

    public DateOnly OrderDate { get; set; }

    public string FundingForm { get; set; } = null!;

    public string ApplicationNumber { get; set; } = null!;

    public int ApplicantId { get; set; }

    public string ApplicantName { get; set; } = null!;

    public int SpecialtyId { get; set; }

    public string SpecialtyCode { get; set; } = null!;

    public string SpecialtyName { get; set; } = null!;

    public string FacultyName { get; set; } = null!;

    public string EducationForm { get; set; } = null!;
}
