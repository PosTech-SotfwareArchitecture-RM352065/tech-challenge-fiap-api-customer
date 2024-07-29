using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Sanduba.Infrastructure.Persistence.SqlServer.Customers.Schemas
{
    public class CustomerRequest
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        public Guid CustomerId { get; set; }

        [Required]
        [Column(TypeName = "datetime")]
        public DateTime RequestedAt { get; set; }

        [Required]
        [MaxLength(10)]
        [Column(TypeName = "varchar(10)")]
        public string Type { get; set; }

        [Required]
        [MaxLength(10)]
        [Column(TypeName = "varchar(10)")]
        public string Status { get; set; }

        //[Required]
        //[MaxLength(50)]
        //[Column(TypeName = "varchar(50)")]
        //public string Name { get; set; }

        //[Required]
        //[MaxLength(200)]
        //[Column(TypeName = "varchar(200)")]
        //public string Address { get; set; }

        //[Required]
        //[MaxLength(200)]
        //[Column(TypeName = "varchar(50)")]
        //public string PhoneNumer { get; set; }

        [MaxLength(200)]
        [Column(TypeName = "varchar(200)")]
        public string? Comments { get; set; }
    }
}
