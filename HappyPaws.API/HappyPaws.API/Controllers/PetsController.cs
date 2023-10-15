﻿using HappyPaws.API.Contracts.DTOs.PetDTOs;
using HappyPaws.Application.Interfaces;
using HappyPaws.Core.Enums;
using Microsoft.AspNetCore.Mvc;

namespace HappyPaws.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class PetsController : ControllerBase
    {
        private readonly IPetService _petsService;
        private readonly IUserService _usersService;

        public PetsController(IPetService petsService, IUserService usersService)
        {
            _petsService = petsService;
            _usersService = usersService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PetDTO>), (StatusCodes.Status200OK))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAsync()
        {
            var pets = await _petsService.GetAllAsync();

            var result = pets.Select(PetDTO.FromDomain).ToList();

            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PetDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var pet = await _petsService.GetAsync(id);

            if (pet == null) return NotFound($"Pet with id {id} does not exist.");

            return Ok(PetDTO.FromDomain(pet));
        }

        [HttpPost]
        [ProducesResponseType(typeof(PetDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync(CreatePetDTO petDTO)
        {
            var owner = await _usersService.GetAsync(petDTO.OwnerId);

            if (owner == null) return BadRequest("Invalid OwnerId. No such user exists.");

            if (owner.Type != UserType.Client) return BadRequest("Invalid OwnerId. Only users of type Client can own pets.");

            var created = await _petsService.AddAsync(CreatePetDTO.ToDomain(petDTO));

            return StatusCode(StatusCodes.Status201Created, PetDTO.FromDomain(created));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(PetDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAsync(Guid id, UpdatePetDTO petDTO)
        {
            var pet = _petsService.GetAsync(id);

            if (pet == null) return NotFound($"Pet with id {id} does not exist.");

            var updated = await _petsService.UpdateAsync(id, UpdatePetDTO.ToDomain(petDTO));

            return Ok(PetDTO.FromDomain(updated));
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var pet = _petsService.GetAsync(id);

            if (pet == null) return NotFound($"Pet with id {id} does not exist.");

            await _petsService.DeleteAsync(id);

            return NoContent();
        }

        //[HttpGet]
        //[ProducesResponseType(typeof(IEnumerable<PetDTO>), (StatusCodes.Status200OK))]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<IActionResult> GetByOwnerAsync(Guid id)
        //{
        //    var pets = await _petsService.GetAllAsync();

        //    var result = pets.Select(PetDTO.FromDomain).ToList();

        //    return Ok(result);
        //}
    }
}