using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetShop.Models
{
	public class Comment
	{
        [Key]
        public int CommentId { get; set; }

        public string Content { get; set; }

        [ForeignKey("Animal")]
        public int AnimalId { get; set; }

        public virtual Animal Animal { get; set; }

        public Comment()
		{

		}

        public override string ToString()
        {
            return Content;
        }
    }
}
