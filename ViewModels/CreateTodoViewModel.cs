using System.ComponentModel.DataAnnotations;

namespace MeuTodo.ViewModels
{
    public class CreateTodoViewModel
    {
        [Required]
        public string Title { get; set; }
    }
}