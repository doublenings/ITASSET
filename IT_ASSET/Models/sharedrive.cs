using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IT_ASSET.Models
{
    public class sharedrive
    {
        public int SD_ID { get; set; }
        public string SD_CODE { get; set; }
        [Required(ErrorMessage = "* กรุณากรอกรหัสพนักงานผู้ขอสิทธิ")]
        [Display(Name = "USER_NO: ")]
        public string USER_NO { get; set; }
        public Nullable<System.DateTime> SD_DATE { get; set; }
        [Required(ErrorMessage = "* กรุณากรอกสิทธิการใช้งาน")]
        [Display(Name = "Allow_STATUS: ")]
        public string ALLOW_STATUS { get; set; }
        [Required(ErrorMessage = "* กรุณากรอก Drive")]
        [Display(Name = "SD_DRIVE: ")]
        public string SD_DRIVE { get; set; }
        [Required(ErrorMessage = "* กรุณากรอก Folder")]
        [Display(Name = "SD_Folder: ")]
        public string SD_FOLDER { get; set; }
        public string SD_NOTE { get; set; }
        [Required(ErrorMessage = "* กรุณากรอก สถานะสิทธิ")]
        [Display(Name = "REQ_AUTH: ")]
        public string REQ_AUTH { get; set; }
        public string SD_REQUESTER { get; set; }
        public string SD_APPROVE_OWNER { get; set; }
        public Nullable<System.DateTime> SD_APPROVE_OWNER_DATE { get; set; }
        public string SD_APPROVE { get; set; }
        public Nullable<System.DateTime> SD_APPROVE_DATE { get; set; }
        public string SD_OPEN_BY { get; set; }
        public Nullable<System.DateTime> SD_OPEN_DATE { get; set; }
        public string SD_ASSIGN_TO { get; set; }
        public Nullable<System.DateTime> SD_ASSIGN_DATE { get; set; }
        public Nullable<System.DateTime> SD_SUBMIT_DATE { get; set; }
        public string SD_CLOSE { get; set; }
        public Nullable<System.DateTime> AF_CLOSE_DATE { get; set; }
        public string REQ_STATUS { get; set; }
        public string USER_CREATE { get; set; }
        public string USER_UPDATE { get; set; }
        public Nullable<System.DateTime> CREATE_DATE { get; set; }
        public Nullable<System.DateTime> UPDATE_DATE { get; set; }
        public string SD_IT_COMMENT { get; set; }
        public string SD_IT_COMMENTER { get; set; }
        public string SD_IT_MANAGER { get; set; }
        public Nullable<System.DateTime> SD_IT_DATE { get; set; }
    }
}