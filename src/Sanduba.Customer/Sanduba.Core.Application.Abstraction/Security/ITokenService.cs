namespace Sanduba.Core.Application.Abstraction.Security
{
    internal interface ITokenService<T, P>
    {
        public T GenerateToken(P parameter);
    }
}
