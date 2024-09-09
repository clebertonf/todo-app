namespace Todo.Models
{
    public class TodoModel
    {
        public int Id { get; set; }
        public string TodoTitle { get; set; }
        public bool IsDone { get; set; }
        public DateTime CreateAt { get; set; }
    }
}
