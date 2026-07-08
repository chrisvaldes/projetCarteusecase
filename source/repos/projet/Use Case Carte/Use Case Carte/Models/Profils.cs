using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Use_Case_Carte.Models.Enum;

namespace Use_Case_Carte.Models
{
    public class ProfilModel
    {
        public Guid Id { get; set; }
         
        public string? UserName { get; set; }

        public string? Userag { get; set; }
         
        public string? Email { get; set; }
         
        public EnumProfil TypeProfile { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public EnumStatut Status { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
