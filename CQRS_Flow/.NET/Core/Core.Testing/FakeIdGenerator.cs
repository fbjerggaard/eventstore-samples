using System;
using Core.Ids;

namespace Core.Testing;

public class FakeIdGenerator: IIdGenerator
{
    public Guid? LastGeneratedId { get; private set; }

    public Guid New()
    {
        return (LastGeneratedId = Guid.NewGuid()).Value;
    }
}
