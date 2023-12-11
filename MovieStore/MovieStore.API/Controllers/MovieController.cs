﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MovieStore.Data.DTOs;
using MovieStore.Data.Entities;
using MovieStore.Data.ViewModels;
using MovieStore.Service.Interfaces;

namespace MovieStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }
        [HttpGet]
        public IActionResult GetAll([FromQuery] string? sort, int page = 1, int size = 10)
        {
            List<MovieViewModel> movieViewModels = _movieService.GetAll(sort, page, size);
            return Ok(movieViewModels);
        }
        [HttpGet("{id}")]
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
            MovieViewModel movieViewModel = _movieService.Add(movieCreateDTO);
            return CreatedAtAction(nameof(GetById), new { id = movieViewModel.Id }, movieViewModel);
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] MovieUpdateDTO movieUpdateDTO)
        {
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