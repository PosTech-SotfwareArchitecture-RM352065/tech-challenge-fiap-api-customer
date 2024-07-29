using AutoMapper;
using Sanduba.Core.Domain.Customers;
using CustomerDomain = Sanduba.Core.Domain.Customers.IdentifiedCustomer;
using CustomerSchema = Sanduba.Infrastructure.Persistence.SqlServer.Customers.Schemas.Customer;
using CustomerRequestDomain = Sanduba.Core.Domain.Customers.Request;
using CustomerRequestSchema = Sanduba.Infrastructure.Persistence.SqlServer.Customers.Schemas.CustomerRequest;

namespace Sanduba.Infrastructure.Persistence.SqlServer.Customers.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<CustomerDomain, CustomerSchema>()
                .ForPath(destination => destination.Id, source => source.MapFrom(col => col.Id))
                .ForPath(destination => destination.CPF, source => source.MapFrom(col => col.RegistryIdentification))
                .ForPath(destination => destination.Name, source => source.MapFrom(col => col.Name))
                .ForPath(destination => destination.Email, source => source.MapFrom(col => col.Email))
                .ForPath(destination => destination.Password, source => source.MapFrom(col => col.Password));

            CreateMap<CustomerSchema, CustomerDomain>()
                .ConstructUsing(source => new CustomerDomain(source.Id, source.CPF, source.Name, source.Email, source.Password));

            CreateMap<CustomerRequestSchema, CustomerRequestDomain>();
            //    .ForPath(destination => destination.Id, source => source.MapFrom(col => col.Id))
            //    .ForPath(destination => destination.Id, source => source.MapFrom(col => col.Id))
            //    .ForPath(destination => destination.Id, source => source.MapFrom(col => col.Id))
            //    .ForPath(destination => destination.Id, source => source.MapFrom(col => col.Id));

        }
    }
}