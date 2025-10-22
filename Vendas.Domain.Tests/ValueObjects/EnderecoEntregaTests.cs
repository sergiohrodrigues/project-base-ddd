using FluentAssertions;
using Vendas.Domain.Common.Exceptions;
using Vendas.Domain.ValueObjects;

namespace Vendas.Domain.Tests.ValueObjects;

public class EnderecoEntregaTests
{
    //Testes de criação bem sucedida
    //---------------------------------------------------
    [Fact(DisplayName = "Deve criar EnderecoEntrega com sucesso quando todos os dados são válidos")]
    public void Criar_DeveRetornarEnderecoValido_QuandoDadosForemValidos()
    {
        // Arrage
        var cep = "12345-648";
        var logradouro = "Rua das Flores";
        var complemento = "Apto 101";
        var bairro = "Centro";
        var estado = "SP";
        var cidade = "São Paulo";
        var pais = "Brasil";
        
        //Act
        var endereco = EnderecoEntrega.Criar(cep, logradouro, complemento, bairro, estado, cidade, pais);
        
        // Asset
        endereco.Should().NotBeNull();
        endereco.Cep.Should().Be(cep);
        endereco.Logradouro.Should().Be(logradouro);
        endereco.Complemento.Should().Be(complemento);
        endereco.Bairro.Should().Be(bairro);
        endereco.Estado.Should().Be(estado);
        endereco.Cidade.Should().Be(cidade);
        endereco.Pais.Should().Be(pais);
        endereco.FormatarEndereco().Should().Contain("Rua das Flores");
    }

    //Testes de falha na criação - formatação de cep
    //---------------------------------------------------

    [Theory(DisplayName = "Deve lançar DomainException quando o CEP for inválido")]
    [InlineData("12345678")] // sem hífen
    [InlineData("12-345678")] // formato incorreto
    [InlineData("ABCDE-123")] // caracteres inválidos
    public void Criar_DeveLancarDomainException_QuandoCepForInvalido(string cepInvalido)
    {
        // Arrage
        var logradouro = "Rua das Flores";
        var complemento = "Casa";
        var bairro = "Centro";
        var estado = "SP";
        var cidade = "São Paulo";
        var pais = "Brasil";

        // Act
        Action act = () => EnderecoEntrega.Criar(cepInvalido, logradouro, complemento, bairro, estado, cidade, pais);

        // Assert
        act.Should().Throw<DomainException>()
            .WithMessage("CEP inválido*");
    }
}