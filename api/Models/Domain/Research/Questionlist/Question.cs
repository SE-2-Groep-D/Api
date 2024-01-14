using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models.Domain.Research.Questionlist; 
public class Question {

  [Key]
  public Guid Id { get; set; }
  
  [Required]
  public QuestionType Type { get; set; }
  
  [Required]
  public string Description { get; set; }

  public List<Answer> PossibleAnswers { get; set; } = new List<Answer>();
  public List<Answer> GivenAnswers { get; set; } = new List<Answer>();
  
  public Guid QuestionListId { get; set; }

  [ForeignKey("QuestionListId")]
  public QuestionList QuestionList { get; set; }

}

public enum QuestionType {
  Open, OneAnwer, MultipleAnswer
}
