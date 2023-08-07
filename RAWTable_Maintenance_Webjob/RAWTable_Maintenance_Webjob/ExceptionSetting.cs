﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

using System.Net.Mail;
namespace RAWTable_Maintenance_Webjob
{
    class ExceptionSetting
    {

        private static String ErrorlineNo, Errormsg, ErrorLocation, extype, exurl, Frommail, ToMail, Sub, HostAdd, EmailHead, EmailSing;


        public static void SendErrorTomail(Exception exmail, String connStr)
        {

            try
            {

                var newline = "<br/>";
                ErrorlineNo = exmail.StackTrace.Substring(exmail.StackTrace.Length - 7, 7);
                Errormsg = exmail.GetType().Name.ToString();
                extype = exmail.GetType().ToString();
                //exurl = context.Current.Request.Url.ToString();
                ErrorLocation = exmail.Message.ToString();
                EmailHead = "<b>Dear Team,</b>" + "<br/>" + "An exception occurred while running RAWTable Maintenance web job With following Details" + "<br/>" + "<br/>";
                EmailSing = newline + "Thanks and Regards" + newline + "    " + "     " + "<b>IIOT Team.TEAL </b>" + "</br>";
                Sub = "Exception occurred" + " " + "in Application";
                //HostAdd = ConfigurationManager.AppSettings["Host"].ToString();
                string errortomail = EmailHead + "<b>Log Written Date: </b>" + " " + DateTime.Now.ToString() + newline + "<b>Web Job: </b>" + " " + " RAWTable Maintenance " + newline + "<b>Error Line No :</b>" + " " + ErrorlineNo + newline + "<b>Error Message:</b>" + " " + Errormsg + newline + "<b>Exception Type:</b>" + " " + extype + newline + "<b> Error Details :</b>" + " " + ErrorLocation + newline + " " + newline + newline + newline + newline + EmailSing;


                using (SqlConnection con = new SqlConnection(connStr))
                {


                    MailMessage mail = new MailMessage();
                    DataTable dt = new DataTable();
                    SqlCommand cmd_mail = new SqlCommand("SELECT * FROM tbl_gmail_settings", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd_mail);
                    da.Fill(dt);
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = dt.Rows[0]["Smtp_host"].ToString();
                    smtp.Port = Convert.ToInt32(dt.Rows[0]["Smtp_port"].ToString());
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = new System.Net.NetworkCredential(dt.Rows[0]["Smtp_user"].ToString(), dt.Rows[0]["Smtp_pass"].ToString());
                    smtp.EnableSsl = true;
                    //foreach (DataRow Row in MailToset.Tables[0].Rows)
                    //{
                    //    string MailTo = Row["Email_ID"].ToString();
                    //    mail.To.Add(MailTo);

                    //}
                    mail.To.Add("arutprakasam.a@titan.co.in");
                

                    mail.From = new MailAddress(dt.Rows[0]["Smtp_user"].ToString());
                    string dts = DateTime.Today.AddDays(-1).ToString("dd-MM-yyyy");
                    mail.Subject = "Exception mail while running  RAWTable Maintenance  webjob";
                    mail.Body = errortomail;
                    mail.IsBodyHtml = true;


                    smtp.Send(mail);
                    Console.WriteLine("Exception Email sent successfully");
                }
            }
            catch (Exception ex)
            {
                //ExceptionSetting.SendErrorTomail(ex, connStr);

                Console.WriteLine("Failed to send Exception email" + ex);
            }
        }

    }
}
