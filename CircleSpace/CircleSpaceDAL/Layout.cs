//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CircleSpaceDAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Layout
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Layout()
        {
            this.Tags = new HashSet<Tag>();
        }
    
        public int ID { get; set; }
        public string LayoutTitle { get; set; }
        public string Content { get; set; }
        public int OwnerID { get; set; }
        public string LayoutType { get; set; }
        public string CSS { get; set; }
    
        public virtual User User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tag> Tags { get; set; }
    }
}