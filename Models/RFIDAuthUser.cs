//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RFIDWebAPI.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class RFIDAuthUser
    {
        public int RFIDUserId { get; set; }
        public string UserUID { get; set; }
        public string CardUID { get; set; }
        public int RFIDDeviceId { get; set; }
        public bool IsAuthorized { get; set; }
        public bool IsFlagged { get; set; }
        public bool IsActive { get; set; }
    }
}
