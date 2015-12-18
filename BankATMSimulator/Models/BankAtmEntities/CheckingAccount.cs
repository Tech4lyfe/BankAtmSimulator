using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace BankATMSimulator.Models.BankAtmEntities
{
   
    public class CheckingAccount
    {
        [Required]
        public int CheckingAccountId { get; set; }
        [Required(ErrorMessage = "Please Enter First Name")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please Enter Last Name")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [DataType(DataType.Currency)]
        public double Balance { get; set; }

        [Required]
        [StringLength(10)]
        [Column(TypeName = "varchar")]
        [RegularExpression(@"\d{6,10}", ErrorMessage = "Account number must be between 6 and 10 digits.")]
        [Display(Name = "Account #")]
        public string AccountNumber { get; set; }

        public string Name
        {
            get { return string.Format("{0} {1}", this.FirstName, this.LastName); }
        }

        public virtual ApplicationUser User { get; set; }

        [Required]
        [HiddenInput(DisplayValue = false)]
        public string ApplicationUserId { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}