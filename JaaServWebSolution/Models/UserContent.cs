using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JaaServWebSolution.Models
{
    public class UserContent
    {
        public string Id { get; set; }

        public string ContentUrl { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

    }
}