using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEST.CORE.Helpers
{
    public class AddRequiredHeaderParameter : Attribute, IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<OpenApiParameter>();

                operation.Parameters.Add(new OpenApiParameter
                    {
                        Name = "Client",
                        In = ParameterLocation.Header,
                        Description = "Cliente Name",
                        Required = true,
                        Example = new OpenApiString("connection_Name")
                    }
            );
        }        
    }
}
