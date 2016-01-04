using AutoMapper;
using Wicresoft.Solution.Entity;
using Wicresoft.Solution.PortalModel;

namespace Wicresoft.Solution.BLL
{
    public class EntityMapping
    {
        static EntityMapping()
        {
            #region 系统管理模块
            ////菜单
            //Mapper.CreateMap<Sys_Menu, Sys_Menu_Model>();
            //Mapper.CreateMap<Sys_Menu_Model, Sys_Menu>();

   

            #endregion

       


        }

        public static T2 Auto<T1, T2>(T1 originEntity, T2 defaultValue = default(T2))
        {
            if (originEntity == null)
            {
                if (defaultValue != null)
                    return defaultValue;
            }
            return Mapper.Map<T1, T2>(originEntity);
        }
    }
}
