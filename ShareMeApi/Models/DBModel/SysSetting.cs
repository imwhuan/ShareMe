using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ShareMeApi.Models.DBModel
{
    /// <summary>
    /// 系统设置
    /// </summary>
    public class SysSetting
    {
        /// <summary>
        /// 系统名称
        /// </summary>
        [MaxLength(20)]
        [Required, Display(Name = "系统名称"), Comment("系统名称")]
        public string? SysName { get; set; }
        /// <summary>
        /// 属性
        /// </summary>
        [MaxLength(20)]
        [Required, Display(Name = "属性"), Comment("属性")]
        public string? SysKey { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        [MaxLength(40)]
        [Required, Display(Name = "值"), Comment("值")]
        public string? SysValue { get; set; }
    }
}
