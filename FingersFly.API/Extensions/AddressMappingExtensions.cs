using FingersFly.API.DTOs;
using FingersFly.Domain.Entities;

namespace FingersFly.API.Extensions
{
    public static class AddressMappingExtensions
    {
        public static AddressDto? ToDto(this Address address)
        {
            if (address == null) return null;
            return new AddressDto
            {
                Line1 = address.Line1,
                Line2 = address.Line2,
                City = address.City,
                PostalCode = address.PostalCode,
                Country = address.Country,
            };
        }

        public static Address ToEntity(this AddressDto dto)
        {
            if (dto == null) throw new ArgumentNullException("addressDto");
            return new Address
            {
                Line1 = dto.Line1,
                Line2 = dto.Line2,
                City = dto.City,
                PostalCode = dto.PostalCode,
                Country = dto.Country,
            };
        }

        public static void UpdateFromDto(this Address address, AddressDto dto)
        {
            if (dto == null) throw new ArgumentNullException("addressDto");
            if (address == null) throw new ArgumentNullException("address");

            address.Line1 = dto.Line1;
            address.Line2 = dto.Line2;
            address.City = dto.City;
            address.PostalCode = dto.PostalCode;
            address.Country = dto.Country;
        }
    }
}
