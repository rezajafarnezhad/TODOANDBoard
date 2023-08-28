using System.ComponentModel.DataAnnotations;

namespace TODOANDBoard.Entities;

public class Board
{
    [Key]
    public int Id { get; set; }
    [Required(ErrorMessage = "This Fild is Required")]
    public string Title { get; set; }
    public ICollection<ToDo> ToDos { get; set; } = new List<ToDo>();

}

