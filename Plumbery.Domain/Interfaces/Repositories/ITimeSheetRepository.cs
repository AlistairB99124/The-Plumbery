using Plumbery.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plumbery.Domain.Interfaces.Repositories {
    /// <summary>
    /// 
    /// </summary>
    public interface ITimeSheetRepository:IBaseRepository<TimeSheet> {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="material"></param>
        void AddMaterialItem(TimeSheetMaterialItem material);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="material"></param>
        void EditMaterialItem(TimeSheetMaterialItem material);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        void DeleteMaterialItem(int Id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        void DeleteMaterialItems(List<TimeSheetMaterialItem> items);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="TimeSheetId"></param>
        /// <returns></returns>
        IEnumerable<TimeSheetMaterialItem> ListMaterialItems(int TimeSheetId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="comment"></param>
        void AddCommentItem(TimeSheetCommentItem comment);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="comment"></param>
        void EditCommentItem(TimeSheetCommentItem comment);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        void DeleteCommentItem(int Id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        void DeleteCommentItems(List<TimeSheetCommentItem> items);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="TimeSheetId"></param>
        /// <returns></returns>
        IEnumerable<TimeSheetCommentItem> ListCommentItems(int TimeSheetId);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<Plumber> ListPlumbers();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<Site> ListSites();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        Plumber GetPlumber(string UserId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Plumber GetPlumber(int Id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Site GetSite(int Id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
        TimeSheet GetTimeSheet(string Code);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<User> GetPlumberUsers();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="plumber"></param>
        /// <returns></returns>
        IEnumerable<Material> ListMaterials(Plumber plumber);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        void DeductFromInventory(List<TimeSheetMaterialItem> items);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        void DeleteSheet(int Id);
    }
}
