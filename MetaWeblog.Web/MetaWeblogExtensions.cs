namespace MetaWeblog.Web
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// The MetaWeblog extension method class
    /// </summary>
    public static class MetaWeblogExtensions
    {
        /// <summary>
        /// Use the MetaWeblog middleware.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="path">The path.</param>
        /// <returns>An <see cref="IApplicationBuilder"/>.</returns>
        public static IApplicationBuilder UseMetaWeblog(this IApplicationBuilder builder, string path)
            => builder.UseMiddleware<MetaWeblogMiddleware>(path);

        /// <summary>
        /// Adds the MetaWeblog service.
        /// </summary>
        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
        /// <param name="coll">The service collection.</param>
        /// <returns>The <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddMetaWeblog<TImplementation>(this IServiceCollection coll) where TImplementation : class, IMetaWeblogProvider
            => coll.AddScoped<IMetaWeblogProvider, TImplementation>().AddScoped<MetaWeblogService>();
    }
}
