using System;
using System.Collections.Generic;

namespace Breakfast.Api.Models.BreakfastOrder
{
    public class BreakfastOrderViewModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the date created.
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the breakfast items.
        /// </summary>
        public ICollection<string> BreakfastItems { get; set; }
    }
}
