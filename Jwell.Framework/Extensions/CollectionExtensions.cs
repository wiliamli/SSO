using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jwell.Framework.Extensions
{
    /// <summary>
    /// Extension methods for Collections.
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        /// Checks whatever given collection object is null or has no item.
        /// </summary>
        public static bool IsNullOrEmpty<T>(this ICollection<T> source)
        {
            return source == null || source.Count <= 0;
        }

        /// <summary>
        /// Adds an item to the collection if it's not already in the collection.
        /// </summary>
        /// <param name="source">Collection</param>
        /// <param name="item">Item to check and add</param>
        /// <typeparam name="T">Type of the items in the collection</typeparam>
        /// <returns>Returns True if added, returns False if not.</returns>
        public static bool AddIfNotContains<T>(this ICollection<T> source, T item)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (source.Contains(item))
            {
                return false;
            }

            source.Add(item);
            return true;
        }

        /// <summary>
        /// 集合添加
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="items"></param>
        public static void AddRange<T>(this ICollection<T> source, IEnumerable<T> items)
        {
            if (items == null)
            {
                return;
            }
            foreach (var item in items)
            {
                source.Add(item);
            }
        }

        /// <summary>
        /// 默认以","分隔，返回以","分隔串联的字符串
        /// </summary>
        /// <param name="values">字符串集合</param>
        /// <returns>以","分隔串联的字符串</returns>
        public static string JoinString(this IEnumerable<string> values)
        {
            return JoinString(values, ",");
        }

        /// <summary>
        /// 以给定字符分隔，返回串联的字符串
        /// </summary>
        /// <param name="values">字符串集合</param>
        /// <param name="split">分隔符</param>
        /// <returns></returns>
        public static string JoinString(this IEnumerable<string> values, string split)
        {
            var result = values.Aggregate(string.Empty, (current, value) => current + (split + value));
            result = result.TrimStart(split.ToCharArray());
            return result;
        }

        /// <summary>
        /// foreach
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IEnumerable<T> Each<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (source != null)
            {
                foreach (var item in source)
                {
                    action(item);
                }
            }
            return source;
        }
    }
}
