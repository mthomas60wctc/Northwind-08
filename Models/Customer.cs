using System.ComponentModel.DataAnnotations;
public class Customer
{
  public int CustomerId { get; set; }
  public string CompanyName { get; set; }
  [Required]
  public string Address { get; set; }
  public string City { get; set; }
  public string Region { get; set; }
  public string PostalCode { get; set; }
  public string Country { get; set; }
  public string Phone { get; set; }
  public string Fax { get; set; }
}