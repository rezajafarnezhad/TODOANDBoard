using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TODOANDBoard.Common;
using TODOANDBoard.DbContext;
using TODOANDBoard.Entities;
using TODOANDBoard.Model;

namespace TODOANDBoard.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BoardApiController : ControllerBase
{

    private readonly ApplicationDbContext _context;
    public BoardApiController(ApplicationDbContext context)
    {
        _context = context;
    }


    [HttpGet]
    public async Task<ApiResponse> GetAll()
    {
        var _boards = await _context.Boards.ToListAsync();
        return _boards != null ? new ApiResponse<IEnumerable<Board>>() { Flag = true, Message = "List _boards", Data = _boards } : new ApiResponse() { Flag = false, Message = "NotFound" };
    }

    [HttpPost]
    public async Task<ApiResponse> Create([FromBody] string BoardTitle)
    {
        if (string.IsNullOrWhiteSpace(BoardTitle))
            return new ApiResponse() { Flag = false, Message = "Data not Valid" };

        var _AddBoard = new Board()
        {
            Title = BoardTitle,
        };
        var _addboard = await _context.Boards.AddAsync(_AddBoard);
        await _context.SaveChangesAsync();
        return new ApiResponse<Board>() { Flag = true, Message = "success added Board", Data = _addboard.Entity };
    }

    [HttpPut]
    public async Task<ApiResponse> Update([FromForm] EditBoard model)
    {
        if (!ModelState.IsValid)
            return new ApiResponse() { Flag = false, Message = "Data not Valid" };

        var _boardForEdit = await _context.Boards.FindAsync(model.Id);
        if (_boardForEdit is null)
            return new ApiResponse() { Flag = false, Message = "NotFound" };

        _boardForEdit.Title = model.Title;
        _context.Boards.Update(_boardForEdit);
        await _context.SaveChangesAsync();
        return new ApiResponse<Board>() { Flag = true, Message = "success Update Board", Data = _boardForEdit };
    }

    [HttpDelete("{id}")]
    public async Task<ApiResponse> Delete([FromRoute] int id)
    {
        if (id < 0)
            return new ApiResponse() { Flag = false, Message = "Data not Valid" };

        var _boardForDelete = await _context.Boards.FindAsync(id);
        if (_boardForDelete is null)
            return new ApiResponse() { Flag = false, Message = "NotFound" };
        _context.Boards.Remove(_boardForDelete);
        await _context.SaveChangesAsync();
        return new ApiResponse() { Flag = true, Message = "success Delete Board"};
    }
}