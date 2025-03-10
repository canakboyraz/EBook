﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCFinalProje.Business.DTOs.BookDTOs
{
    public class BookUpdateDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime PublisDate { get; set; }
        public Guid PublisherId { get; set; }
        public Guid AuthorId { get; set; }
    }
}
