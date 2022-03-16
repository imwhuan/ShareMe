using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace ShareMeApi.DBContext
{
    /// <summary>
    /// 自动生成时间
    /// </summary>
    public class CreateTimeGenerator : ValueGenerator<DateTime>
    {
        /// <summary>
        /// GeneratesTemporaryValues
        /// </summary>
        public override bool GeneratesTemporaryValues => false;
        /// <summary>
        /// 生成的值
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        public override DateTime Next(EntityEntry entry)
        {
            return DateTime.Now;
        }
    }
}
