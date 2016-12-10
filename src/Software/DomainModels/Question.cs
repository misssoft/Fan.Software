using Software.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Software.DomainModels
{
    public class Question
    {
        [Key]
        public int QuestioinId { get; set; }

        public ApplicationUser Questioner { get; set; }

        public DateTime DateTime { get; set; }

        public string Subject { get; set; }

        public string Topic { get; set; }

        public string QuestionMessage { get; set; }

        public IEnumerable<Answer> Answers { get; set; }

    }
}
