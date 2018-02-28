using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using TF.CommonUtility;
using TF.DB.DBUtility; 
using System.Data.SqlTypes;
using System.Data.SqlClient;

namespace TF.YA.Sync
{
    public class ImportDeptDAL
    {
        public List<DeptCls>  GetAreaList()
        {

            List<DeptCls> LstStation = new List<DeptCls>();
            string sql = "select *  from TAB_Org_Area";
            var dt = SqlHelper.ExecuteDataset(PublicVar.Connstr, System.Data.CommandType.Text, sql).Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DeptCls ObjStation = new DeptCls();
                ObjStation.DeptId = dt.Rows[i]["strGUID"].ToString();
                ObjStation.DeptName = dt.Rows[i]["strAreaName"].ToString();
                ObjStation.ParentDeptID = "";// dt.Rows[i]["strJWDNumber"].ToString();
                LstStation.Add(ObjStation);
            }
            return LstStation;

        }
        public List<DeptCls> GetWorkList()
        {

            List<DeptCls> LstStation = new List<DeptCls>();
            string sql = "select strWorkShopGUID,strAreaGUID,strWorkShopName  from TAB_Org_WorkShop";
            var dt = SqlHelper.ExecuteDataset(PublicVar.Connstr, System.Data.CommandType.Text, sql).Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DeptCls ObjStation = new DeptCls();
                ObjStation.DeptId = dt.Rows[i]["strWorkShopGUID"].ToString();
                ObjStation.DeptName = dt.Rows[i]["strWorkShopName"].ToString();
                ObjStation.ParentDeptID = dt.Rows[i]["strAreaGUID"].ToString();
                LstStation.Add(ObjStation);
            }
            return LstStation;

        }
        public List<DeptCls> GetGuideGroupList()
        {

            List<DeptCls> LstStation = new List<DeptCls>();
            string sql = "select strGuideGroupGUID,strWorkShopGUID,strGuideGroupName  from TAB_Org_GuideGroup";
            var dt = SqlHelper.ExecuteDataset(PublicVar.Connstr, System.Data.CommandType.Text, sql).Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DeptCls ObjStation = new DeptCls();
                ObjStation.DeptId = dt.Rows[i]["strGuideGroupGUID"].ToString();
                ObjStation.DeptName = dt.Rows[i]["strGuideGroupName"].ToString();
                ObjStation.ParentDeptID = dt.Rows[i]["strWorkShopGUID"].ToString();
                LstStation.Add(ObjStation);
            }
            return LstStation;

        }
        public List<DeptCls> GetSmallGuideGroupList()
        {

            List<DeptCls> LstStation = new List<DeptCls>();
            string sql = "select *  from TAB_Org_SmallGuidGroup";
            var dt = SqlHelper.ExecuteDataset(PublicVar.Connstr, System.Data.CommandType.Text, sql).Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DeptCls ObjStation = new DeptCls();
                ObjStation.ParentDeptID = dt.Rows[i]["strGuideGroupGUID"].ToString();
                ObjStation.DeptName = dt.Rows[i]["strSmallGuideGroupName"].ToString();
                ObjStation.DeptId = dt.Rows[i]["strSmallGuideGroupNumber"].ToString();
                LstStation.Add(ObjStation);
            }
            return LstStation;

        }
        public int deleteArea(DeptCls lstDept)
        {
            string sql1 = "delete  from TAB_Org_Area    where strGUID=@deptid";
            
                SqlParameter[] sqlparam = new SqlParameter[]{
                     
              
                       new SqlParameter("deptid",lstDept.DeptId)
               
                
                     };


             int    ret = SqlHelper.ExecuteNonQuery(PublicVar.Connstr, CommandType.Text, sql1, sqlparam);
             return ret;
            
        }
        public int deleteWork(DeptCls lstDept)
        {
            string sql1 = "delete  from TAB_Org_WorkShop    where strWorkShopGUID=@deptid";

            SqlParameter[] sqlparam = new SqlParameter[]{
                     
              
                       new SqlParameter("deptid",lstDept.DeptId)
               
                
                     };


            int ret = SqlHelper.ExecuteNonQuery(PublicVar.Connstr, CommandType.Text, sql1, sqlparam);
            return ret;

        }
        public int deleteGroupBig(DeptCls lstDept)
        {
            string sql1 = "delete  from TAB_Org_GuideGroup    where strGuideGroupGUID=@deptid";

            SqlParameter[] sqlparam = new SqlParameter[]{
                     
              
                       new SqlParameter("deptid",lstDept.DeptId)
               
                
                     };


            int ret = SqlHelper.ExecuteNonQuery(PublicVar.Connstr, CommandType.Text, sql1, sqlparam);
            return ret;

        }
        public int deleteGroupSmall(DeptCls lstDept)
        {
            string sql1 = "delete  from TAB_Org_SmallGuidGroup    where strSmallGuideGroupGUID=@deptid";

            SqlParameter[] sqlparam = new SqlParameter[]{
                     
              
                       new SqlParameter("deptid",lstDept.DeptId)
               
                
                     };


            int ret = SqlHelper.ExecuteNonQuery(PublicVar.Connstr, CommandType.Text, sql1, sqlparam);
            return ret;

        }
        public int UpdateArea(DeptCls lstDept)
        {
         
            string deptid = lstDept.DeptId;
            string deptname = lstDept.DeptName;
            int ret = 0;
          string  sql1 = "update  TAB_Org_Area  set strAreaName=@deptname ,strJWDNumber=@deptid  where strGUID=@deptid1";
               if(lstDept.DeptType=="2")
               { 
                SqlParameter[] sqlparam = new SqlParameter[]{
                      new SqlParameter("deptname",deptname) ,
                      new SqlParameter("deptid",deptid),
              
                       new SqlParameter("deptid1",deptid)
               
                
                     };


             ret = SqlHelper.ExecuteNonQuery(PublicVar.Connstr, CommandType.Text, sql1, sqlparam);
               }
          return ret;
        }
        public int UpdateWK(DeptCls lstDept)
        {

            string deptid = lstDept.DeptId;
            string deptname = lstDept.DeptName;
            string parnetdeptid = lstDept.ParentDeptID;
            int ret = 0;
            string sql1 = "";
            if (lstDept.DeptType == "3")
            {
                string areaguid = "";
                DataTable newdt = GetAreaTab();
                if (newdt != null && newdt.Rows.Count > 0)
                {
                    for (int ii = 0; ii < newdt.Rows.Count; ii++)
                    {
                        if (newdt.Rows[ii]["strJWDNumber"].ToString() == parnetdeptid)
                        {
                            areaguid = newdt.Rows[ii]["strGUID"].ToString();
                            if (!string.IsNullOrEmpty(areaguid))
                            {
                                sql1 = "update TAB_Org_WorkShop  set strAreaGUID=@areaguid,strWorkShopName=@deptname ,strWorkShopNumber=@deptid,nIsYunZhuan=1 where strWorkShopGUID=@deptid1";
                                SqlParameter[] sqlparam = new SqlParameter[]{
                                    new SqlParameter("deptname",deptname) ,
                                    new SqlParameter("deptid",deptid),
                                    new SqlParameter("areaguid",areaguid),
                                    new SqlParameter("deptid1",deptid)
               
                
                     };


                                ret = SqlHelper.ExecuteNonQuery(PublicVar.Connstr, CommandType.Text, sql1, sqlparam);
                            }
                        }
                    }
                }
            }
            return ret;
        }
        public int AddWK(DeptCls lstDept)
        {

            string deptid = lstDept.DeptId;
            string deptname = lstDept.DeptName;
            string parnetdeptid = lstDept.ParentDeptID;
            int ret = 0;
            string sql1 = "";
            if (lstDept.DeptType == "3")
            {
                string areaguid = "";
                DataTable newdt = GetAreaTab();
                if (newdt != null && newdt.Rows.Count > 0)
                {
                    for (int ii = 0; ii < newdt.Rows.Count; ii++)
                    {
                        if (newdt.Rows[ii]["strJWDNumber"].ToString() == parnetdeptid)
                        {
                            areaguid = newdt.Rows[ii]["strGUID"].ToString();
                            if (!string.IsNullOrEmpty(areaguid))
                            {
                                  sql1 = "insert into TAB_Org_WorkShop (strWorkShopGUID,strAreaGUID,strWorkShopName,strWorkShopNumber,nIsYunZhuan)values(@deptid ,@areaguid,@deptname ,@deptid1 ,1)";

                               
                                    SqlParameter[] sqlparam = new SqlParameter[]{
                                    new SqlParameter("deptname",deptname) ,
                                    new SqlParameter("deptid",deptid),
                                    new SqlParameter("areaguid",areaguid),
                                    new SqlParameter("deptid1",deptid)
               
                
                     };

                                    object obj = DataExistWS(deptid);
                                if(obj==null || obj==System.DBNull.Value)
                                    ret = SqlHelper.ExecuteNonQuery(PublicVar.Connstr, CommandType.Text, sql1, sqlparam);
                            }
                        }
                    }
                }
            }
            return ret;
        }
        public int UpdateGuideGroup(DeptCls lstDept)
        {

            string deptid = lstDept.DeptId;
            string deptname = lstDept.DeptName;
            string parnetdeptid = lstDept.ParentDeptID;
            int ret = 0;
            string sql1 = "";
            if (lstDept.DeptType == "4")
            {
                  string wkguid = "";
                DataTable newdt = GetWorkTab();
                if (newdt != null && newdt.Rows.Count > 0)
                {
                    for (int ii = 0; ii < newdt.Rows.Count; ii++)
                    {
                        if (newdt.Rows[ii]["strWorkShopNumber"].ToString() == parnetdeptid)
                        {
                            wkguid = newdt.Rows[ii]["strWorkShopGUID"].ToString();
                            if (!string.IsNullOrEmpty(wkguid))
                            {
                                sql1 = "update  TAB_Org_GuideGroup  set strWorkShopGUID=@wkguid,strGuideGroupName=@deptname   where strGuideGroupGUID=@deptid";
                                SqlParameter[] sqlparam = new SqlParameter[]{
                                     new SqlParameter("deptname",deptname) ,
                                  new SqlParameter("deptid",deptid),
                                  new SqlParameter("wkguid",wkguid) 
                    
               
                
                     };


                                object obj = DataExistGroup(deptid);
                                if(obj==null|| obj==System.DBNull.Value)
                                ret = SqlHelper.ExecuteNonQuery(PublicVar.Connstr, CommandType.Text, sql1, sqlparam);
                            }
                        }
                    }
                }
            }
            return ret;
        }
         public int AddGuideGroup(DeptCls lstDept)
        {

            string deptid = lstDept.DeptId;
            string deptname = lstDept.DeptName;
            string parnetdeptid = lstDept.ParentDeptID;
            int ret = 0;
            string sql1 = "";
            if (lstDept.DeptType == "4")
            {
                  string wkguid = "";
                DataTable newdt = GetWorkTab();
                if (newdt != null && newdt.Rows.Count > 0)
                {
                    for (int ii = 0; ii < newdt.Rows.Count; ii++)
                    {
                        if (newdt.Rows[ii]["strWorkShopNumber"].ToString() == parnetdeptid)
                        {
                            wkguid = newdt.Rows[ii]["strWorkShopGUID"].ToString();
                            if (!string.IsNullOrEmpty(wkguid))
                            {
                                 sql1 = "insert into TAB_Org_GuideGroup (strGuideGroupGUID,strWorkShopGUID,strGuideGroupName )values(@deptid,@wkguid,@deptname)";

                                
                                    SqlParameter[] sqlparam = new SqlParameter[]{
                                     new SqlParameter("deptname",deptname) ,
                                     new SqlParameter("deptid",deptid),
                                    new SqlParameter("wkguid",wkguid) 
                                    };
                    
               
                
                   


                                    ret = SqlHelper.ExecuteNonQuery(PublicVar.Connstr, CommandType.Text, sql1, sqlparam);
                            }
                        }
                    }
                }
            }
            return ret;
        }
        public int UpdateSmallGroup(DeptCls lstDept)
        {

            string deptid = lstDept.DeptId;
            string deptname = lstDept.DeptName;
            string parnetdeptid = lstDept.ParentDeptID;
            int ret = 0;
            string sql1 = "";
            if (lstDept.DeptType == "5")
            {
                  string groupguid = "";
                DataTable newdt = GetGroup();
                if (newdt != null && newdt.Rows.Count > 0)
                {
                    for (int ii = 0; ii < newdt.Rows.Count; ii++)
                    {

                        if (newdt.Rows[ii]["strGuideGroupGUID"].ToString() == parnetdeptid)//为啥没有了编号？
                        {
                            groupguid = newdt.Rows[ii]["strGuideGroupGUID"].ToString();
                            if (!string.IsNullOrEmpty(groupguid))
                            {
                                sql1 = "update TAB_Org_SmallGuidGroup set strGuideGroupGUID=@groupguid,strSmallGuideGroupName=@deptname ,strSmallGuideGroupNumber =@deptid    where strSmallGuideGroupGUID=@deptid1";

                                SqlParameter[] sqlparam = new SqlParameter[]{
                                new SqlParameter("groupguid",groupguid) ,
                                new SqlParameter("deptname",deptname) ,
                                 new SqlParameter("deptid",deptid),
                                new SqlParameter("deptid1",deptid) 
                    
               
                
                     };
                                ret = SqlHelper.ExecuteNonQuery(PublicVar.Connstr, CommandType.Text, sql1, sqlparam);
                            }
                        }
                    }
                }
            }
            return ret;
        }
        public int AddSmallGroup(DeptCls lstDept)
        {

            string deptid = lstDept.DeptId;
            string deptname = lstDept.DeptName;
            string parnetdeptid = lstDept.ParentDeptID;
            int ret = 0;
            string sql1 = "";
            if (lstDept.DeptType == "5")
            {
                string groupguid = "";
                DataTable newdt = GetGroup();
                if (newdt != null && newdt.Rows.Count > 0)
                {
                    for (int ii = 0; ii < newdt.Rows.Count; ii++)
                    {

                        if (newdt.Rows[ii]["strGuideGroupGUID"].ToString() == parnetdeptid)//为啥没有了编号？
                        {
                            groupguid = newdt.Rows[ii]["strGuideGroupGUID"].ToString();
                            if (!string.IsNullOrEmpty(groupguid))
                            {
                                   sql1 = "insert into TAB_Org_SmallGuidGroup (strSmallGuideGroupGUID,strGuideGroupGUID,strSmallGuideGroupName,strSmallGuideGroupNumber )values(@deptid  ,@groupguid,@deptname,@deptid1)";

                               
                                    SqlParameter[] sqlparam = new SqlParameter[]{
                                    new SqlParameter("groupguid",groupguid) ,
                                 new SqlParameter("deptname",deptname) ,
                                 new SqlParameter("deptid",deptid),
                                new SqlParameter("deptid1",deptid) 
                    
               
                
                     };



                                    ret = SqlHelper.ExecuteNonQuery(PublicVar.Connstr, CommandType.Text, sql1, sqlparam);
                            }
                        }
                    }
                }
            }
            return ret;
        }
        public int AddDeptBatch(DeptCls lstDept)
        {
            int ret = 0;
            string deptid = lstDept.DeptId.Trim();
           // int level = lstDept.DeptLevel;
            string deptname = lstDept.DeptName;
            string type = lstDept.DeptType;
           // string fullparentid = lstDept.FullParentID;
            //string fullparentname = lstDept.FullParentName;
            //string parnetdeptid = lstDept.ParentDeptID;
           // string parnetDeptName = lstDept.ParentDeptName;
            if (type == "2")//机务段
            {
                string sql1 = "insert into TAB_Org_Area (strGUID,strAreaName,strJWDNumber)values(@deptid,@deptname,@deptid1)";

                    SqlParameter[] sqlparam = new SqlParameter[]{
                
                   new SqlParameter("deptid",deptid),
                    new SqlParameter("deptname",deptname) ,
                    new SqlParameter("deptid1",deptid)
               
                
                     };

                 object obj=   DataExistArea(deptid);
                if(obj==null || obj==System.DBNull.Value)
                    ret = SqlHelper.ExecuteNonQuery(PublicVar.Connstr, CommandType.Text, sql1, sqlparam);
                }
                

            return ret;


        }
        public List<Station> GetStationAll()
        {
            List<Station> LstStation = new List<Station>();
            string sql = "select strStationGUID,strStationNumber,strStationName,strStationPY  from TAB_Base_Station";
            var dt = SqlHelper.ExecuteDataset(PublicVar.Connstr, System.Data.CommandType.Text, sql).Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Station ObjStation = new Station();
                ObjStation.StationName = dt.Rows[i]["strStationName"].ToString();
                ObjStation.NameJP = dt.Rows[i]["strStationPY"].ToString();
                ObjStation.TMISNumber =  dt.Rows[i]["strStationNumber"].ToString() ;
                LstStation.Add(ObjStation);
            }
            return LstStation;

        }
        public int AddStat(Station lststation)
        {
           
            int ret = 0;
            
                string mc = lststation.StationName;
                string jp = lststation.NameJP;
                if (string.IsNullOrEmpty(lststation.TMISNumber))
                    lststation.TMISNumber = "0";
                int tmis = 0;
                try
                {
                    tmis =int.Parse( lststation.TMISNumber);
                }
                catch
                {
                     
                }
                string sql = "insert into TAB_Base_Station(strStationGUID,strStationNumber,strStationName,strStationPY)values(@tmis,@ntmis,@mc,@jp)";
                SqlParameter[] sqlparam = new SqlParameter[]{
                
                new SqlParameter("tmis",mc),
                new SqlParameter("ntmis",mc),
                new SqlParameter("mc",mc),
                new SqlParameter("jp",jp)
           };
              
                //object o = DataExistStation(tmis);
                //if (o == null)
                //{
                    ret = SqlHelper.ExecuteNonQuery(PublicVar.Connstr, CommandType.Text, sql, sqlparam);
                   
                   
                //}
                return ret;

        }

        public int UpdateStat(Station lststation)
        {
           
            int ret = 0;
            
                string mc = lststation.StationName;
                string jp = lststation.NameJP;
                if (string.IsNullOrEmpty(lststation.TMISNumber))
                lststation.TMISNumber = "0";
               // int tmis = int.Parse(lststation.TMISNumber);
                string sql = "update TAB_Base_Station set  strStationNumber=@tmis,strStationName=@mc,strStationPY=@jp  where strStationGUID=@gid";
                SqlParameter[] sqlparam = new SqlParameter[]{
                
                new SqlParameter("mc",mc),
                new SqlParameter("jp",jp),
                new SqlParameter("tmis",mc),
                new SqlParameter("gid",mc)
           };
                 ret = SqlHelper.ExecuteNonQuery(PublicVar.Connstr, CommandType.Text, sql, sqlparam);
                return ret;
          
        }

        public int DeleteStat(Station lststation)
        {
            if (string.IsNullOrEmpty(lststation.TMISNumber))
                lststation.TMISNumber = "0";
               // int tmis = int.Parse(lststation.TMISNumber);
                string sql = "delete from  TAB_Base_Station where strStationGUID=@tmis";
                SqlParameter[] sqlparam = new SqlParameter[]{
                
                new SqlParameter("tmis",lststation.StationName)
           };

                int ret = SqlHelper.ExecuteNonQuery(PublicVar.Connstr, CommandType.Text, sql, sqlparam);
                return ret;
        }

        public List<Section> GetSectionAll()
        {
            List<Section> LstSection = new List<Section>();
            string sql = "select strJWDNumber,strSectionName,strSectionID,strQDNumber  from TAB_Base_Writecard_Section";
            var dt = SqlHelper.ExecuteDataset(PublicVar.Connstr, System.Data.CommandType.Text, sql).Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Section ObjSection = new Section();
                ObjSection.JWDNumber = dt.Rows[i]["strJWDNumber"].ToString();
                ObjSection.JWDName = "";
                ObjSection.ICSectionName = dt.Rows[i]["strSectionName"].ToString();
                ObjSection.ICSectionNumber = int.Parse(dt.Rows[i]["strQDNumber"].ToString());

                LstSection.Add(ObjSection);
            }
            return LstSection;

        }
        public int AddSections( Section lstplace)
        {
            int ret=0;
               string strJWDNumber = lstplace.JWDNumber;
            string strSectionName = lstplace.ICSectionName;
            string strSectionID = lstplace.ICSectionNumber.ToString();
            string strQDNumber = lstplace.ICSectionNumber.ToString();
            string sql = "insert into TAB_Base_Writecard_Section(strJWDNumber,strSectionName,strSectionID,strQDNumber)values(@strJWDNumber,@strSectionName,@strSectionID,@strQDNumber)";
              SqlParameter[] sqlparam = new SqlParameter[]{
                
                new SqlParameter("strJWDNumber",strJWDNumber),
                new SqlParameter("strSectionName",strSectionName),
                new SqlParameter("strSectionID",strSectionID),
                new SqlParameter("strQDNumber",strQDNumber)
           };
              //object o1 = DataExistSection(strQDNumber);
              //  if (o1 == null)
                   

                  ret = SqlHelper.ExecuteNonQuery(PublicVar.Connstr, CommandType.Text, sql, sqlparam);
                return ret;
             
           
        }
        public int UpdateSections(Section lstplace)
        {

            string strJWDNumber = lstplace.JWDNumber;
            string strSectionName = lstplace.ICSectionName;
            string strSectionID = lstplace.ICSectionNumber.ToString();
            string strQDNumber = lstplace.ICSectionNumber.ToString();

            string sql = "update TAB_Base_Writecard_Section set strJWDNumber=@strJWDNumber,strSectionName=@strSectionName,strSectionID=@strSectionID   where  strQDNumber=@strQDNumber";
                SqlParameter[] sqlparam = new SqlParameter[]{
                
                new SqlParameter("strJWDNumber",strJWDNumber),
                new SqlParameter("strSectionName",strSectionName),
                new SqlParameter("strSectionID",strSectionID),
                new SqlParameter("strQDNumber",strQDNumber)
           };

                int ret = SqlHelper.ExecuteNonQuery(PublicVar.Connstr, CommandType.Text, sql, sqlparam);
                return ret;

             
        }
        public int DeleteSections(Section lstplace)
        {
          
                
                string sql = "delete from  TAB_Base_Writecard_Section   where  strQDNumber=@qdh";//'" + lstplace.ICSectionNumber.ToString() + "'";
                SqlParameter[] sqlparam = new SqlParameter[]{
                
                new SqlParameter("qdh",lstplace.ICSectionNumber.ToString())
           };

                int ret = SqlHelper.ExecuteNonQuery(PublicVar.Connstr, CommandType.Text, sql, sqlparam);
                return ret;

             
        }
        public List<Place> GetPlaceAll()
        {
            List<Place> LstPlace = new List<Place>();
            string sql = "select strPlaceID,strPlaceName  from TAB_Base_DutyPlace";
            var dt = SqlHelper.ExecuteDataset(PublicVar.Connstr, System.Data.CommandType.Text, sql).Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Place ObjPlace = new Place();
                ObjPlace.PlaceID = dt.Rows[i]["strPlaceID"].ToString();
                ObjPlace.PlaceName = dt.Rows[i]["strPlaceName"].ToString();

                LstPlace.Add(ObjPlace);
            }
            return LstPlace;
        }
        public int AddPlace(Place lstplace)
        {

            int ret = 0;

            string id = lstplace.PlaceID;
            string name = lstplace.PlaceName;
            string sql = "insert into TAB_Base_DutyPlace(strPlaceID,strPlaceName)values(@id,@name)";
            SqlParameter[] sqlparam = new SqlParameter[]{
                new SqlParameter("name",name),
                new SqlParameter("id",id)
           };
            //object o1 = DataExistplace(id);

            //if (o1 == null)
            //{
                ret = SqlHelper.ExecuteNonQuery(PublicVar.Connstr, CommandType.Text, sql, sqlparam);

            //}






            return ret;

        }
        public int UpdatePlace(Place lstplace)
        {
         
          
                string id = lstplace.PlaceID;
                string name = lstplace.PlaceName;
                string sql = "update TAB_Base_DutyPlace set strPlaceName=@name where strPlaceID=@id";
                SqlParameter[] sqlparam = new SqlParameter[]{
                new SqlParameter("name",name),
                new SqlParameter("id",id)
           };

                int ret = SqlHelper.ExecuteNonQuery(PublicVar.Connstr, CommandType.Text, sql, sqlparam);
                return ret;

        
            
            
        }
        public int DeletePlace(Place lstplace)
        {
                string id = lstplace.PlaceID;
                string sql = "delete from  TAB_Base_DutyPlace   where strPlaceID=@id";
                SqlParameter[] sqlparam = new SqlParameter[]{
                
                new SqlParameter("id",id)
           };

                int ret = SqlHelper.ExecuteNonQuery(PublicVar.Connstr, CommandType.Text, sql, sqlparam);
                return ret;
        }

        public object DataExistSite(string NUM)
        {

            StringBuilder sql = new StringBuilder();
            sql.Append("select strSiteGUID  from  TAB_Base_Site  WHERE  strSiteGUID='" + NUM + "'  ");

            var intret = SqlHelper.ExecuteScalar(PublicVar.Connstr, CommandType.Text, sql.ToString());
            return intret;
        }
        public object DataExistStation(int NUM)
        {


            string sql = "select strStationNumber  from  TAB_Base_Station  WHERE  strStationNumber=@NUM" ;

          SqlParameter[] sqlparam = new SqlParameter[]{
                
                new SqlParameter("NUM",NUM)
           };

          object ret = SqlHelper.ExecuteScalar(PublicVar.Connstr, CommandType.Text, sql, sqlparam);
          return ret;
            
        }
        public object DataExistplace(string NUM)
        {

           
           string sql="select strPlaceID  from  TAB_Base_Site_DutyPlace  WHERE  strPlaceID=@NUM";

            SqlParameter[] sqlparam = new SqlParameter[]{
                
                new SqlParameter("NUM",NUM)
           };
           
            var    ret = SqlHelper.ExecuteScalar(PublicVar.Connstr, CommandType.Text, sql, sqlparam);


            return ret;
        }
        public object DataExistSection( string strQDNumber)
        {

           
           string sql="select strQDNumber  from  TAB_Base_Writecard_Section  WHERE    strQDNumber=@strQDNumber";

            SqlParameter[] sqlparam = new SqlParameter[]{
                
                new SqlParameter("strQDNumber",strQDNumber)
           };
           
            var    ret = SqlHelper.ExecuteScalar(PublicVar.Connstr, CommandType.Text, sql, sqlparam);


            return ret;
        }
        public object DataExistJWD(string NUM)
        {

            StringBuilder sql = new StringBuilder();
            sql.Append("select strPlaceID  from  TAB_Base_Site_DutyPlace  WHERE  strPlaceID='" + NUM + "'  ");

            var intret = SqlHelper.ExecuteScalar(PublicVar.Connstr, CommandType.Text, sql.ToString());
            return intret;
        }
        public object DataExistArea(string NUM)
        {

            StringBuilder sql = new StringBuilder();
            sql.Append("select strGUID  from  TAB_Org_Area WHERE  strGUID=@NUM");// strAreaName='" + NAME + "' AND
            SqlParameter[] sqlparam = new SqlParameter[]{
                
                new SqlParameter("NUM",NUM)
           };

         object ret = SqlHelper.ExecuteScalar(PublicVar.Connstr, CommandType.Text, sql.ToString(), sqlparam);
         return ret;
        }
        public object DataExistWS(string NUM)
        {

            StringBuilder sql = new StringBuilder();
            sql.Append("select strWorkShopGUID  from  TAB_Org_WorkShop WHERE   strWorkShopNumber=@NUM");//strWorkShopName='" + NAME + "' AND

            SqlParameter[] sqlparam = new SqlParameter[]{
                
                new SqlParameter("NUM",NUM)
           };

            object ret = SqlHelper.ExecuteScalar(PublicVar.Connstr, CommandType.Text, sql.ToString(), sqlparam);
            return ret;
        }
        public object DataExistGroup(string NUM)
        {

            StringBuilder sql = new StringBuilder();
            sql.Append("select strGuideGroupGUID  from  TAB_Org_GuideGroup WHERE  strGuideGroupGUID=@NUM");

            SqlParameter[] sqlparam = new SqlParameter[]{
                
                new SqlParameter("NUM",NUM)
           };

            object ret = SqlHelper.ExecuteScalar(PublicVar.Connstr, CommandType.Text, sql.ToString(), sqlparam);
            return ret;
        }
        public object DataExistSmalGroup(string NUM, string NAME)
        {

            StringBuilder sql = new StringBuilder();
            sql.Append("select strSmallGuideGroupGUID  from  TAB_Org_SmallGuidGroup WHERE  strSmallGuideGroupName=@NAME  AND strSmallGuideGroupNumber=@NUM");
            SqlParameter[] sqlparam = new SqlParameter[]{
                  new SqlParameter("NAME",NAME),
                new SqlParameter("NUM",NUM)
           };

            object ret = SqlHelper.ExecuteScalar(PublicVar.Connstr, CommandType.Text, sql.ToString(), sqlparam);
            return ret;
        }
        public DataTable GetAreaTab()
        {
            string sql = "select strGUID,strJWDNumber,strAreaName from  TAB_Org_Area";
            var dt = SqlHelper.ExecuteDataset(PublicVar.Connstr, CommandType.Text, sql).Tables[0];
            return dt;
        }
        public DataTable GetWorkTab()
        {
            string sql = "select strWorkShopGUID,strWorkShopNumber,strWorkShopName from TAB_Org_WorkShop";
            var dt = SqlHelper.ExecuteDataset(PublicVar.Connstr, CommandType.Text, sql).Tables[0];
            return dt;
        }
        public DataTable GetGroup()
        {
            string sql = "select strGuideGroupGUID,strGuideGroupName from TAB_Org_GuideGroup";
            var dt = SqlHelper.ExecuteDataset(PublicVar.Connstr, CommandType.Text, sql).Tables[0];
            return dt;
        }
    }
}
