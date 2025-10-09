using Core.Entity;
using Core.Input;
using Core.Repository;

namespace Core.Services;

public class JogoService
{

    private readonly IJogoRepository _jogoRepository;

    public JogoService(IJogoRepository jogoRepository)
    {
        _jogoRepository = jogoRepository;
    }

    public async Task<List<JogoInput>> BuscarTodosJogos()
    {
        var games = await _jogoRepository.GetByConditionAsync(g => true);

        return games.Select(g => new JogoInput
        {
            Titulo = g.Titulo,
            Genero = g.Genero,
            Plataforma = g.Plataforma,
            DtLancamento = g.DtLancamento,
            Multiplayer = g.Multiplayer
        }).ToList();
    }
    public async Task<JogoInput?> GetByIdAsync(int id)
    {
        var game = await _jogoRepository.BuscarAsync(id);
        if (game == null) return null;

        return new JogoInput
        {
            Titulo = game.Titulo,
            Genero = game.Genero,
            Plataforma = game.Plataforma,
            DtLancamento = game.DtLancamento,
            Multiplayer = game.Multiplayer
        };
    }

    public async Task<JogoOutput> CreateAsync(JogoInput jogoinput)
    {
        var jogo = new Jogo
        {
            Titulo = jogoinput.Titulo,
            Genero = jogoinput.Genero,
            Plataforma = jogoinput.Plataforma,
            DtLancamento = jogoinput.DtLancamento,            
            Multiplayer = jogoinput.Multiplayer,
            DataCriacao = DateTime.UtcNow,
        };

        await _jogoRepository.AdicionarAsync(jogo);

        return new JogoOutput
        {
            Titulo = jogo.Titulo,
            Genero = jogo.Genero,
            Plataforma = jogo.Plataforma,
            DtLancamento = jogo.DtLancamento,
            Multiplayer = jogo.Multiplayer
        };
    }

    public async Task<bool> UpdateAsync(int id, JogoUpdateInput jogoupdateinput)
    {
        var jogo = await _jogoRepository.BuscarAsync(id);
        if (jogo == null) return false;

        jogo.Titulo = jogoupdateinput.Titulo;
        jogo.Genero = jogoupdateinput.Genero;
        jogo.Plataforma = jogoupdateinput.Plataforma;
        jogo.DtLancamento = jogoupdateinput.DtLancamento;
        jogo.Multiplayer = jogoupdateinput.Multiplayer;

        await _jogoRepository.AlterarAsync (jogo);
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _jogoRepository.DeleteAsync(id);
    }
}