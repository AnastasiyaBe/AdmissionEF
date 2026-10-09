using System;
using System.Collections.Generic;

namespace AdmissionEF.Models;

public partial class Application
{
    public int ApplicationId { get; set; }

    public string ApplicationNumber { get; set; } = null!;

    public int ApplicantId { get; set; }

    public int SpecialtyId { get; set; }

    public int Priority { get; set; }

    public DateTime SubmissionDateTime { get; set; }

    public string Status { get; set; } = null!;

    public virtual Applicant Applicant { get; set; } = null!;

    public virtual OrderEnrollment? OrderEnrollment { get; set; }

    public virtual Specialty Specialty { get; set; } = null!;
}
