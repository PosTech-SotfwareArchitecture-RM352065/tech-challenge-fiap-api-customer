using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Sanduba.Core.Domain.Customers;
using CustomerDomain = Sanduba.Core.Domain.Customers.Customer<Sanduba.Core.Domain.Customers.CPF>;

namespace Sanduba.Infrastructure.Persistence.SqlServer.Customers.Schema
{
    public class Customer
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
    }
}
