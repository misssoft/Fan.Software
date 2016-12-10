using System.ComponentModel.DataAnnotations;
using Software.Models;

namespace Software.DomainModels
{
    public class Answer
    {
        [Key]
        public int AnswerId { get; set; }
        public string AnswerMessage { get; set; }
        public ApplicationUser Answerer { get; set; }

    }
}
