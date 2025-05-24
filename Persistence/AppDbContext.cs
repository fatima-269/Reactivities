using System;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class AppDbcontext(DbContextOptions<AppDbcontext> options) : DbContext(options)
{
  public required DbSet<Activity> Activities { get; set; }
}
