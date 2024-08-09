using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sanduba.Infrastructure.Persistence.SqlServer.Customers.Schemas
{
    public class Customer
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [MaxLength(11)]
        [Column(TypeName = "varchar(11)")]
        public string? CPF { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string? Name { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string? Email { get; init; }

        [Column(TypeName = "BINARY(64)")]
        public string? Password { get; init; }

        [Column(TypeName = "BIT")]
        public bool Active { get; init; }

        public ICollection<CustomerRequest> Requests { get; init; }
    }
}
