using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Software.DomainModels
{
    public class QuizViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Question { get; set; }

        public string Answer { get; set; }

        public string Notes { get; set; }

        public Topic Topic { get; set; }

        [Required]
        public int TopicId { get; set; }

        public IEnumerable<Topic> Topics { get; set; }
    }
}
