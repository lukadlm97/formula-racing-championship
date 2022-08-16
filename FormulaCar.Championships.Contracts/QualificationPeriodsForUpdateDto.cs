namespace FormulaCar.Championships.Contracts;

public class QualificationPeriodsForUpdateDto
{
    public int QualificationPeriodsId { get; set; }
    public int OrderNumber { get; set; }
    public string PeriodName { get; set; }
    public string ShortPeriodName { get; set; }
}