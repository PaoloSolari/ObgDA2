using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using obg.BusinessLogic.Interface;
using obg.Domain.Enums;
using obg.Exceptions;
using obg.WebApi.Dtos;
using System;

namespace obg.WebApi.Filters
{
    public class OwnerAuthorizationAttributeFilter : Attribute, IAuthorizationFilter
    {
        private readonly ISessionService _sessionService;
        public OwnerAuthorizationAttributeFilter(ISessionService sessionService)
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
                    IsSuccess = false
                };
                context.Result = new ObjectResult(response)
                {
                    StatusCode = 401,
                };
            }
            else
            {

                if (!_sessionService.IsTokenValid(token))
                {
                    ResponseDTO response = new ResponseDTO
                    {
                        Code = 3002,
                        ErrorMessage = "Token inválido.",
                        IsSuccess = false
                    };
                    context.Result = new ObjectResult(response)
                    {
                        StatusCode = 403,
                    };
                }
                else
                {
                    try
                    {
                        RoleUser userRole = _sessionService.GetUserRole(token);
                        if (userRole != RoleUser.Owner)
                        {
                            ResponseDTO response = new ResponseDTO
                            {
                                Code = 3003,
                                ErrorMessage = "No tienes el rol para hacer esto.",
                                IsSuccess = false
                            };
                            context.Result = new ObjectResult(response)
                            {
                                StatusCode = 403,
                            };
                        }
                    }
                    catch (NotFoundException)
                    {
                        ResponseDTO response = new ResponseDTO
                        {
                            Code = 3004,
                            ErrorMessage = "El usuario no está registrado.",
                            IsSuccess = false
                        };
                        context.Result = new ObjectResult(response)
                        {
                            StatusCode = 404,
                        };
                    }
                }
            }
        }
    }
}
