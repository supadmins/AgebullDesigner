using System;
using System.Collections;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Media;
using Agebull.EntityModel.Designer;
using Agebull.EntityModel.Config;
using Agebull.Common.Mvvm;

namespace Agebull.Common.Config.Designer
{
    /// <summary>
    /// 关系连接检查
    /// </summary>
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    internal sealed class MySqlImporter : ConfigCommandBase<ProjectConfig>, IAutoRegister
    {
        /// <summary>
        /// 注册代码
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            NoButton = true;
            Signle = true;
            CommandCoefficient.RegisterCommand<ProjectConfig, MySqlImporter>();
        }


        public override CommandItem ToCommand(object arg, Func<object, IEnumerator> enumerator = null)
        {
            return new CommandItem
            {
                NoButton = true,
                Command = new AsyncCommand<string, string>(ImportStructParpare, ImportStruct, ImportStructEnd),
                Caption = "导入MySql数据库",
                Catalog = "解决方案",
                Image = Application.Current.Resources["tree_Assembly"] as ImageSource
            };
        }

        public bool ImportStructParpare(string arg, Action<string> setAction)
        {
            if (DataModelDesignModel.Current.Context.SelectProject == null)
            {
                MessageBox.Show("请选择一个项目并正确设置连接信息后继续");
                return false;
            }
            if(MessageBox.Show($"确认在【{DataModelDesignModel.Current.Context.SelectProject.Caption}】中执行【导入MySql数据库】操作吗?",
                           "对象编辑", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
                return false;
            DataModelDesignModel.Current.Editor.ShowTrace();
            return true;
        }


        /// <summary>
        ///     导入MySql数据库
        /// </summary>
        /// <returns></returns>
        internal string ImportStruct(string arg)
        {
            var ctx = DataModelDesignModel.Current.Context;
            new MySqlImport().Import(ctx.CurrentTrace.TraceMessage, ctx.SelectProject, DataModelDesignModel.Current.Dispatcher);
            return string.Empty;
        }

        internal void ImportStructEnd(CommandStatus status, Exception ex, string code)
        {
            DataModelDesignModel.Current.Editor.ShowTrace();
            if (ex != null)
                DataModelDesignModel.Current.Context.CurrentTrace.TraceMessage.Track = ex.ToString();
        }
    }
}