using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyLifeline.DotNetClientSqlServerAPI.Data;
using MyLifeline.DotNetClientSqlServerAPI.Models;

namespace MyLifeline.DotNetClientSqlServerAPI.Controllers
{
    [Route("/")]
    [ApiController]
    public class RootController : ControllerBase
    {
        bool databaseConnected = false, tokenvalid = false;
        string error = "none";
        int count = 0;
        public RootController()
        {
            var mllsql = Environment.GetEnvironmentVariable("MLLSQL");
            var mlltoken = Environment.GetEnvironmentVariable("MLLTOKEN");
            if (!string.IsNullOrEmpty(mllsql))
            {
                var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
                optionsBuilder.UseSqlServer(mllsql);
                ApplicationDbContext dbContext = new ApplicationDbContext(optionsBuilder.Options);
                try
                {
                    count = dbContext.DeviceLogs.Count();
                    databaseConnected = true;
                }
                catch (SqlException ex)
                {
                    databaseConnected = false;
                    error = ex.Message;
                }
            }
            else
            {
                databaseConnected = false;
                error = "environment variable MLLSQL is empty.";
            }

            if (!string.IsNullOrEmpty(mlltoken))
            {
                tokenvalid = true;
                if (mlltoken.Length < 10)
                {
                    tokenvalid = false;
                    error = "environment variable MLLTOKEN should be a value greater than 10 characters.";
                }
            }
            else
            {
                tokenvalid = false;
                error = "environment variable MLLTOKEN is empty.";
            }
        }
        public ContentResult Index()
        {
            string db = (databaseConnected) ? "connected" : "error";
            string token = (tokenvalid) ? "valid" : "invalid";
            string html = $"<html><body>Database: {db}<br/>";
            html += $"Token: {token}<br/>";
            if (!databaseConnected || !tokenvalid)
                html += $"Error: {error}<br/>";
            else
                html += $"Record count: {count}<br/>";
            html += "</body></html>";

            return new ContentResult
            {
                ContentType = "text/html",
                StatusCode = (int)HttpStatusCode.OK,
                Content = html
            };
        }
    }
}