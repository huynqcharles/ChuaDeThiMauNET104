using ChuaDeThiMau.Models;
using Microsoft.EntityFrameworkCore;

namespace ChuaDeThiMau.Context;

public class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
    {
        
    }

    public DbSet<NhanVien> NhanViens { get; set; }
}