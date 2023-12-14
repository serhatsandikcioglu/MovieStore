using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MovieStore.Data.DTOs;
using MovieStore.Data.Entities;
using MovieStore.Data.Interfaces;
using MovieStore.Data.ViewModels;
using MovieStore.Service.Interfaces;
using System.IO;

namespace MovieStore.API.Controllers
{
    [Route("api/v1/directors")]
    [ApiController]
    public class DirectorController : ControllerBase
    {
        private readonly IDirectorService _directorService;
        private readonly IValidator<DirectorCreateDTO> _createValidator;
        private readonly IValidator<DirectorUpdateDTO> _updateValidator;

        public DirectorController(IDirectorService directorService, IValidator<DirectorCreateDTO> createValidator, IValidator<DirectorUpdateDTO> updateValidator)
        {
            _directorService = directorService;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] string? sort, int page = 1, int size = 10)
        {
            List<DirectorViewModel> directorViewModels = _directorService.GetAll(sort, page, size);
            return Ok(directorViewModels);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            bool directorExist = _directorService.IsExist(id);
            if (directorExist)
            {
                DirectorViewModel directorViewModel = _directorService.GetById(id);
                return Ok(directorViewModel);
            }
            return NotFound();
        }
        [HttpPost]
        public IActionResult Create([FromBody] DirectorCreateDTO directorCreateDTO)
        {
            var validateResult = _createValidator.Validate(directorCreateDTO);
            if (!validateResult.IsValid)
            {
                return BadRequest();
            }
            DirectorViewModel directorViewModel = _directorService.Add(directorCreateDTO);
            return CreatedAtAction(nameof(GetById), new { id = directorViewModel.Id }, directorViewModel);
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] DirectorUpdateDTO directorUpdateDTO)
        {
            var validateResult = _updateValidator.Validate(directorUpdateDTO);
            if (!validateResult.IsValid)
            {
                return BadRequest();
            }
            bool directorExist = _directorService.IsExist(id);
            if (directorExist)
            {
                _directorService.Update(id, directorUpdateDTO);
                return Ok();
            }
            return NotFound();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            bool directorExist = _directorService.IsExist(id);
            if (directorExist)
            {
                _directorService.Delete(id);
                return NoContent();
            }
            return NotFound();
        }
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] JsonPatchDocument<Director> patchDoc)
        {
            bool directorExist = _directorService.IsExist(id);
            if (directorExist)
            {
                _directorService.Patch(id, patchDoc);
                return Ok();
            }
            return NotFound();
        }
    }
}
