using System;
using System.Collections.Generic;

namespace AdmissionEF.Models;

public partial class Faculty
{
    public int FacultyId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Specialty> Specialties { get; set; } = new List<Specialty>();
}
