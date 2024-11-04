using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public class ClsLoginModel
    {
        public int? id { get; set; }
        public string? Username { get; set; }

        public string? Password { get; set; }

        public int? UtypeId { get; set; }

        public string? EmployeeName { get; set; }

        public int? ApprovalStatus { get; set; }

        public int? IsActive { get; set; }

        public int? Ulbid { get; set; }
    }
}
