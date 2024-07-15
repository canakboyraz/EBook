using MVCFinalProje.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCFinalProje.Core.BaseEntities
{
    public class AuditableEntity : BaseEntity, IDeletableEntity // SoftDelete için buradan kalıtım alacağız.
    {
        public string? DeletedBy { get; set; } 
        public DateTime? DeletedDate { get; set; }
    }
}
