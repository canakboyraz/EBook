using MVCFinalProje.Core.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCFinalProje.Domain.Entities
{
    public class Author : AuditableEntity
    {
        public Author()
        {
            Books = new HashSet<Book>();
        }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public virtual IEnumerable<Book> Books { get; set; }
    }
}
