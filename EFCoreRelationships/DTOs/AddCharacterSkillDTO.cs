using System.ComponentModel.DataAnnotations;

namespace EFCoreRelationships.DTOs
{
    public class AddCharacterSkillDTO
    {
        [Required(ErrorMessage = "Must have pass a valid character id")]
        public int CharacterId { get; set; }
        [Required(ErrorMessage = "Must have pass a valid skill id")]
        public int SkillId { get; set; }
    }
}
