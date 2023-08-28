using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TODOANDBoard.Entities;

[Index(nameof(Title), IsUnique = true)]
public class ToDo
{
    [Key]
    public int Id { get; set; }
    [Required(ErrorMessage = "This Fild is Required")]
    public string Title { get; set; }
    public StatusTodo Status { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime? UpdateDate { get; set; }


    public int BoardId { get; set; }

    [ForeignKey(nameof(BoardId))]
    public Board Board { get; set; }

}
public enum StatusTodo : byte
{
    Taskdone,
    Tasknotdone
}