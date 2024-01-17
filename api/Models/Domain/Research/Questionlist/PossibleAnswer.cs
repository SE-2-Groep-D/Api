using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models.Domain.Research.Questionlist; 
public class PossibleAnswer {

  [Key]
  public Guid Id { get; set; }
  public string Value { get; set; }
  
  public Guid QuestionId { get; set; }

  [ForeignKey("QuestionId")]
  public Question Question { get; set; }

}