using Core.Entity;
using Core.Input;
using Core.Repository;
using Core.Services;
using Moq;

namespace CloudGames.Test.Core.Services
{
    public class UsuarioServiceTest
    {
        //public static DbContextOptions<ApplicationDbContext> dbContextOptions { get }

        [Fact(DisplayName = "Email inv�lido")]
        public async Task ValidandoEmail()
        {
            var repoMock = new Mock<IUsuarioRepository>();
            var service = new UsuarioService(repoMock.Object);
            var dto = new UsuarioInput { Nome = "Teste XUnit", Email = "xunit", Senha = "Senhateste123", IsAdmin = false };

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => service.RegisterAsync(dto));
            Assert.Equal("E-mail inv�lido.", ex.Message);
        }

        [Fact(DisplayName = "Verifica e-mail se tem email cadastrado")]
        public async Task ValidaEmailjaCadastrado()
        {
            var repoMock = new Mock<IUsuarioRepository>();
            repoMock.Setup(r => r.ObterUsuarioPorEmailAsync(It.IsAny<string>())).ReturnsAsync(new Usuario());
            var service = new UsuarioService(repoMock.Object);
            var dto = new UsuarioInput { Nome = "Teste XUnit", Email = "teste@teste.com.br", Senha = "Senhateste123@", IsAdmin = false };

            var ex = await Assert.ThrowsAsync<InvalidOperationException>(() => service.RegisterAsync(dto));
            Assert.Equal("E-mail j� cadastrado.", ex.Message);
        }

        [Fact(DisplayName = "Deve cadastrar usu�rio v�lido com sucesso")]
        public async Task CadastrarUsuarioValido()
        {
            var repoMock = new Mock<IUsuarioRepository>();
            repoMock.Setup(r => r.ObterUsuarioPorEmailAsync(It.IsAny<string>())).ReturnsAsync((Usuario?)null);
            repoMock.Setup(r => r.AdicionarAsync(It.IsAny<Usuario>()))
                .ReturnsAsync((Usuario u) => u);

            var service = new UsuarioService(repoMock.Object);
            var dto = new UsuarioInput { Nome = "Teste XUnit", Email = "xunit@teste.com.br", Senha = "Samurai1234@", IsAdmin = false };

            var result = await service.RegisterAsync(dto);

            Assert.NotNull(result);
            Assert.Equal(dto.Email, result.Email);
            Assert.Equal(dto.Nome, result.Nome);
            Assert.Equal(dto.IsAdmin, result.IsAdmin);
        }

        [Fact(DisplayName = "Dado um usu�rio com senha fraca, deve lan�ar exce��o")]
        public async Task UsuarioSenhaFraca_DeveFalhar()
        {
            var repoMock = new Mock<IUsuarioRepository>();
            var service = new UsuarioService(repoMock.Object);
            var dto = new UsuarioInput { Nome = "Teste XUnit", Email = "xunit@testesenha.com.br", Senha = "1", IsAdmin = false };

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => service.RegisterAsync(dto));
            Assert.Equal("Senha fraca. A senha deve ter no m�nimo 8 caracteres, incluir n�meros, letras e caracteres especiais.", ex.Message);
        }
    }
}