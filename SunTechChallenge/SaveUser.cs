using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SunTechChallenge.Models.DTO;
using System.Web.Http;
using System.Configuration;
using Microsoft.Azure.Cosmos;
using Microsoft.AspNetCore.Hosting;

namespace SunTechChallenge
{
    public static class SaveUser
    {
        [FunctionName("SaveUser")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] UserDto user,
            [CosmosDB(databaseName:"NoSQLDB", containerName: "MyContainer", Connection = "CosmosDbConnectionString")] IAsyncCollector<Models.User> usersOut,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            try
            {
                var newUser = new Models.User
                {
                    id = Guid.NewGuid().ToString(),
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    BirthdayInEpoch = user.BirthdayInEpoch
                };

                await usersOut.AddAsync(newUser);
            }
            catch (Exception ex)
            {
                log.LogError("Error with SaveUser: {0}", ex.Message);

                return new InternalServerErrorResult();
            }

            return new OkResult();
        }
    }
}
