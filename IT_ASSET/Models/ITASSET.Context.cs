﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IT_ASSET.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class IT_ASSET_MANAGEMENTEntities : DbContext
    {
        public IT_ASSET_MANAGEMENTEntities()
            : base("name=IT_ASSET_MANAGEMENTEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tbl_assesories> tbl_assesories { get; set; }
        public virtual DbSet<tbl_buy_reason> tbl_buy_reason { get; set; }
        public virtual DbSet<tbl_categories> tbl_categories { get; set; }
        public virtual DbSet<tbl_cost_section> tbl_cost_section { get; set; }
        public virtual DbSet<tbl_costcenter> tbl_costcenter { get; set; }
        public virtual DbSet<tbl_device_types> tbl_device_types { get; set; }
        public virtual DbSet<tbl_doc_group> tbl_doc_group { get; set; }
        public virtual DbSet<tbl_doc_type> tbl_doc_type { get; set; }
        public virtual DbSet<tbl_document> tbl_document { get; set; }
        public virtual DbSet<tbl_hw_assets> tbl_hw_assets { get; set; }
        public virtual DbSet<tbl_hw_log_case> tbl_hw_log_case { get; set; }
        public virtual DbSet<tbl_hw_options_addon> tbl_hw_options_addon { get; set; }
        public virtual DbSet<tbl_hw_sw_standard> tbl_hw_sw_standard { get; set; }
        public virtual DbSet<tbl_hw_terminate> tbl_hw_terminate { get; set; }
        public virtual DbSet<tbl_incident_case> tbl_incident_case { get; set; }
        public virtual DbSet<tbl_incident_part> tbl_incident_part { get; set; }
        public virtual DbSet<tbl_Incident_status> tbl_Incident_status { get; set; }
        public virtual DbSet<tbl_it_staff> tbl_it_staff { get; set; }
        public virtual DbSet<tbl_pc_standards> tbl_pc_standards { get; set; }
        public virtual DbSet<tbl_pc_sw_standard> tbl_pc_sw_standard { get; set; }
        public virtual DbSet<tbl_staff_member> tbl_staff_member { get; set; }
        public virtual DbSet<tbl_status_hw> tbl_status_hw { get; set; }
        public virtual DbSet<tbl_supplier> tbl_supplier { get; set; }
        public virtual DbSet<tbl_sw_assets> tbl_sw_assets { get; set; }
        public virtual DbSet<tbl_sw_type> tbl_sw_type { get; set; }
        public virtual DbSet<tbl_year_document_number> tbl_year_document_number { get; set; }
        public virtual DbSet<tbl_configure> tbl_configure { get; set; }
        public virtual DbSet<View_costcenter> View_costcenter { get; set; }
        public virtual DbSet<View_device_type> View_device_type { get; set; }
        public virtual DbSet<View_document> View_document { get; set; }
        public virtual DbSet<View_hw_assets> View_hw_assets { get; set; }
        public virtual DbSet<View_hw_options_addon> View_hw_options_addon { get; set; }
        public virtual DbSet<View_incident_case> View_incident_case { get; set; }
        public virtual DbSet<View_pc_sw_standard> View_pc_sw_standard { get; set; }
        public virtual DbSet<View_staff_member> View_staff_member { get; set; }
        public virtual DbSet<View_sw_assets> View_sw_assets { get; set; }
        public virtual DbSet<View_inc_status> View_inc_status { get; set; }
        public virtual DbSet<tbl_req_types> tbl_req_types { get; set; }
        public virtual DbSet<tbl_supplier_type> tbl_supplier_type { get; set; }
        public virtual DbSet<View_supplier> View_supplier { get; set; }
        public virtual DbSet<View_hw_sw_standard> View_hw_sw_standard { get; set; }
        public virtual DbSet<tbl_hw_ass> tbl_hw_ass { get; set; }
        public virtual DbSet<tbl_user> tbl_user { get; set; }
        public virtual DbSet<View_pc_standards> View_pc_standards { get; set; }
        public virtual DbSet<View_incident_all> View_incident_all { get; set; }
        public virtual DbSet<tbl_req_af_status> tbl_req_af_status { get; set; }
        public virtual DbSet<tbl_req_status> tbl_req_status { get; set; }
        public virtual DbSet<tbl_req_af> tbl_req_af { get; set; }
        public virtual DbSet<View_req_af_register> View_req_af_register { get; set; }
        public virtual DbSet<tbl_req_allow_status> tbl_req_allow_status { get; set; }
        public virtual DbSet<View_req_af_follow> View_req_af_follow { get; set; }
        public virtual DbSet<tbl_req_auth> tbl_req_auth { get; set; }
        public virtual DbSet<View_req_sd> View_req_sd { get; set; }
        public virtual DbSet<tbl_req_mini> tbl_req_mini { get; set; }
        public virtual DbSet<tbl_req_sd> tbl_req_sd { get; set; }
        public virtual DbSet<View_req_sd_follow> View_req_sd_follow { get; set; }
        public virtual DbSet<View_req_mini> View_req_mini { get; set; }
        public virtual DbSet<View_req_mini_follow> View_req_mini_follow { get; set; }
    
        public virtual int AddIncident(ObjectParameter iNC_CODE, Nullable<System.DateTime> iNC_DATE, string iNC_REQUESTER, string iNC_TOPIC, string iNC_STATUS, string iNC_DESCRIPTION)
        {
            var iNC_DATEParameter = iNC_DATE.HasValue ?
                new ObjectParameter("INC_DATE", iNC_DATE) :
                new ObjectParameter("INC_DATE", typeof(System.DateTime));
    
            var iNC_REQUESTERParameter = iNC_REQUESTER != null ?
                new ObjectParameter("INC_REQUESTER", iNC_REQUESTER) :
                new ObjectParameter("INC_REQUESTER", typeof(string));
    
            var iNC_TOPICParameter = iNC_TOPIC != null ?
                new ObjectParameter("INC_TOPIC", iNC_TOPIC) :
                new ObjectParameter("INC_TOPIC", typeof(string));
    
            var iNC_STATUSParameter = iNC_STATUS != null ?
                new ObjectParameter("INC_STATUS", iNC_STATUS) :
                new ObjectParameter("INC_STATUS", typeof(string));
    
            var iNC_DESCRIPTIONParameter = iNC_DESCRIPTION != null ?
                new ObjectParameter("INC_DESCRIPTION", iNC_DESCRIPTION) :
                new ObjectParameter("INC_DESCRIPTION", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("AddIncident", iNC_CODE, iNC_DATEParameter, iNC_REQUESTERParameter, iNC_TOPICParameter, iNC_STATUSParameter, iNC_DESCRIPTIONParameter);
        }
    
        public virtual ObjectResult<string> AllIncident()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("AllIncident");
        }
    
        public virtual int AddReqAF(ObjectParameter aF_CODE, string mEM_NO, Nullable<System.DateTime> aF_DATE, string aLLOW_STATUS, string iD_APPLICANT, string aF_SITE, string aF_FOLDER, string aF_STATUS, string aF_NOTE, string aF_REQUESTER, string rEQ_STATUS)
        {
            var mEM_NOParameter = mEM_NO != null ?
                new ObjectParameter("MEM_NO", mEM_NO) :
                new ObjectParameter("MEM_NO", typeof(string));
    
            var aF_DATEParameter = aF_DATE.HasValue ?
                new ObjectParameter("AF_DATE", aF_DATE) :
                new ObjectParameter("AF_DATE", typeof(System.DateTime));
    
            var aLLOW_STATUSParameter = aLLOW_STATUS != null ?
                new ObjectParameter("ALLOW_STATUS", aLLOW_STATUS) :
                new ObjectParameter("ALLOW_STATUS", typeof(string));
    
            var iD_APPLICANTParameter = iD_APPLICANT != null ?
                new ObjectParameter("ID_APPLICANT", iD_APPLICANT) :
                new ObjectParameter("ID_APPLICANT", typeof(string));
    
            var aF_SITEParameter = aF_SITE != null ?
                new ObjectParameter("AF_SITE", aF_SITE) :
                new ObjectParameter("AF_SITE", typeof(string));
    
            var aF_FOLDERParameter = aF_FOLDER != null ?
                new ObjectParameter("AF_FOLDER", aF_FOLDER) :
                new ObjectParameter("AF_FOLDER", typeof(string));
    
            var aF_STATUSParameter = aF_STATUS != null ?
                new ObjectParameter("AF_STATUS", aF_STATUS) :
                new ObjectParameter("AF_STATUS", typeof(string));
    
            var aF_NOTEParameter = aF_NOTE != null ?
                new ObjectParameter("AF_NOTE", aF_NOTE) :
                new ObjectParameter("AF_NOTE", typeof(string));
    
            var aF_REQUESTERParameter = aF_REQUESTER != null ?
                new ObjectParameter("AF_REQUESTER", aF_REQUESTER) :
                new ObjectParameter("AF_REQUESTER", typeof(string));
    
            var rEQ_STATUSParameter = rEQ_STATUS != null ?
                new ObjectParameter("REQ_STATUS", rEQ_STATUS) :
                new ObjectParameter("REQ_STATUS", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("AddReqAF", aF_CODE, mEM_NOParameter, aF_DATEParameter, aLLOW_STATUSParameter, iD_APPLICANTParameter, aF_SITEParameter, aF_FOLDERParameter, aF_STATUSParameter, aF_NOTEParameter, aF_REQUESTERParameter, rEQ_STATUSParameter);
        }
    
        public virtual int EditInc(Nullable<int> iNC_CODE, string iNC_STATUS)
        {
            var iNC_CODEParameter = iNC_CODE.HasValue ?
                new ObjectParameter("INC_CODE", iNC_CODE) :
                new ObjectParameter("INC_CODE", typeof(int));
    
            var iNC_STATUSParameter = iNC_STATUS != null ?
                new ObjectParameter("INC_STATUS", iNC_STATUS) :
                new ObjectParameter("INC_STATUS", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("EditInc", iNC_CODEParameter, iNC_STATUSParameter);
        }
    
        public virtual ObjectResult<UserLogin_Result> UserLogin(string uSER_NO, string uSER_PASSWORD)
        {
            var uSER_NOParameter = uSER_NO != null ?
                new ObjectParameter("USER_NO", uSER_NO) :
                new ObjectParameter("USER_NO", typeof(string));
    
            var uSER_PASSWORDParameter = uSER_PASSWORD != null ?
                new ObjectParameter("USER_PASSWORD", uSER_PASSWORD) :
                new ObjectParameter("USER_PASSWORD", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UserLogin_Result>("UserLogin", uSER_NOParameter, uSER_PASSWORDParameter);
        }
    
        public virtual int AddIncident1(ObjectParameter iNC_CODE, Nullable<System.DateTime> iNC_DATE, string iNC_REQUESTER, string iNC_TOPIC, string iNC_STATUS, string iNC_DESCRIPTION)
        {
            var iNC_DATEParameter = iNC_DATE.HasValue ?
                new ObjectParameter("INC_DATE", iNC_DATE) :
                new ObjectParameter("INC_DATE", typeof(System.DateTime));
    
            var iNC_REQUESTERParameter = iNC_REQUESTER != null ?
                new ObjectParameter("INC_REQUESTER", iNC_REQUESTER) :
                new ObjectParameter("INC_REQUESTER", typeof(string));
    
            var iNC_TOPICParameter = iNC_TOPIC != null ?
                new ObjectParameter("INC_TOPIC", iNC_TOPIC) :
                new ObjectParameter("INC_TOPIC", typeof(string));
    
            var iNC_STATUSParameter = iNC_STATUS != null ?
                new ObjectParameter("INC_STATUS", iNC_STATUS) :
                new ObjectParameter("INC_STATUS", typeof(string));
    
            var iNC_DESCRIPTIONParameter = iNC_DESCRIPTION != null ?
                new ObjectParameter("INC_DESCRIPTION", iNC_DESCRIPTION) :
                new ObjectParameter("INC_DESCRIPTION", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("AddIncident1", iNC_CODE, iNC_DATEParameter, iNC_REQUESTERParameter, iNC_TOPICParameter, iNC_STATUSParameter, iNC_DESCRIPTIONParameter);
        }
    
        public virtual int AddReqAF1(ObjectParameter aF_CODE, string uSER_NO, Nullable<System.DateTime> aF_DATE, string aLLOW_STATUS, string aF_SITE, string aF_FOLDER, string aF_STATUS, string aF_NOTE, string aF_REQUESTER, string rEQ_STATUS)
        {
            var uSER_NOParameter = uSER_NO != null ?
                new ObjectParameter("USER_NO", uSER_NO) :
                new ObjectParameter("USER_NO", typeof(string));
    
            var aF_DATEParameter = aF_DATE.HasValue ?
                new ObjectParameter("AF_DATE", aF_DATE) :
                new ObjectParameter("AF_DATE", typeof(System.DateTime));
    
            var aLLOW_STATUSParameter = aLLOW_STATUS != null ?
                new ObjectParameter("ALLOW_STATUS", aLLOW_STATUS) :
                new ObjectParameter("ALLOW_STATUS", typeof(string));
    
            var aF_SITEParameter = aF_SITE != null ?
                new ObjectParameter("AF_SITE", aF_SITE) :
                new ObjectParameter("AF_SITE", typeof(string));
    
            var aF_FOLDERParameter = aF_FOLDER != null ?
                new ObjectParameter("AF_FOLDER", aF_FOLDER) :
                new ObjectParameter("AF_FOLDER", typeof(string));
    
            var aF_STATUSParameter = aF_STATUS != null ?
                new ObjectParameter("AF_STATUS", aF_STATUS) :
                new ObjectParameter("AF_STATUS", typeof(string));
    
            var aF_NOTEParameter = aF_NOTE != null ?
                new ObjectParameter("AF_NOTE", aF_NOTE) :
                new ObjectParameter("AF_NOTE", typeof(string));
    
            var aF_REQUESTERParameter = aF_REQUESTER != null ?
                new ObjectParameter("AF_REQUESTER", aF_REQUESTER) :
                new ObjectParameter("AF_REQUESTER", typeof(string));
    
            var rEQ_STATUSParameter = rEQ_STATUS != null ?
                new ObjectParameter("REQ_STATUS", rEQ_STATUS) :
                new ObjectParameter("REQ_STATUS", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("AddReqAF1", aF_CODE, uSER_NOParameter, aF_DATEParameter, aLLOW_STATUSParameter, aF_SITEParameter, aF_FOLDERParameter, aF_STATUSParameter, aF_NOTEParameter, aF_REQUESTERParameter, rEQ_STATUSParameter);
        }
    
        public virtual int AddReqShareDrive(ObjectParameter sD_CODE, string uSER_NO, Nullable<System.DateTime> sD_DATE, string aLLOW_STATUS, string sD_DRIVE, string sD_FOLDER, string sD_NOTE, string rEQ_AUTH, string sD_REQUESTER, string rEQ_STATUS)
        {
            var uSER_NOParameter = uSER_NO != null ?
                new ObjectParameter("USER_NO", uSER_NO) :
                new ObjectParameter("USER_NO", typeof(string));
    
            var sD_DATEParameter = sD_DATE.HasValue ?
                new ObjectParameter("SD_DATE", sD_DATE) :
                new ObjectParameter("SD_DATE", typeof(System.DateTime));
    
            var aLLOW_STATUSParameter = aLLOW_STATUS != null ?
                new ObjectParameter("ALLOW_STATUS", aLLOW_STATUS) :
                new ObjectParameter("ALLOW_STATUS", typeof(string));
    
            var sD_DRIVEParameter = sD_DRIVE != null ?
                new ObjectParameter("SD_DRIVE", sD_DRIVE) :
                new ObjectParameter("SD_DRIVE", typeof(string));
    
            var sD_FOLDERParameter = sD_FOLDER != null ?
                new ObjectParameter("SD_FOLDER", sD_FOLDER) :
                new ObjectParameter("SD_FOLDER", typeof(string));
    
            var sD_NOTEParameter = sD_NOTE != null ?
                new ObjectParameter("SD_NOTE", sD_NOTE) :
                new ObjectParameter("SD_NOTE", typeof(string));
    
            var rEQ_AUTHParameter = rEQ_AUTH != null ?
                new ObjectParameter("REQ_AUTH", rEQ_AUTH) :
                new ObjectParameter("REQ_AUTH", typeof(string));
    
            var sD_REQUESTERParameter = sD_REQUESTER != null ?
                new ObjectParameter("SD_REQUESTER", sD_REQUESTER) :
                new ObjectParameter("SD_REQUESTER", typeof(string));
    
            var rEQ_STATUSParameter = rEQ_STATUS != null ?
                new ObjectParameter("REQ_STATUS", rEQ_STATUS) :
                new ObjectParameter("REQ_STATUS", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("AddReqShareDrive", sD_CODE, uSER_NOParameter, sD_DATEParameter, aLLOW_STATUSParameter, sD_DRIVEParameter, sD_FOLDERParameter, sD_NOTEParameter, rEQ_AUTHParameter, sD_REQUESTERParameter, rEQ_STATUSParameter);
        }
    
        public virtual int AddReqMini(ObjectParameter mINI_CODE, string uSER_NO, Nullable<System.DateTime> mINI_DATE, string aLLOW_STATUS, string mINI_SAMPLE, string mINI_PROTO, string mINI_INFORM, string mINI_CHM, string mINI_REQUESTER, string rEQ_STATUS)
        {
            var uSER_NOParameter = uSER_NO != null ?
                new ObjectParameter("USER_NO", uSER_NO) :
                new ObjectParameter("USER_NO", typeof(string));
    
            var mINI_DATEParameter = mINI_DATE.HasValue ?
                new ObjectParameter("MINI_DATE", mINI_DATE) :
                new ObjectParameter("MINI_DATE", typeof(System.DateTime));
    
            var aLLOW_STATUSParameter = aLLOW_STATUS != null ?
                new ObjectParameter("ALLOW_STATUS", aLLOW_STATUS) :
                new ObjectParameter("ALLOW_STATUS", typeof(string));
    
            var mINI_SAMPLEParameter = mINI_SAMPLE != null ?
                new ObjectParameter("MINI_SAMPLE", mINI_SAMPLE) :
                new ObjectParameter("MINI_SAMPLE", typeof(string));
    
            var mINI_PROTOParameter = mINI_PROTO != null ?
                new ObjectParameter("MINI_PROTO", mINI_PROTO) :
                new ObjectParameter("MINI_PROTO", typeof(string));
    
            var mINI_INFORMParameter = mINI_INFORM != null ?
                new ObjectParameter("MINI_INFORM", mINI_INFORM) :
                new ObjectParameter("MINI_INFORM", typeof(string));
    
            var mINI_CHMParameter = mINI_CHM != null ?
                new ObjectParameter("MINI_CHM", mINI_CHM) :
                new ObjectParameter("MINI_CHM", typeof(string));
    
            var mINI_REQUESTERParameter = mINI_REQUESTER != null ?
                new ObjectParameter("MINI_REQUESTER", mINI_REQUESTER) :
                new ObjectParameter("MINI_REQUESTER", typeof(string));
    
            var rEQ_STATUSParameter = rEQ_STATUS != null ?
                new ObjectParameter("REQ_STATUS", rEQ_STATUS) :
                new ObjectParameter("REQ_STATUS", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("AddReqMini", mINI_CODE, uSER_NOParameter, mINI_DATEParameter, aLLOW_STATUSParameter, mINI_SAMPLEParameter, mINI_PROTOParameter, mINI_INFORMParameter, mINI_CHMParameter, mINI_REQUESTERParameter, rEQ_STATUSParameter);
        }
    }
}
