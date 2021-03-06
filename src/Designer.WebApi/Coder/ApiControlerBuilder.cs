using System.IO;
using System.Text;
using Agebull.EntityModel.RobotCoder;

namespace Agebull.EntityModel.Designer.WebApi
{


    public sealed class ApiControlerBuilder : EntityCoderBase
    {
        /// <summary>
        /// 是否可写
        /// </summary>
        protected override bool CanWrite => true;

        /// <summary>
        /// 名称
        /// </summary>
        protected override string FileSaveConfigName => "File_Api_Controller_cs";

        /// <summary>
        /// 是否客户端代码
        /// </summary>
        protected override bool IsClient => true;

        /// <summary>
        ///     生成实体代码
        /// </summary>
        protected override void CreateBaCode(string path)
        {

            string code = $@"using System;
using System.Web.Http;
using GoodLin.Common.Ioc;
using Yizuan.Service.Api;
using Yizuan.Service.Api.WebApi;

namespace {NameSpace}.WebApi.EntityApi
{{
    /// <summary>
    /// 身份验证服务API
    /// </summary>
    public class {Entity.Name}ApiController : ApiController
    {{
        /// <summary>
        ///     新增
        /// </summary>
        /// <param name=""data"">数据</param>
        /// <returns>如果为真并返回结果数据</returns>
        [HttpPost, Route(""entity/{Entity.Name}/AddNew"")]
        [ApiAccessOptionFilter(ApiAccessOption.Internal | ApiAccessOption.Public | ApiAccessOption.Anymouse)]
        public ApiResponseMessage<ApiResult<{Entity.Name}>> AddNew([FromBody]{Entity.Name} data)
        {{
            var lg = new {Entity.Name}ApiLogical() as I{Entity.Name}Api;
            var result = lg.AddNew(data);
            return Request.ToResponse(result);
        }}

        /// <summary>
        ///     修改
        /// </summary>
        /// <param name=""data"">数据</param>
        /// <returns>如果为真并返回结果数据</returns>
        [HttpPost, Route(""entity/{Entity.Name}/Update"")]
        [ApiAccessOptionFilter(ApiAccessOption.Internal | ApiAccessOption.Public | ApiAccessOption.Anymouse)]
        public ApiResponseMessage<ApiResult<{Entity.Name}>> Update([FromBody]{Entity.Name} data)
        {{
            var lg = new {Entity.Name}ApiLogical() as I{Entity.Name}Api;
            var result = lg.Update(data);
            return Request.ToResponse(result);
        }}

        /// <summary>
        ///     删除
        /// </summary>
        /// <param name=""dataKey"">数据主键</param>
        /// <returns>如果为否将阻止后续操作</returns>
        [HttpPost, Route(""entity/{Entity.Name}/Delete"")]
        [ApiAccessOptionFilter(ApiAccessOption.Internal | ApiAccessOption.Public | ApiAccessOption.Anymouse)]
        public ApiResponseMessage Delete([FromBody]Argument<int> dataKey)
        {{
            var lg = new {Entity.Name}ApiLogical() as I{Entity.Name}Api;
            var result = lg.Delete(dataKey);
            return Request.ToResponse(result);
        }}

        /// <summary>
        ///     分页
        /// </summary>
        [HttpPost, Route(""entity/{Entity.Name}/Query"")]
        [ApiAccessOptionFilter(ApiAccessOption.Internal | ApiAccessOption.Public | ApiAccessOption.Anymouse)]
        public ApiResponseMessage<ApiResult<ApiPageData<{Entity.Name}>>> Query([FromBody]PageArgument arg)
        {{
            var lg = new {Entity.Name}ApiLogical() as I{Entity.Name}Api;
            var result = lg.Query(arg);
            return Request.ToResponse(result);
        }}

    }}
}}
";
            var file = ConfigPath(Entity, FileSaveConfigName, path, "Controllers", $"{Entity.Name}Controller.cs");
            SaveCode(file, code);
        }


        /// <summary>
        ///     生成实体代码
        /// </summary>
        protected override void CreateExCode(string path)
        {
            if (Project.ApiItems.Count == 0)
                return;
            Default1(path);
            Default2(path);
        }

