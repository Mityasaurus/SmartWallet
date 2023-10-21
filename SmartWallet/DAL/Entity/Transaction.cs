using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace SmartWallet.DAL.Entity;

public class Transaction
{
    [Key]
    public int Id { get; set; }
    
    public int SenderId { get; set; }
    public int SenderCardId { get; set; }
    public int RecipientId { get; set; }
    public int RecipientCardId { get; set; }
    public User Sender { get; set; }
    public Card SenderCard { get; set; }
    public User Recipient { get; set; }
    public Card RecipientCard { get; set; }
    
    public double Amount { get; set; }
    public double Rate { get; set; }
}