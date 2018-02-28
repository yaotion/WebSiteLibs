using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.YA.Sync
{
  public  class ImportDeptDataCls
    {
          ImportDeptDAL  deptdal=new ImportDeptDAL();
          public int DoArea(string url)
          {

              string msg = "";
              if (string.IsNullOrEmpty(url))
                  url = "http://192.168.1.201:20011/AshxService/QueryProcess.ashx?DataType=TF.YA.Org.GetAllDepts";
              else
                  url = url + "/AshxService/QueryProcess.ashx?DataType=TF.YA.Org.GetAllDepts";
              RespData bret = HttpCommon.Get(url, out msg);
              DeptClsList deptlist = bret.Data;
             
              List<DeptCls> local = deptdal.GetAreaList();
              List<DeptCls> remtlst = deptlist.Depts;
              CompareUtils<DeptCls> cmp = new CompareUtils<DeptCls>();

              if (cmp.Compare(local, remtlst))
              {
                  return 0;
              }
              int count = 0;
              //添加 
              for (int i = 0; i < cmp.NewList.Count; i++)
              {
                  count = count + deptdal.AddDeptBatch(cmp.NewList[i]);
              }
              //修改 
              for (int i = 0; i < cmp.UpdateList.Count; i++)
              {
                  count = count + deptdal.UpdateArea(cmp.UpdateList[i]);
              }

              //删除
              for (int i = 0; i < cmp.RemoveList.Count; i++)
              {
                  count = count + deptdal.deleteArea(cmp.RemoveList[i]);

              }

              return count;

              
          }
          public int DoWorkShop(string url)
          {

              string msg = "";
              if (string.IsNullOrEmpty(url))
                  url = "http://192.168.1.166:20011/AshxService/QueryProcess.ashx?DataType=TF.YA.Org.GetAllDepts";
              else
                  url = url + "/AshxService/QueryProcess.ashx?DataType=TF.YA.Org.GetAllDepts";
              RespData bret = HttpCommon.Get(url, out msg);
              DeptClsList deptlist = bret.Data;

              List<DeptCls> local = deptdal.GetWorkList();
              List<DeptCls> remtlst = deptlist.Depts;
              CompareUtils<DeptCls> cmp = new CompareUtils<DeptCls>();

              if (cmp.Compare(local, remtlst))
              {
                  return 0;
              }
              int count = 0;
              //添加 
              for (int i = 0; i < cmp.NewList.Count; i++)
              {
                  count = count + deptdal.AddWK(cmp.NewList[i]);
              }
              //修改 
              for (int i = 0; i < cmp.UpdateList.Count; i++)
              {
                  count = count + deptdal.UpdateWK(cmp.UpdateList[i]);
              }

              //删除
              for (int i = 0; i < cmp.RemoveList.Count; i++)
              {
                  count = count + deptdal.deleteWork(cmp.RemoveList[i]);

              }

              return count;


          }
          public int DoGroup(string url)
          {

              string msg = "";
              if (string.IsNullOrEmpty(url))
                  url = "http://192.168.1.166:20011/AshxService/QueryProcess.ashx?DataType=TF.YA.Org.GetAllDepts";
              else
                  url = url + "/AshxService/QueryProcess.ashx?DataType=TF.YA.Org.GetAllDepts";
              RespData bret = HttpCommon.Get(url, out msg);
              DeptClsList deptlist = bret.Data;

              List<DeptCls> local = deptdal.GetGuideGroupList();
              List<DeptCls> remtlst = deptlist.Depts;
              CompareUtils<DeptCls> cmp = new CompareUtils<DeptCls>();

              if (cmp.Compare(local, remtlst))
              {
                  return 0;
              }
              int count = 0;
              //添加 
              for (int i = 0; i < cmp.NewList.Count; i++)
              {
                  count = count + deptdal.AddGuideGroup(cmp.NewList[i]);
              }
              //修改 
              for (int i = 0; i < cmp.UpdateList.Count; i++)
              {
                  count = count + deptdal.UpdateGuideGroup(cmp.UpdateList[i]);
              }

              //删除
              for (int i = 0; i < cmp.RemoveList.Count; i++)
              {
                  count = count + deptdal.deleteGroupBig(cmp.RemoveList[i]);

              }

              return count;


          }
          public int DoSmallGroup(string url)
          {

              string msg = "";
              if (string.IsNullOrEmpty(url))
                  url = "http://192.168.1.166:20011/AshxService/QueryProcess.ashx?DataType=TF.YA.Org.GetAllDepts";
              else
                  url = url + "/AshxService/QueryProcess.ashx?DataType=TF.YA.Org.GetAllDepts";
              RespData bret = HttpCommon.Get(url, out msg);
              DeptClsList deptlist = bret.Data;

              List<DeptCls> local = deptdal.GetSmallGuideGroupList();
              List<DeptCls> remtlst = deptlist.Depts;
              CompareUtils<DeptCls> cmp = new CompareUtils<DeptCls>();

              if (cmp.Compare(local, remtlst))
              {
                  return 0;
              }
              int count = 0;
              //添加 
              for (int i = 0; i < cmp.NewList.Count; i++)
              {
                  count = count + deptdal.AddSmallGroup(cmp.NewList[i]);
              }
              //修改 
              for (int i = 0; i < cmp.UpdateList.Count; i++)
              {
                  count = count + deptdal.UpdateSmallGroup(cmp.UpdateList[i]);
              }

              //删除
              for (int i = 0; i < cmp.RemoveList.Count; i++)
              {
                  count = count + deptdal.deleteGroupSmall(cmp.RemoveList[i]);

              }

              return count;


          }
          public List<Station> exportStations(string url)
          {

              string msg = "";
              if (string.IsNullOrEmpty(url))
                  url = "http://192.168.1.166:20011/AshxService/QueryProcess.ashx?DataType=TF.YA.Base.GetAllStations&Data={'PageIndex':'1','PageCount':'20000'}";
              else
                  url = url + "/AshxService/QueryProcess.ashx?DataType=TF.YA.Base.GetAllStations&Data={'PageIndex':'1','PageCount':'20000'}";
              String bret = HttpCommon.GetStr(url, out msg);
              resultStation stationlist = Newtonsoft.Json.JsonConvert.DeserializeObject<resultStation>(bret);

              return stationlist.Data.stations;
          }
      public int DoStation(string url)
      {
          List<Station> LstRemt = exportStations(url);
          List<Station> local = GetStationAll();
          CompareUtils<Station> cmp = new CompareUtils<Station>();
          if (cmp.Compare( local,LstRemt))
          {
              return 0;
          }
          int count = 0;
          //添加 
          for (int i = 0; i < cmp.NewList.Count;i++ )
          { 
              count =count+ deptdal.AddStat(cmp.NewList[i]);
          }
          //修改 
          for (int i = 0; i < cmp.UpdateList.Count; i++)
          {
              count = count + deptdal.UpdateStat(cmp.UpdateList[i]);
          }

          //删除
          for (int i = 0; i < cmp.RemoveList.Count; i++)
          {
              count = count + deptdal.DeleteStat(cmp.RemoveList[i]);

          }

          return count;
      }
      public List<Station> GetStationAll()
      {

          return deptdal.GetStationAll();
      }
      public int DoPlace(string url)
      {
          List<Place> LstRemt = exportPlaces(url);
          List<Place> local = GetPlaceAll();
          CompareUtils<Place> cmp = new CompareUtils<Place>();
          if (cmp.Compare(local, LstRemt))
          {
              return 0;
          }
          int count = 0;
          //添加 
          for (int i = 0; i < cmp.NewList.Count; i++)
          {
              count =count+ deptdal.AddPlace(cmp.NewList[i]);
          }
          //修改 
          for (int i = 0; i < cmp.UpdateList.Count; i++)
          {
              count = count + deptdal.UpdatePlace(cmp.UpdateList[i]);

          }

          //删除 
          for (int i = 0; i < cmp.RemoveList.Count; i++)
          {
              count = count + deptdal.DeletePlace(cmp.RemoveList[i]);
          }

          return count;
      }
      public List<Place> exportPlaces(string url)
      {

          string msg = "";
          if (string.IsNullOrEmpty(url))
          url = "http://192.168.1.166:20011/AshxService/QueryProcess.ashx?DataType=TF.YA.Base.GetAllPlaces&Data={}";
          else

              url = url + "/AshxService/QueryProcess.ashx?DataType=TF.YA.Base.GetAllPlaces&Data={}";
          String bret = HttpCommon.GetStr(url, out msg);
          PlaceAll plist = Newtonsoft.Json.JsonConvert.DeserializeObject<PlaceAll>(bret);

          return plist.Data.places;
      }
      public List<Place> GetPlaceAll()
      {
          return deptdal.GetPlaceAll();
      }
      public int DoSection(string url)
      {
          List<Section> LstRemt = exportSection(url);
          List<Section> local = GetSectionAll();
          CompareUtils<Section> cmp = new CompareUtils<Section>();
          if (cmp.Compare(local, LstRemt))
          {
              return 0;
          }
          int count = 0;
          //添加 
          for (int i = 0; i < cmp.NewList.Count; i++)
          {
              count = count+deptdal.AddSections(cmp.NewList[i]);
          }
          //修改 
          for (int i = 0; i < cmp.UpdateList.Count; i++)
          {
              count = count + deptdal.UpdateSections(cmp.UpdateList[i]);

          }

          //删除 
          for (int i = 0; i < cmp.RemoveList.Count; i++)
          {
              count = count + deptdal.DeleteSections(cmp.RemoveList[i]);
          }

          return count;
      }
      public List<Section> exportSection(string url)
      {

          string msg = "";
          if (string.IsNullOrEmpty(url))
              url = "http://192.168.1.166:20011/AshxService/QueryProcess.ashx?DataType=TF.TF.LCBaseIF.GetAllICSections&Data={}";
          else
              url = url + "/AshxService/QueryProcess.ashx?DataType=TF.TF.LCBaseIF.GetAllICSections&Data={}";
          String bret = HttpCommon.GetStr(url, out msg);
          SectionAll plist = Newtonsoft.Json.JsonConvert.DeserializeObject<SectionAll>(bret);
          return plist.Data.SectionList;
      }
      public List<Section> GetSectionAll()
      {
          return deptdal.GetSectionAll();
      }
    }
}
