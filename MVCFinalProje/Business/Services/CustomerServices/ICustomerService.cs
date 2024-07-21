using MVCFinalProje.Business.DTOs.CustomerDTOs;
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
    }
}
