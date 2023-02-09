using Domain;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly IDiskSpaceRepository _diskSpaceRepository;

        public MetrixController(ILogger<MetrixController> logger, IMetricRepository metricRepository, IDiskSpaceRepository diskSpaceRepository)
        {
            _logger = logger;
            _metricRepository = metricRepository;
            _diskSpaceRepository = diskSpaceRepository;
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
        public async Task<OkResult> Update(Guid id, [FromBody] CreateMetricsDto request)
        {
            var metric = new Metric
            {
                IpAddress = request.IpAddress.ToString(),
                Cpu = request.CPU.Usage,
                TotalMemory = request.Memory.TotalMemory,
                FreeMemory = request.Memory.FreeMemory,
            };
            _metricRepository.Update(metric, id);

            var diskSpaces = await _diskSpaceRepository
                .GetAll()
                .Where(x => x.MetricId == id)
                .ToListAsync();

            foreach (var disk in diskSpaces)
            {
                foreach (var newDisk in request.DiskSpaces)
                {
                    if (disk.MetricId == metric.Id)
                    {
                        var entity = new DiskSpace
                        {
                            Name = newDisk.Name,
                            TotalDiskSpace = newDisk.TotalSpace,
                            FreeDiskSpace = newDisk.FreeSpace,
                            MetricId = metric.Id,
                            Metric = metric
                        };
                        _diskSpaceRepository.Update(entity, disk.Id);
                    }
                }
            }
            return Ok();
        }
        [HttpPost("create")]
        public async Task<OkResult> Post([FromBody] CreateMetricsDto request)
        {
            var metric = new Metric
            {
                IpAddress = request.IpAddress.ToString(),
                Cpu = request.CPU.Usage,
                TotalMemory = request.Memory.TotalMemory,
                FreeMemory = request.Memory.FreeMemory,
            };
            await _metricRepository.CreateAsync(metric);

            foreach (var disk in request.DiskSpaces)
            {
                var entity = new DiskSpace
                {
                    Name = disk.Name,
                    TotalDiskSpace = disk.TotalSpace,
                    FreeDiskSpace = disk.FreeSpace,
                    MetricId = metric.Id,
                    Metric = metric,
                };
                await _diskSpaceRepository.CreateAsync(entity);
            }
            return Ok();
        }

        [HttpDelete("delete")]
        public async Task<OkResult> Delete(Guid id)
        {
            var obj = await _metricRepository.GetAll()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            _metricRepository.Delete(obj);

            var diskSpaces = await _diskSpaceRepository.GetAll()
                .Where(x => x.MetricId == obj.Id)
                .ToListAsync();

            foreach (var diskSpace in diskSpaces)
            {
                _diskSpaceRepository.Delete(diskSpace);
            }
            return Ok();
        }
    }
}