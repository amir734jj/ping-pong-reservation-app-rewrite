using System;
using System.Collections.Generic;
using Api.Enums;
using static System.String;

namespace Api.Extensions
{
    public static class StringExtension
    {        
        /// <summary>
        /// Parses connection url and transforms it to connection string
        /// </summary>
        /// <param name="urlStr"></param>
        /// <returns></returns>
        public static Dictionary<ConnectionTokens, string> ToConnectionTokens(this string urlStr)
        {
            if (Uri.TryCreate(urlStr, UriKind.Absolute, out var url))
            {
                return new Dictionary<ConnectionTokens, string>()
                {
                    {ConnectionTokens.Host, url.Host},
                    {ConnectionTokens.Username, url.UserInfo.Split(':')[0]},
                    {ConnectionTokens.Password, url.UserInfo.Split(':')[0]},
                    {ConnectionTokens.Port, url.Port.ToString()},
                    {ConnectionTokens.Database, url.LocalPath.Substring(1)}
                };
            }

            return new Dictionary<ConnectionTokens, string>()
            {
                {ConnectionTokens.Host, Empty},
                {ConnectionTokens.Username, Empty},
                {ConnectionTokens.Password, Empty},
                {ConnectionTokens.Port, Empty},
                {ConnectionTokens.Database, Empty}
            };
        }
    }
}