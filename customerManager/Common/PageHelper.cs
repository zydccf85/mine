/*
 * Created by SharpDevelop.
 * User: Lenovo
 * Date: 2019/7/11
 * Time: 13:13
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Linq;
namespace CustomerManager.Common
{
	/// <summary>
	/// Description of PageHelper.
	/// </summary>
	public class PageHelper<T>
	{
		public PageHelper()
		{
		}
		public int PageNo { get; set; }
		public int PageSize { get; set; }
		public int counts{get;set;}
		public int Pages { get; set; }
		public int BeginIndex{get;set;}
		public int EndIndex{get;set;}
		public List<T> original = new List<T>();
		public List<T> newList = new List<T>();
		public bool IsFirstPage(){
			return this.PageNo == 1;
		}
		public bool IsLastPage()
		{
			return this.PageNo == this.Pages;
		}

		public PageHelper(List<T> li,int pageSize,int pageNo)
		{
			if(li.Count>0){
				this.original = li;
			this.PageSize = pageSize;
			this.PageNo = pageNo;
			this.Count();
//			this.AddRange(li.GetRange(this.BeginIndex,this.EndIndex-this.BeginIndex+1));
			newList.AddRange(li.GetRange(this.BeginIndex,this.EndIndex-this.BeginIndex+1));
                if (BeginIndex > EndIndex)
                {
                    newList.AddRange(li);
                }
			}
			
			
		}
		public void Count()
		{
			this.counts = this.original.Count;
			this.Pages =(int)Math.Ceiling((double)this.original.Count/this.PageSize);
			this.BeginIndex =(this.PageNo-1)*this.PageSize;
			this.EndIndex = this.PageNo*this.PageSize-1;
			if(this.IsLastPage()){
                //int temp = this.counts%this.PageSize;
                //this.EndIndex = (this.PageNo-1)*this.PageSize+temp-1;
                EndIndex = this.counts - 1;
			}
		}
		public void NextPage()
		{
			this.PageNo++;
			this.Count();
			this.newList.Clear();
			newList.AddRange(this.original.GetRange(this.BeginIndex,this.EndIndex-this.BeginIndex+1));
		}
		public void BeforePage()
		{
			this.PageNo--;
			this.Count();
			this.newList.Clear();
			newList.AddRange(this.original.GetRange(this.BeginIndex,this.EndIndex-this.BeginIndex+1));
		}
		public void GetFristPage()
		{
			this.PageNo =1;
			this.Count();
			this.newList.Clear();
			newList.AddRange(this.original.GetRange(this.BeginIndex,this.EndIndex-this.BeginIndex+1));
		}
		public void GetLastPage()
		{
			this.PageNo =this.Pages;
			this.Count();
			this.newList.Clear();
			newList.AddRange(this.original.GetRange(this.BeginIndex,this.EndIndex-this.BeginIndex+1));
		}
		public void SetPage(int newPage)
		{
			this.PageNo =newPage;
			if(newPage>this.Pages) this.PageNo=this.Pages;
			if(newPage<1) this.PageNo =1;
			this.Count();
			this.newList.Clear();
			newList.AddRange(this.original.GetRange(this.BeginIndex,this.EndIndex-this.BeginIndex+1));
		}
	}
}
