using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Classes.Helper
{
    public class CustomSelectListItem : SelectListItem
    {
        public string HtmlClass { get; set; }
        //public string Disabled { get; set; }
        //public string SelectedValue { get; set; }

        public CustomSelectListItem()
        {

        }

        public CustomSelectListItem(SelectListItem item)
        {
            this.Text = item.Text;
            this.Value = item.Value;
            this.Selected = item.Selected;
            this.Group = item.Group;
            this.Disabled = item.Disabled;
        }
    }

    public static class CustomHelpers
    {
        public static MvcHtmlString CustomDropdownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<CustomSelectListItem> list, string selectedValue, string optionLabel, object htmlAttributes = null)
        {
            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression<TModel, TProperty>(expression, htmlHelper.ViewData);
            string name = ExpressionHelper.GetExpressionText((LambdaExpression)expression);
            return CustomDropdownList(htmlHelper, metadata, name, optionLabel, list, selectedValue, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        private static MvcHtmlString CustomDropdownList(this HtmlHelper htmlHelper, ModelMetadata metadata, string name, string optionLabel, IEnumerable<CustomSelectListItem> list, string selectedValue, IDictionary<string, object> htmlAttributes)
        {
            string fullName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);
            if (String.IsNullOrEmpty(fullName))
            {
                throw new ArgumentException("name");
            }

            TagBuilder dropdown = new TagBuilder("select");
            dropdown.Attributes.Add("name", fullName);
            dropdown.MergeAttribute("data-val", "true");
            dropdown.MergeAttribute("data-val-required", "Mandatory field.");
            //dropdown.MergeAttribute("data-val-number", "The field must be a number.");
            dropdown.MergeAttributes(htmlAttributes); //dropdown.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            dropdown.MergeAttributes(htmlHelper.GetUnobtrusiveValidationAttributes(name, metadata));

            StringBuilder options = new StringBuilder();

            // Make optionLabel the first item that gets rendered.
            if (optionLabel != null)
                options = options.Append("<option value='" + String.Empty + "'>" + optionLabel + "</option>");

            foreach (var item in list)
            {
                if (item.Selected && item.Disabled)
                    options = options.Append("<option value='" + item.Value + "' class='" + item.HtmlClass + "' selected='" + item.Selected + "' disabled='" + item.Disabled + "'>" + item.Text + "</option>");
                else if (!item.Selected && item.Disabled)
                    options = options.Append("<option value='" + item.Value + "' class='" + item.HtmlClass + "' disabled='" + item.Disabled + "'>" + item.Text + "</option>");
                else if (item.Selected && !item.Disabled)
                    options = options.Append("<option value='" + item.Value + "' class='" + item.HtmlClass + "' selected='" + item.Selected + "'>" + item.Text + "</option>");
                else
                    options = options.Append("<option value='" + item.Value + "' class='" + item.HtmlClass + "'>" + item.Text + "</option>");
            }
            dropdown.InnerHtml = options.ToString();
            return MvcHtmlString.Create(dropdown.ToString(TagRenderMode.Normal));
        }

        //public static MvcHtmlString CustomDropdownList(this HtmlHelper htmlHelper, string name, SelectList list, string optionLabel, string selectedValue, IDictionary<string, object> htmlAttributes)
        //{
        //    string fullName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);
        //    if (String.IsNullOrEmpty(fullName))
        //    {
        //        throw new ArgumentException("name");
        //    }

        //    TagBuilder dropdown = new TagBuilder("select");
        //    dropdown.Attributes.Add("name", fullName);
        //    dropdown.MergeAttribute("data-val", "true");
        //    dropdown.MergeAttribute("data-val-required", "Mandatory field.");
        //    dropdown.MergeAttribute("data-val-number", "The field must be a number.");
        //    dropdown.MergeAttributes(htmlAttributes); //dropdown.MergeAttributes(new RouteValueDictionary(htmlAttributes));
        //    dropdown.MergeAttributes(htmlHelper.GetUnobtrusiveValidationAttributes(name, metadata));

        //    StringBuilder options = new StringBuilder();

        //    // Make optionLabel the first item that gets rendered.
        //    if (optionLabel != null)
        //        options = options.Append("<option value='" + String.Empty + "'>" + optionLabel + "</option>");

        //    foreach (var item in list)
        //    {
        //        if (item.Selected && item.Disabled)
        //            options = options.Append("<option value='" + item.Value + "' class='" + item.HtmlClass + "' selected='" + item.Selected + "' disabled='" + item.Disabled + "'>" + item.Text + "</option>");
        //        else if (!item.Selected && item.Disabled)
        //            options = options.Append("<option value='" + item.Value + "' class='" + item.HtmlClass + "' disabled='" + item.Disabled + "'>" + item.Text + "</option>");
        //        else if (item.Selected && !item.Disabled)
        //            options = options.Append("<option value='" + item.Value + "' class='" + item.HtmlClass + "' selected='" + item.Selected + "'>" + item.Text + "</option>");
        //        else
        //            options = options.Append("<option value='" + item.Value + "' class='" + item.HtmlClass + "'>" + item.Text + "</option>");
        //    }
        //    dropdown.InnerHtml = options.ToString();
        //    return MvcHtmlString.Create(dropdown.ToString(TagRenderMode.Normal));
        //}
    }
}