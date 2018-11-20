using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Domain.Entities.HR
{
    [Table("HR_Employee")]
    public class HREmployee
    {
        public string GID { get; set; }

        public string EmpCode { get; set; }

        public string EmpName { get; set; }

        public string FormerName { get; set; }



        public string PinYin { get; set; }


        public string DeptGID { get; set; }

        public string Category { get; set; }


        public string Position { get; set; }

        public string EmpJob { get; set; }


        public string Staffings { get; set; }


        public string EmpJobLevel { get; set; }

        public string EmpSex { get; set; }


        public string EmpBirthday { get; set; }


        public string ChineseBirthday { get; set; }


        public int YearOld { get; set; }
        public string EmpNational { get; set; }

        public string EmpDegree { get; set; }

        public string EmpPhoto { get; set; }

        public string Nationality { get; set; }

        public string EmpPolitical { get; set; }

        public DateTime PartyTime { get; set; }

        public string EmpCardType { get; set; }

        public string EmpCardID { get; set; }

        public string EmpBirthplace { get; set; }

        public string EmpMarriage { get; set; }


        public string StartWorkTime { get; set; }

        public DateTime HireDate { get; set; }

        public int GroupYear { get; set; }

        public int CompanyYear { get; set; }

        public DateTime LeaveDate { get; set; }

        public string FirstDegree { get; set; }

        public string Diploma { get; set; }


        public string EmpTitle { get; set; }
        public string EmpTitleSpecialty { get; set; }
        public string JobKinds { get; set; }
        public string JobKindsNature { get; set; }
        public string SkillLevel { get; set; }
        public string ArchivesNo { get; set; }
        public DateTime ArchivesUpdateTime { get; set; }
        public string FundNo { get; set; }
        public string MedicareNo { get; set; }
        public string PensionNo { get; set; }
        public string Specialty { get; set; }
        public string Hobby { get; set; }
        public string FstForeign { get; set; }
        public string SndForeign { get; set; }
        public string Bank { get; set; }
        public string HealthStatus { get; set; }
        public string BloodType { get; set; }
        public int Height { get; set; }
        public int WT { get; set; }
        public int EyeSkill { get; set; }
        public string IDNo { get; set; }
        public string LoginName { get; set; }
        public DateTime AddedDate { get; set; }
        public int DisplayOrder { get; set; }
        public string AddedType { get; set; }
        public string HelpCode { get; set; }
        public string EmpStatus { get; set; }
        public char IsTrial { get; set; }
        public string Attachment { get; set; }
        public string ReportTo { get; set; }
        public char KeyPersonnel { get; set; }
        public string ReportTo2 { get; set; }
        public char IsPositive { get; set; }
        public DateTime PositiveDate { get; set; }
        public string Depth { get; set; }
        public string JianZhi { get; set; }
        public string ZhuangTai { get; set; }
        public string ShenFenZhengHao { get; set; }
        public string SuoShuGongSi { get; set; }
        public string LianXiDianHua { get; set; }
        public string ShenFenZhengDiZhi { get; set; }
        public string Dept2GID { get; set; }
        public string VirtualOrg { get; set; }
        public string ZuQunMingCheng { get; set; }
        public string XuLieMingCheng { get; set; }
        public string ZiNvShuLiang { get; set; }
        public DateTime RuZhiShiJianBenDanWei { get; set; }
        public string PanGangJiTuanGangWeiFenZu { get; set; }
        public string CaiWuXiTongTongJiGangWeiFenZu { get; set; }
        public string ZhiJieShangJiGangWei { get; set; }
        public string XiangMu { get; set; }
        public string GangWeiLeiXing { get; set; }
        public string OAHao { get; set; }
        public string YuanGongBianHao { get; set; }
        public string TongXunDiZhi { get; set; }
        public DateTime inPanGangDate { get; set; }
        public string YinXingMingCheng { get; set; }
        public string KaiHuXing { get; set; }
        public string YinXingKaHao { get; set; }
        public string GeRenYouXiang { get; set; }
        public string YanFaLeiBie { get; set; }
        public DateTime RuDangShiJian { get; set; }
        public string BuMen { get; set; }
        public DateTime NiZhuanZhengRiQi { get; set; }
        public string ChengBenDanWei { get; set; }
        public string ZhuanYeJiShuZhiWu { get; set; }
        public string YouHeTeChang { get; set; }
        public string ShuXiZhuanYe { get; set; }
        public string ZaiZhiJiaoYu { get; set; }
        public string BiYeYuanXiaoXiJiZhuanYe { get; set; }
        public string note { get; set; }
    }
}
