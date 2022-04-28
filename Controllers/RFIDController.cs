using RFIDWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RFIDWebAPI.Controllers
{
    public class RFIDController : ApiController
    {
        public string get(string card_uid, string device_key)
        {
            using (loginEntities l = new loginEntities())
            {
                try
                {
                    //get the device
                    var d = l.RFIDDevices.Where(i => i.DeviceKey == device_key);
                    //check if there is async device and isactive
                    var device = d.FirstOrDefault();
                    if (d.Any() && d.FirstOrDefault().isActive == true)
                    {
                        //get users with that specific device ID 
                        var u = l.RFIDAuthUsers.Where(i => i.CardUID == card_uid && i.RFIDDeviceId == device.RFIDDeviceId).FirstOrDefault();
                        //checked if its null and is authorized, is active
                        if (u != null && u.IsActive == true && u.IsAuthorized == true)
                        {
                            //take rows from userlogs where they have logged inn
                            var logs = l.RFIDUserLogs.Where(i => i.RFIDUserId == u.RFIDUserId && i.TimeOut == null).FirstOrDefault();
                            //add log out time else add new log in time
                            if (logs != null)
                            {
                                logs.TimeOut = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                                l.Entry(logs).State = System.Data.Entity.EntityState.Modified;
                                l.SaveChanges();
                                return "Out";
                            }
                            else
                            {
                                RFIDUserLog addlog = new RFIDUserLog();
                                addlog.RFIDUserId = u.RFIDUserId;
                                addlog.TimeIn = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                                l.RFIDUserLogs.Add(addlog);
                                var res = l.SaveChanges();
                                return "In";
                            }
                        }
                        else
                        {
                            return "Unauthorized User";
                        }
                    }
                    else
                    {
                        return "Unauthorized Device";
                    }
                }catch (Exception ex)
                {
                    return "Error";
                }
            }
        }
        public bool getAddUser(string UserUID, string CardUID, string RFIDDeviceId, bool IsAuthorized, bool IsActive, string password)
        {
            using (loginEntities l = new loginEntities())
            {
                try
                {
                    if (password=="ctuerproot@123321" && UserUID != null && CardUID!= null && RFIDDeviceId!=null && IsAuthorized != null && IsActive != null)
                    {
                        RFIDAuthUser adduser = new RFIDAuthUser();
                        adduser.UserUID = UserUID;
                        adduser.CardUID = CardUID;
                        adduser.RFIDDeviceId = Convert.ToInt32(RFIDDeviceId);
                        adduser.IsAuthorized = IsAuthorized;
                        adduser.IsActive = IsActive;
                        l.RFIDAuthUsers.Add(adduser);
                        var res = l.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }catch(Exception e) 
                { return false; }
            }
        }
    }
}
