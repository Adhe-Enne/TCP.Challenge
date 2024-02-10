using AutoMapper;
using Core.Framework;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace TCP.Api.Controllers
{
    public class BaseController : Controller
    {
        protected IMapper _mapper;
        public BaseController(IMapper mapper)
        {
            _mapper = mapper;
        }

        protected GenericResult HandleException(Exception exception)
        {
            string message = ExceptionHelper.GetMessage(exception);
            Log.Error(message, exception);
            return new GenericResult(message, true);
        }

        protected GenericResult HandleException(string message, Exception exception)
        {
            string exMessage = ExceptionHelper.GetMessage(exception);
            Log.Error(message, exception);
            return new GenericResult($"{message} - {exMessage}", true);
        }

        protected void LogInfo(string message, object? entity = null)
        {
            if (entity is null)
                Log.Information(message);
            else
            {
                var data = Core.Externals.JsonConvert.Serialize(entity);
                Log.Information(message + ": {@data}", data);
            }
        }
    }
}
