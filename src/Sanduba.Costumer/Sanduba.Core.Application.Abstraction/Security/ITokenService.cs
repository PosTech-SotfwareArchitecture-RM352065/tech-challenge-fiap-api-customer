using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanduba.Core.Application.Abstraction.Security
{
    internal interface ITokenService<T, P>
    {
        public T GenerateToken(P parameter);
    }
}
