using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TODOANDBoard.Common;
using TODOANDBoard.DbContext;
using TODOANDBoard.Entities;
using TODOANDBoard.Model;

namespace TODOANDBoard.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TaskApiController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public TaskApiController(ApplicationDbContext context)
    {
        _context = context;
    }


    [HttpGet("{id}")]
    public async Task<ApiResponse> GetAllTaskByBoard([FromRoute] int id)
    {
        if (id < 0)
            return new ApiResponse() { Flag = false, Message = "Data not Valid" };

        var _ListTask = await _context.ToDos.Where(c => c.BoardId == id).ToListAsync();
        return _ListTask != null ? new ApiResponse<IEnumerable<ToDo>>() { Flag = true, Message = "List Tasks board", Data = _ListTask } : new ApiResponse() { Flag = false, Message = "NotFound" };
    }

    [HttpGet]
    public async Task<ApiResponse> GetAllTasksNotDone()
    {
        var _ListTaskNotDone = await _context.ToDos.Where(c => c.Status == StatusTodo.Tasknotdone).ToListAsync();
        return _ListTaskNotDone != null ? new ApiResponse<IEnumerable<ToDo>>() { Flag = true, Message = "List Tasks NotDone", Data = _ListTaskNotDone } : new ApiResponse() { Flag = false, Message = "There is no data to display" };
    }

    [HttpPost]
    public async Task<ApiResponse> AddTask([FromForm] AddTask model)
    {
        if (!ModelState.IsValid)
            return new ApiResponse() { Flag = false, Message = "Data not Valid" };
        
        var _addTask = new ToDo()
        {
            BoardId = model.BoardId,
            Title = model.Title,
            CreationDate = DateTime.Now,
            UpdateDate = null,
            Status = StatusTodo.Tasknotdone,
        };

        var _AddTODO = await _context.ToDos.AddAsync(_addTask);
        if (await _context.ToDos.AnyAsync(c => c.Title == _AddTODO.Entity.Title))
            return new ApiResponse() { Flag = false, Message = "DuplicateInformation" };

        await _context.SaveChangesAsync();
        return new ApiResponse<ToDo>() { Flag = true, Message = "Success Add Todo", Data = _AddTODO.Entity };
    }

    [HttpPut]
    public async Task<ApiResponse> UpdateTask([FromForm] EditTask model)
    {
        if (!ModelState.IsValid)
            return new ApiResponse() { Flag = false, Message = "Data not Valid" };

        var _TaskForEdit = await _context.ToDos.FindAsync(model.Id);
        if (_TaskForEdit is null)
            return new ApiResponse() { Flag = false, Message = "NotFound" };

        _TaskForEdit.Title = model.Title;
        _TaskForEdit.UpdateDate = DateTime.Now;
        _TaskForEdit.Status = model.Status;
        if (await _context.ToDos.AnyAsync(c => c.Title == _TaskForEdit.Title && c.Id != model.Id))
            return new ApiResponse() { Flag = false, Message = "DuplicateInformation" };
        _context.ToDos.Update(_TaskForEdit);
        await _context.SaveChangesAsync();
        return new ApiResponse<ToDo>() { Flag = true, Message = "success Update Board", Data = _TaskForEdit };
    }

    [HttpDelete("{id}")]
    public async Task<ApiResponse> DeleteToDo([FromRoute] int id)
    {

        if (id < 0)
            return new ApiResponse() { Flag = false, Message = "Data not Valid" };

        var _todoForDelete = await _context.ToDos.FindAsync(id);
        if (_todoForDelete is null)
            return new ApiResponse() { Flag = false, Message = "NotFound" };
        _context.ToDos.Remove(_todoForDelete);
        await _context.SaveChangesAsync();
        return new ApiResponse() { Flag = true, Message = "success Delete ToDo" };
    }
}