
namespace TaskManagerAPI.Models
{
    public class Task 
    {
        public Guid id { get; set; }
        public string text { get; set; }
        public string day { get; set; }
        public bool reminder { get; set; }

    }
}
