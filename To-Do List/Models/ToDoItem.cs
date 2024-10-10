namespace To_Do_List.Models
{
    public class ToDoItem
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? File { get; set; }
        public DateTime Deadline { get; set; }
        public Customer Customer { get; set; }
        public int  CustomerId { get; set; }
    }
}
