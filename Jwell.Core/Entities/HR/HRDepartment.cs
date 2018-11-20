using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Domain.Entities.HR
{
    [Table("HR_Department")]
    public class HRDepartment
    {
        public string GID { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string ParentID { get; set; }
        public string Competent { get; set; }
        public string Responsibility { get; set; }
        public string Contact { get; set; }
        public string Telephone { get; set; }
        public string Tax { get; set; }
        public string Address { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateTime { get; set; }
        public string HelpCode { get; set; }
        public string Depth { get; set; }
        public string IsShowChart { get; set; }
        public string CategoryID { get; set; }
        public string OrgGID { get; set; }
        public char IsJob { get; set; }
        public char IsPerformance { get; set; }
        public char IsSalary { get; set; }
        public string DanWei { get; set; }
        public string BaoBiaoZuZhi { get; set; }
    }
}
