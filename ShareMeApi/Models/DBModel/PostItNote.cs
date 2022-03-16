using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ShareMeApi.Models.DBModel
{
    /// <summary>
    /// 便利贴
    /// </summary>
    public class PostItNote
    {
        /// <summary>
        /// 便利贴ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 小标题
        /// </summary>
        [MaxLength(40)]
        [Required, Display(Name = "标题"),Comment("标题")]
        public string? Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [MaxLength(200)]
        [Required, Display(Name = "内容"), Comment("内容")]
        public string? Body { get; set; }

        /// <summary>
        /// 用户信息
        /// </summary>
        [Required, Display(Name = "用户"), Comment("用户")]
        public virtual UserInfo? UserInfo { get; set; }


        /// <summary>
        /// 发布时间
        /// </summary>
        [Display(Name = "发布时间"), Comment("发布时间")]
        [DataType(DataType.DateTime)]
        public DateTime? CreateDate { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        [Display(Name = "修改时间"), Comment("修改时间")]
        [DataType(DataType.DateTime)]
        public DateTime? ModifyDate { get; set; }
    }
}
