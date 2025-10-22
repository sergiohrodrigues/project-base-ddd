using FluentAssertions;
using Vendas.Domain.ValueObjects;

namespace Vendas.Domain.Tests.ValueObjects;

public class EnderecoEntregaTests
{
    [Test]
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
}