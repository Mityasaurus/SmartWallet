using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Windows.Documents;

namespace SmartWallet.DAL.Entity;

public class Card
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string Number { get; set; }
    
    [Required]
    public DateTime DateExpire { get; set; }
    
    [Required]
    public string Cvv { get; set; }
    
    [Required]
    public string Type { get; set; } // TODO Make Enum
    
    [Required]
    public string Currency { get; set; } // TODO Make Enum
    
    [Required]
    public double Balance { get; set; }
    
    public List<Transaction> Transactions { get; set; }
    
    public int UserId { get; set; }
    public User User { get; set; }
}