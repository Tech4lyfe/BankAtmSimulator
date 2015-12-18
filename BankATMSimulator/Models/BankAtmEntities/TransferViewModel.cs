using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BankATMSimulator.Models.BankAtmEntities
{
    public class TransferViewModel
    {
        [Required]
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }

        [Required]
        [Display(Name = "To Account #")]
        public string TargetCheckingAccountNumber { get; set; }

        [Required]
        public int CheckingAccountId { get; set; }

    }
}