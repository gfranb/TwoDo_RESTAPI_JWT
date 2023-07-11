using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TwoDo.Models
{
    public class Assignment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Column(TypeName = "Date")]
        public DateTime CreationDate { get; set; }

        [Column(TypeName = "Date")]
        public DateTime DueDate { get; set; }

        public bool IsDone { get; set; } = false;

        [Column(TypeName = "Date")]
        public DateTime CompleteDate { get; set; }

        [JsonIgnore]
        public int UserId { get; set; }
        [JsonIgnore]
        public virtual User? User { get; set; }

    }
}
