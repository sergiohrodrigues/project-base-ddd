namespace Vendas.Domain.Common.Base;

public abstract class Entity
{
    public Guid Id { get; protected set; }
    public DateTime DataCriacao { get; protected set; }
    public DateTime? DataAtualizacao { get; protected set; }

    protected Entity()
    {
        Id = new Guid();
        DataCriacao = DateTime.Now;
    }

    protected void SetDataAtualizacao()
    {
        DataAtualizacao = DateTime.Now;
    }

    protected Entity(Guid id)
    {
        Id = id;
    }

    // Garante que duas entidades com mesmo Id sejam tratadas como iguais, mesmo que sejam instâncias diferentes. Ajuda a definir a igualdade "lógica" da entidade.
    public override bool Equals(object obj)
    {
        if(obj is not Entity other) return false; // Se o objeto não for uma Entity, são diferentes.
        if(ReferenceEquals(this, other)) return true; // Se referenciam o MESMO objeto na memória, são iguais.

        return Id.Equals(other.Id); // Caso contrário, compara pelo Id.
    }

    // Retorna um número baseado no Id.
    // Necessário porque, ao sobrescrever Equals, também é obrigatório sobrescrever GetHashCode.
    // Usado principalmente em coleções como HashSet ou como chave de dicionário (Dictionary), para garantir que entidades com mesmo Id gerem mesmo hash.
    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public static bool operator ==(Entity left, Entity right)
    {
        if(ReferenceEquals(left, null))
            return ReferenceEquals(right, null); // Se o lado esquerdo é null, verifica se o direito também é null.

        return left.Equals(right); // Se não é null, usa o Equals para comparar.
    }

    public static bool operator !=(Entity left, Entity right)
    {
        return !(left == right); // Inverso de ==
    }
}