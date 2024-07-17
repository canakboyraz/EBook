using Mapster;
using Microsoft.AspNetCore.Mvc;
using MVCFinalProje.Business.DTOs.AuthorDTOs;
using MVCFinalProje.Business.DTOs.PublisherDTOs;
using MVCFinalProje.Business.Services.AuthorServices;
using MVCFinalProje.Business.Services.PublisherServices;
using MVCFinalProje.UI.Areas.Admin.Models.AuthorVM;
using MVCFinalProje.UI.Models.PublisherVM;

namespace MVCFinalProje.UI.Areas.Admin.Controllers
{
    public class PublisherController : AdminBaseController
    {
        private readonly IPublisherService _publisherService;

        public PublisherController(IPublisherService publisherService)
        {
            _publisherService = publisherService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _publisherService.GetAllAsync();
            if (!result.IsSuccess)
            {
                ErrorNotyf(result.Message);
                //await Console.Out.WriteLineAsync(result.Message);
                return View(result.Data.Adapt<List<AdminPublisherListVM>>());
            }
            SuccesNotyf(result.Message);
            return View(result.Data.Adapt<List<AdminPublisherListVM>>());
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AdminPublisherCreateVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var publisherCreateDTO = model.Adapt<PublisherCreateDTO>();
            var result = await _publisherService.AddAsync(publisherCreateDTO);

            if (!result.IsSuccess)
            {
                ErrorNotyf(result.Message);
                return View(model);
            }

            SuccesNotyf(result.Message);
            return RedirectToAction("Index");
        }

        // edit ve delete

        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _publisherService.DeleteByAsync(id);
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
            var result = await _publisherService.GetByIdAsync(id);
            if (!result.IsSuccess)
            {
                ErrorNotyf(result.Message);
                return RedirectToAction("Index");
            }
            SuccesNotyf(result.Message);
            return View(result.Data.Adapt<AdminPublisherUpdateVM>());
        }


        [HttpPost]
        public async Task<IActionResult> Edit(AdminPublisherUpdateVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _publisherService.UpdateByAsync(model.Adapt<PublisherUpdateDTO>());
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
