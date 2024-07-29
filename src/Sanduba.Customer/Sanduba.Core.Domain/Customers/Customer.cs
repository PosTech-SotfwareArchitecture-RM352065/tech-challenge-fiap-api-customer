using Sanduba.Core.Domain.Commons.Assertions;
using Sanduba.Core.Domain.Commons.Types;
using System;
using System.Collections.Generic;

namespace Sanduba.Core.Domain.Customers
{
    public abstract class Customer<T>(Guid id) : Entity<Guid>(id)
    {
        protected List<Request> _requests;
        public T? RegistryIdentification { get; init; }
        public IdentityType IdentityType { get; init; }
        public string Name { get; init; }
        public string Email { get; init; }
        public string Password { get; init; }
        public List<Request> Requests { get; init; }

        public override void ValidateEntity()
        {
            AssertionConcern.AssertArgumentNotEmpty(Name, "Nome inválido. Não deve ser vazio");
            AssertionConcern.AssertArgumentNotEmpty(Email, "E-mail inválido. Não deve ser vazio");
        }
    }
}
