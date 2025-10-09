using Core.Input;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CloudGamesAPI.Controllers;

[ApiController]
[Route("/[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly UsuarioService _usuarioService;

    public UsuarioController(UsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    //Retorna Todos os usuário cadastrados
    [HttpGet("admin")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<List<UsuarioInput>>> BuscarTodosUsuarios()
    {
        var usuarios = await _usuarioService.BuscarTodosUsuarios() ;
        return Ok(usuarios);
    }

    //Retorna um usuário específico pelo ID
    [HttpGet("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<UsuarioService>> Get([FromRoute] int id)
    {
        var usuario = await _usuarioService.GetByIdAsync(id);
        if (usuario == null)
            return NotFound($"Usuário com ID {id} não encontrado.");
        return Ok(usuario);
        
    }

    //Cadastra um novo usuário
    [HttpPost("admin")]    
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Post([FromBody] UsuarioInput input)
    {
        try
        {
            var usuario = await _usuarioService.RegisterAsync(input);
            return Ok(usuario);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    //Atualiza um usuário existente    
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<UsuarioService>> Put([FromBody] UsuarioUpdateInput input, int id)
    {
        var usuario = await _usuarioService.UpdateAsync(id, input);
        if (usuario == null)
            return NotFound($"Usuário com ID {id} não encontrado para atualização.");
        return Ok(usuario);
    }

    //Deleta um usuário pelo ID        
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var deletetar = await _usuarioService.DeleteAsync(id);
        if (!deletetar)
            return NotFound($"Usuário com ID {id} não encontrado para remoção.");
        return Ok(new { deletetar });
    }
}