using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using obg.BusinessLogic.Interface;
using obg.BusinessLogic.Interface.Interfaces;
using obg.WebApi.Dtos;

namespace obg.DataAccess.Filters
{
    public class AuthorizationAttributeFilter : Attribute, IAuthorizationFilter
    {
        private readonly ISessionService _sessionService;
        public AuthorizationAttributeFilter(ISessionService sessionService)
        {
            this._sessionService = sessionService;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string token = context.HttpContext.Request.Headers["Token"];
            if (String.IsNullOrEmpty(token))
            {
                ResponseDTO response = new ResponseDTO
                {
                    Code = 3001,
                    ErrorMessage = "Debe ingresar un token en el Header.",
                    IsSucess = false
                };
                context.Result = new ObjectResult(response)
                {
                    StatusCode = 401,
                };
            } else
            {

                if (!_sessionService.IsTokenValid(token))
                {
                    ResponseDTO response = new ResponseDTO
                    {
                        Code = 3002,
                        ErrorMessage = "Token inválido.",
                        IsSucess = false
                    };
                    context.Result = new ObjectResult(response)
                    {
                        StatusCode = 403,
                    };
                }
            }
        }
    }
}
