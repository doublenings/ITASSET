using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IT_ASSET.Models
{
    public class incident_case
    {
        public int INC_ID { get; set; }
        public string INC_CODE { get; set; }
        public System.DateTime INC_DATE { get; set; }
        [Required(ErrorMessage = "กรูณากรอกเรื่องที่แจ้ง")]
        [Display(Name = "INC_TOPIC")]
        public string INC_TOPIC { get; set; }
        public string INC_REQUESTER { get; set; }
        public string INC_OPEN_BY { get; set; }
        public string INC_ASSIGN_TO { get; set; }
        public string INC_STATUS { get; set; }
        public string INC_CATEGORIES { get; set; }
        public string INC_PRIORITY { get; set; }
        public string INC_BILL_NO { get; set; }
        public Nullable<System.DateTime> INC_BILL_DATE { get; set; }
        public Nullable<System.DateTime> INC_OPEN_DATE { get; set; }
        public Nullable<System.DateTime> INC_DUE_DATE { get; set; }
        public Nullable<System.DateTime> INC_RESOLVE_DATE { get; set; }
        public Nullable<float> INC_COST { get; set; }
        public string INC_WARANTEE { get; set; }
        [Required(ErrorMessage = "กรูณากรอกรายละเอียดของเรื่องที่แจ้ง")]
        [Display(Name = "INC_DESCRIPTION")]
        public string INC_DESCRIPTION { get; set; }
        public string INC_COMMENTS { get; set; }
        public string USER_CREATE { get; set; }
        public string USER_UPDATE { get; set; }
        public Nullable<System.DateTime> CREATE_DATE { get; set; }
        public Nullable<System.DateTime> UPDATE_DATE { get; set; }
        public byte[] upsize_ts { get; set; }
        public string HW_CODE { get; set; }
        public Nullable<System.DateTime> INC_CLOSE_DATE { get; set; }
        public string COST_ID { get; set; }
        public Nullable<System.DateTime> INC_SUBMIT_DATE { get; set; }
        public Nullable<System.DateTime> INC_ASSIGN_DATE { get; set; }
        public string COST_SECTION { get; set; }
        public Nullable<System.DateTime> INC_CANCEL_DATE { get; set; }
    }
}