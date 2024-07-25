using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MVCFinalProje.Business.DTOs.CustomerDTOs;
using MVCFinalProje.Business.Services.AccountServices;
using MVCFinalProje.Domain.Entities;
using MVCFinalProje.Infrastructure.Repositories.CustomerRepository;
using MVCFinalProje.Utilities.Concretes;
using MVCFinalProje.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
            var str = await _customerRepository.CreateExecutionStrategy();
            await str.ExecuteAsync(async () =>
            {
                var transactionScope = await _customerRepository.BeginTransaction().ConfigureAwait(false);
                try
                {
                    var identityResult = await _accountService.CreateUserAsync(user, Domain.Enums.Roles.Customer);
                    if (!identityResult.Succeeded)
                    {
                        result = new ErrorResult(identityResult.ToString());
                        transactionScope.Rollback();
                        return;
                    }
                    var newCustomer = customerCreateDTO.Adapt<Customer>();
                    newCustomer.IdentityId = user.Id;
                    await _customerRepository.AddAsync(newCustomer);
                    await _customerRepository.SaveChangeAsync();
                    result = new SuccessResult("Müşteri Ekleme başarılı");
                    transactionScope.Commit();
                }
                catch (Exception ex)
                {
                    result = new ErrorResult("Hata: " + ex.Message);
                    transactionScope.Rollback();
                }
                finally
                {
                    transactionScope.Dispose();
                }
                
            });
            return result;

        }
        // Delete - Edit
        public async Task<IResult> DeleteByAsync(Guid id)
        {
            // Müşteriyi ID'ye göre veritabanından alıyoruz.
            var customer = await _customerRepository.GetByIdAsync(id);

            // Eğer müşteri bulunamazsa, hata sonucu döndür.
            if (customer == null)
            {
                return new ErrorResult("Müşteri bulunamadı");
            }

            Result result = new Result();

            // Veritabanı işlem stratejisi oluşturuyoruz.
            var str = await _customerRepository.CreateExecutionStrategy();
            await str.ExecuteAsync(async () =>
            {
                var transactionScope = await _customerRepository.BeginTransaction().ConfigureAwait(false);
                try
                {
                    // Müşteriyi veritabanından siliyoruz.
                    await _customerRepository.DeleteAsync(customer);

                    // İlgili müşteri kimliğini kullanarak hesabı siliyoruz.
                    var identityResult = await _accountService.DeleteByAsync(customer.IdentityId);

                    // Eğer hesap silme işlemi başarısız olursa, hata sonucu döndür ve işlemi geri al.
                    if (!identityResult.Succeeded)
                    {
                        result = new ErrorResult(identityResult.ToString());
                        transactionScope.Rollback();
                        return;
                    }
                    // Veritabanı değişikliklerini kaydediyoruz.
                    await _customerRepository.SaveChangeAsync();
                    result = new SuccessResult("Müşteri silme başarılı");
                    transactionScope.Commit();
                }
                catch (Exception ex)
                {
                    // Bir hata oluşursa, hata sonucu döndür ve işlemi geri al.
                    result = new ErrorResult("Hata: " + ex.Message);
                    transactionScope.Rollback();
                }
                finally
                {
                    // İşlem kapsamını temizliyoruz.
                    transactionScope.Dispose();
                }
            });

            return result;
        }

        public async Task<IDataResult<List<CustomerListDTO>>> GetAllAsync()
        {
            var customerDTOs = (await _customerRepository.GetAllAsync()).Adapt<List<CustomerListDTO>>();
            return new SuccessDataResult<List<CustomerListDTO>>(customerDTOs,"Müşteri listeleme başarılı");
        }

        public async Task<IDataResult<CustomerDTO>> GetByIdAsync(Guid id)
        {
            try
            {
                var customer = await _customerRepository.GetByIdAsync(id);
                if (customer == null)
                {
                    return new ErrorDataResult<CustomerDTO>("Müşteri bulunamadı");
                }
                var customerDTO = customer.Adapt<CustomerDTO>();
                return new SuccessDataResult<CustomerDTO>(customerDTO, "Müşteri başarıyla getirildi");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<CustomerDTO>("Hata: " + ex.Message);
            }
        }

        public async Task<IResult> UpdateByAsync(CustomerUpdateDTO customerUpdateDTO)
        {
            // Müşteriyi ID'ye göre veritabanından alıyoruz.
            var customer = await _customerRepository.GetByIdAsync(customerUpdateDTO.Id);

            // Eğer müşteri bulunamazsa, hata sonucu döndür.
            if (customer == null)
            {
                return new ErrorResult("Müşteri bulunamadı");
            }

            Result result = new Result();
            var str = await _customerRepository.CreateExecutionStrategy();

            // İşlem stratejisini kullanarak işlemleri yürütüyoruz.
            await str.ExecuteAsync(async () =>
            {
                var transactionScope = await _customerRepository.BeginTransaction().ConfigureAwait(false);
                try
                {
                    // Gelen DTO'dan müşteriye verileri kopyalıyoruz.
                    customer = customerUpdateDTO.Adapt(customer);
                    // Müşteriyi veritabanında güncelliyoruz.
                    await _customerRepository.UpdateAsync(customer);

                    // Eğer e-posta adresi değiştiyse, Identity kullanıcısını da güncelle
                    if (customer.Email != customerUpdateDTO.Email)
                    {
                        var identityResult = await _accountService.UpdateEmailAsync(customer.IdentityId, customerUpdateDTO.Email);

                        // Eğer hesap güncelleme işlemi başarısız olursa, hata sonucu döndür ve işlemi geri al.
                        if (!identityResult.Succeeded)
                        {
                            result = new ErrorResult(identityResult.ToString());
                            transactionScope.Rollback();
                            return;
                        }
                    }
                    // Veritabanı değişikliklerini kaydediyoruz.
                    await _customerRepository.SaveChangeAsync();
                    result = new SuccessResult("Müşteri güncelleme başarılı");
                    transactionScope.Commit();
                }
                catch (Exception ex)
                {
                    result = new ErrorResult("Hata: " + ex.Message);
                    transactionScope.Rollback();
                }
                finally
                {
                    // İşlem kapsamını temizliyoruz.
                    transactionScope.Dispose();
                }
            });
            return result;
        }
    }
}
