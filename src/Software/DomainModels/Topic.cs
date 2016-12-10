using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Software.DomainModels
{
    public class Topic
    {
        [Key]
        public int Id { get; set; }

        public string Summary { get; set; }

        public string Intro { get; set; }

        public int Rate { get; set; }

        public DateTime TimeStamp { get; set; }

    }
}
