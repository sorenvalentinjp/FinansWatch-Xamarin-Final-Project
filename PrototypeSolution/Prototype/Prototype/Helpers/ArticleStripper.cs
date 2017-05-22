using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Prototype.Models;

namespace Prototype.Helpers
{
    public static class ArticleStripper
    {
        /// <summary>
        /// Removes all html unordered list tags and their contents from an article's bodytext
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        public static Article StripArticleBodyText(Article article)
        {
            if (!string.IsNullOrEmpty(article.bodyText))
            {
                article.bodyText = StripRelatedArticles(article.bodyText);
            }
            return article;
        }

        /// <summary>
        /// Removes all html paragraph tags from an article's teasers.
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        public static Article StripArticleTeasers(Article article)
        {
            if (article.teasers != null)
            {
                article.teasers.FRONTPAGE = StripAllHtmlParagraphTags(article.teasers.FRONTPAGE);
                article.teasers.DEFAULT = StripAllHtmlParagraphTags(article.teasers.DEFAULT);
            }
            return article;
        }

        /// <summary>
        /// Strips the input string for all html paragraph tags
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
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

            var pattern = "<ul>[\\s\\S]*?</ul>";

            return Regex.Replace(html, pattern, "");
        }
    }
}
