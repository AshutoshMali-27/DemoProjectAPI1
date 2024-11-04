using System;
using System.Collections.Generic;

namespace DemoProjectAPI1;

public partial class UserMastertable
{
    public int Id { get; set; }

    public int? UtypeId { get; set; }

    public string? EmployeeName { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public int? ApprovalStatus { get; set; }

    public int? IsActive { get; set; }

    public int? Ulbid { get; set; }
}
