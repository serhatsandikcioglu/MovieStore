using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MovieStore.Data.DTOs;
using MovieStore.Data.Entities;
using MovieStore.Data.ViewModels;
using MovieStore.Service.Interfaces;

namespace MovieStore.API.Controllers
{
    [Route("api/v1/movies")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;
        private readonly IDirectorService _directorService;
        private readonly IValidator<MovieCreateDTO> _createValidator;
        private readonly IValidator<MovieUpdateDTO> _updateValidator;

        public MovieController(IMovieService movieService, IDirectorService directorService, IValidator<MovieCreateDTO> createValidator, IValidator<MovieUpdateDTO> updateValidator)
        {
            _movieService = movieService;
            _directorService = directorService;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }
        [HttpGet]
        public IActionResult GetAll([FromQuery] string? sort, int page = 1, int size = 10)
        {
            List<MovieViewModel> movieViewModels = _movieService.GetAll(sort, page, size);
            return Ok(movieViewModels);
        }
        [HttpGet("{id}/actors")]
        public IActionResult GetById(int id)
        {
            bool movieExist = _movieService.IsExist(id);
            if (movieExist)
            {
                MovieViewModel movieViewModel = _movieService.GetById(id);
                return Ok(movieViewModel);
            }
            return NotFound();
        }
        [HttpPost]
        public IActionResult Create([FromBody] MovieCreateDTO movieCreateDTO)
        {
            var validateResult = _createValidator.Validate(movieCreateDTO);
            if (!validateResult.IsValid)
            {
                return BadRequest();
            }
            bool directorExist = _directorService.IsExist(movieCreateDTO.DirectorId);
            if (directorExist)
            {
            MovieViewModel movieViewModel = _movieService.Add(movieCreateDTO);
            return CreatedAtAction(nameof(GetById), new { id = movieViewModel.Id }, movieViewModel);
            }
            return NotFound();
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] MovieUpdateDTO movieUpdateDTO)
        {
            var validateResult = _updateValidator.Validate(movieUpdateDTO);
            if (!validateResult.IsValid)
            {
                return BadRequest();
            }
            bool movieExist = _movieService.IsExist(id);
            if (movieExist)
            {
                movieUpdateDTO.Id = id;
                _movieService.Update(movieUpdateDTO);
                return Ok();
            }
            return NotFound();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            bool movieExist = _movieService.IsExist(id);
            if (movieExist)
            {
                _movieService.Delete(id);
                return NoContent();
            }
            return NotFound();
        }
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] JsonPatchDocument<Movie> patchDoc)
        {
            bool movieExist = _movieService.IsExist(id);
            if (movieExist)
            {
                _movieService.Patch(id, patchDoc);
                return Ok();
            }
            return NotFound();
        }
    }
}
