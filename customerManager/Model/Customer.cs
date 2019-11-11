using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;
using Dos.ORM;
namespace CustomerManager.Model
{
    /// <summary>
    /// 实体类Customer 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class Customer : Entity
    {
        public Customer() : base("customer") { }

        #region Model
        private int _Id;
        private string _Name;
        private string _Sid;
        private string _Createtime;
        /// <summary>
        /// 
        /// </summary>
        public int Id
        {
            get { return _Id; }
            set
            {
                this.OnPropertyValueChange(_.Id, _Id, value);
                this._Id = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set
            {
                this.OnPropertyValueChange(_.Name, _Name, value);
                this._Name = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Sid
        {
            get { return _Sid; }
            set
            {
                this.OnPropertyValueChange(_.Sid, _Sid, value);
                this._Sid = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Createtime
        {
            get { return _Createtime; }
            set
            {
                this.OnPropertyValueChange(_.Createtime, _Createtime, value);
                this._Createtime = value;
            }
        }
        #endregion

        #region Method
        /// <summary>
        /// 获取实体中的主键列
        /// </summary>
        public override Field[] GetPrimaryKeyFields()
        {
            return new Field[] {
                _.Id};
        }
        /// <summary>
        /// 获取列信息
        /// </summary>
        public override Field[] GetFields()
        {
            return new Field[] {
                _.Id,
                _.Name,
                _.Sid,
                _.Createtime};
        }
        /// <summary>
        /// 获取值信息
        /// </summary>
        public override object[] GetValues()
        {
            return new object[] {
                this._Id,
                this._Name,
                this._Sid,
                this._Createtime};
        }
        /// <summary>
        /// 给当前实体赋值
        /// </summary>
        public override void SetPropertyValues(IDataReader reader)
        {
            this._Id = DataUtils.ConvertValue<int>(reader["id"]);
            this._Name = DataUtils.ConvertValue<string>(reader["name"]);
            this._Sid = DataUtils.ConvertValue<string>(reader["sid"]);
            this._Createtime = DataUtils.ConvertValue<string>(reader["createtime"]);
        }
        /// <summary>
        /// 给当前实体赋值
        /// </summary>
        public override void SetPropertyValues(DataRow row)
        {
            this._Id = DataUtils.ConvertValue<int>(row["id"]);
            this._Name = DataUtils.ConvertValue<string>(row["name"]);
            this._Sid = DataUtils.ConvertValue<string>(row["sid"]);
            this._Createtime = DataUtils.ConvertValue<string>(row["createtime"]);
        }
        #endregion

        #region _Field
        /// <summary>
        /// 字段信息
        /// </summary>
        public class _
        {
            /// <summary>
            /// * 
            /// </summary>
            public readonly static Field All = new Field("*", "customer");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field Id = new Field("id", "customer", "id");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field Name = new Field("name", "customer", "name");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field Sid = new Field("sid", "customer", "sid");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field Createtime = new Field("createtime", "customer", "createtime");
        }
        #endregion


    }

}