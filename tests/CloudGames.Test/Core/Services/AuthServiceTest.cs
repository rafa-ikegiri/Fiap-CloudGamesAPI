using Core.Entity;
using Core.Input;
using Core.Repository;
using Core.Services;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;

namespace CloudGames.Test.Core.Services
{
    public class AuthServiceTest
    {
        [Fact(DisplayName = "Login Sucesso, gera Token")]
        public async Task Login_UsuarioValido()
        {
            var mock = new Mock<IUsuarioRepository>();
            var mockConfig = new Mock<IConfiguration>();

            var usuario = new Usuario {Id = 1,Nome = "Teste",Email = "teste@email.com",Senha = "Senha@123",IsAdmin = false};

            mock.Setup(r => r.ObterUsuarioPorEmailAsync(usuario.Email)).ReturnsAsync(usuario);
            mockConfig.Setup(c => c["Jwt:Key"]).Returns("Teste@AutorizacaoTesteTeste12345678910@");
            mockConfig.Setup(c => c["Jwt:Issuer"]).Returns("Test");
            mockConfig.Setup(c => c["Jwt:Audience"]).Returns("TestAudience");

            var service = new AuthService(mock.Object, mockConfig.Object);
            var dto = new LoginInput { Nome = usuario.Nome, Email = usuario.Email, Senha = "Senha@123" };
            
            var result = await service.LoginAsync(dto);
            
            result.Should().NotBeNull();
            result.Token.Should().NotBeNullOrWhiteSpace();
            result.Nome.Should().Be(usuario.Nome);
            result.Email.Should().Be(usuario.Email);
            result.IsAdmin.Should().Be(usuario.IsAdmin);
        }        

        [Fact(DisplayName = "Senha inválida")]
        public async Task Login_SenhaInvalida()
        {
            var mock = new Mock<IUsuarioRepository>();
            var mockConfig = new Mock<IConfiguration>();            

            var user = new Usuario{ Id = 1, Nome = "Teste", Email = "rafael.cardoso@fiap.com.br", Senha = "Mortadela@123456789", IsAdmin = false};

            mock.Setup(r => r.ObterUsuarioPorEmailAsync(user.Email)).ReturnsAsync(user);

            var service = new AuthService(mock.Object, mockConfig.Object);
            var dto = new LoginInput {Nome = user.Nome, Email = user.Email, Senha = "Senha54645645646464@" };

            var act = async () => await service.LoginAsync(dto);            
            await act.Should().ThrowAsync<UnauthorizedAccessException>()
                .WithMessage("Usuário ou senha inválidos.");
        }

        [Fact(DisplayName = "Usuario não existe na base")]
        public async Task Login_UsuarioNaoExiste()
        {
            var mock = new Mock<IUsuarioRepository>();
            var mockConfig = new Mock<IConfiguration>();

            mock.Setup(r => r.ObterUsuarioPorEmailAsync(It.IsAny<string>())).ReturnsAsync((Usuario?)null);

            var service = new AuthService(mock.Object, mockConfig.Object);
            var dto = new LoginInput { Nome = "Joao Fiap", Email = "emailfiap@teste.com", Senha = "Senha@123testesteste" };

            // Act
            var act = async () => await service.LoginAsync(dto);

            // Assert
            await act.Should().ThrowAsync<UnauthorizedAccessException>()
                .WithMessage("Usuário ou senha inválidos.");
        }
    }
}