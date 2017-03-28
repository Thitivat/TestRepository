using BND.Services.Payments.iDeal.Api;
using Swashbuckle.Application;
using Swashbuckle.Swagger;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using WebActivatorEx;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace BND.Services.Payments.iDeal.Api
{
    /// <summary>
    /// Class SwaggerConfig is a class for intializing swagger component.
    /// </summary>
    public class SwaggerConfig
    {
        /// <summary>
        /// Registers swagger component to api.
        /// </summary>
        public static void Register()
        {
            var AuthHeader = new SwaggerHeaderParameter
            {
                Description = "Authorization header to enter api key.",
                Key = "Authorization",
                Name = "Authorization"
            };

            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "BND.Services.Payments.iDeal.Api");
                    c.IncludeXmlComments(GetXmlCommentsPath());
                    AuthHeader.Apply(c);
                })
                .EnableSwaggerUi(c => { c.DisableValidator(); });
        }

        /// <summary>
        /// Gets the XML comments path.
        /// </summary>
        /// <returns>The XML comments path.</returns>
        protected static string GetXmlCommentsPath()
        {
            return System.String.Format(@"{0}\bin\WebApiSwagger.XML", System.AppDomain.CurrentDomain.BaseDirectory);
        }

        /// <summary>
        /// Class SwaggerHeaderParameter for providing header in swagger.
        /// <remarks>
        /// How to create Header Parameters using Swashbuckle
        /// See this http://analogcoder.com/2015/11/how-to-create-header-using-swashbuckle/
        /// </remarks>
        /// </summary>
        public class SwaggerHeaderParameter : IOperationFilter
        {
            /// <summary>
            /// Gets or sets the description.
            /// </summary>
            /// <value>The description.</value>
            public string Description { get; set; }
            /// <summary>
            /// Gets or sets the key.
            /// </summary>
            /// <value>The key.</value>
            public string Key { get; set; }
            /// <summary>
            /// Gets or sets the name.
            /// </summary>
            /// <value>The name.</value>
            public string Name { get; set; }
            /// <summary>
            /// Gets or sets the default value.
            /// </summary>
            /// <value>The default value.</value>
            public string DefaultValue { get; set; }

            /// <summary>
            /// Applies header to swagger.
            /// </summary>
            /// <param name="c">The swagger configuration.</param>
            public void Apply(SwaggerDocsConfig c)
            {
                c.ApiKey(Key).Name(Name).Description(Description).In("header");
                c.OperationFilter(() => this);
            }

            /// <summary>
            /// Applies header to swagger.
            /// </summary>
            /// <param name="operation">The operation.</param>
            /// <param name="schemaRegistry">The schema registry.</param>
            /// <param name="apiDescription">The API description.</param>
            public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
            {
                operation.parameters = operation.parameters ?? new List<Parameter>();
                operation.parameters.Add(new Parameter
                {
                    name = Name,
                    description = Description,
                    @in = "header",
                    required = true,
                    type = "string",
                    @default = DefaultValue
                });
            }
        }
    }
}
