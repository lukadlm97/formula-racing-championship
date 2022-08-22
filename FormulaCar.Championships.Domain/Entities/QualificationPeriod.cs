namespace FormulaCar.Championships.Domain.Entities;

public class QualificationPeriod
{
    public int Id { get; set; }
    public int OrderNumber { get; set; }
    public string PeriodName { get; set; }
    public string ShortPeriodName { get; set; }
    public ICollection<QualificationClassification> QualificationClassifications { get; set; }
}