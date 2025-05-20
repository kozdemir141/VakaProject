namespace VakaProject.Domain.Concrete;

public class IndividualData
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public string BirthPlace { get; set; }
    public DateTime BirthDate { get; set; }
    public string Nationality { get; set; }
    public long IdentityNumber { get; set; }
}