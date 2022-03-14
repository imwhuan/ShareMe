using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ShareMeApi.Models.DBModel
{
    public class PostItNote
    {
        public int Id { get; set; }

        [MaxLength(40)]
        [Required, Display(Name = "标题"),Comment("标题")]
        public string? Title { get; set; }

        [MaxLength(200)]
        [Required, Display(Name = "内容"), Comment("内容")]
        public string? Body { get; set; }

        [Required, Display(Name = "用户"), Comment("用户")]
        public virtual UserInfo? UserInfo { get; set; }


        [Display(Name = "发布时间"), Comment("发布时间")]
        [DataType(DataType.DateTime)]
        public DateTime? CreateDate { get; set; }

        [Display(Name = "修改时间"), Comment("修改时间")]
        [DataType(DataType.DateTime)]
        public DateTime? ModifyDate { get; set; }
    }
}
