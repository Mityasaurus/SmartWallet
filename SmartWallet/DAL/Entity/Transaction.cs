using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace SmartWallet.DAL.Entity;

public class Transaction
{
    [Key]
    public int Id { get; set; }
    
    public int UserId { get; set; }
    public int UserCardId { get; set; }
    public User User { get; set; }
    public Card UserCard { get; set; }
    
    public double Amount { get; set; }
    public string OperationType { get; set; } // Get or Send // TODO make Enum
}