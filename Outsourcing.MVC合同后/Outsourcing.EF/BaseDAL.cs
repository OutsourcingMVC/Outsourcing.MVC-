 using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outsourcing.EF
{

    public class BaseDAL<T> where T : class
    {

        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public int Insert(T t)
        {
            var ct = new DB();
            try
            {
               
                int effect = -1;
                ct.Set<T>().Add(t);
                effect = ct.SaveChanges();
                return effect;

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                ct.Dispose();
            }

        }

        /// <summary>
        /// 修改一条数据
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public int Update(T t)
        {
            var ct = new DB();
            try
            {
                int effect = -1;
                ct.Entry(t).State = System.Data.Entity.EntityState.Modified;
                effect = ct.SaveChanges();
                return effect;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ct.Dispose();
            }


        }

        /// <summary>
        /// 直接从数据库删除一条数据
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public int Delete(T t)
        {
            var ct = new DB();
            try
            {
                int effect = -1;
                ct.Entry(t).State = System.Data.Entity.EntityState.Deleted;
                effect = ct.SaveChanges();
                return effect;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ct.Dispose();
            }


        }


        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public List<T> GetList()
        {
            var ct = new DB();
            try
            {
                string TName = typeof(T).Name;
                string sql = "select * from " + TName + " where IsDelete=0 order by CreateTime Desc";
                List<T> list = ct.Database.SqlQuery<T>(sql, null).ToList();
                return list;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                ct.Dispose();
            }

        }

        /// <summary>
        /// 根据条件获取列表
        /// </summary>
        /// <param name="Str">条件（直接写Sql语句）</param>
        /// <returns></returns>
        public List<T> GetListByStr(string Str)
        {
            var ct = new DB();
            try
            {
                string TName = typeof(T).Name;
                string sql = "select * from " + TName + " where IsDelete=0 " + Str + " order by CreateTime Desc";
                List<T> list = ct.Database.SqlQuery<T>(sql, null).ToList();
                return list;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                ct.Dispose();
            }


        }



        /// <summary>
        /// 无条件普通分页（按插入时间倒序）
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="totalRecord">总记录数</param>
        /// <returns></returns>
        public List<T> GetPageList(int pageIndex, int pageSize, out int totalRecord)
        {
            var ct = new DB();
            try
            {
                string TName = typeof(T).Name;

                List<T> list = null;
                string sql = "select * from " + TName + " where IsDelete=0  order by CreateTime Desc";
                totalRecord = ct.Database.SqlQuery<T>(sql, null).Count();

                if (totalRecord <= 0)
                    return new List<T>();

                int SkipPage = (pageIndex - 1) * pageSize;
                int TakePage = pageSize;
                sql += " OFFSET " + SkipPage + " ROW FETCH NEXT " + TakePage + " ROWS ONLY";

                list = ct.Database.SqlQuery<T>(sql, null).ToList();

                return list;

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                ct.Dispose();
            }

        }

        /// <summary>
        /// 条件分页（按插入时间倒序）
        /// </summary>
        /// <param name="str">查询条件</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="totalRecord">总记录数</param>
        /// <returns></returns>
        public List<T> GetPageListByStr(string str, SqlParameter[] para, int pageIndex, int pageSize, out int totalRecord)
        {
            var ct = new DB();
            try
            {
                string TName = typeof(T).Name;

                List<T> list = null;
                string sql = "select * from " + TName + " where IsDelete=0  " + str + " order by CreateTime Desc";
                totalRecord = ct.Database.SqlQuery<T>(sql, para).Count();

                if (totalRecord <= 0)
                    return new List<T>();

                int SkipPage = (pageIndex - 1) * pageSize;
                int TakePage = pageSize;
                sql += " OFFSET " + SkipPage + " ROW FETCH NEXT " + TakePage + " ROWS ONLY";
                list = ct.Database.SqlQuery<T>(sql, para).ToList();

                return list;

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                ct.Dispose();
            }

        }

    }
}

