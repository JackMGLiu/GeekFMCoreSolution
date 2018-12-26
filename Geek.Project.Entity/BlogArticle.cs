using System;

namespace Geek.Project.Entity
{
    /// <summary>
    /// 博客文章
    /// </summary>
    public class BlogArticle
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string BlogId { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string BlogSubmitter { get; set; }

        /// <summary>
        /// 博客标题
        /// </summary>
        public string BlogTitle { get; set; }

        /// <summary>
        /// 博客类别
        /// </summary>
        public string BlogCategory { get; set; }

        /// <summary>
        /// 博客内容
        /// </summary>
        public string BlogContent { get; set; }

        /// <summary>
        /// 访问量
        /// </summary>
        public int BlogTraffic { get; set; }

        /// <summary>
        /// 评论数量
        /// </summary>
        public int BlogCommentNum { get; set; }

        /// <summary> 
        /// 修改时间
        /// </summary>
        public DateTime BlogUpdateTime { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime BlogCreateTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string BlogRemark { get; set; }
    }
}
