using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Services;
using System.Dynamic;

namespace Discourse
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string json = null,path = "";
            string likes_given="", likes_received="", reply_count="", reply_to_post_number="", last_seen_at="", post_count="";
            List<Users> users;
            Posts posts;
            Items items;
            Discourse.Util util = new Discourse.Util();

            //lectura y asignación de los archivos json
            Console.WriteLine("USERS");
            path = @"c:\Users\Germán\Discourse\users.json";
            using (StreamReader jsonStream = File.OpenText(path))
            {
                json = jsonStream.ReadToEnd();
                users = JsonConvert.DeserializeObject<List<Users>>(json);
                foreach (Users user in users)
                {
                    Console.WriteLine(user.id);
                    Console.WriteLine(user.name);
                    Console.WriteLine(user.username);
                    Console.WriteLine("last seen " + user.last_seen_at); //ultima conexion
                    if (user.last_seen_at != null)
                        last_seen_at = user.last_seen_at.Value.ToString();
                }
            }
            Console.WriteLine("POSTS");
            path = @"c:\Users\Germán\Discourse\posts.json";
            using (StreamReader jsonStream = File.OpenText(path))
            {
                json = jsonStream.ReadToEnd();
                posts = JsonConvert.DeserializeObject<Posts>(json);
                foreach (LatestPost post in posts.latest_posts)
                {
                    Console.WriteLine("post_number " + post.post_number); 
                    Console.WriteLine(post.name);
                    Console.WriteLine(post.updated_at);
                    Console.WriteLine(post.username);
                    Console.WriteLine("reply_count " + post.reply_count); //respuestas a mensajes de otros
                    Console.WriteLine("reply to post number " + post.reply_to_post_number);//respuestas a mensajes de otros
                    reply_count = post.reply_count.ToString();
                    if (post.reply_to_post_number != null)
                        reply_to_post_number = post.reply_to_post_number.ToString();
                    util.ReplyCountAndMessages(post.username, users,post.reply_count);
                }
            }

            Console.WriteLine("ITEMS");
            path = @"c:\Users\Germán\Discourse\items.json";
            using (StreamReader jsonStream = File.OpenText(path))
            {
                json = jsonStream.ReadToEnd();
                items = JsonConvert.DeserializeObject<Items>(json);
                foreach (DirectoryItem directoryitem in items.directory_items)
                {
                    Console.WriteLine(directoryitem.likes_given); //elogios y likes dados
                    Console.WriteLine(directoryitem.likes_received); //elogios y likes recibidos
                    Console.WriteLine(directoryitem.post_count); //mensajes nuevos
                    Console.WriteLine(directoryitem.posts_read);
                    Console.WriteLine(directoryitem.user.id);
                    Console.WriteLine(directoryitem.user.name);
                    Console.WriteLine(directoryitem.user.username);
                    likes_given = directoryitem.likes_given.ToString();
                    likes_received = directoryitem.likes_received.ToString();
                    post_count = directoryitem.post_count.ToString();
                    util.UserInfo(directoryitem.user.username, users, directoryitem);
                }
            }

            //escribir en google sheets

            //leyendo ruta de plan ampliado y plan de ruta via el ID del documento en Google Sheets
            //Club F 1pWKYYJwTbBO-o5HHByQasJvtTfz8Ws8p71vh0MG9NW4
            var gsh = new Discourse.GoogleDiscourse("c:\\Users\\Germán\\Discourse\\Discourse-881c6d91b081.json", "1pWKYYJwTbBO-o5HHByQasJvtTfz8Ws8p71vh0MG9NW4");
            var gsp = new GoogleSheetParameters() { RangeColumnStart = 4, RangeRowStart = 3, RangeColumnEnd = 11, RangeRowEnd = 11, FirstRowIsHeaders = false, SheetName = "Estudiante F" };
            var rowValues = gsh.GetDataFromSheet(gsp);

            foreach (Users usu in users)
            {
                string[] url = util.BuscaRuta(usu.username,rowValues);
                //si ruta tiene valor, escribe en plan ampliado
                if (url[0] != "" && url[1] != "")
                {
                    Console.WriteLine(usu.username);
                    //modelo plan ampliado
                    ////url[0] = "1PXQgoSWvsnK3gnJVFUM-xJGrwdXcBhMFePJSiQlQMEw";
                    //modelo plan de ruta
                    //url[1] = "1PXRsXCFj8vAw7-9bc3rxddlfyRLinpM8vGIhENJquao";
                    
                    gsh = new Discourse.GoogleDiscourse("c:\\Users\\Germán\\Discourse\\Discourse-881c6d91b081.json", url[0]);
                    var gsh1 = new Discourse.GoogleDiscourse("c:\\Users\\Germán\\Discourse\\Discourse-881c6d91b081.json", url[1]);
                    //var row1 = new GoogleSheetRow();
                    var row2 = new GoogleSheetRow();

                    //var cell1 = new GoogleSheetCell() { CellValue = "Header 1", IsBold = true, BackgroundColor = Color.DarkGoldenrod};
                    //var cell2 = new GoogleSheetCell() { CellValue = "Header 2", BackgroundColor = Color.Cyan };

                    var cell3 = new GoogleSheetCell() { CellValue = usu.last_seen_at.ToString()};
                    var cell4 = new GoogleSheetCell() { CellValue = usu.likes_received + "/" + usu.likes_given};
                    var cell5 = new GoogleSheetCell() { CellValue = usu.reply_count.ToString()};
                    var cell6 = new GoogleSheetCell() { CellValue = usu.post_count.ToString()};

                    //row1.Cells.AddRange(new List<GoogleSheetCell>() {cell1, cell2});
                    row2.Cells.AddRange(new List<GoogleSheetCell>() { cell3, cell4, cell5, cell6 });
                    var rows = new List<GoogleSheetRow>() { /*row1,*/ row2 };
                    gsh.AddCells(new GoogleSheetParameters() { SheetName = "Plan", RangeColumnStart = 9, RangeRowStart = 4 }, rows);
                    gsh1.AddCells(new GoogleSheetParameters() { SheetName = "Estudiante", RangeColumnStart = 2, RangeRowStart = 6 }, rows);
                }
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
