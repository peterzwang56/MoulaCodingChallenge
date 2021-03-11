using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MoulaCodingChallenge
{
    public class AuthRequirementFilter : IOperationFilter
    {
        /// <summary>
        /// Updates <see cref="Operation"/> instance according to <see cref="OperationFilterContext"/> object
        /// </summary>
        /// <param name="operation">Instance of <see cref="Operation"/></param>
        /// <param name="context">Instance of <see cref="OperationFilterContext"/></param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var scope = context.MethodInfo.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Select(attr => attr.Policy)
                .Distinct(); ;
            if (!scope.Any())
                return;
            var scheme = new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "JWT" } };
            var security = new List<OpenApiSecurityRequirement>();
            security.Add(new OpenApiSecurityRequirement
            {
                [scheme] = scope.ToList()
            });
            operation.Security = security;
        }

    }
}
