using Microsoft.EntityFrameworkCore;
using TODOANDBoard.Entities;

namespace TODOANDBoard.DbContext;

public class ApplicationDbContext : Microsoft.EntityFrameworkCore.DbContext
{

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Board> Boards{ get; set; }
    public DbSet<ToDo> ToDos { get; set; }

}
