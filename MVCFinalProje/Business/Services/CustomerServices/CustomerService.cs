using Microsoft.AspNetCore.Identity;
using MVCFinalProje.Business.DTOs.CustomerDTOs;
using MVCFinalProje.Business.Services.AccountServices;
using MVCFinalProje.Infrastructure.Repositories.CustomerRepository;
using MVCFinalProje.Utilities.Concretes;
using MVCFinalProje.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCFinalProje.Business.Services.CustomerServices
{
    public class CustomerService : ICustomerService
    {
        private readonly IAccountService _accountService;
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(IAccountService accountService, ICustomerRepository customerRepository)
        {
            _accountService = accountService;
            _customerRepository = customerRepository;
        }

        public async Task<IResult> AddAsync(CustomerCreateDTO customerCreateDTO)
        {
            if (await _accountService.AnyAsync(x=>x.Email == customerCreateDTO.Email))
            {
                return new ErrorResult("Email adresi kullanılıyor");
            }
            IdentityUser user = new()
            {
                Email = customerCreateDTO.Email,
                NormalizedEmail = customerCreateDTO.Email.ToUpperInvariant(),
                UserName = customerCreateDTO.Email,
                NormalizedUserName = customerCreateDTO.Email.ToUpperInvariant(),
                EmailConfirmed = true

            };
            Result result = new Result();
            // var str = await _customerRepository.
        }
    }
}
