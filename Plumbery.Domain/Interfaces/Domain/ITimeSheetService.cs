using Plumbery.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plumbery.Domain.Interfaces.Domain {
    public interface ITimeSheetService {
        Task<int> AddTimeSheet(TimeSheet timeSheet);
        Task<int> AddMaterialItem(TimeSheetMaterialItem material);
        Task<int> EditMaterialItem(TimeSheetMaterialItem material);
        Task<int> DeleteMaterialItem(int Id);
        Task<IEnumerable<TimeSheetMaterialItem>> ListMaterialItems(int TimeSheetId);
        Task<int> AddCommentItem(TimeSheetCommentItem comment);
        Task<int> EditCommentItem(TimeSheetCommentItem comment);
        Task<int> DeleteCommentItem(int Id);
        Task<IEnumerable<TimeSheetCommentItem>> ListCommentItems(int TimeSheetId);
        Task<IEnumerable<Plumber>> ListPlumbers();
        Task<IEnumerable<Site>> ListSites();
        Task<Plumber> GetPlumber(string UserId);
        Task<Plumber> GetPlumber(int Id);
        Task<Site> GetSite(int Id);
        Task<TimeSheet> GetTimeSheet(string Code);
        Task<IEnumerable<User>> GetPlumberUsers();
        Task<IEnumerable<Material>> ListMaterials(Plumber plumber);
        Task<int> DeleteMaterialItems(List<TimeSheetMaterialItem> items);
        Task<int> DeleteCommentItems(List<TimeSheetCommentItem> items);
        Task<int> DeductFromInventory(List<TimeSheetMaterialItem> items);
        Task<IEnumerable<TimeSheet>> GetAll();
        Task<int> DeleteSheet(int id);
    }
}
