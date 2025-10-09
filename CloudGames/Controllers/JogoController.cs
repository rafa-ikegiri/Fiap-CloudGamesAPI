using Core.Input;
using Core.Repository;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CloudGamesAPI.Controllers;

[ApiController]
[Route("/[controller]")]
public class JogoController : ControllerBase
{
    private readonly JogoService _jogoService;

    public JogoController(JogoService jogoService)
    {
        _jogoService = jogoService;
    }


    [HttpGet]
    [Authorize(Roles = "Admin,User")]
    public async Task<IActionResult> GetAll()
    {
        var games = await _jogoService.BuscarTodosJogos();
        return Ok(games);
    }

    [HttpGet("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var jogo = await _jogoService.GetByIdAsync(id);
        if (jogo == null)
            return NotFound("Jogo não encontrado.");
        return Ok(jogo);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Post([FromBody] JogoInput input)
    {
        try
        {
            var jogo = await _jogoService.CreateAsync(input);
            return Ok(jogo);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, [FromBody] JogoUpdateInput jogoupdateinput)
    {
        var updated = await _jogoService.UpdateAsync(id, jogoupdateinput);
        if (!updated) return NotFound("Jogo não encontrado.");
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var deleted = await _jogoService.DeleteAsync(id);
        if (!deleted) return NotFound("Jogo não encontrado.");
        return NoContent();
    }
}