﻿using HappyPaws.API.Contracts.DTOs.UserDTOs;
using HappyPaws.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HappyPaws.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]

    public class UsersController : ControllerBase
    {
        private readonly IUserService _usersService;

        public UsersController(IUserService usersSerevice) 
        {
            _usersService = usersSerevice;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserDTO>), (StatusCodes.Status200OK))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAsync()
        {
            var users = await _usersService.GetAllAsync();

            var result = users.Select(UserDTO.FromDomain).ToList();

            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var user = await _usersService.GetAsync(id);

            if (user == null) return NotFound($"User with id {id} does not exist.");

            return Ok(UserDTO.FromDomain(user));
        }

        [HttpPost]
        [ProducesResponseType(typeof(UserDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync(CreateUserDTO userDTO)
        {
            var created = await _usersService.AddAsync(CreateUserDTO.ToDomain(userDTO));

            return StatusCode(StatusCodes.Status201Created, UserDTO.FromDomain(created));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(UserDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAsync(Guid id, UpdateUserDTO userDTO)
        {
            var user = _usersService.GetAsync(id);

            if (user == null) return NotFound($"User with id {id} does not exist.");

            var updated = await _usersService.UpdateAsync(id, UpdateUserDTO.ToDomain(userDTO));

            return Ok(UserDTO.FromDomain(updated));
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var user = _usersService.GetAsync(id);

            if (user == null) return NotFound($"User with id {id} does not exist.");

            await _usersService.DeleteAsync(id);

            return NoContent();
        }
    }
}