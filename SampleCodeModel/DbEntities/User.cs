using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SampleCodeModel.DbEntities;

public partial class User
{
    [Key]
    public int UserId { get; set; }

    [StringLength(50)]
    public string FirstName { get; set; }

    [StringLength(50)]
    public string LastName { get; set; }

    [StringLength(500)]
    public string Password { get; set; }

    [StringLength(50)]
    public string EmailId { get; set; }
}
