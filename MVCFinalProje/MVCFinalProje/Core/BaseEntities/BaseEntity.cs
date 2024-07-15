using MVCFinalProje.Core.Interfaces;
using MVCFinalProje.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCFinalProje.Core.BaseEntities
{
    public class BaseEntity : IUpdatebleEntity // HardDelete istiyorsak buradan kalıtım alıyoruz.
    {
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid Id { get; set; } = Guid.NewGuid();
        public Status Status { get; set; }
    }
}
