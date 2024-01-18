using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Api.Models.Domain.Research;
using Api.Models.Domain.Research.Questionlist;

namespace Api.Models.Domain.Research.Questionlist; 
public class QuestionList {

  [Key]
  public Guid Id { get; set; }
  
  [Required]
  public string Title { get; set; }
  
  [Required]
  public string Description { get; set; }
  
  [Required]
  public Guid OnderzoekId { get; set; }
  
  [ForeignKey("OnderzoekId")]
  public Onderzoek Onderzoek { get; set; }

  public List<Question> Questions { get; set; }
}