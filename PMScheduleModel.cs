//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Kazan_Session3_API_16_9
{
    using System;
    using System.Collections.Generic;
    
    public partial class PMScheduleModel
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public Nullable<long> PMScheduleTypeID { get; set; }
    
        public virtual PMScheduleType PMScheduleType { get; set; }
    }
}
