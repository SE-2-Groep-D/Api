﻿

using Api.Models.Domain.Research.Questionlist;
using Api.Models.Domain.Research.Tracking;
using Api.Models.Domain.User;

namespace Api.Models.Domain.Research;
public class Onderzoek {

  public Guid Id { get; set; }
  //duur

  public string Titel { get; set; }

  public int AantalParticipanten { get; set; }
  public string websiteUrl { get; set; }
  public DateTime StartDatum { get; set; }
  public string Omschrijving { get; set; }
  public double Vergoeding { get; set; }
  public string Locatie { get; set; }
  public Status Status { get; set; }


  public ICollection<Questionlist.QuestionList> Vragenlijst { get; } = new List<Questionlist.QuestionList>();
  public ICollection<TrackingOnderzoek> TrackingResultaten { get; } = new List<TrackingOnderzoek>();
  public List<Ervaringsdeskundige> Ervaringsdeskundigen { get; } = new();
  public List<OnderzoekErvaringsdekundige> OnderzoekErvaringsdekundigen { get; } = new();
  public Guid BedrijfId { get; set; }
  public Bedrijf Bedrijf { get; set; } = null!;

}

public enum Status {
  open,
  active,
  ended
}
