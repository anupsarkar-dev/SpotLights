using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpotLights.Certification.Core.Interfaces;
using SpotLights.Certification.Domain.Dto;
using SpotLights.Certification.Domain.Model;

namespace SpotLights.Certification.Api.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class CertificationController : ControllerBase
  {
    private readonly ICertificationService _service;

    public CertificationController(ICertificationService service)
    {
      _service = service;
    }

    [HttpGet()]
    public async Task<IEnumerable<CertificateDto>?> GetAllAsync()
    {
      var result = await _service.GetAllAsync();

      return result.Adapt<List<CertificateDto>>();
    }

    [HttpGet("{id}")]
    public async Task<CertificateDto?> GetByIdAsync(int id)
    {
      Certificate? result = await _service.GetByIdAsync(id);

      return result.Adapt<CertificateDto>();
    }

    [HttpPost()]
    public async Task<bool> AddAsync(CertificateDto Certification)
    {
      var item = Certification.Adapt<Certificate>();
      return await _service.AddAsync(item);
    }

    [HttpPut()]
    public async Task<bool> UpdateAsync(CertificateDto Certification)
    {
      Certificate item = Certification.Adapt<Certificate>();
      return await _service.UpdateAsync(item);
    }

    [HttpDelete("{id}")]
    public async Task<bool> DeleteAsync(int id)
    {
      return await _service.DeleteAsync(id);
    }
  }
}
