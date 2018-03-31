using System;

namespace Breakfast.Api.Configuration
{
    public class BreakfastApiOptions
    {
        /// <summary>
        /// Gets or sets the breakfast order expire time.
        /// </summary>
        public TimeSpan BreakfastOrderExpireTime { get; set; } = TimeSpan.FromHours(1);
    }
}
