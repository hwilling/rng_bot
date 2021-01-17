using System;
using System.Collections.Generic;
using System.Text;

namespace rng_bot.Extensions
{
    public static class CustomExtensions
    {
        /// <summary>
        /// Returns true if a string is null, otherwise returns false
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNull(this string str) => str == null;

        /// <summary>
        /// Returns true if a trimmed string is an empty string, otherwise returns false
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsEmpty(this string str) => str.Trim().Equals(string.Empty);

        /// <summary>
        /// Returns true if a string is null or empty, otherwise returns false
        /// Determines if a string is null or empty based on string.IsNull or string.IsEmpty
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string str) => str.IsNull() || str.IsEmpty();
    }
}
