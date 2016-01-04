
namespace Wicresoft.Solution.Utils.MailUtils
{
    public class TemplateController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">is the object type corresponing to the tempalte</typeparam>
        /// <param name="tPath">xls template path</param>
        /// <returns></returns>
        protected virtual ActionResult Template<T>(string tPath, T Model) where T : class
        {
            TemplateResult<T> tr = new TemplateResult<T>(tPath);
            tr.Object = (T)Model;
            return tr;
        }
    }
}
