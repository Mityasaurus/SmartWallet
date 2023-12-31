﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartWallet.DAL.Entity;

public class User
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string LastName { get; set; }
    
    [Required]
    public string Phone { get; set; }
    
    [Required]
    public string Email { get; set; }
    
    [Required]
    public string Password { get; set; }
    
    [Required]
    public DateTime RegistrationDate { get; set; }
    
    public List<Card> Cards { get; set; }
}