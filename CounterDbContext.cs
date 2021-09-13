using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using webapi;

#nullable disable

namespace CounterApp
{
    public partial class CounterDbContext : DbContext
    {

        public async Task GetCounter(string path, HttpContext context, ILogger _logger)
        {
            var key = Md5(path);
            _logger.LogInformation($"--- > path = {path} md5 = {key}");


            var cnt = await  Counters.SingleOrDefaultAsync(a=> a.Id == key);
            
             if (cnt is null)
             {
                 cnt = new Counter(id: key, val: 0);
                 await context.Response.WriteAsync($"{cnt.Val.ToString()}");
                 // await context.Response.WriteAsJsonAsync(cnt);
                 cnt.Val++;
                 Add(cnt);
             }
             else
             {
                 await context.Response.WriteAsync($"{cnt.Val.ToString()}");
                 // await context.Response.WriteAsJsonAsync(cnt);
                 cnt.Val++;
                 Update(cnt);
             }
             await SaveChangesAsync();
        }

        public long GetCounter(string path)
        {
            
            var key = Md5(path);
            // _logger.LogInformation($"--- > path = {path} md5 = {key}");


            var cnt = Counters.SingleOrDefault(a=> a.Id == key);
            
             if (cnt is null)
             {
                 cnt = new Counter(id: key, val: 0);
                 cnt.Val++;
                 Add(cnt);
             }
             else
             {
                 cnt.Val++;
                 Update(cnt);
             }
             SaveChanges();
             var res = cnt!.Val;
             return cnt!.Val; //res--;
        }
        
        
        public CounterDbContext() { }

        public CounterDbContext(DbContextOptions<CounterDbContext> options) : base(options) { }

        public virtual DbSet<Counter> Counters { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https: //go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=tcp:dm.database.windows.net,1433;Initial Catalog=counter;Persist Security Info=False;User ID=dk;Password=123abceng@gmail.com;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Counter>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(32);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
        
        private static string Md5(string s)
        {
            string result;

            using MD5 md5 = MD5.Create();
            result = string.Join
            (
                "",
                from ba in md5.ComputeHash (
                    Encoding.UTF8.GetBytes(s)
                )
                select ba.ToString("x2")
                    
            );

            return result;
        }
    }
}
