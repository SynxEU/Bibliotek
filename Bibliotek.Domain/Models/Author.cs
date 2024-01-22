namespace Bibliotek.Domain.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime DOB { get; set; }
    }
}