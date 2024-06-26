using System;
using System.Collections.Generic;

namespace StartUpX.Entity.DataModels;

public partial class EmployeeCountMaster
{
    public int EmployeeCountId { get; set; }

    public string? EmployeeCount { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public bool IsActive { get; set; }
}
