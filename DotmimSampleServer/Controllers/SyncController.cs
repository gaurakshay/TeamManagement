using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dotmim.Sync.Web.Server;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DotmimSampleServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SyncController : ControllerBase
    {
        private readonly WebServerManager webServerManager;

        public SyncController(WebServerManager webServerManager)
        {
            this.webServerManager = webServerManager;
        }

        // GET: api/<SyncController>
        [HttpGet]
        public Task GetAsync() => webServerManager.HandleRequestAsync(this.HttpContext);

        // POST api/<SyncController>
        [HttpPost]
        public Task PostAsync() => webServerManager.HandleRequestAsync(this.HttpContext);
        
    }
}
