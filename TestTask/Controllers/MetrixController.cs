using Domain;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Repository.Repositories;
using Repository.Repositories.Interfaces;
using System.Net;

namespace TestTask.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MetrixController : ControllerBase
    {

        private readonly ILogger<MetrixController> _logger;
        private readonly IMetricRepository _metricRepository;

        public MetrixController(ILogger<MetrixController> logger, IMetricRepository metricRepository)
        {
            _logger = logger;
            _metricRepository = metricRepository;
        }

        [HttpGet("get_list")]
        public IQueryable<Metric> Get()
        {
            var metrics = _metricRepository.GetAll();
            return metrics;
        }

        [HttpGet("get_by_id")]
        public Metric GetById(Guid id)
        {
            var objec = _metricRepository.GetAll()
                .Where(x => x.Id == id)
                .FirstOrDefault();

            return objec;
        }

        [HttpPut("update")]
        public OkResult Update(Guid id, [FromBody] CreateMetricsDto request)
        {
            var metric = new Metric
            {
                IpAddress = request.IpAddress.ToString(),
                Cpu = request.CPU.Usage,
                TotalMemory = request.Memory.TotalMemory,
                FreeMemory = request.Memory.FreeMemory,
            };
            _metricRepository.Update(metric, id);
            return Ok();
        }

        [HttpPost("create")]
        public OkResult Post([FromBody] CreateMetricsDto request)
        {
            var metric = new Metric
            {
                IpAddress = request.IpAddress.ToString(),
                Cpu = request.CPU.Usage,
                TotalMemory = request.Memory.TotalMemory,
                FreeMemory = request.Memory.FreeMemory,
            };
            _metricRepository.Create(metric);
            return Ok();
        }

        [HttpDelete("delete")]
        public OkResult Delete(Guid id)
        {
            var obj = _metricRepository.GetAll()
                .Where(x => x.Id == id)
                .FirstOrDefault();

            _metricRepository.Delete(obj);

            return Ok();
        }
    }
}