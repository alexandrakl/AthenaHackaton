using athenahackathon.Models;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace athenahackathon.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }


        // GET: My Account -> Closet
        [AllowAnonymous]
        public ActionResult MyAccount()
        {
            string user = User.Identity.Name;
            string userId = string.Empty;
            string closetName = string.Empty;
            List<string> closetsList = new List<string>();
            List<int> closetIdsList = new List<int>();
            List<string> outfitsList = new List<string>();
            List<string> closetInviteList = new List<string>();
            List<string> outfitInviteList = new List<string>();

            Debug.WriteLine(user);
            // get User Id for user name
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["UserConnection"].ConnectionString))
            {
                connection.Open();
                StringBuilder sb = new StringBuilder();
                sb.Append("Select TOP 1 [Id] from [dbo].[AspNetUsers] where [Email]='" + user + "'");

                using (SqlCommand command = new SqlCommand(sb.ToString(), connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            userId = reader["Id"].ToString();
                        }
                    }
                }

                // get closets(userId)
                StringBuilder closetstring = new StringBuilder();
                closetstring.Append("Select [ClosetId],[ClosetName] from [dbo].[Closet] where [UserId]='" + userId + "'");

                using (SqlCommand command = new SqlCommand(closetstring.ToString(), connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            closetsList.Add(reader["ClosetName"].ToString());
                            closetIdsList.Add(Convert.ToInt32(reader["ClosetId"]));
                            //Debug.WriteLine(reader["ClosetName"].ToString());
                        }
                    }
                }


                //get outfits(userId)
                StringBuilder outfitstring = new StringBuilder();
                outfitstring.Append("Select [OutfitId],[OutfitName] from [dbo].[Outfit] where [OwnerId]='" + userId + "'");

                using (SqlCommand command = new SqlCommand(outfitstring.ToString(), connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            outfitsList.Add(reader["OutfitName"].ToString());
                            //Debug.WriteLine(reader["OutfitName"].ToString());
                        }
                    }
                }


                //get outfitsInvite(userId)
                StringBuilder outfitInvitestring = new StringBuilder();
                outfitInvitestring.Append("Select [OutfitId] from [dbo].[Invite] where [ReceiverId]='" + userId + "'");

                using (SqlCommand command = new SqlCommand(outfitstring.ToString(), connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            outfitInviteList.Add(reader["OutfitId"].ToString());
                            //Debug.WriteLine(reader["OutfitName"].ToString());
                        }
                    }
                }

                //get outfitsInvite(userId)
                StringBuilder closetInvitestring = new StringBuilder();
                closetInvitestring.Append("Select [ClosetId] from [dbo].[Invite] where [ReceiverId]='" + userId + "'");

                using (SqlCommand command = new SqlCommand(closetInvitestring.ToString(), connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            closetInviteList.Add(reader["ClosetId"].ToString());
                            //Debug.WriteLine(reader["OutfitName"].ToString());
                        }
                    }
                }


            }


            ViewBag.Message = user;
            ViewBag.Closets = closetsList;
            ViewBag.Outfits = outfitsList;
            ViewBag.InviteClosets = closetInviteList;
            ViewBag.InviteOutfits = outfitInviteList;
            ViewBag.ClosetIdsList = closetIdsList;

            return View();
        }

        public ActionResult SimpleInterest()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult CalculateSimpleInterestResult()
        //{
        //    decimal principle = Convert.ToDecimal(Request["txtAmount"].ToString());
        //    decimal rate = Convert.ToDecimal(Request["txtRate"].ToString());

        //    decimal simpleInteresrt = (principle * rate) / 100;

        //    StringBuilder sbInterest = new StringBuilder();
        //    sbInterest.Append("<b>Amount :</b> " + principle + "<br/>");
        //    sbInterest.Append("<b>Rate :</b> " + rate + "<br/>");
        //    return Content(sbInterest.ToString());
        //}


        // GET: My Closet Pass in User Id
        [AllowAnonymous]
        public ActionResult MyCloset(string closetIdString = "1")
        {
            string temp = "temprary string";
            int closetId = 1;
            List<string> clothesList = new List<string>();

            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["UserConnection"].ConnectionString))
            {
                connection.Open();
                StringBuilder clothesstring = new StringBuilder();
                clothesstring.Append("Select [ClothesId], [ClothesName], [ImageUrl] from [dbo].[Clothes] where [ClosetId]='" + closetId + "'");

                using (SqlCommand command = new SqlCommand(clothesstring.ToString(), connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Debug.WriteLine(reader["ImageUrl"].ToString());
                            clothesList.Add(reader["ImageUrl"].ToString());
                        }

                        clothesList.Add(temp);
                    }
                }

            }

            ViewBag.Message = "My Shirt Closet";
            ViewBag.Clothes = clothesList;
            return View();
        }


        //
        // POST: /MyAccount
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public ActionResult MyCloset(string returnUrl)
        //{
        //    return RedirectToAction("MyCloset", "User");

        //}



        // GET: My Outfit pass in outfit id
        [AllowAnonymous]
        public ActionResult MyOutfit(string outfitIdString)
        {

            int outfitId = Convert.ToInt32(outfitIdString);
            List<string> clothesListOutfit = new List<string>();
            string clothesIdString = string.Empty;
            int clothesId;

            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["UserConnection"].ConnectionString))
            {
                connection.Open();
                StringBuilder clothesoutfit = new StringBuilder();
                clothesoutfit.Append("Select [ClothesId] from [dbo].[Cloth_Outfit] where [OutfitId]='" + outfitId + "'");

                using (SqlCommand command = new SqlCommand(clothesoutfit.ToString(), connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Debug.WriteLine(reader["ClothesId"].ToString());
                            clothesIdString = reader["ClothesId"].ToString();
                            clothesId = Convert.ToInt32(clothesIdString);
                            //clothesList.Add(reader["ClothesName"].ToString());
                        }
                    }
                }

                clothesListOutfit.Add("temp");
                //    StringBuilder clothesoutfitstring = new StringBuilder();
                //    clothesoutfit.Append("Select [ClothesName] from [dbo].[Clothes] where [ClothesId]='" + clothesIdString + "'");

                //    using (SqlCommand command = new SqlCommand(clothesoutfitstring.ToString(), connection))
                //    {
                //        using (SqlDataReader reader = command.ExecuteReader())
                //        {
                //            while (reader.Read())
                //            {
                //                Debug.WriteLine(reader["ClothesName"].ToString());
                //                clothesListOutfit.Add(reader["ClothesName"].ToString());
                //            }
                //            clothesListOutfit.Add("temp");
                //        }
                //    }

                //}
            }
            ViewBag.Message = " Inside my outfit view";
            ViewBag.ClothesOutfit = clothesListOutfit;
            return View();

        }

        // GET: My Invite -> pass in outfit id and closet Id (one is always null, also receiver Id get from view of the owner user
        // get list of closetIDs and list OF Outfit Ids
        // in receiver list display lists in View of Receiver (not done)
        [AllowAnonymous]
        public ActionResult MyInvite()
        {
            string receiverName = User.Identity.Name;
            string receiverId = string.Empty;




            Debug.WriteLine(receiverName);

            List<string> closetIdsList = new List<string>();
            List<string> outfitIdsList = new List<string>();

            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["UserConnection"].ConnectionString))
            {
                connection.Open();

                StringBuilder sb = new StringBuilder();
                sb.Append("Select TOP 1 [Id] from [dbo].[AspNetUsers] where [Email]='" + receiverName + "'");

                using (SqlCommand command = new SqlCommand(sb.ToString(), connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            receiverId = reader["Id"].ToString();
                        }
                    }


                }

                StringBuilder invitestring = new StringBuilder();

                invitestring.Append("Select [OutfitId], [ClosetId] from [dbo].[Invite] where [ReceiverId]='" + receiverId + "'");
                using (SqlCommand command = new SqlCommand(invitestring.ToString(), connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader["ClosetId"] == DBNull.Value)
                            {
                                Debug.WriteLine(reader["OutfitId"].ToString());
                                outfitIdsList.Add(reader["OutfitId"].ToString());
                            }
                            else
                            {
                                Debug.WriteLine(reader["ClosetId"].ToString());
                                closetIdsList.Add(reader["ClosetId"].ToString());
                            }

                        }
                    }
                }
            }
            ViewBag.Outfits = outfitIdsList;
            ViewBag.Closets = closetIdsList;
            return View();
        }
    }
}



