using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCFinalProje.Core.Interfaces
{
    public interface ICreatableEntity: IEntity // Interface ınterface e kalıtım verir.
    {
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
