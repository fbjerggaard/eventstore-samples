namespace Core.Ids;

public class NulloIdGenerator: IIdGenerator
{
    public Guid New()
    {
        return Guid.NewGuid();
    }
}
