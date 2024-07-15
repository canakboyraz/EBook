using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCFinalProje.Core.Interfaces
{
    public interface IUpdatebleEntity : ICreatableEntity
    {
        public string? UpdatedBy { get; set; } // ? ekleyerek null olabilir dedik.
        public DateTime? UpdatedDate { get; set; }
    }
}