//List<string> receiversList = new List<string>();
//string clothesId = string.Empty;

//using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["UserConnection"].ConnectionString))
//{
//    connection.Open();
//    if (closetId == 0)
//    {
//        StringBuilder invitestring = new StringBuilder();
//        invitestring.Append("Select [ReceiverId] from [dbo].[Invite] where [OutfitId]='" + outfitId + "'");
//        using (SqlCommand command = new SqlCommand(invitestring.ToString(), connection))
//        {
//            using (SqlDataReader reader = command.ExecuteReader())
//            {
//                while (reader.Read())
//                {
//                    Debug.WriteLine(reader["ReceiverId"].ToString());
//                    receiversList.Add(reader["ReceiverId"].ToString());
//                }
//            }
//        }
//    }

//    if (outfitId == 0) {
//        StringBuilder invitestring2 = new StringBuilder();
//        invitestring2.Append("Select [ReceiverId] from [dbo].[Invite] where [ClosetId]='" + closetId + "'");
//        using (SqlCommand command = new SqlCommand(invitestring2.ToString(), connection))
//        {
//            using (SqlDataReader reader = command.ExecuteReader())
//            {
//                while (reader.Read())
//                {
//                    Debug.WriteLine(reader["ReceiverId"].ToString());
//                    receiversList.Add(reader["ReceiverId"].ToString());
//                }
//            }
//        }
//    }     