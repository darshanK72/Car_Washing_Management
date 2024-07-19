﻿using CarWashAPI.Interface;
using CarWashAPI.Model;
using CarWashAPI.DTO; 
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarWash2.DTO;

namespace CarWashAPI.Controllers
{
    [Route("api/washers")]
    [ApiController]
    public class WashersController : ControllerBase
    {
        private readonly IWasherRepository _washerRepository;

        public WashersController(IWasherRepository washerRepository)
        {
            _washerRepository = washerRepository ?? throw new ArgumentNullException(nameof(washerRepository));
        }

        private WasherDto MapWasherToDto(Washer washer)
        {
            return new WasherDto
            {
                WasherId = washer.WasherId,
                Name = washer.Name,
                Email = washer.Email,
                Password = washer.Password,
                PhoneNumber = washer.PhoneNumber,
                IsActive = washer.IsActive
            };
        }

        private Washer MapDtoToWasher(WasherDto washerDto)
        {
            return new Washer
            {
                WasherId = washerDto.WasherId,
                Name = washerDto.Name,
                Email = washerDto.Email,
                Password = washerDto.Password,
                PhoneNumber = washerDto.PhoneNumber,
                IsActive = washerDto.IsActive
            };
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WasherDto>>> GetWashers()
        {
            try
            {
                var washers = await _washerRepository.GetAllWashersAsync();
                var washerDtos = new List<WasherDto>();
                foreach (var washer in washers)
                {
                    washerDtos.Add(MapWasherToDto(washer));
                }
                return Ok(washerDtos);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WasherDto>> GetWasher(int id)
        {
            try
            {
                var washer = await _washerRepository.GetWasherByIdAsync(id);
                if (washer == null)
                {
                    return NotFound();
                }
                return Ok(MapWasherToDto(washer));
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<ActionResult<WasherDto>> PostWasher(WasherDto washerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var washer = MapDtoToWasher(washerDto);
                var createdWasher = await _washerRepository.AddWasherAsync(washer);

                return CreatedAtAction(nameof(GetWasher), new { id = createdWasher.WasherId }, MapWasherToDto(createdWasher));
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutWasher(int id, WasherDto washerDto)
        {
            if (id != washerDto.WasherId)
            {
                return BadRequest();
            }

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var washer = MapDtoToWasher(washerDto);
                var updatedWasher = await _washerRepository.UpdateWasherAsync(washer);
                if (updatedWasher == null)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWasher(int id)
        {
            try
            {
                var result = await _washerRepository.DeleteWasherAsync(id);
                if (!result)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "This Washer Id Have a Pending order So this washer profile Cannot be deleted");
            }
        }
    }
}
