﻿using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using CustomerManager.DB;
using CustomerManager.Model;
using System.Collections.Generic;
using DevExpress.Xpf.Core;
using System.Windows;
namespace CustomerManager.ViewModels
{
    [POCOViewModel]
    public class EditDialogViewModel
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; } = string.Empty;
        public virtual string Sid { get; set; } = string.Empty;
        public Access<Customer> Cus { get; set; } = new Access<Customer>();
        public virtual int Status { get; set; } = 0;

        #region 保存按钮
        public void Save()
        {
            List<Customer> list =Cus.GetList("select * from customer where Sid = @Sid",new Customer { Sid = Sid.Trim().ToUpper() });
            if(list.Count > 0 && Status==0)
            {
                DXMessageBox.Show("更新记录失败\n因为数据库中存在"+list.Count+"条，与此次新增的记录身份证号相同","警告提示",MessageBoxButton.OK,MessageBoxImage.Error,MessageBoxResult.OK);
                return;
            }
            MessageBox.Show(Name + ":" + Sid+":"+Id);
            long num = -1;
            if(Status == 0)
            {
               // num = Cus.Insert(new Customer(){ Name = Name.Trim(), Sid = Sid.Trim().ToUpper(), Createtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") });
                num = DB.DB.Content.Insert<Customer>(new Customer(){ Name = Name.Trim(), Sid = Sid.Trim().ToUpper(), Createtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") });
                DXMessageBox.Show(num>0?"成功":"失败");
            }
            else if(Status == 1)
            {
                MessageBox.Show("update [customer] set Sid = " + Sid.Trim().ToUpper() + " , Name = " + Name.Trim() + " where id =" + Id);
             //   num = Cus.Update("update [customer] set Sid = "+Sid.Trim().ToUpper()+",Name = "+Name.Trim() +"  where id =" + Id );
                Customer customer = new Customer()
                {
                    Name = Name,
                    Sid = Sid

                };
                 num = DB.DB.Content.Update<Customer>(customer,item=>item.Id==Id);
                MessageBox.Show("这是正是执行的数" + num);
            }
          
            DXMessageBox.Show(num>0?"数据更新成功":"数据更新失败","重要提示",MessageBoxButton.OK,num>0?MessageBoxImage.Information:MessageBoxImage.Error,MessageBoxResult.OK);
            if (num > 0) Name = Sid = string.Empty;
        }
        public bool CanSave()
        {
            return (!string.IsNullOrEmpty(Name) )&& (!string.IsNullOrEmpty(Sid));
        }
        #endregion

        #region 重置按钮
        public void Reset()
        {
            Name = Sid = string.Empty;
        }
        public bool CanReset()
        {
            if(string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Sid))
            {
                return false;
            }
           return Name.Length > 0 || Sid.Length > 0;
        }
        #endregion
    }
}