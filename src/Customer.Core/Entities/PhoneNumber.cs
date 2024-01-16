namespace Customer.Core.Entities;

public class PhoneNumber : Entity
{
    public string AreaCode { get; set; }
    public string Number { get; set;}
    public PhoneType Type { get; set;}
    
    private PhoneNumber() { }

    public PhoneNumber(string areaCode, string number, PhoneType type)
    {
        AreaCode = areaCode;
        Number = number;
        Type = type;
    }

    protected IEnumerable<object> GetEqualityComponents()
    {
        yield return AreaCode;
        yield return Number;
        yield return Type;
    }
}