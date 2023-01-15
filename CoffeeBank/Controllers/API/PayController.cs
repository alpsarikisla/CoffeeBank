using CoffeeBank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CoffeeBank.Controllers.API
{
    public class PayController : ApiController
    {
        CoffeBank_DBEntities db = new CoffeBank_DBEntities();
        // GET: api/Pay
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Pay/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Pay
        public string Post(string SaticiKodu, string SaticiSifre, string KartNo, string SonkullanmaAy, string SonKullamaYil, string CVCKodu, decimal Bakiye)
        {
            SanalPosMusteriler spm = db.SanalPosMusteriler.FirstOrDefault(x => x.SaticiKodu == SaticiKodu && x.Sifre == SaticiSifre);
            if (spm != null)
            {
                if (spm.IsActive == true)
                {
                    Kartlar k = db.Kartlar.FirstOrDefault(x => x.KartNo == KartNo);
                    if (k != null)
                    {
                        if (Convert.ToInt32("20"+k.SonKullanmaYil) >= Convert.ToInt32(DateTime.Now.Year))
                        {
                            if (Convert.ToInt32(k.SonKullanmaAy) >= Convert.ToInt32(DateTime.Now.Month))
                            {
                                if (k.CVCKodu == CVCKodu)
                                {
                                    if (k.Hesaplar.IsActive == true)
                                    {
                                        if (k.Hesaplar.Bakiye >= Bakiye)
                                        {
                                            Hesaplar h = db.Hesaplar.Find(k.Hesap_ID);
                                            h.Bakiye -= Bakiye;
                                            db.SaveChanges();
                                            return "101";
                                        }
                                        else
                                        {
                                            return "301";
                                        }
                                    }
                                    else
                                    {
                                        return "401";
                                    }
                                }
                                else
                                {
                                    return "501";
                                }
                            }
                            else
                            {
                                return "601";
                            }
                        }
                        else
                        {
                            return "601";
                        }
                    }
                    else
                    {
                        return "701";
                    }
                }
                else
                {
                    return "801";
                }
            }
            else
            {
                return "901";
            }
        }

        // PUT: api/Pay/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Pay/5
        public void Delete(int id)
        {
        }
    }
}
