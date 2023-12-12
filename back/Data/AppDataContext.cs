using back.Models;
using Microsoft.EntityFrameworkCore;

namespace back.Data;

public class AppDataContext : DbContext
{
    public AppDataContext(DbContextOptions<AppDataContext> options) :
     base(options)
    { }
    public DbSet<Aluno> Alunos { get; set; }
    public DbSet<Imc> Imcs { get; set; }
}
