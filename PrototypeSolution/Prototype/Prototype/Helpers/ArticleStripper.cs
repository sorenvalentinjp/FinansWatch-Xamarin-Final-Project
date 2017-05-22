using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Prototype.Models;

namespace Prototype.Helpers
{
    public static class ArticleStripper
    {
        public static Article StripArticleBodyText(Article article)
        {
            article.bodyText = StripRelatedArticles(article.bodyText);
            return article;
        }

        public static Article StripArticleTeasers(Article article)
        {
            if (article.teasers != null)
            {
                article.teasers.FRONTPAGE = StripAllHtmlParagraphTags(article.teasers.FRONTPAGE);
                article.teasers.DEFAULT = StripAllHtmlParagraphTags(article.teasers.DEFAULT);
            }
            return article;
        }


        public static string StripAllHtmlParagraphTags(string html)
        {
            if (html == null)
            {
                return "";
            }
            var pattern = "<p>|<\\/p>";
            return Regex.Replace(html, pattern, "");
        }

        /// <summary>
        /// Strips html text with related articles from a html string. Used in every article at the end of its bodytext.
        /// Works by stripping <ul></ul> tags, so it might strip lists who arent meant to be stripped.
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string StripRelatedArticles(string html)
        {
            if (html == null)
            {
                return "";
            }
            var pattern = "<ul.*</ul>";
            return Regex.Replace(html, pattern, "");
        }
    }
}
