using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using System;

namespace CXY.CJS.Model
{
    public class Menu : Entity<string>, IHasModificationTime, IHasDeletionTime, ISoftDelete
    {
        public string ParentId { get; set; }
        public string MenuName { get; set; }

        public int MenuLeval { get; set; }
        public string MenuUrl { get; set; }

        public int MenuLayer { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime? DeletionTime { get; set; }

        /// <summary>
        /// 是否系统菜单
        /// </summary>
        public  bool IsSys { get; set; }

        /// <summary>
        /// 是否为外部链接
        /// </summary>
        public bool IsOut { get; set; }

        /// <summary>
        /// 是否拥有下级菜单
        /// </summary>
        public  bool IsParent { get; set; }


        /// <summary>
        /// 目标框架
        /// </summary>
        public string TargetFrame { get; set; }


        /// <summary>
        /// 权重
        /// </summary>
        public  int Weight { get; set; }

        public Menu()
        {
            LastModificationTime = Clock.Now;
            IsDeleted = false;
        }

        public DateTime? LastModificationTime { get; set; }
    }
}
