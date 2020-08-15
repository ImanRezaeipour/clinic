using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Clinic.Core.Extensions
{
    /// <summary>
    /// </summary>
    public static class StringExtension
    {
        private static readonly Regex MatchAllTags =
            new Regex(@"<(.|\n)*?>", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        public static int WordsCount(this string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return 0;
            }

            text = CleanTags(text).Trim();
            text = text.Replace("\t", " ");
            text = text.Replace("\n", " ");
            text = text.Replace("\r", " ");

            var words = text.Split(
                new[] {' ', ',', ';', '.', '!', '"', '(', ')', '?', ':', '\'', '«', '»', '+', '-'},
                StringSplitOptions.RemoveEmptyEntries);
            return words.Length;
        }

        private static string CleanTags(this string data)
        {
            return data.Replace("\n", "\n ").RemoveHtmlTags();
        }

        private static string RemoveHtmlTags(this string text)
        {
            return string.IsNullOrEmpty(text) ? string.Empty : MatchAllTags.Replace(text, " ").Replace("&nbsp;", " ");
        }


        /// <summary>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool HasValue(this string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }

        /// <summary>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNotEmpty(this string value)
        {
            return !string.IsNullOrEmpty(value);
        }

        /// <summary>
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public static string DefaultIfNull(this string word)
        {
            return word ?? "";
        }

        /// <summary>
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public static string GetExtension(this string word)
        {
            return Path.GetExtension(word);
        }

        /// <summary>
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public static string PlusSlash(this string word)
        {
            return word + "/";
        }

        /// <summary>
        /// </summary>
        /// <param name="word">     </param>
        /// <param name="afterWord"></param>
        /// <returns></returns>
        public static string PlusWord(this string word, string afterWord)
        {
            return word + afterWord;
        }

        /// <summary>
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public static string RemoveTilde(this string word)
        {
            return word.Replace("~", "");
        }

        /// <summary>
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public static string ZeroIfNull(this string word)
        {
            return word ?? "0";
        }



        public static string Repeat(this string str, int count)
        {
            var result = string.Empty;
            for (int i = 0; i < count; i++)
            {
                result += str;
            }
            return result;
        }

        public static string AddSlug(this string url, string slug)
        {
            if (slug == null)
                return url;
            return url + "/" + slug.Replace(" ", "-");
        }

        public static string BeforeWord(this string str, string word)
        {
            if (word == null)
                return str;
            return word + str;
        }

        public static string ReturnIntegerCodeAsync(this string code)
        {
            string variable = string.Empty;
            int value = 0;
            for (int i = 0; i < code.Length; i++)
            {
                if (Char.IsDigit(code[i]))
                    variable += code[i];
            }
            if (variable.Length > 0)
                value = int.Parse(variable);
            return (value + 1).ToString();
        }


        public static string ExtractNumeric(this string code)
        {
            string variable = string.Empty;
            int value = 0;
            for (int i = 0; i < code.Length; i++)
            {
                if (Char.IsDigit(code[i]))
                    variable += code[i];
            }
            if (variable.Length > 0)
                value = int.Parse(variable);
            return (value + 1).ToString();
        }

        public static bool IsValidSortDirection(this string sortDirection)
        {
            if (string.IsNullOrEmpty(sortDirection))
                return false;

            if (sortDirection != "Desc" || sortDirection != "Asc")
                return false;

            return true;
        }

        public static bool IsIn<TSource>(this TSource source, List<TSource> list)
        {
            return list.Contains(source);
        }

        public static List<string> GetStaticMembers<TSource>(TSource source)
        {
            var list = new List<string>();
            if (typeof(TSource).IsClass)
            {
                foreach (var Member in typeof(TSource).GetFields(BindingFlags.Static))
                {
                    list.Add(Member.Name);
                }
            }
            return list;
        }

    

        #region GetUserManagerErros

        /// <summary>
        /// </summary>
        /// <param name="errors"></param>
        /// <returns></returns>
        public static string GetUserManagerErros(this IEnumerable<string> errors)
        {
            return errors.Aggregate(string.Empty, (current, error) => current + $"{error} \n");
        }


        public static string GetNameViewModel(this string name)
        {
            return name.Substring(name.IndexOf('<') + 1, name.IndexOf('>') - 1);
        }
        #endregion
    }
}