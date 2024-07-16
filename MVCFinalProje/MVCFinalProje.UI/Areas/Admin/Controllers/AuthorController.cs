using Mapster;
using Microsoft.AspNetCore.Mvc;
using MVCFinalProje.Business.DTOs.AuthorDTOs;
using MVCFinalProje.Business.Services.AuthorServices;
using MVCFinalProje.UI.Areas.Admin.Models.AuthorVM;
using MVCFinalProje.UI.Models.AuthorVM;

namespace MVCFinalProje.UI.Areas.Admin.Controllers
{
    public class AuthorController : AdminBaseController
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _authorService.GetAllAsync();
            if (!result.IsSuccess)
            {
                ErrorNotyf(result.Message);
                //await Console.Out.WriteLineAsync(result.Message);
                return View(result.Data.Adapt<List<AdminAuthorListVM>>());
            }
            SuccesNotyf(result.Message);
            return View(result.Data.Adapt<List<AdminAuthorListVM>>());
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AdminAuthorCreateVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var authorCreateDTO = model.Adapt<AuthorCreateDTO>();
            var result = await _authorService.AddAsync(authorCreateDTO);

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
            var result = await _authorService.DeleteByAsync(id);
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
            var result = await _authorService.GetByIdAsync(id);
            if (!result.IsSuccess)
            {
                ErrorNotyf(result.Message);
                return RedirectToAction("Index");
            }
            SuccesNotyf(result.Message);
            return View(result.Data.Adapt<AdminAuthorUpdateVM>());
        }


        [HttpPost]
        public async Task<IActionResult> Edit(AdminAuthorUpdateVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _authorService.UpdateByAsync(model.Adapt<AuthorUpdateDTOs>());
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