        void Default1(string path)
        {
            StringBuilder code = new StringBuilder();
            code.Append($@"using System;
using System.Collections.Generic;
using System.Web.Http;
using GoodLin.Common.Ioc;
using Yizuan.Service.Api;
using Yizuan.Service.Api.WebApi;

namespace {NameSpace}.WebApi
{{
    /// <summary>
    /// 身份验证服务API
    /// </summary>
    public class {Project.ApiName}Controller : ApiController
    {{");

            foreach (var item in Project.ApiItems)
            {
                code.Append($@"
        /// <summary>
        ///     {item.Caption}:{item.Description}:
        /// </summary>");
                if (item.Argument != null)
                {
                    code.Append($@"
        /// <param name=""arg"">{item.Argument?.Caption}</param>");
                }
                if (item.Result == null)
                {
                    code.Append(@"
        /// <returns>操作结果</returns>");
                }
                else
                {
                    code.Append($@"
        /// <returns>{item.Result.Caption}</returns>");
                }
                var res = item.Result == null ? null : ("<ApiResult<" + item.ResultArg + ">>");
                var arg = item.Argument == null ? null : ($"[FromBody]{item.CallArg} arg");

                code.Append($@"
        [HttpPost, Route(""{item.RoutePath}"")]
        //[ApiAccessOptionFilter(ApiAccessOption.Internal | ApiAccessOption.Public | ApiAccessOption.Anymouse)]
        public ApiResponseMessage{res} {item.Name}({arg})
        {{
            var lg = new {Project.ApiName}Logical() as I{Project.ApiName};");

                if (item.Argument == null)
                {
                    code.Append($@"
            var result = lg.{item.Name}();");
                }
                else
                {
                    code.Append($@"
            var result = lg.{item.Name}(arg);");
                }
                code.Append(@"
            return Request.ToResponse(result);
        }");
            }

            code.Append(@"
    }
}
");
            var file = ConfigPath(Project, FileSaveConfigName, path, "Controllers", $"{Project.Name}_Controller.cs");
            WriteFile(file, code.ToString());
        }
        void Default2(string path)
        {
            StringBuilder code = new StringBuilder();
            code.Append($@"using System;
using System.Collections.Generic;
using System.Web.Http;
using GoodLin.Common.Ioc;
using Yizuan.Service.Api;
using Yizuan.Service.Api.WebApi;

namespace {NameSpace}.WebApi
{{
    /// <summary>
    /// 身份验证服务API
    /// </summary>
    public class {Project.ApiName}Controller : ApiController
    {{");

            foreach (var item in Project.ApiItems)
            {
                code.Append($@"
        /// <summary>
        ///     {item.Caption}:{item.Description}:
        /// </summary>");
                if (item.Argument != null)
                {
                    code.Append($@"
        /// <param name=""arg"">{item.Argument?.Caption}</param>");
                }
                if (item.Result == null)
                {
                    code.Append(@"
        /// <returns>操作结果</returns>");
                }
                else
                {
                    code.Append($@"
        /// <returns>{item.Result.Caption}</returns>");
                }
                var res = item.Result == null ? null : "<ApiResult<" + item.ResultArg + ">>";
                var res2 = item.Result == null ? "ApiResult" : "ApiResult<" + item.ResultArg + ">";
                var arg = item.Argument == null ? null : ($"[FromBody]{item.CallArg} arg");

                code.Append($@"
        [HttpPost, Route(""{item.RoutePath}"")]
        public ApiResponseMessage{res} {item.Name}({arg})
        {{");
                if (item.Result != null)
                {
                    code.Append($@"
            return Request.ToResponse({res2}.Succees({HelloCode(item.Result)}));
        }}");
                }
                else
                code.Append($@"
            return Request.ToResponse({res2}.Succees());
        }}");
            }

            code.Append(@"
    }
}
");
            var file = ConfigPath(Project, FileSaveConfigName+"_HelloCode", path, "Controllers", $"{Project.Name}_Controller.HelloCode.cs");
            WriteFile(file, code.ToString());
        }
    }

}
