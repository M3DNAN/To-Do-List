namespace To_Do_List.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ToDoItem> toDoItems { get; set; } = new List<ToDoItem>();
    }
}
