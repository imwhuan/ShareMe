using Microsoft.EntityFrameworkCore;
using ShareMeApi.Models.DBModel;

namespace ShareMeApi.DBContext
{
    public class ShareMeDBContext: DbContext
    {
#pragma warning disable CS8618 // 在退出构造函数时，不可为 null 的字段必须包含非 null 值。请考虑声明为可以为 null。
        public ShareMeDBContext(DbContextOptions<ShareMeDBContext> options):base(options)
#pragma warning restore CS8618 // 在退出构造函数时，不可为 null 的字段必须包含非 null 值。请考虑声明为可以为 null。
        {

        }
        public DbSet<PostItNote> PostItNotes { get; set; }
        public DbSet<UserInfo> UserInfos  { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserInfo>().Property(u=>u.CreateDate).ValueGeneratedOnAdd().HasValueGenerator(typeof(CreateTimeGenerator));
            modelBuilder.Entity<PostItNote>().Property(p=>p.CreateDate).ValueGeneratedOnAdd().HasValueGenerator(typeof(CreateTimeGenerator));
            modelBuilder.Entity<PostItNote>().Property(p => p.ModifyDate).ValueGeneratedOnAddOrUpdate().HasValueGenerator(typeof(CreateTimeGenerator));
            base.OnModelCreating(modelBuilder);
        }
    }
}
