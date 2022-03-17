using Microsoft.EntityFrameworkCore;
using ShareMeApi.Models.DBModel;

namespace ShareMeApi.DBContext
{
    /// <summary>
    /// 自定义数据库上下文
    /// </summary>
    public class ShareMeDBContext: DbContext
    {
#pragma warning disable CS8618 // 在退出构造函数时，不可为 null 的字段必须包含非 null 值。请考虑声明为可以为 null。
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="options"></param>
        public ShareMeDBContext(DbContextOptions<ShareMeDBContext> options):base(options)
#pragma warning restore CS8618 // 在退出构造函数时，不可为 null 的字段必须包含非 null 值。请考虑声明为可以为 null。
        {

        }
        /// <summary>
        /// 便利贴数据表
        /// </summary>
        public DbSet<PostItNote> PostItNotes { get; set; }
        /// <summary>
        /// 用户信息数据表
        /// </summary>
        public DbSet<UserInfo> UserInfos  { get; set; }
        /// <summary>
        /// 系统设置表
        /// </summary>
        public DbSet<SysSetting> SysSettings { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserInfo>().Property(u=>u.CreateDate).ValueGeneratedOnAdd().HasValueGenerator(typeof(CreateTimeGenerator));
            modelBuilder.Entity<PostItNote>().Property(p=>p.CreateDate).ValueGeneratedOnAdd().HasValueGenerator(typeof(CreateTimeGenerator));
            modelBuilder.Entity<PostItNote>().Property(p => p.ModifyDate).ValueGeneratedOnAddOrUpdate().HasValueGenerator(typeof(CreateTimeGenerator));
            modelBuilder.Entity<SysSetting>().HasKey(s => new { s.SysName, s.SysKey });
            base.OnModelCreating(modelBuilder);
        }
        /// <summary>
        /// 配置上下文
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.LogTo(Console.WriteLine,LogLevel.Warning);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
