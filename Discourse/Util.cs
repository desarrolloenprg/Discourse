using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Discourse
{
    internal class Util
    {
        public void ReplyCountAndMessages(string username, List<Users> users, int replycount)
        {
            foreach (Users usu in users)
            {
                if (usu.username == username)
                {
                    usu.reply_count = usu.reply_count + replycount;
                    usu.post_count = usu.post_count + 1;
                    break;
                }
            }
        }

        public void UserInfo(string username, List<Users> users, DirectoryItem directoryitem)
        {
            foreach (Users usu in users)
            {
                if (usu.username == username)
                {
                    usu.likes_received = directoryitem.likes_received;
                    usu.likes_given = directoryitem.likes_given;
                    break;
                }
            }
        }

        public string[] BuscaRuta(string username, List<System.Dynamic.ExpandoObject> rowValues)
        {
            string[] rutas = { "", "" };
            foreach (System.Dynamic.ExpandoObject rowValue in rowValues)
            {
                var usu = rowValue.ElementAt(0).Value;
                if ((string)usu == username)
                {
                    rutas[0] = (string)rowValue.ElementAt(7).Value; //ampliado
                    rutas[1] = (string)rowValue.ElementAt(2).Value; //plan de ruta
                    return rutas;
                }
                
            }
            return rutas;
        }
    }
}