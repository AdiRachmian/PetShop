using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetShop.Models
{
    public class Animal
	{
		public int AnimalId { get; set; }
		public string Name { get; set; }

        [Range(0, 300, ErrorMessage ="Age must be between 0-300")]
		public double Age { get; set; }
		public string PictureName { get; set; }

        [NotMapped] 
        [DisplayName("Animal Image")]
        public IFormFile? ImageFile { get; set; }

        public string Description { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        
        [Column("Comment")]
        public virtual ICollection<Comment> Comments { get; set; }

        public Animal()
		{

		}
	}
}
