using CarWashAPI.Interface;
using CarWashAPI.Model;
using CarWashAPI.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarWashAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackagesController : ControllerBase
    {
        private readonly IPackageRepository _packageRepository;

        public PackagesController(IPackageRepository packageRepository)
        {
            _packageRepository = packageRepository ?? throw new ArgumentNullException(nameof(packageRepository));
        }

        private PackageDto MapPackageToDto(Package package)
        {
            return new PackageDto
            {
                PackageId = package.PackageId,
                Name = package.Name,
                Description = package.Description,
                Price = package.Price
            };
        }

        private Package MapDtoToPackage(PackageDto packageDto)
        {
            return new Package
            {
                PackageId = packageDto.PackageId,
                Name = packageDto.Name,
                Description = packageDto.Description,
                Price = packageDto.Price
            };
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PackageDto>>> GetPackages()
        {
            try
            {
                var packages = await _packageRepository.GetAllPackagesAsync();
                var packageDtos = new List<PackageDto>();
                foreach (var package in packages)
                {
                    packageDtos.Add(MapPackageToDto(package));
                }
                return Ok(packageDtos);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PackageDto>> GetPackage(int id)
        {
            try
            {
                var package = await _packageRepository.GetPackageByIdAsync(id);
                if (package == null)
                {
                    return NotFound();
                }
                return Ok(MapPackageToDto(package));
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<ActionResult<PackageDto>> PostPackage(PackageDto packageDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var package = MapDtoToPackage(packageDto);
                var createdPackage = await _packageRepository.AddPackageAsync(package);

                return CreatedAtAction(nameof(GetPackage), new { id = createdPackage.PackageId }, MapPackageToDto(createdPackage));
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPackage(int id, PackageDto packageDto)
        {
            if (id != packageDto.PackageId)
            {
                return BadRequest();
            }

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var package = MapDtoToPackage(packageDto);
                var updatedPackage = await _packageRepository.UpdatePackageAsync(package);
                if (updatedPackage == null)
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
        public async Task<IActionResult> DeletePackage(int id)
        {
            try
            {
                var success = await _packageRepository.DeletePackageAsync(id);
                if (!success)
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
    }
}
