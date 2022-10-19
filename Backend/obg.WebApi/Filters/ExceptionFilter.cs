using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using obg.WebApi.Dtos;
using System;
using obg.Exceptions;

namespace obg.WebApi.Filters
{
    public class ExceptionFilter : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            int statusCode = 500;
            ResponseDTO response = new ResponseDTO()
            {
                Code = 2002,
                IsSuccess = false,
                ErrorMessage = context.Exception.Message
            };

            if (context.Exception is NotFoundException || context.Exception is NullReferenceException)
            {
                statusCode = 404;
                response.Code = 2003;
            }
            else
            {
                if (context.Exception is DemandException || context.Exception is InvitationException || context.Exception is MedicineException
                 || context.Exception is PetitionException || context.Exception is PharmacyException
                  || context.Exception is PurchaseException || context.Exception is PurchaseLineException
                   || context.Exception is SessionException || context.Exception is UserException)
                {
                    statusCode = 400;
                    response.Code = 2004;
                } else
                {
                    if (context.Exception is Exception)
                    {
                        statusCode = 500;
                        response.Code = 2003;
                        response.ErrorMessage = "Error interno.";
                    }
                }
            }
            context.Result = new ObjectResult(response)
            {
                StatusCode = statusCode
            };
        }
    }
}
