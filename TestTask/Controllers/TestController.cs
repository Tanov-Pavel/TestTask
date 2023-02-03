using Domain;

using Microsoft.AspNetCore.Mvc;
using Repository.Repositories.Interfaces;
using System.Net;

namespace TestTask.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {

        private readonly ILogger<TestController> _logger;
        private readonly IMetricRepository _metricRepository;

        public TestController(ILogger<TestController> logger, IMetricRepository metricRepository)
        {
            _logger = logger;
            _metricRepository = metricRepository;
        }

        [HttpGet(Name = "1")]
        public IQueryable<Metric> Get()
        {
            var metrics = _metricRepository.GetAll();
            return metrics;
        }

        [HttpPost(Name = "2")]
        public OkResult Post(string ipAddress, int diskSpace, double cpu, int ramSpaceFree, int ramSpaceTotal)
        {
            var metric = new Metric
            {
                IpAddress = ipAddress,
                DiskSpace = diskSpace,
                Cpu = cpu,   
                RamSpaceFree = ramSpaceFree,
                RamSpaceTotal = ramSpaceTotal



            };
            _metricRepository.Create(metric);
            return Ok();
        }

        [HttpDelete(Name = "3")]
        public OkResult DELETE(string IpAdress)
        {
            var objec = _metricRepository.GetAll()
                .Where(x => x.IpAddress == IpAdress )
                .FirstOrDefault();

            _metricRepository.Delete(objec);


     
            return Ok();
        }
    }
}