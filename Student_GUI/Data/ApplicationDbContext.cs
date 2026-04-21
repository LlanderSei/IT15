using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Student_GUI.Data {
  public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext(options) {
    protected override void OnModelCreating(ModelBuilder builder) {
      base.OnModelCreating(builder);
    }
  }
}
