using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Sanduba.Core.Domain.Costumers;
using CostumerDomain = Sanduba.Core.Domain.Costumers.Costumer<Sanduba.Core.Domain.Costumers.CPF>;

namespace Sanduba.Infrastructure.Persistence.SqlServer.Costumers.Schema
{
    public class Costumer
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(11)]
        [Column(TypeName = "varchar(11)")]
        public string CPF { get; set; }

        [Required]
        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Email { get; init; }

        [Required]
        [Column(TypeName = "BINARY(64)")]
        public string Password { get; init; }

        public CostumerDomain ToDomain()
        {
            return IdentifiedCostumer.CreateCostumer(Id, CPF, Name, Email, Password);
        }

        public static Costumer ToSchema(CostumerDomain costumer)
        {
            return new Costumer
            {
                Id = costumer.Id,
                CPF = costumer?.RegistryIdentification?.ToString(),
                Email = costumer.Email,
                Name = costumer.Name
            };
        }
    }
}
