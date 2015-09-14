using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GProyOficial.Controllers
{
    public class ProccessorController : Controller
    {
        // GET: Proccessor
        public ActionResult Index()
        {
            return View();
        }

        public List<object[]> Parser(IEnumerable<object> jsonsList)
        {
            var final = new List<object[]>();
            foreach (var o in jsonsList)
            {
                string a = o.ToString();
                string res = "";
                int posini = 0;
                int posfin = 0;
                for (int i = 0; i < a.Count(); i++)
                {
                    if (a[i] == '[')
                        posini = i + 1;
                    if (a[i] == ']')
                        posfin = i - 1;

                }
                res = a.Substring(posini, posfin - posini);
                var lista = new List<object>();
                string temp = "";
                for (var i = 0; i < res.Count(); i++)
                {

                    if (res[i] != '"' && res[i] != '\r' && res[i] != '\n' && res[i] != ',' && res[i] != ' ')
                        temp += res[i];
                    if (res[i] == ',')
                    {
                        i = i + 1;
                        lista.Add(temp);
                        temp = "";
                    }
                }
                lista.Add(temp);
                var objctarray = new object[lista.Count];
                for (int i = 0; i < lista.Count; i++)
                {
                    objctarray[i] = lista[i];
                }
                final.Add(objctarray);
            }

          
            foreach (object[] t in final)
            {

                if ((string)t[0] != "undefined")
                {
                    if (t[1].ToString() == "NO")
                        t[1] = 0;
                    if (t[1].ToString() == "SI")
                        t[1] = 1;
                    t[0] = long.Parse(t[0].ToString());
                }
            }
            for (int i = 0; i < final.Count; i++)
            {
                if (final[i][0].ToString() == "undefined")
                {
                    final.RemoveAt(i);
                    i--;
                }
            }

            return final;
        }



    }
}