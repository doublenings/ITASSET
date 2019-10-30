using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IT_ASSET.Models
{
    public class AF
    {
        public int AF_ID { get; set; }
        public string AF_CODE { get; set; }
        [Required(ErrorMessage = "* กรุณากรอกรหัสพนักงาน")]
        [Display(Name = "USER_NO: ")]
        public string USER_NO { get; set; }
        public Nullable<System.DateTime> AF_DATE { get; set; }
        [Required(ErrorMessage = "* กรุณากรอกสิทธิการใช้งาน")]
        [Display(Name = "Allow_STATUS: ")]
        public string ALLOW_STATUS { get; set; }
        [Required(ErrorMessage = "* กรุณากรอกSite")]
        [Display(Name = "AF_Site: ")]
        public string AF_SITE { get; set; }
        [Required(ErrorMessage = "* กรุณากรอกFolder")]
        [Display(Name = "AF_Folder: ")]
        public string AF_FOLDER { get; set; }
        public string AF_STATUS { get; set; }
        public string AF_NOTE { get; set; }
        public string AF_REQUESTER { get; set; }
        public string AF_APPROVE_OWNER { get; set; }
        public Nullable<System.DateTime> AF_APPROVE_OWNER_DATE { get; set; }
        public string AF_APPROVE { get; set; }
        public Nullable<System.DateTime> AF_APPROVE_DATE { get; set; }
        public string AF_OPEN_BY { get; set; }
        public Nullable<System.DateTime> AF_OPEN_DATE { get; set; }
        public string AF_ASSIGN_TO { get; set; }
        public Nullable<System.DateTime> AF_ASSIGN_DATE { get; set; }
        public Nullable<System.DateTime> AF_SUBMIT_DATE { get; set; }
        public string AF_CLOSE { get; set; }
        public Nullable<System.DateTime> AF_CLOSE_DATE { get; set; }
        public string REQ_STATUS { get; set; }
        public string AF_BILL_NO { get; set; }
        public Nullable<System.DateTime> AF_BILL_DATE { get; set; }
        public Nullable<float> AF_COST { get; set; }
        public string USER_CREATE { get; set; }
        public string USER_UPDATE { get; set; }
        public Nullable<System.DateTime> CREATE_DATE { get; set; }
        public Nullable<System.DateTime> UPDATE_DATE { get; set; }
        public string AF_IT_COMMENT { get; set; }
        public string AF_IT_COMMENTER { get; set; }
        public string AF_IT_MANAGER { get; set; }
        public Nullable<System.DateTime> AF_IT_DATE { get; set; }
    }
}