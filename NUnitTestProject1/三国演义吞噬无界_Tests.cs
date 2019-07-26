using baofengCrack;
using NUnit.Framework;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            int keyId;
            var postdata = 三国演义吞噬无界.DecodeRequestData("100c17971d9b52a6f4ee8f0b4e73f8dc1a8WmZD7458AS7ew8dypYetLpRLowsEvPxycBLOQ6g0xtwMy70iQaRkk3AzUh8xQaX5LfC6jUcTWA_aDRd2_qd2_q_RmR4wcy_aDRjoH6joH6_RmRn6sy_aDmNMLqrTVR0R_qdT9fd2VxnoKAjoHeC2fsdocynTLsC69yda45jqE1jR_7_1efG14R0RL-rM_7_19arQBR0aVmjoC60oPt", out keyId);
            Assert.IsNotNull(postdata);

            var respdata = 三国演义吞噬无界.DecodeResponseData("c4bdc0034fbb5450524f0849c35ec876cSyOnWcTx4gOi6jacR9OKvoMtLxlQCaQAIFJL8Dtl80IgwEUWcu4zf4HtMlBoRXmgb_3vL41jon9eg_pDZaFmN3LvZ_tYsXVRLqLvLJrSQJ-3w-PFP-bS0bQN0PQH-aLULlvgb_3vLP9NDGx_-_hRmNhE6qhON3tg*", out keyId);
            Assert.IsNotNull(respdata); 

            Assert.Pass();
        }

        [Test]
        public void Test2()
        {
            int keyId;
            string aEncodePostData = "100eb6801c3f2cfda00f253a6fd8fb0c8edYhiiODIIU5sJFplgfzPvG2ZX99v9WL8b5G7YhJoLA1R3jh54JAzuNJkWTeQA9Rjo0cpJUefzKi_1hL_zDowVOTdPme45defzdRdVsRdYdT9P0rpJUefe0zyYU5sz_6sr_ofQOmyYH5dV42sJyhfJ01dJs2fQd5se_i_1Nh-1hzyF_3_zEEsJ_ryzNEs1_gwQy1yznLwY_osrNEdraLd1O5szNz_eme-1no_zDgdVOR9P0r75nos6Hefe_GdVOe9P0e-Jy1yt_efzdTdY_i_cwT7e_3wVd69P0A71yCnZhmyF_3dPmepJT17o_3jo_mdP_3jo0X_zDGwYsrfY46wzdT9P0W_zDGfVK6dZRq9P0A71yCH6h3yF_3wVs6weme7twc_zDG9P0gytwg_zDm9P0isJTc_zDej1xe9P0hs6EC_zD6fV_5drs5vU**";
            var postdata = 三国演义吞噬无界.DecodeRequestData(aEncodePostData, out keyId);
            Assert.IsNotNull(postdata);
            string respdata_encode = 三国演义吞噬无界.EncodeRequestData(postdata.ToString(Newtonsoft.Json.Formatting.None), aEncodePostData.Substring(35, 62), keyId);
            Assert.AreEqual(respdata_encode, aEncodePostData);

            string aEncodeRespData = "ac01c325e21203130f7ebc6a5f449d5eOOHVcwo3Yb2Kob3YC38mVxwJ0aKIaWuBBLj7W8YiZRFLxBr3QuQFA1VghXV1MFxmgbTaKs41GDuJPgTzhldamfasKlTopc403sesKcfsmfasKlT1Gzr0BsesK2fzjDdvdRMjyV";
            var respdata = 三国演义吞噬无界.DecodeResponseData(aEncodeRespData, out keyId);
            Assert.IsNotNull(respdata);
            respdata_encode = 三国演义吞噬无界.EncodeResponseData(respdata.ToString(Newtonsoft.Json.Formatting.None), aEncodeRespData.Substring(32, 62), keyId);
           // Assert.AreEqual(respdata, aEncodeRespData);

            Assert.Pass();
        }
        [Test]
        public void Test3()
        {
            int keyId;            
            string aEncodeRespData = "2b6274317f125fe7c0c4832238b513838f73tdQ81tTrN4RRhUCSpTY96yr10uysTugtH5AepQiGi3PxlcAJSgUegYWVMOXmgbT5Kw41GAP7WgTUDZd5mN5wKZTvxBt6iwfwKWNytAfy46dyZm9KgbT5KwHQLlTUDZdyZWdnOin3DZT5est6qrTUDZNUTWfyOaNKgb23**";
            var respdata = 三国演义吞噬无界.DecodeResponseData(aEncodeRespData, out keyId);
            Assert.IsNotNull(respdata);
            string respdata_enc = 三国演义吞噬无界.EncodeResponseData(respdata.ToString(Newtonsoft.Json.Formatting.None), aEncodeRespData.Substring(32, 62), keyId);
           // Assert.AreEqual(respdata, aEncodeRespData);

            Assert.Pass();
        }
    }
}