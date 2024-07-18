namespace MVCFinalProje.UI.Areas.Admin.Models.BookVM
{
    public class AdminBookListVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime PublisDate { get; set; }
        public Guid PublisherId { get; set; }
        public Guid AuthorId { get; set; }
    }
}
