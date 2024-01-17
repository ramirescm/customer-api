namespace Customer.Core.Entities;

public class Customer : Entity
{
    public string FullName { get; set; }
    public string Email { get; set; }
    public List<PhoneNumber> Phones { get; set; } = new();

    public void UpdatePhoneNumber(int phoneId, string newAreaCode, string newNumber, PhoneType newType)
    {
        var phoneToUpdate = Phones.FirstOrDefault(p => p.Id == phoneId);

        if (phoneToUpdate != null)
        {
            phoneToUpdate.AreaCode = newAreaCode;
            phoneToUpdate.Number = newNumber;
            phoneToUpdate.Type = newType;
        }
    }
}