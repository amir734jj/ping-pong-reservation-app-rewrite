using Microsoft.AspNetCore.Hosting;

namespace Api.Extensions
{    
    /// <summary>
    /// Extension methods that can be used to switch environment configuration on runtime
    /// </summary>
    public static class EnvironmentExtension
    {
        private const string Development = "Development";
        private const string LocalHost = "Localhost";
        private const string Master = "Master";
        private const string Release = "Release"; 
        
        /// <summary>
        /// Flag is environment is development
        /// </summary>
        /// <param name="env"></param>
        /// <returns></returns>
        public static bool IsDevelopment(this IHostingEnvironment env)
        {
            return env.IsEnvironment(Development);
        }
        
        /// <summary>
        /// Flag is environment is localhost
        /// </summary>
        /// <param name="env"></param>
        /// <returns></returns>
        public static bool IsLocalHost(this IHostingEnvironment env)
        {
            return env.IsEnvironment(LocalHost);
        }
        
        /// <summary>
        /// Flag is environment is master
        /// </summary>
        /// <param name="env"></param>
        /// <returns></returns>
        public static bool IsMaster(this IHostingEnvironment env)
        {
            return env.IsEnvironment(Master);
        }
        
        /// <summary>
        /// Flag is environment is release
        /// </summary>
        /// <param name="env"></param>
        /// <returns></returns>
        public static bool IsRelease(this IHostingEnvironment env)
        {
            return env.IsEnvironment(Release);
        }
    }
}