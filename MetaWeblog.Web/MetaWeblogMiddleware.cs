namespace MetaWeblog.Web
{
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// The MetaWeblog middleware class
    /// </summary>
    public class MetaWeblogMiddleware
    {
        private readonly ILogger logger;
        private readonly RequestDelegate next;
        private readonly string urlEndpoint;

        /// <summary>
        /// Initializes a new instance of the <see cref="MetaWeblogMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next request delegate.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        /// <param name="urlEndpoint">The URL endpoint.</param>
        public MetaWeblogMiddleware(RequestDelegate next, ILoggerFactory loggerFactory, string urlEndpoint)
        {
            this.next = next;
            this.logger = loggerFactory.CreateLogger<MetaWeblogMiddleware>();
            this.urlEndpoint = urlEndpoint;
        }

        /// <summary>
        /// Invokes the service for the specified request.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="service">The service.</param>
        /// <returns>A <see cref="Task"/>.</returns>
        public async Task Invoke(HttpContext context, MetaWeblogService service)
        {
            if (context.Request.Method == "POST" &&
                context.Request.Path.StartsWithSegments(this.urlEndpoint) &&
                context.Request != null &&
                context.Request.ContentType.ToLower().Contains("text/xml"))
            {
                context.Response.ContentType = "text/xml";

                using var rdr = new StreamReader(context.Request.Body);

                var xml = await rdr.ReadToEndAsync();

                this.logger.LogInformation($"Request XMLRPC: {xml}");

                var result = await service.InvokeAsync(xml);

                this.logger.LogInformation($"Result XMLRPC: {result}");

                await context.Response.WriteAsync(result, Encoding.UTF8);

                return;
            }

            // Continue On
            await this.next.Invoke(context);
        }
    }
}
