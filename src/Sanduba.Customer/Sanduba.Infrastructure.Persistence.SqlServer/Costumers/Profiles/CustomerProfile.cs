using AutoMapper;
using Sanduba.Core.Domain.Customers;
using CustomerDomain = Sanduba.Core.Domain.Customers.Customer<Sanduba.Core.Domain.Customers.CPF>;
using CustomerSchema = Sanduba.Infrastructure.Persistence.SqlServer.Customers.Schema.Customer;

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
                .ConstructUsing(source => IdentifiedCustomer.CreateCustomer(source.Id, source.CPF, source.Name, source.Email, source.Password));
        }
    }
}