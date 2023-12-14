using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MovieStore.Data.DTOs;
using MovieStore.Data.Entities;
using MovieStore.Data.ViewModels;
using MovieStore.Service.Interfaces;
using System.Numerics;

namespace MovieStore.API.Controllers
{
    [Route("api/v1/actors")]
    [ApiController]
    public class ActorController : ControllerBase
    {
        private readonly IActorService _actorService;
        private readonly IValidator<ActorCreateDTO> _createValidator;
        private readonly IValidator<ActorUpdateDTO> _updateValidator;

        public ActorController(IActorService actorService, IValidator<ActorCreateDTO> createValidator, IValidator<ActorUpdateDTO> updateValidator)
        {
            _actorService = actorService;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }
        [HttpGet]
        public IActionResult GetAll([FromQuery] string? sort, int page = 1, int size = 10)
        {
            List<ActorViewModel> actorViewModels = _actorService.GetAll(sort, page, size);
            return Ok(actorViewModels);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            bool actorExist = _actorService.IsExist(id);
            if (actorExist)
            {
                ActorViewModel actorViewModel = _actorService.GetById(id);
                return Ok(actorViewModel);
            }
            return NotFound();
        }
        [HttpPost]
        public IActionResult Create([FromBody] ActorCreateDTO actorCreateDTO)
        {
            var validateResult = _createValidator.Validate(actorCreateDTO);
            if (!validateResult.IsValid)
            {
                return BadRequest();
            }
            ActorViewModel actorViewModel = _actorService.Add(actorCreateDTO);
            return CreatedAtAction(nameof(GetById), new { id = actorViewModel.Id }, actorViewModel);
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ActorUpdateDTO actorUpdateDTO)
        {
            var validateResult = _updateValidator.Validate(actorUpdateDTO);
            if (!validateResult.IsValid)
            {
                return BadRequest();
            }
            bool actorExist = _actorService.IsExist(id);
            if (actorExist)
            {
                _actorService.Update(id,actorUpdateDTO);
                return Ok();
            }
            return NotFound();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            bool actorExist = _actorService.IsExist(id);
            if (actorExist)
            {
                _actorService.Delete(id);
                return NoContent();
            }
            return NotFound();
        }
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] JsonPatchDocument<Actor> patchDoc)
        {
            bool actorExist = _actorService.IsExist(id);
            if (actorExist)
            {
                _actorService.Patch(id, patchDoc);
                return Ok();
            }
            return NotFound();
        }
    }
}
