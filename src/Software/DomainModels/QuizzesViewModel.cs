using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Software.DomainModels
{
    public class QuizzesViewModel
    {
        public int Id { get; set; }
        public string Question { get; set; }

        public string Answer { get; set; }

        public string Notes { get; set; }

        public Topic Topic { get; set; }

        public int TopicId { get; set; }

        public IEnumerable<Topic> Topics { get; set; }
    }
}
