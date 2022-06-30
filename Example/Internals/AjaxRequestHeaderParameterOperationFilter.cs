using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
namespace AspNetCoreFile.Example.Internals;
internal class AjaxRequestHeaderParameterOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.Parameters ??= new List<OpenApiParameter>();
        operation.Parameters.Add(new()
        {
            Name = "X-Requested-With",
            In = ParameterLocation.Header,
            Required = false,
            Example = new OpenApiString("XMLHttpRequest"),
        });
    }
}
