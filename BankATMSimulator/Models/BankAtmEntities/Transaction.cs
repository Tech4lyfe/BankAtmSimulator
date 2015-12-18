using System.ComponentModel.DataAnnotations;

namespace BankATMSimulator.Models.BankAtmEntities
{
   public class Transaction
    {
       [Required]
        public int TransactionId { get; set; }
       [DataType(DataType.Currency)]
        public double Amount { get; set; }

        [Required]
        public int CheckingAccountId { get; set; }
        public virtual CheckingAccount CheckingAccount { get; set; }

    }
}
