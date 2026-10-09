using System;
using System.Collections.Generic;

namespace AdmissionEF.Models;

public partial class VApplicantResult
{
    public int ResultId { get; set; }

    public int ApplicantId { get; set; }

    public string ApplicantName { get; set; } = null!;

    public int TestId { get; set; }

    public string TestName { get; set; } = null!;

    public string TestForm { get; set; } = null!;

    public decimal Score { get; set; }

    public DateOnly ResultDate { get; set; }

    public string CertificateNumber { get; set; } = null!;
}
