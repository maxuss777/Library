﻿using System;
using System.Text;
using System.Web.Mvc;
using Library.UI.Models;

namespace Library.UI.Helpers
{
    public static class PagingHelpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html,
            PagingInfo paginglnfo,
            Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 1; i <= paginglnfo.TotalPages; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = i.ToString();
                if (i == paginglnfo.CurrentPage)
                    tag.AddCssClass("selected");
                result.Append(tag);
            }
            return MvcHtmlString.Create(result.ToString());
        }
    }
}