using Mapster;
using Microsoft.AspNetCore.Mvc;
using MVCFinalProje.Business.DTOs.AuthorDTOs;
using MVCFinalProje.Business.Services.AuthorServices;
using MVCFinalProje.UI.Models.AuthorVM;

namespace MVCFinalProje.UI.Controllers
{
    public class AuthorController : Controller
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
                await Console.Out.WriteLineAsync(result.Message);
                return View(result.Data.Adapt<List<AuthorListVM>>());
            }
            await Console.Out.WriteLineAsync(result.Message);
            return View(result.Data.Adapt<List<AuthorListVM>>());
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AuthorCreateVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var authorCreateDTO =  model.Adapt<AuthorCreateDTO>();
            var result = await _authorService.AddAsync(authorCreateDTO);
            if (!result.IsSuccess)
            {
                await Console.Out.WriteLineAsync(result.Message);
                return View(model);
            }

            await Console.Out.WriteLineAsync(result.Message);
            return RedirectToAction("Index");
        }

        // edit ve delete

        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _authorService.DeleteByAsync(id);
            if (!result.IsSuccess)
            {
                await Console.Out.WriteLineAsync(result.Message);
                return RedirectToAction("Index");
            }
            await Console.Out.WriteLineAsync(result.Message);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var result = await _authorService.GetByIdAsync(id);
            if (!result.IsSuccess)
            {
                await Console.Out.WriteLineAsync(result.Message);
                return RedirectToAction("Index");
            }

            return View(result.Data.Adapt<AuthorUpdateVM>());
        }


        [HttpPost]
        public async Task<IActionResult> Edit(AuthorUpdateVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _authorService.UpdateByAsync(model.Adapt<AuthorUpdateDTOs>());
            if (!result.IsSuccess)
            {
                await Console.Out.WriteLineAsync(result.Message);
                return View(model);
            }
            await Console.Out.WriteLineAsync(result.Message);
            return RedirectToAction("Index");
        }


    }
}
