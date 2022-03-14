using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShareMeApi.Models.DBModel
{
    public class UserInfo
    {
        public int Id { get; set; }

        [MaxLength(40)]
        [Required, Display(Name = "名称"), Comment("名称")]
        public string? Name { get; set; }

        [MaxLength(200)]
        [Display(Name = "个人描述"), Comment("个人描述")]
        public string? Description { get; set; }

        [MaxLength(20)]
        [Required, Display(Name = "密码"), Comment("密码")]
        public string? Password { get; set; }

        [Display(Name = "性别"), Comment("性别")]
        public int? Sex { get; set; }

        [Display(Name = "生日"), Comment("生日")]
        [DataType(DataType.Date)]
        public DateTime? Birthday { get; set; }

        [Display(Name = "创建时间"), Comment("创建时间")]
        [DataType(DataType.DateTime)]
        public DateTime? CreateDate { get; set; }

        /// <summary>
        /// 显示性别
        /// </summary>
        [NotMapped]
        public string DisplaySex
        {
            get
            {
                return this.Sex switch
                {
                    1 => "男",
                    2 => "女",
                    _ => "未知"
                };
            }

        }
    }
}
