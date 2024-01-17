namespace Customer.Core.Entities;

public class PhoneNumber : Entity
{
    private PhoneNumber()
    {
    }

    public PhoneNumber(string areaCode, string number, PhoneType type)
    {
        AreaCode = areaCode;
        Number = number;
        Type = type;
    }

    public string AreaCode { get; set; }
    public string Number { get; set; }
    public PhoneType Type { get; set; }

    protected IEnumerable<object> GetEqualityComponents()
    {
        yield return AreaCode;
        yield return Number;
        yield return Type;
    }
}