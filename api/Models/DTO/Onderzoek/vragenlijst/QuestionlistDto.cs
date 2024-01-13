using Api.Models.Domain.Research;
using System;
using System.Collections.Generic;

namespace Api.Models.DTO.Onderzoek;


public class QuestionlistDto {
  public Guid Id { get; set; }
  public string Title { get; set; }
  public string Description { get; set; }
  public Guid OnderzoekId { get; set; }
  public ICollection<AddQuestionRequestDto> Questions { get; set; }
  public int? Participants { get; set; }
  public int? TotalQuestions { get; set; }
  public int? TotalAwnsers { get; set; }
}




public class QuestionDto {
  public Guid Id { get; set; }
  public string Title { get; set; }
  public string Type { get; set; }
  public IEnumerable<AddQAnsweRequestDto> PossibleAnswers { get; set; }
}




public class AddQuestionlistRequestDto
{
  public string Title { get; set; }
  public string Description { get; set; }
  public ICollection<AddQuestionRequestDto> Questions { get; set; }
  public Guid OnderzoekId { get; set; }
  

}





public class AddQuestionRequestDto
{
  public string Title { get; set; }
  public string Type { get; set; }
  public IEnumerable<AddQAnsweRequestDto>? PossibleAnswers { get; set; }
 
 

}

public class AddQAnsweRequestDto {

  public string Answertext { get; set; }

}


//voor submited atnwoorden

public class SubmitedRequestDto {

  public Guid Id { get; set; }
  public IEnumerable<SubmitedAnswerRequestDto> GivenAnswers { get; set; } 
}


public class SubmitedAnswerRequestDto {

  public Guid GivenAnswerQuestionId { get; set; }
  public string Answertext { get; set; }

}




public class UpdateQuestionlistRequestDto {


  public string? Title { get; set; }
  public string? Description { get; set; }
  public ICollection<UpdateQuestionRequestDto>? Questions { get; set; }
  public int? Participants { get; set; }
  public int? TotalQuestions { get; set; }
  public int? TotalAwnsers { get; set; }
  
}



public class UpdateQuestionRequestDto
{
  public Guid Id { get; set; } 
  public string Title { get; set; }
  public string Type { get; set; }
  public IEnumerable<UpdateAnsweRequestDto>? PossibleAnswers { get; set; }
 
 

}

public class UpdateAnsweRequestDto {
  public Guid Id { get; set; } 
  public string Answertext { get; set; }

}


