using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyLifeline.DotNetClientSqlServerAPI.Data;
using MyLifeline.DotNetClientSqlServerAPI.Models;

namespace MyLifeline.DotNetClientSqlServerAPI.Controllers
{
    [Route("api/log")]
    [Authorize]
    [ApiController]
    public class DeviceLogsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DeviceLogsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/DeviceLogs
        [HttpPost]
        public async Task<ActionResult<DeviceLog>> PostDeviceLog(DeviceLog deviceLog)
        {
            _context.DeviceLogs.Add(deviceLog);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool DeviceLogExists(Guid id)
        {
            return _context.DeviceLogs.Any(e => e.DeviceLogID == id);
        }
    }
}
