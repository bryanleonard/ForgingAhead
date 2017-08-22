using System;
using System.ComponentModel.DataAnnotations; // allows for things like Display(name) and Required

namespace ForgingAhead.Models;

{
	public class Character
	{
		//data validation with DataAnnotations, adds front-end, backend, and entity requirements for DB
        [Key] //prevent dups
        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        [Display(Name = "Is Active")]
		public bool IsActive { get; set; }

        [Required]
        [Range(1, 20)
		public int Level { get; set; }

        [Required(ErrorMessage = "This is a custom error message")]
		public int Strength { get; set; }

		public int Dexterity { get; set; }

		public int Intelligence { get; set; }

        public List<Equipment> Equipment { get; set; }
	}
}
