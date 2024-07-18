using CarWashAPI.Interface;
using CarWashAPI.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarWashAPI.DTO;

namespace CarWashAPI.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ICarRepository _carRepository;

        public CarsController(ICarRepository carRepository)
        {
            _carRepository = carRepository ?? throw new ArgumentNullException(nameof(carRepository));
        }

        private Car MapDtoToModel(CarDTO carDto)
        {
            return new Car
            {
                UserId = carDto.UserId,
                Make = carDto.Make,
                Model = carDto.Model,
                Year = carDto.Year,
                LicensePlate = carDto.LicensePlate,
                ImageUrl = carDto.ImageUrl
            };
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car>>> GetCars()
        {
            try
            {
                var cars = await _carRepository.GetAllCarsAsync();
                return Ok(cars);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetCar(int id)
        {
            try
            {
                var car = await _carRepository.GetCarByIdAsync(id);
                if (car == null)
                {
                    return NotFound();
                }
                return Ok(car);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("user/{UserId}")]
        public async Task<ActionResult<Car>> GetCarByUserId(int UserId)
        {
            try
            {
                var car = await _carRepository.GetCarByUserIdAsync(UserId);
                if (car == null)
                {
                    return NotFound();
                }
                return Ok(car);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Car>> PostCar(CarDTO carDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var car = MapDtoToModel(carDto);
                var createdCar = await _carRepository.AddCarAsync(car);

                // Optionally, you can return the created Car object directly
                return CreatedAtAction(nameof(GetCar), new { id = createdCar.CarId }, createdCar);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCar(int id, CarDTO carDto)
        {
            if (id != carDto.UserId)
            {
                return BadRequest();
            }

            try
            {
                var car = MapDtoToModel(carDto);
                var updatedCar = await _carRepository.UpdateCarAsync(car);
                if (updatedCar == null)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            try
            {
                var result = await _carRepository.DeleteCarAsync(id);
                if (!result)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
