//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TaskManager.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Task
    {
        public int Task_Id { get; set; }
        public Nullable<int> Parent_Id { get; set; }
        public string Task1 { get; set; }
        public System.DateTime Start_date { get; set; }
        public System.DateTime End_date { get; set; }
        public int Priority { get; set; }
    }
}
