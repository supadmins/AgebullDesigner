namespace Agebull.EntityModel.Config
{
    /// <summary>
    /// 表示为命令节点
    /// </summary>
    public interface ICommandItem
    {
        /// <summary>
        /// API原始命令请求参数名称
        /// </summary>
        string OrgArg { get; }

        /// <summary>
        /// 客户端命令请求参数名称
        /// </summary>
        string CurArg { get; }
        /*// <summary>
        /// 原始定义内容
        /// </summary>
        string DefaultCode { get; }
        /// <summary>
        /// API对应的命令号
        /// </summary>
        string CommandId { get; }

        /// <summary>
        /// 本地命令(不转发)
        /// </summary>
        bool LocalCommand { get; }*/

    }
}