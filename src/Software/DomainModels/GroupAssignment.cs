using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Software.Models;

namespace Software.DomainModels
{
    public class GroupAssignment
    {
        public GroupWork Work { get; set; }

        public ApplicationUser Member { get; set; }

        public int WorkId { get; set; }

        public string MemberId { get; set; }
    }
}

