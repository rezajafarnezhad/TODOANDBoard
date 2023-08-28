using System.ComponentModel.DataAnnotations;
using TODOANDBoard.Entities;

namespace TODOANDBoard.Model;

public class AddTask
{
    [Required(ErrorMessage = "This Fild is Required")]
    public string Title { get; set; }
    public int BoardId { get; set; }
}

public class EditTask
{
    public int Id { get; set; }
    [Required(ErrorMessage = "This Fild is Required")]
    public string Title { get; set; }
    public int BoardId { get; set; }
    public StatusTodo Status { get; set; }
}

public class EditBoard
{
    public int Id { get; set; }
    public string Title { get; set; }
}