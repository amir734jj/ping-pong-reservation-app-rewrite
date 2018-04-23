using Microsoft.Extensions.Configuration;

namespace API.Attributes
{
    /// <summary>
    /// Holds application root configuration variable
    /// </summary>
    public struct Root
    {
        /// <summary>
        /// Application root configuration variable
        /// </summary>
        public static IConfigurationRoot Configuration { get; private set; }

        /// <summary>
        /// Sets the root configuration
        /// </summary>
        /// <param name="configuration"></param>
        public static IConfigurationRoot SetConfiguration(IConfigurationRoot configuration)
        {
            return Configuration = configuration;
        }
    }
}