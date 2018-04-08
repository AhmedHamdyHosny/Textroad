using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Textroad.DataLayer;
using Textroad.Models;

namespace Textroad.Controllers
{
    public class NewsController : BaseController<News, NewsViewModel, NewsIndexViewModel, NewsDetailsViewModel, NewsCreateBindModel, NewsEditBindModel, NewsEditModel, News, NewsModel<News>, NewsModel<NewsViewModel>>
    {
        public override void FuncPreDetailsView(object id, ref List<NewsDetailsViewModel> items)
        {
            items = new NewsModel<NewsDetailsViewModel>().GetData(NewsID: id, fromView: true).ToList();
        }
        public override void FuncPreCreate(ref NewsCreateBindModel model)
        {
            model.NewsID = Guid.NewGuid();
            model.CreateUserId = User.UserId;
            model.CreateDate = DateTime.Now;
        }
        public override void FuncPreInitEditView(object id, ref News EditItem, ref NewsEditModel model)
        {
            if (EditItem == null)
            {
                //get the item by id
                EditItem = new NewsModel<News>().Get(id);
            }
            if (EditItem != null)
            {
                model = new NewsEditModel();
                model.EditItem = EditItem;
            }
        }
        public override void FuncPreEdit(ref object id, ref NewsEditBindModel EditItem)
        {
            id = EditItem.NewsID;
            var item = new NewsModel<News>().GetData(NewsID: id).SingleOrDefault();
            EditItem.CreateUserId = item.CreateUserId;
            EditItem.CreateDate = item.CreateDate;
            EditItem.NoView = item.NoView;
            EditItem.ModifyUserId = User.UserId;
            EditItem.ModifyDate = DateTime.Now;
        }
    }
}