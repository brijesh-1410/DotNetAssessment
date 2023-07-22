using BackendApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendApp.Handler
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            string excptionMessage = string.Empty;
            excptionMessage = JsonConvert.SerializeObject(context.Exception);
            var result = new Response() { Message = excptionMessage, StatusCode = System.Net.HttpStatusCode.InternalServerError, Data = null};
            Console.WriteLine(excptionMessage);
            context.ExceptionHandled = true; // mark exception as handled
            context.Result = new JsonResult(result);
        }
    }
}
