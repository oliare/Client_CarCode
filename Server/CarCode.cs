using System.ComponentModel.DataAnnotations;

public class CarCode
{
    public int Id { get; set; }
    [Required, MaxLength(10)]
    public string Code { get; set; }
    [Required, MaxLength(50)]
    public string Region { get; set; }

}
