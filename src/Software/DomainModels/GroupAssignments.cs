using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Software.Models;

namespace Software.DomainModels
{
    public class GroupAssignments
    {
        public GroupWork Work { get; set; }

        public ApplicationUser Member { get; set; }

        [Column(Order = 1)]
        public int WorkId { get; set; }

        [Column(Order = 2)]
        public int MemberId { get; set; }

    }
}
