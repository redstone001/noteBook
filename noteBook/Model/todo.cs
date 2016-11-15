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
namespace db.Model
{
	/// <summary>
	/// todo:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class todo
	{
		public todo()
		{}
		#region Model
		private int _id;
		private DateTime? _datevalue;
		private DateTime? _deaddate;
		private string _content;
		private int? _importantstar;
		private int? _urgencystar;
		/// <summary>
		/// 
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 日期
		/// </summary>
		public DateTime? dateValue
		{
			set{ _datevalue=value;}
			get{return _datevalue;}
		}
		/// <summary>
		/// 截止日期
		/// </summary>
		public DateTime? deadDate
		{
			set{ _deaddate=value;}
			get{return _deaddate;}
		}
		/// <summary>
		/// 内容
		/// </summary>
		public string content
		{
			set{ _content=value;}
			get{return _content;}
		}
		/// <summary>
		/// 重要程度
		/// </summary>
		public int? importantStar
		{
			set{ _importantstar=value;}
			get{return _importantstar;}
		}
		/// <summary>
		/// 紧急程度
		/// </summary>
		public int? urgencyStar
		{
			set{ _urgencystar=value;}
			get{return _urgencystar;}
		}
		#endregion Model

	}
}

