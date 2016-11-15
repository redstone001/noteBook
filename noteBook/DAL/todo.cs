/**  版本信息模板在安装目录下，可自行修改。
* todo.cs
*
* 功 能： N/A
* 类 名： todo
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2016/11/11 1:06:19   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
using System.Text;
using System.Data.OleDb;
using Maticsoft.DBUtility;//Please add references
namespace db.DAL
{
	/// <summary>
	/// 数据访问类:todo
	/// </summary>
	public partial class todo
	{
		public todo()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperOleDb.GetMaxID("ID", "todo"); 
		}


		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from todo");
			strSql.Append(" where ID="+ID+" ");
			return DbHelperOleDb.Exists(strSql.ToString());
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(db.Model.todo model)
		{
			StringBuilder strSql=new StringBuilder();
			StringBuilder strSql1=new StringBuilder();
			StringBuilder strSql2=new StringBuilder();
			if (model.dateValue != null)
			{
				strSql1.Append("dateValue,");
				strSql2.Append("'"+model.dateValue+"',");
			}
			if (model.deadDate != null)
			{
				strSql1.Append("deadDate,");
				strSql2.Append("'"+model.deadDate+"',");
			}
			if (model.content != null)
			{
				strSql1.Append("content,");
				strSql2.Append("'"+model.content+"',");
			}
			if (model.importantStar != null)
			{
				strSql1.Append("importantStar,");
				strSql2.Append(""+model.importantStar+",");
			}
			if (model.urgencyStar != null)
			{
				strSql1.Append("urgencyStar,");
				strSql2.Append(""+model.urgencyStar+",");
			}
			strSql.Append("insert into todo(");
			strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
			strSql.Append(")");
			strSql.Append(" values (");
			strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
			strSql.Append(")");
			int rows=DbHelperOleDb.ExecuteSql(strSql.ToString());
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(db.Model.todo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update todo set ");
			if (model.dateValue != null)
			{
				strSql.Append("dateValue='"+model.dateValue+"',");
			}
			else
			{
				strSql.Append("dateValue= null ,");
			}
			if (model.deadDate != null)
			{
				strSql.Append("deadDate='"+model.deadDate+"',");
			}
			else
			{
				strSql.Append("deadDate= null ,");
			}
			if (model.content != null)
			{
				strSql.Append("content='"+model.content+"',");
			}
			else
			{
				strSql.Append("content= null ,");
			}
			if (model.importantStar != null)
			{
				strSql.Append("importantStar="+model.importantStar+",");
			}
			else
			{
				strSql.Append("importantStar= null ,");
			}
			if (model.urgencyStar != null)
			{
				strSql.Append("urgencyStar="+model.urgencyStar+",");
			}
			else
			{
				strSql.Append("urgencyStar= null ,");
			}
			int n = strSql.ToString().LastIndexOf(",");
			strSql.Remove(n, 1);
			strSql.Append(" where ID="+ model.ID+"");
			int rowsAffected=DbHelperOleDb.ExecuteSql(strSql.ToString());
			if (rowsAffected > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from todo ");
			strSql.Append(" where ID="+ID+"" );
			int rowsAffected=DbHelperOleDb.ExecuteSql(strSql.ToString());
			if (rowsAffected > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}		/// <summary>
		/// 批量删除数据
		/// </summary>
		public bool DeleteList(string IDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from todo ");
			strSql.Append(" where ID in ("+IDlist + ")  ");
			int rows=DbHelperOleDb.ExecuteSql(strSql.ToString());
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public db.Model.todo GetModel(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  ");
			strSql.Append(" ID,dateValue,deadDate,content,importantStar,urgencyStar ");
			strSql.Append(" from todo ");
			strSql.Append(" where ID="+ID+"" );
			db.Model.todo model=new db.Model.todo();
			DataSet ds=DbHelperOleDb.Query(strSql.ToString());
			if(ds.Tables[0].Rows.Count>0)
			{
				return DataRowToModel(ds.Tables[0].Rows[0]);
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public db.Model.todo DataRowToModel(DataRow row)
		{
			db.Model.todo model=new db.Model.todo();
			if (row != null)
			{
				if(row["ID"]!=null && row["ID"].ToString()!="")
				{
					model.ID=int.Parse(row["ID"].ToString());
				}
				if(row["dateValue"]!=null && row["dateValue"].ToString()!="")
				{
					model.dateValue=DateTime.Parse(row["dateValue"].ToString());
				}
				if(row["deadDate"]!=null && row["deadDate"].ToString()!="")
				{
					model.deadDate=DateTime.Parse(row["deadDate"].ToString());
				}
				if(row["content"]!=null)
				{
					model.content=row["content"].ToString();
				}
				if(row["importantStar"]!=null && row["importantStar"].ToString()!="")
				{
					model.importantStar=int.Parse(row["importantStar"].ToString());
				}
				if(row["urgencyStar"]!=null && row["urgencyStar"].ToString()!="")
				{
					model.urgencyStar=int.Parse(row["urgencyStar"].ToString());
				}
			}
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID,dateValue,deadDate,content,importantStar,urgencyStar ");
			strSql.Append(" FROM todo ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperOleDb.Query(strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM todo ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			object obj = DbHelperOleDb.GetSingle(strSql.ToString());
			if (obj == null)
			{
				return 0;
			}
			else
			{
				return Convert.ToInt32(obj);
			}
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T.ID desc");
			}
			strSql.Append(")AS Row, T.*  from todo T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperOleDb.Query(strSql.ToString());
		}

		/*
		*/

		#endregion  Method
		#region  MethodEx

		#endregion  MethodEx
	}
}

