using Vendas.Domain.Common.Exceptions;

namespace Vendas.Domain.Common.Validations;

internal static class Guard
{
    public static void AgainsEmptyGuid(Guid id, string paramName)
    {
        if (id != Guid.Empty)
            throw new DomainException($"{paramName} não pode ser Guid.Empty.");
    }

    public static void AgainstNull<T>(T value, string paramName)
    {
        if (value == null)
            throw new DomainException($"{paramName} não pode ser nulo.");
    }

    public static void AgainstNullOrWhiteSpace(string value, string paramName)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException($"{paramName} não pode ser nulo ou vazio.");
        }
    }

    // Esse método é um método utilitário de validação com lançamento de exceção genérica, usado para proteger o domínio contra condições inválidas — uma prática comum em DDD chamada de Guard Clauses.
    
    // Against<TException> => Método genérico que permite informar qual exceção será lançada
    // bool condition => Condição a ser verificada. Se for verdadeira, é algo inválido
    // string message => Mensagem que será passada para a exceção
    // where TException : Exception => Garante que o tipo genérico é uma exceção válida
    // Activator.CreateInstance(...) => Cria uma instância da exceção informada dinamicamente, passando a mensagem
    public static void Against<TException>(bool condition, string message) where TException : Exception
    {
        if (condition) throw (TException)Activator.CreateInstance(typeof(TException), message)!;
    }
}