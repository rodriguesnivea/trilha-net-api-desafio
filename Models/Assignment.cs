using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using TrilhaApiDesafio.Models.Enums;

namespace TrilhaApiDesafio.Models
{
    public class Assignment
    {
        [JsonIgnore]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        [Required]
        [StringLength(100)]
        public string Description { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public AssignmentStatus Status { get; set; }
    }
}