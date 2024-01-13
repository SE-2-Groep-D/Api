namespace Api.Models.Domain.Research;
public class Questionlist {

  public Guid Id { get; set; }
  public string Title { get; set; }
  public string Description { get; set; }

  public Guid OnderzoekId { get; set; }
  public Onderzoek Onderzoek { get; set; } = null!;
  public ICollection<Question> Questions { get; } = new List<Question>();
  
  public int? Participants { get; set; }
  public int? TotalQuestions { get; set; }
  public int? TotalAwnsers { get; set; }
}

public class Question {

  public Guid Id { get; set; }
  public QuestionType Type { get; set; }
  public string Title { get; set; }
  
  public Guid QuestionlistId { get; set; }
  public Questionlist Questionlist { get; set; } = null!;
  
  public ICollection<Answer>? PossibleAnswers { get; set; }
  public ICollection<Answer> GivenAnswers { get; set; } = new List<Answer>();

}

public enum QuestionType {
  open, one_choice, multiple_choice,
}

public class Answer {
  public Guid Id { get; set; }
  public string Answertext { get; set; }
  
  // Foreign Keys
  public Guid PossibleAnswerQuestionId { get; set; }
  public Guid? GivenAnswerQuestionId { get; set; }

  // Navigation properties
  public Question QuestionAsPossibleAnswer { get; set; }
  public Question? QuestionAsGivenAnswer { get; set; }

}


