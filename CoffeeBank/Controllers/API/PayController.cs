using CoffeeBank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CoffeeBank.Controllers.API
{
    public class PayModel
    {
        public string cartNumber { get; set; }
        public string ReqMon { get; set; }
        public string ReqYear { get; set; }
        public string cvc { get; set; }
        public decimal Price { get; set; }
        public string SaticiKodu { get; set; }
        public string SaticiSifre { get; set; }
    }
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
        public string Post(PayModel model)
        {
            SanalPosMusteriler spm = db.SanalPosMusteriler.FirstOrDefault(x => x.SaticiKodu == model.SaticiKodu && x.Sifre == model.SaticiSifre);
            if (spm != null)
            {
                if (spm.IsActive == true)
                {
                    Kartlar k = db.Kartlar.FirstOrDefault(x => x.KartNo == model.cartNumber);
                    if (k != null)
                    {
                        if (Convert.ToInt32("20"+k.SonKullanmaYil) >= Convert.ToInt32(DateTime.Now.Year))
                        {
                            if (Convert.ToInt32(k.SonKullanmaAy) >= Convert.ToInt32(DateTime.Now.Month))
                            {
                                if (k.CVCKodu == model.cvc)
                                {
                                    if (k.Hesaplar.IsActive == true)
                                    {
                                        if (k.Hesaplar.Bakiye >= model.Price)
                                        {
                                            Hesaplar h = db.Hesaplar.Find(k.Hesap_ID);
                                            h.Bakiye -= model.Price;
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
