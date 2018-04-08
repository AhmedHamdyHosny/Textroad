using Classes.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Textroad.DataLayer;
using Textroad.Models;

namespace Textroad.Controllers
{
    public class PublishedArticleController : BaseController<PublishedArticle, PublishedArticleViewModel, PublishedArticleIndexViewModel, PublishedArticleDetailsViewModel, PublishedArticleCreateBindModel, PublishedArticleEditBindModel, PublishedArticleEditModel, PublishedArticle, PublishedArticleModel<PublishedArticle>, PublishedArticleModel<PublishedArticleViewModel>>
    {
        public override void FuncPreDetailsView(object id, ref List<PublishedArticleDetailsViewModel> items)
        {
            items = new PublishedArticleModel<PublishedArticleDetailsViewModel>().GetData(ArticleID: id, fromView: true).ToList();
        }
        public override void FuncPreInitCreateView()
        {
            //get article types
            var articleTypes = new ArticleTypeModel<ArticleType>().GetData(isBlock: false);
            ViewBag.ArticleTypes = articleTypes.Select(x => new CustomSelectListItem() { Text = x.ArticleTypeName, Value = x.ArticleTypeID.ToString() });
            //get scopes
            var scopes = new ScopeModel<Scope>().GetData(isBlock: false);
            ViewBag.Scopes = scopes.Select(x => new CustomSelectListItem() { Text = x.ScopeName, Value = x.ScopeID.ToString() });
            //get authors
            var authors = new AuthorModel<Author>().GetData(isBlock: false);
            ViewBag.Authors = authors.Select(x => new CustomSelectListItem() { Text = x.AuthorName, Value = x.AuthorID.ToString() });
            base.FuncPreInitCreateView();
        }
        public override void FuncPreCreate(ref PublishedArticleCreateBindModel model)
        {
            model.PublishedArticleID = Guid.NewGuid();
            model.CreateUserId = User.UserId;
            model.CreateDate = DateTime.Now;
            //prepare PublishArticleScope
            model.PublishArticleScope = model.PublishArticleScope.Select(x => { x.PublishedArticleScopeID = Guid.NewGuid(); x.CreateUserId = User.UserId; x.CreateDate = DateTime.Now; return x; }).ToList();
            //prepare PublishArticleAuthor
            model.PublishArticleAuthor = model.PublishArticleAuthor.Select(x=> { x.ArticleAuthorID = Guid.NewGuid(); x.CreateUserId = User.UserId; x.CreateDate = DateTime.Now; return x; }).ToList();
        }
        public override ActionResult FuncPostCreate(ref PublishedArticleCreateBindModel model, ref PublishedArticle insertedItem, bool UseAjaxCall = false, bool Success = true, string Message = null)
        {
            return base.FuncPostCreate(ref model, ref insertedItem, true, Success, Message);
        }
        public override void FuncPreInitEditView(object id, ref PublishedArticle EditItem, ref PublishedArticleEditModel model)
        {
            if (EditItem == null)
            {
                //get the item by id
                EditItem = new PublishedArticleModel<PublishedArticle>().Get(id);
            }
            if (EditItem != null)
            {
                model = new PublishedArticleEditModel();
                model.EditItem = EditItem;
                var selectedItem = EditItem;
                var articleTypes = new ArticleTypeModel<ArticleType>().GetData();
                ViewBag.ArticleTypeID = articleTypes.Select(x => new CustomSelectListItem() { Text = x.ArticleTypeName, Value = x.ArticleTypeID.ToString(), Selected = (x.ArticleTypeID == selectedItem.PublishedArticleID) }).OrderBy(x=>x.Text).ToList();
            }
        }
        public override void FuncPreEdit(ref object id, ref PublishedArticleEditBindModel EditItem)
        {
            id = EditItem.PublishedArticleID;
            EditItem.ModifyUserId = User.UserId;
            EditItem.ModifyDate = DateTime.Now;
        }

        public override ActionResult FuncPostEdit(ref PublishedArticleEditBindModel EditItem, bool UseAjaxCall = false, bool Success = true, string Message = null)
        {
            return base.FuncPostEdit(ref EditItem, true, Success, Message);
        }

    }
}