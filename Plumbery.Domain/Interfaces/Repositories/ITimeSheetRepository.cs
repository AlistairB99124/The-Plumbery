using Plumbery.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plumbery.Domain.Interfaces.Repositories {
    public interface ITimeSheetRepository:IBaseRepository<TimeSheet> {
        void AddMaterialItem(TimeSheetMaterialItem material);
        void EditMaterialItem(TimeSheetMaterialItem material);
        void DeleteMaterialItem(int Id);
        void DeleteMaterialItems(List<TimeSheetMaterialItem> items);
        IEnumerable<TimeSheetMaterialItem> ListMaterialItems(int TimeSheetId);
        void AddCommentItem(TimeSheetCommentItem comment);
        void EditCommentItem(TimeSheetCommentItem comment);
        void DeleteCommentItem(int Id);
        void DeleteCommentItems(List<TimeSheetCommentItem> items);
        IEnumerable<TimeSheetCommentItem> ListCommentItems(int TimeSheetId);
        IEnumerable<Plumber> ListPlumbers();
        IEnumerable<Site> ListSites();
        Plumber GetPlumber(string UserId);
        Plumber GetPlumber(int Id);
        Site GetSite(int Id);
        TimeSheet GetTimeSheet(string Code);
        IEnumerable<User> GetPlumberUsers();
        IEnumerable<Material> ListMaterials(Plumber plumber);

        void DeductFromInventory(List<TimeSheetMaterialItem> items);
        void DeleteSheet(int Id);
    }
}
