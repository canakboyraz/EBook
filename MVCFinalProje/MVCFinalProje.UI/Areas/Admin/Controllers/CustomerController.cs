using Mapster;
using Microsoft.AspNetCore.Mvc;
using MVCFinalProje.Business.DTOs.CustomerDTOs;
using MVCFinalProje.Business.DTOs.PublisherDTOs;
using MVCFinalProje.Business.Services.CustomerServices;
using MVCFinalProje.UI.Areas.Admin.Models.CustomerVM;
using MVCFinalProje.UI.Models.PublisherVM;

namespace MVCFinalProje.UI.Areas.Admin.Controllers
{
    public class CustomerController : AdminBaseController
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _customerService.GetAllAsync();
            SuccesNotyf(result.Message);
            return View(result.Data.Adapt<List<AdminCustomerListVM>>());
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AdminCustomerCreateVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _customerService.AddAsync(model.Adapt<CustomerCreateDTO>());
            if (!result.IsSuccess)
            {
                ErrorNotyf(result.Message);
                return View(model);
            }

            SuccesNotyf(result.Message);
            return RedirectToAction("Index");
        }

        //Edit Delete

        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _customerService.DeleteByAsync(id);
            if (!result.IsSuccess)
            {
                ErrorNotyf(result.Message);
                return RedirectToAction("Index");
            }
            SuccesNotyf(result.Message);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var result = await _customerService.GetByIdAsync(id);
            if (!result.IsSuccess)
            {
                ErrorNotyf(result.Message);
                return RedirectToAction("Index");
            }
            var editVM = result.Data.Adapt<AdminCustomerEditVM>();
            return View(editVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AdminCustomerEditVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await _customerService.UpdateByAsync(model.Adapt<CustomerUpdateDTO>());
            if (!result.IsSuccess)
            {
                ErrorNotyf(result.Message);
                return View(model);
            }
            SuccesNotyf(result.Message);
            return RedirectToAction("Index");
        }
        
    }
}
