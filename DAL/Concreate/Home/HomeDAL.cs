using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface;
using DAL.Concreate;
using System.Data.SqlClient;
using System.Data;
using Model.Models.UserDetail;

using System.Data.Entity.Core.Objects;
using WEB.Helper;
using Model.Models.Account;

namespace DAL.Concreate
{
    public class HomeDAL : BaseClassDAL, IHomeDAL
    {
        LFZ_TruckParkEntities entities = new LFZ_TruckParkEntities();

        public ResponseInfo CheckLoginDAL(UserDetailModel model)
        {
            ResponseInfo respInfo = new ResponseInfo();
            List<SqlParameter> parameters = new List<SqlParameter>();
            
            parameters.Add(new SqlParameter()
            {
                ParameterName = "@EmailId",
                SqlDbType = SqlDbType.VarChar,
                Value = model.EmailId,
                Direction = System.Data.ParameterDirection.Input


            });           

            //Rahul Added
            DataSet objData = new DataSet();
            objData = SqlManager.ExecuteDataSet("CheckUser", parameters.ToArray());
            string databasePassword = objData.Tables[0].Rows[0]["Password"].ToString();
            string saltKey = objData.Tables[0].Rows[0]["saltKey"].ToString();
            string checkPassword = helperClass.EncodePassword(model.Password, saltKey); //Create password and check below.
            model.Password = checkPassword;
            //Rahul End


            parameters.Add(new SqlParameter()
            {
                ParameterName = "@password",
                SqlDbType = SqlDbType.VarChar,
                Value = model.Password,
                Direction = System.Data.ParameterDirection.Input
            });

            SqlParameter message = new SqlParameter()
            {
                ParameterName = "@OutError",
                SqlDbType = SqlDbType.VarChar,
                Size = 1000,
                Direction = System.Data.ParameterDirection.Output
            };

            parameters.Add(message);

            var dtRuleData = SqlManager.ExecuteNonQuery("UserLogin", parameters.ToArray());
            string errormessage = message.Value.ToString();
            respInfo.ID = 0;
            respInfo.Status = errormessage;
            respInfo.IsSuccess = true;
            return respInfo;
        }

        public ResponseInfo ChangePassword(ChangePassword model)
        {
            ResponseInfo respInfo = new ResponseInfo();
            List<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter()
            {
                ParameterName = "@UDID",
                SqlDbType = SqlDbType.VarChar,
                Value = model.UDID,
                Direction = System.Data.ParameterDirection.Input


            });
            
            parameters.Add(new SqlParameter()
            {
                ParameterName = "@SaltKey",
                SqlDbType = SqlDbType.VarChar,
                Value = model.SaltKey,
                Direction = System.Data.ParameterDirection.Input

            });

            parameters.Add(new SqlParameter()
            {
                ParameterName = "@Password",
                SqlDbType = SqlDbType.VarChar,
                Value = model.Password,
                Direction = System.Data.ParameterDirection.Input
            });

            SqlParameter message = new SqlParameter()
            {
                ParameterName = "@OutError",
                SqlDbType = SqlDbType.VarChar,
                Size = 1000,
                Direction = System.Data.ParameterDirection.Output
            };

            parameters.Add(message);

            var dtRuleData = SqlManager.ExecuteNonQuery("UserChangePassword", parameters.ToArray());
            string errormessage = message.Value.ToString();
            respInfo.ID = 0;
            respInfo.Status = errormessage;
            respInfo.IsSuccess = true;
            return respInfo;
        }

        public DashboardModel Dashboard(int RoleId, int UserID, int? orgId)
        {
            DashboardModel model = new DashboardModel();
            DataSet ds = new DataSet();

            List<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter()
            {
                ParameterName = "@roleId",
                SqlDbType = SqlDbType.VarChar,
                Value = RoleId,
                Direction = System.Data.ParameterDirection.Input


            });

            parameters.Add(new SqlParameter()
            {
                ParameterName = "@UDID",
                SqlDbType = SqlDbType.VarChar,
                Value = UserID,
                Direction = System.Data.ParameterDirection.Input

            });

            parameters.Add(new SqlParameter()
            {
                ParameterName = "@OrgId",
                SqlDbType = SqlDbType.VarChar,
                Value = orgId,
                Direction = System.Data.ParameterDirection.Input

            });

            ds = SqlManager.ExecuteDataSet("Dashboard_G", parameters.ToArray());
            if (ds.Tables.Count > 0)
            {
                model.CheckedInCount = Sql.ToInt(ds.Tables[0].Rows[0]["CheckedIn"]);
                model.CheckedOutCount = Sql.ToInt(ds.Tables[0].Rows[0]["CheckedOut"]);
                model.Forecasted = Sql.ToInt(ds.Tables[0].Rows[0]["Forecasted"]);

                model.Hours0to4 = Sql.ToInt(ds.Tables[1].Rows[0]["Hours0to4"]);
                model.Hours4to8= Sql.ToInt(ds.Tables[1].Rows[0]["Hours4to8"]); 
                model.Hours8to16= Sql.ToInt(ds.Tables[1].Rows[0]["Hours8to16"]);
                model.Hours16to24= Sql.ToInt(ds.Tables[1].Rows[0]["Hours16to24"]);
                model.Hours24to48= Sql.ToInt(ds.Tables[1].Rows[0]["Hours24to48"]); ;
                model.Hours48to72= Sql.ToInt(ds.Tables[1].Rows[0]["Hours48to72"]); ;
                model.Hours72More= Sql.ToInt(ds.Tables[1].Rows[0]["Hours72More"]);


                model.StatusOfTruckList = (from DataRow row in ds.Tables[3].Rows
                                           select new StatusOfTruck
                                           {
                                               OrganizationShortName = Sql.ToString(row["OrgShortName"]),
                                               TotalTrucks = Sql.ToString(row["TotalTrucks"]),
                                               FiveDayParkedTrucks = Sql.ToString(row["FiveDaysParkedTrucks"]),
                                               TodayTrucks = Sql.ToString(row["TodayTrucks"]),
                                               Capacity10 = Sql.ToString(row["Capacity10"]),
                                               Capacity15 = Sql.ToString(row["Capacity15"]),
                                               Capacity20 = Sql.ToString(row["Capacity20"]),
                                               Capacity25 = Sql.ToString(row["Capacity25"]),
                                               Capacity30 = Sql.ToString(row["Capacity30"]),
                                               Capacity40 = Sql.ToString(row["Capacity40"]),
                                               Capacity40nMore = Sql.ToString(row["Capacity40nMore"])
                                           }).ToList();

                model.MovementOfTruckList = (from DataRow rw in ds.Tables[2].Rows
                                             select new MovementOfTruck
                                             {
                                                 OrganizationName = Sql.ToString(rw["OrgShortName"]),
                                                 TimePeriod = Sql.ToString(rw["TimePeriod"]),
                                                 CheckInCount = Sql.ToInt(rw["CheckInCount"]),
                                                 CheckOutCount = Sql.ToInt(rw["CheckOutCount"])
                                             }).ToList();
            }
            return model;
        }

        public ValididateUser_OnePortal ValidUser(ValididateUser_OnePortal Email)
        {
            var result = entities.ValidateUser_OnePortal(Email.EmailID).FirstOrDefault();
            ValididateUser_OnePortal userdetails = Mapping<ValididateUser_OnePortal>(result);
            return userdetails;
        }


    }
}
