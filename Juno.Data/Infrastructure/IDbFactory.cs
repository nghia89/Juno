using System;

namespace Juno.Data.Infrastructure
{
    public interface IDbFactory: IDisposable
    {
        JunoDBContext Init();
    }
}