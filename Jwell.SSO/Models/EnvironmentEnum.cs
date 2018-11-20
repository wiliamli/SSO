using System.ComponentModel;

namespace Jwell.SSO.Models
{
    /// <summary>
    /// 部署环境
    /// </summary>
    public enum EnvironmentEnum
    {
        /// <summary>
        /// 冒烟测试环境
        /// </summary>
        [Description("测试")]
        TEST,

        /// <summary>
        /// QA环境
        /// </summary>
        [Description("QA")]
        QA,

        /// <summary>
        /// 预发环境
        /// </summary>
        [Description("预发")]
        GRAY,

        /// <summary>
        /// 生产
        /// <code>Description为空，生产不需要显示环境名称</code>
        /// </summary>
        [Description("")] 
        PRODUCT
    }
}