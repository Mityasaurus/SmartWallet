using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

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
    public Currency Currency { get; set; }
    
    [Required]
    public double Balance { get; set; }

    public byte[] Background { get; set; }
    
    public int UserId { get; set; }
    public User User { get; set; }
}