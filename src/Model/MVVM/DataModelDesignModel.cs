﻿// /***********************************************************************************************************************
// 工程：Agebull.EntityModel.Designer
// 项目：CodeRefactor
// 文件：DataAccessDesignModel.cs
// 作者：Administrator/
// 建立：2015－07－13 12:26
// ****************************************************文件说明**********************************************************
// 对应文档：
// 说明摘要：
// 作者备注：
// ****************************************************修改记录**********************************************************
// 日期：
// 人员：
// 说明：
// ************************************************************************************************************************
// 日期：
// 人员：
// 说明：
// ************************************************************************************************************************
// 日期：
// 人员：
// 说明：
// ***********************************************************************************************************************/

#region 命名空间引用

using Agebull.EntityModel.Config;

#endregion

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// 数据模型设计模型
    /// </summary>
    public sealed class DataModelDesignModel : ModelBase
    {
        #region 设计对象 

        public DesignContext Context { get; }
        public DesignGlobal Global { get; }
        public TreeModel Tree { get; }
        public ConfigIoModel ConfigIo { get; }
        public NormalCodeModel NormalCode { get; }

        /// <summary>
        /// 编辑器模型
        /// </summary>
        public EditorModel Editor { get;}


        public ExtendConfigModel ExtendConfig { get; }


        public static DataModelDesignModel Current { get; set; }

        /// <summary>
        /// 构造
        /// </summary>
        public DataModelDesignModel()
        {
            Current = this;
            Context = new DesignContext
            {
                Model = this
            };

            Global = new DesignGlobal
            {
                Model = this,
                Context = Context
            };
            GlobalConfig.SetGlobal(Global);
            ExtendConfig = new ExtendConfigModel
            {
                Model = this,
                Context = Context
            };
            Tree = new TreeModel
            {
                Model = this,
                Context = Context
            };
            ConfigIo = new ConfigIoModel
            {
                Model = this,
                Context = Context
            };
            NormalCode = new NormalCodeModel
            {
                Model = this,
                Context = Context
            };
            Editor = new EditorModel
            {
                Model = this,
                Context = Context
            };
            Editor.CreateMenus(this);
            ExtendConfig.Editor = Editor;
            Context.Editor = Editor;
            ConfigIo.Editor = Editor;
            NormalCode.Editor = Editor;
            Tree.Editor = Editor;
            Global.Editor = Editor;
        }

        #endregion

        #region 初始化

        /// <summary>
        ///     初始化
        /// </summary>
        protected override void DoInitialize()
        {
            base.DoInitialize();
            ExtendConfig.ViewModel = ViewModel;
            Context.ViewModel = ViewModel;
            ConfigIo.ViewModel = ViewModel;
            NormalCode.ViewModel = ViewModel;
            Tree.ViewModel = ViewModel;
            Global.ViewModel = ViewModel;

            ExtendConfig.Dispatcher = Dispatcher;
            Context.Dispatcher = Dispatcher;
            ConfigIo.Dispatcher = Dispatcher;
            NormalCode.Dispatcher = Dispatcher;
            Tree.Dispatcher = Dispatcher;
            Global.Dispatcher = Dispatcher;


            Editor.Initialize();
            Context.Initialize();
            Global.Initialize();
            Tree.Initialize();
            ExtendConfig.Initialize();
            NormalCode.Initialize();
            ConfigIo.Initialize();
        }

        /// <summary>
        /// 同步解决方案变更
        /// </summary>
        public void OnSolutionChanged()
        {
            Global.OnSolutionChanged();
            Editor.OnSolutionChanged();
            ExtendConfig.OnSolutionChanged();
            Context.OnSolutionChanged();
            ConfigIo.OnSolutionChanged();
            Tree.OnSolutionChanged();
            NormalCode.OnSolutionChanged();
            FirstSelect();

        }
        /// <summary>
        /// 保证载入后选择正常
        /// </summary>
        internal void FirstSelect()
        {
            GlobalConfig.CurrentConfig = null;
            Tree.SetSelect(null);
            Tree.SetSelect(Tree.TreeRoot.Items[0]);
            GlobalConfig.CurrentSolution.GodMode = true;
        }
        /// <summary>
        /// 新增对象
        /// </summary>
        /// <typeparam name="TConfig"></typeparam>
        /// <param name="title"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public bool CreateNew<TConfig>(string title, out TConfig config)
            where TConfig : ConfigBase, new()
        {
            config = new TConfig();
            return CommandIoc.NewConfigCommand(title, config);
        }

        #endregion

    }
}