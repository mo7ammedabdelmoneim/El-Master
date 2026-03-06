using System.ComponentModel.DataAnnotations;

namespace El_Master.Domain.Entities
{
    public class Grade:BaseEntity
    {
        
        [Required,StringLength(30)]
        public string Name { get; set; }
    }
}