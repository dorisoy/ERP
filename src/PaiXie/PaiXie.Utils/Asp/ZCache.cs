using System;
using System.Web;
using System.Web.Caching;
using System.Collections;
using System.Text.RegularExpressions;

namespace PaiXie.Utils
{
    /// <summary>
    /// <para>　</para>
    /// 　常用工具类——缓存类
    /// <para>　--------------------------------------------------------------------------------</para>
    /// <para>　SetCache：设置当前应用程序指定CacheKey的Cache值[ +2方法重载 ]</para>
    /// <para>　GetCache：获取当前应用程序指定CacheKey的Cache值</para>
    /// <para>　RemoveCache：删除缓存</para>
    /// </summary>
    public class ZCache
    {

        static Cache GetCacheObject()
        {
            HttpContext context = HttpContext.Current;
            if (context != null)
            {
                return context.Cache;
            }
            else
            {
                return HttpRuntime.Cache;
            }
        }

        #region 设置当前应用程序指定CacheKey的Cache值
        /// <summary>
        /// 设置当前应用程序指定CacheKey的Cache值
        /// </summary>
        /// <param name="CacheKey">键</param>
        /// <param name="objObject"></param>
        public static void SetCache(string CacheKey, object objObject)
        {
            System.Web.Caching.Cache objCache = GetCacheObject();
            objCache.Insert(CacheKey, objObject);
        }
        #endregion

        #region 获取当前应用程序指定CacheKey的Cache值
        /// <summary>
        /// 获取当前应用程序指定CacheKey的Cache值
        /// </summary>
        /// <param name="CacheKey"></param>
        /// <returns></returns>
        public static object GetCache(string CacheKey)
        {
            System.Web.Caching.Cache objCache = GetCacheObject();
            return objCache[CacheKey];
        }
        #endregion

        #region 设置当前应用程序指定CacheKey的Cache值[带过期时间等参数]
        /// <summary>
        /// 设置当前应用程序指定CacheKey的Cache值
        /// </summary>
        /// <param name="CacheKey"></param>
        /// <param name="objObject"></param>
        /// <param name="absoluteExpiration">可设置为Cache.NoAbsoluteExpiration</param>
        /// <param name="slidingExpiration">可设置为Cache.NoSlidingExpiration</param>
        public static void SetCache(string CacheKey, object objObject, DateTime absoluteExpiration, TimeSpan slidingExpiration)
        {
            System.Web.Caching.Cache objCache = GetCacheObject();
            objCache.Insert(CacheKey, objObject, null, absoluteExpiration, slidingExpiration);
        }
        #endregion

        #region 删除缓存
        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="CacheKey"></param>
        public static void RemoveCache(string CacheKey)
        {
            System.Web.Caching.Cache objCache = GetCacheObject();
            objCache.Remove(CacheKey);
        }
        #endregion

        /// <summary>
        /// 清空Cash对象
        /// </summary>
        public static void Clear()
        {
            Cache _cache = GetCacheObject();
            IDictionaryEnumerator CacheEnum = _cache.GetEnumerator();
            ArrayList al = new ArrayList();
            while (CacheEnum.MoveNext())
            {
                al.Add(CacheEnum.Key);
            }
            foreach (string key in al)
            {
                _cache.Remove(key);
            }
        }

        /// <summary>
        /// 根据正则表达式的模式移除Cache
        /// </summary>
        /// <param name="pattern">模式</param>
        public static void RemoveByPattern(string pattern)
        {
            Cache _cache = GetCacheObject();
            IDictionaryEnumerator CacheEnum = _cache.GetEnumerator();
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);
            while (CacheEnum.MoveNext())
            {
                if (regex.IsMatch(CacheEnum.Key.ToString()))
                    _cache.Remove(CacheEnum.Key.ToString());
            }
        }
    }
}

