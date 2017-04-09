using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
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
            List<string> outfitsList = new List<string>();

            Debug.WriteLine(user);
            // get User Id for user name
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["UserConnection"].ConnectionString)) {
                connection.Open();
                StringBuilder sb = new StringBuilder();
                sb.Append("Select TOP 1 [Id] from [dbo].[AspNetUsers] where [Email]='" + user + "'");

                using (SqlCommand command = new SqlCommand(sb.ToString(), connection)) {
                    using (SqlDataReader reader = command.ExecuteReader()) {
                        while (reader.Read()) {
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
            }

            
            return View();
        }

        // GET: My Closet Pass in User Id
        [AllowAnonymous]
        public ActionResult MyCloset(int closetId) {
            List<string> clothesList = new List<string>();

            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["UserConnection"].ConnectionString))
            {
                connection.Open();
                StringBuilder clothesstring = new StringBuilder();
                clothesstring.Append("Select [ClothesId], [ClothesName] from [dbo].[Clothes] where [ClosetId]='" + closetId + "'");

                using (SqlCommand command = new SqlCommand(clothesstring.ToString(), connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Debug.WriteLine(reader["ClothesName"].ToString());
                           //clothesList.Add(reader["ClothesName"].ToString());
                        }
                    }
                }

            }
                return View();
        }

         // GET: My Outfit pass in outfit id
        [AllowAnonymous]
        public ActionResult MyOutfit(int outfitId)
        {
            List<string> clothesListOutfit = new List<string>();
            string clothesId = string.Empty;

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
                            clothesId = reader["ClothesId"].ToString();
                            //clothesList.Add(reader["ClothesName"].ToString());
                        }
                    }
                }

                StringBuilder clothesoutfitstring = new StringBuilder();
                clothesoutfit.Append("Select [ClothesName] from [dbo].[Clothes] where [ClothesId]='" + clothesId + "'");

                using (SqlCommand command = new SqlCommand(clothesoutfitstring.ToString(), connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Debug.WriteLine(reader["ClothesName"].ToString());
                            clothesListOutfit.Add(reader["ClothesName"].ToString());
                        }
                    }
                }

            }
            return View();
        }

        // GET: My Invite -> pass in outfit id and closet Id (one is always null, also receiver Id get from view of the owner user
        // get list of closetIDs and list OF Outfit Ids
        // in receiver list display lists in View of Receiver (not done)
        [AllowAnonymous]
        public ActionResult MyInvite(int closetId, int outfitId, int receiverId)
        {

            List<string> closetIdsList = new List<string>();
            List<string> outfitIdsList = new List<string>();

            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["UserConnection"].ConnectionString))
            {
                connection.Open();
                StringBuilder invitestring = new StringBuilder();
                if (closetId == 0) {
                    invitestring.Append("Select [OutfitId] from [dbo].[Invite] where [ReceiverId]='" + receiverId + "'");
                    using (SqlCommand command = new SqlCommand(invitestring.ToString(), connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Debug.WriteLine(reader["OutfitId"].ToString());
                                outfitIdsList.Add(reader["OutfitId"].ToString());
                            }
                        }
                    }
                }

                if (outfitId == 0)
                {
                    invitestring.Append("Select [ClosetId] from [dbo].[Invite] where [ReceiverId]='" + receiverId + "'");
                    using (SqlCommand command = new SqlCommand(invitestring.ToString(), connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Debug.WriteLine(reader["ClosetId"].ToString());
                                closetIdsList.Add(reader["ClosetId"].ToString());
                            }
                        }
                    }
                }

            }

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