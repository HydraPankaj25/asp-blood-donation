using System.ComponentModel.DataAnnotations;

namespace BloodDonation.DTO.Doctor
{
    public class DoctorLoginDetails
    {
        [Required(ErrorMessage ="Email Must Be Required")]
        [DataType(DataType.EmailAddress)]
        public string? DoctorEmail {  get; set; } 
        
        
        
        
        [Required(ErrorMessage ="Password Must Be Required")]
        [DataType(DataType.Password)]
        public string? DoctorPassword {  get; set; }
    }
}
