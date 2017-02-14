using Plumbery.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plumbery.Domain.Interfaces.Domain {
    public interface ITimeSheetService {
        void AddTimeSheet(TimeSheet timeSheet);
        void AddMaterialItem(TimeSheetMaterialItem material);
        void EditMaterialItem(TimeSheetMaterialItem material);
        void DeleteMaterialItem(int Id);
        IEnumerable<TimeSheetMaterialItem> ListMaterialItems(int TimeSheetId);
        void AddCommentItem(TimeSheetCommentItem comment);
        void EditCommentItem(TimeSheetCommentItem comment);
        void DeleteCommentItem(int Id);
        IEnumerable<TimeSheetCommentItem> ListCommentItems(int TimeSheetId);
        IEnumerable<Plumber> ListPlumbers();
        IEnumerable<Site> ListSites();
        Plumber GetPlumber(string UserId);
        Plumber GetPlumber(int Id);
        Site GetSite(int Id);
        TimeSheet GetTimeSheet(string Code);
        IEnumerable<User> GetPlumberUsers();
        IEnumerable<Material> ListMaterials(Plumber plumber);
        void DeleteMaterialItems(List<TimeSheetMaterialItem> items);
        void DeleteCommentItems(List<TimeSheetCommentItem> items);
        void DeductFromInventory(List<TimeSheetMaterialItem> items);
        decimal GetInventoryLeft(string stockCode, int warehouseId);
        IEnumerable<TimeSheet> GetAll();
        void DeleteSheet(int id);
        List<Plumber> GetPlumbers();
        void GetMaterialByCode(string stockCode);
        Material GetMaterialByStockCode(string stockCode);
        int TimeSheetCount(int plumberId);
        Site GetSiteByName(string siteName);
        TimeSheet GetTimeSheet(int timeSheetId);
    }
}
