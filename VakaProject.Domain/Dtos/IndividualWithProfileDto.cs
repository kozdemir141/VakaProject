namespace VakaProject.Domain.Dtos;

public class IndividualWithProfileDto
{
    public int      Id             { get; set; }
    public string   FirstName      { get; set; } = default!;
    public string?  MiddleName     { get; set; }
    public string   LastName       { get; set; } = default!;
    public string   BirthPlace     { get; set; } = default!;
    public DateTime BirthDate      { get; set; }
    public string   Nationality    { get; set; } = default!;
    public long     IdentityNumber { get; set; } 
    public int      ProfileId      { get; set; } 
}