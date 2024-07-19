using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVCFinalProje.UI.Areas.Admin.Models.BookVM
{
    public class AdminBookCreateVM
    {
        public string Name { get; set; }
        public DateTime PublisDate { get; set; }
        public Guid PublisherId { get; set; }

        public Guid AuthorId { get; set; }
        public SelectList Authors { get; set; }
        public SelectList Publishers { get; set; }
    }
}
