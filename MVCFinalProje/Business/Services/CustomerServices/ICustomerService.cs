using MVCFinalProje.Business.DTOs.CustomerDTOs;
using MVCFinalProje.Business.DTOs.PublisherDTOs;
using MVCFinalProje.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCFinalProje.Business.Services.CustomerServices
{
    public interface ICustomerService
    {
        Task<IResult> AddAsync(CustomerCreateDTO customerCreateDTO);
        Task<IDataResult<List<CustomerListDTO>>> GetAllAsync();

        Task<IResult> DeleteByAsync(Guid id);

        Task<IDataResult<CustomerDTO>> GetByIdAsync(Guid id);

        Task<IResult> UpdateByAsync(CustomerUpdateDTO customerUpdateDTO);
    }
}
