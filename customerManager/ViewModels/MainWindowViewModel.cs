using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using System.Data;
using System.Data.OleDb;
using CustomerManager.Model;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CustomerManager.DB;
using CustomerManager.Common;
using Dapper;
using DevExpress.Xpf.Core;
using System.Windows;
namespace CustomerManager.ViewModels
{
    [POCOViewModel]
    public class MainWindowViewModel
    {
        public virtual ObservableCollection<Customer> AllData { get; set; } = new ObservableCollection<Customer>();
        public virtual string Name { get; set; } = string.Empty;
        public virtual string Sid { get; set; } = string.Empty;
        public PageHelper<Customer> Helper { get; set; }
        public virtual string PageInfo { get; set; }
        public virtual string PageRecord { get; set; }
        public virtual int PageSize { get; set; } = 30;
        public Access<Customer> Cus{ get; set; } = new Access<Customer>();
        public void Query()
        {
            InitData();
        }
        public bool CanQuery()
        {
            return true;
        }
        public MainWindowViewModel()
        {
            InitData();
          
        }
        public void Reset()
        {
            Name = Sid = string.Empty;
        }
        public bool CanReset()
        {
            return Name.Length > 0 || Sid.Length > 0;
           
        }
        public void First()
        {
            Helper.GetFristPage();
            Fresh();

        }
        public bool CanFirst()
        {
            return Helper.PageNo >1;
        }
        public void Prev()
        {
            Helper.BeforePage();
            Fresh();
        }
        public bool CanPrev()
        {
            return Helper.PageNo > 1;
        }
        public void Next()
        {
            Helper.NextPage();
            Fresh();
        }
        public bool CanNext()
        {
            return Helper.PageNo < Helper.Pages;
        }
        public void Last()
        {
            Helper.GetLastPage();
            Fresh();
        }
        public bool CanLast()
        {
            return Helper.PageNo < Helper.Pages; 
        }
        //#region 修改按钮
        //public void Edit(Customer cus)
        //{
        //    EditDialog de = new EditDialog();
        //    EditDialogViewModel devm = de.DataContext as EditDialogViewModel;
        //    devm.Id = cus.Id;
        //    devm.Name = cus.Name;
        //    devm.Sid = cus.Sid;
        //    de.ShowDialog();
            
               
        //}
        //public bool CanEdit(Customer cus)
        //{
        //    return true;
        //}
        //#endregion

        #region 增加按钮
        public void Add()
        {
            EditDialog de = new EditDialog();
           
            de.Closed += (x, y) =>
            {
                InitData();
            };
            EditDialogViewModel edvm = de.DataContext as EditDialogViewModel;
            int maxId = DB.DB.Content.FromSql("select max(id) from customer").First<int>();
            edvm.Id = maxId+1;
            de.ShowDialog();
           
        }
        public bool CanAdd()
        {
            return true;
        }
        #endregion

        #region 删除按钮
        public void Delete(Customer sid)
        {

            MessageBoxResult result =DXMessageBox.Show("是否确定删除？\n"+sid.ToString(), "重要提示", System.Windows.MessageBoxButton.OKCancel,
                System.Windows.MessageBoxImage.Warning, System.Windows.MessageBoxResult.Cancel);
            if (result == MessageBoxResult.Cancel) return;
            //   bool flag = Cus.Delete(sid);
            bool flag = DB.DB.Content.Delete<Customer>(sid) > 0;
            int oldPage = Helper.PageNo;
            InitData();
            if (oldPage > Helper.Pages)
            {
                Helper.SetPage(Helper.Pages);
            }
            else
            {
                Helper.SetPage(oldPage);
            }
            Fresh();
        }
        public bool CanDelete(Customer sid)
        {
            return true;
        }
        #endregion

        #region 修改按钮
        public void Edit(Customer customer)
        {
            EditDialog de = new EditDialog();
            EditDialogViewModel edvm = de.DataContext as EditDialogViewModel;
            edvm.Id = customer.Id;
            edvm.Name = customer.Name;
            edvm.Sid = customer.Sid;
            edvm.Status = 1;
            de.ShowDialog();
        }
        public bool CanEdit(Customer customer)
        {
            return true;
        }
        #endregion
        private void InitData()
        {
          
              
                string sqlStr = string.Format(@"Select  id,name,sid,IIF(Left(Right(sid,2),1) Mod 2 =0,'女','男') as gender,
                Val(DateDiff('yyyy',Mid(sid,7,4) & '-' & Mid(sid,11,2),Format(now(),'yyyy-mm'))) as age,createtime from customer 
                where name like @Name and sid like @Sid order by id ");
            // List<Customer> list = Cus.GetList(sqlStr, new { Name = string.Concat("%", this.Name ?? string.Empty, "%"), Sid = string.Concat("%", this.Sid ?? string.Empty, "%") });
            List<Customer> list = DB.DB.Content.From<Customer>()
                .Where(item=>item.Name.Contains(Name??string.Empty)&&item.Sid.Contains(Sid??string.Empty) ).OrderBy(Customer._.Id.Desc)
                .ToList();

            Helper = new PageHelper<Customer>(list,PageSize,1);
            PageRecord = string.Format("第 {0}页，共 {1}页",Helper.PageNo,Helper.Pages);
            PageInfo = string.Format("显示 {0}至 {1}条记录，共 {2}条记录",Helper.BeginIndex+1,Helper.EndIndex+1,Helper.counts);
            AllData.Clear();
            Helper.newList.ForEach(item => AllData.Add(item));
        } 
        private void Fresh()
        {
            PageRecord = string.Format("第 {0}页，共 {1}页", Helper.PageNo, Helper.Pages);
            PageInfo = string.Format("显示 {0}至 {1}条记录，共 {2}条记录", Helper.BeginIndex + 1, Helper.EndIndex + 1, Helper.counts);
            AllData.Clear();
            Helper.newList.ForEach(item => AllData.Add(item));
        }
    }
}