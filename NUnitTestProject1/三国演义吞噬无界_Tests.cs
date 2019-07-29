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
            string aEncodePostData = "1004bea0cb4b5864856739aea2e21672956E1EBv4iM11PnsKRq9UznOF6BIBsOauAVmAiOQKWL737QmzINz3pTuQoLXKlGJ9XoARhcpYfY_Y9lAzb60E_zDYdlgvfxtZdl_i_18Bul_-dlmY46Ej_zDYr1rzuzr1dwpPfw4Pdx_UdxjZfxOPdxdorw8Ed1OmrcdY9lAitcJT_zDYX12Y9lAEt6aM_zDodPjodPpJLp**";
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
            //string aEncodeRespData = "b630823baf76c2190d80f92ab99ace83bQE43Fh1Csip46v7DIZMALs4fyKyu4A3tHmHD6zDAUjXUszgjiGvER2pozPvUJLmgPA0Kw415jWJVgAzU3dwmQ0wK3Ah2ELGnwfwKwlUuVlu6xYYodlroQltu7lM1UlYkglUuyl9waYYoIYYoAdXKgYYoxlMoUlYuZlp-wlroQYYopp6tkMh2741Bkt6_aAwmQ0wK3A15zpeBwfwKodqd6djHjqnr_";
            string aEncodeRespData = "aacde6b764841c92df431069da9f899bDwHTh9MzoJSN9bL1FfcrWZ1UP9NB3JJ6BbIi9cAxf20S2VRsOuUsB68EuM38laXmgbUMKw4TckUzp7d0diVNp7UM9mePYwfwKwUwmtMwK7UB9CewU3UVKiVNp7UM9DrIAwfwKmaKgbUMKwrIcz5oU3UVORtzNDtDUDdVUiVNp7UM9YePYwfwKWfVOmtwmtMwK7U1RDQoU3UZitMwK7UM2jVNp7UMK7UMKw5JNwfwKwdDdDUwmtMwK7UMK7UM9g5PsiQFU3UMaczpBcq-PBl3_lh5mwaKgbUMK7UMK7UBsxrMU3UMUwaKgbUMK7UMK7U1sqrIAwfwKwUwmtMwK7UMK7UM9l-Bso-oU3UMUwaKgbUMK7UMK7U19q46AwfwKwXRmw5JRBPMU3PMUotzU6d1AWQMgxfVARa0N8tDOCeJdWdMRqdJAWQ1eWf0Y6dIek4I8BPMUiPM9l-Jrvru94Uzl4Uz2qtVZ1Q1ODaJeTtVUCtVsqewgxQzqqa0eTe6Q1e0YRfVrwdM8m-1r4UwW4U1tn-BtR-JGAXP2c4RmwfciRaVAitomTaV7ifMmWtwmWtcgiPM9z-68DrJRc0BGC4RmwfciodMmWdVKidFmoaVOidwmRaVOmPFW4U1cYdBt4Uzl-JDARd0QraZiWdVA8PFW-d0O8PFW-d0KRfGgiJDtraZiRt0O6PFW-d0NoPFW-d0KRfGRraZmw-BGCdBt4Uzl-JDAmPFW-d02raZiWdZgiJDOmPFW-dD2raZiRdZgiJDGraZiWdZRraZmw46ckQ6Wc4RmwfcimaVKidMmmaVKidMmmaV2rvFUiVNp7UMK7UMKwQIZgQ0KwfwKwdzKWfFLTaDU6UVK3dVK3dVKwaKgbUMK7UMK7U1sqrIAWUzp7UzUmd0YnfMLWUVUDfzARfzKmUwmtMwK7UMK7UM9m41cn41cgXFU3UMUCtFUiVNp7UMK7UMKw5JRBUzp7Uz46t0qwd0O6a0KWtIOCtVNTtMRqd02Ya04DfIOTf0A8QzUgQF8m-14waKgbUMK7UMK7UBs84IAwfwKwd0KmtMUiVNp7UMK7UMKwrJYwfwKwd0KDU7gbUMK7UugiVNp7UMK7XmgbUMK7UMK7U1cYUzp7UzeWdMUiVNp7UMK7UMKwrIcg-IAwfwKw8e6G83oA8eJZ8eMLUwmtMwK7UMK7UM9gXuNwfwKwUwmtMwK7UMK7UM9YePscUzp7UwUiVNp7UMK7UMKw5J8g41LwfwKwUwmtMwK7UMK7UM9wePtcUzp7UBC4U1cCQRmwfcmwd6sztVZzdD4Ct0QctMggdD21a07WQzUCeJtqe0ATdINRezKWaB2kQRmwaZmw5JRBPTsoPMU3PM9wQIdxe6twewgWQzQqa0NWtDeCfVOoQFRwQJQwQJdgfVQct6ek4I8BPMUiPM9gXP2cPMU3dFW4U1cYdBt4Uzl-JDARd09raZiRt0OoaVARd0QraZiRt0OoaVARd0QraZiRt0OoaVARd0QraZiRt0OoaVARd0QraZiRt0OoaVARd0QrPFW4U18R-09DPMU3JRi6PFW-dDKifZgiJDOmfMmWfZgiJDUmfMmRdZgiJDdxfMmWdV2raZi6tV7idzKmPGgiPM9mePcDPMU3JDeidDKid0KxaVUmfMmDfV7itzNxPFW4UBtl-1riQPt4Uzl-dMmmaVKidMmmaV2raZmwe6HR-BsDPMU3JDAitFmRaVAitFmDPPgwaKgbUMK7UMK7U1sqrIAmUzp7UzUmd0YntoLotwKmfzKmfzKmUwmtMwK7UMK7UM9YePscdFU3UMUodVO8aD7ndFKodDpRf0pmdMUiVNp7UMK7UMKw4u9l-T9lruYwfwKwdMUiVNp7UMK7UMKw5JRBUzp7U1UmtDd6tz4xaJGYfIACtV7mfMg8Q1QcaJdoQJdRez7xfIZwtF8m-14waKgbUMK7UMK7UBs84IAwfwKwd0KmtFUiVNp7UMK7UMKwrJYwfwKwd0KDU7gbUMK7UugiVNp7UMK7XmgbUMK7UMK7U1cYUzp7UzODdVUwaKgbUMK7UMK7UBslrIWcUzp7Uk5-Cf5JifX1zSJUhFUiVNp7UMK7UMKwruqgUzp7Uk52yXJJBfFxkSJZyf5-Cf5JifwuiS547f5JifX9wf54yfSLzfwE1X5eyS5-Cf5JifX1zSJUhXSLzfwnCSFxkSJZyfXiYXX3iSSL7FUiVNp7UMK7UMKwQIZgQFU3UMUwaKgbUMK7UMK7U1ckru9nUzp7UwUiVNp7UMK7UMKwe1ZDQFU3UM9jPM9lQut4Uzl-tGgiPM9krJRDPMU3JDUmdV2raZmwe6HYQGmwfcmwPM9HUwmtMwK7UMK7UM9YePscdMU3UMUodVO8aD4ntFKmfzKmfzKmUwmtMwK7UMK7UM9YePscdFU3UMUodVO8aD4ndDO7dzd3t0Y3t0YwaKgbUMK7UMK7UB2o5JHo5Ps8Uzp7UzKwaKgbUMK7UMK7U1cCQoU3UMUwaKgbUMK7UMK7UBs84IAwfwKwd0KmdFUiVNp7UMK7UMKwrJYwfwKwdMUtMwK7UM2HaKgbUMK7UuitMwK7UMK7UM9lQMU3UMUWdDNgUwmtMwK7UMK7UM9g5PsiQFU3UMaciU-Yk30cqQu1z5UwaKgbUMK7UMK7UBsxrMU3UMUwaKgbUMK7UMK7U1sqrIAwfwKwUwmtMwK7UMK7UM9l-Bso-oU3UMUwaKgbUMK7UMK7U19q46AwfwKwXRmw5JRBPMU3PM9cezr1dzUotMRqe6A8a0scf04CfJsztMRcQz4mdz2wt0UTd0Ok4I8BPMUiPM9l-Jrvru94Uzl4U1eDezeWeDYWa0Yge6UCtI9YtFgxQVYgaJeRt076d0Z1d0Y8fM8m-1r4UwW4UBtT5Psz5OcYdBt4Uzl-JDARd0QraZiRt0O6PFW-t0AWtcgiJDARd0QraZiRt0O6PFW-t0AWdcgiJDARd09raZiRt0OoPFW-t0AWdcgiJDARd09raZiRt0OoaVARd0QraZiRt0OoaVARd0QraZiRt0OoaVARd0QraZiRt0OoaVARd0QraZiRt0OoaVARd0QraZiRt0OoaVARd0QraZiRt0OoaVARd0QrPFW4UBtT5Psz5O8R-09DPMU3JRiWPFW-dD2raZiWdZgiJDOmPFW-d0KmPFW-dGgiJDGraZi6PFW-d0GraZiodZgiJDdmaVqraZiDdMmxPFW-t0KifZgiJDemaVOxPFW-d0AmaVd6PFW-dzAmaVe6PFW-dDAmaV7xPGgiPM9lQut4Uzl-dwmWtVUidzKTaVORfVdidzAxdomWt0Uid0YoaVYodMmotz7id0UgaVYWtFm8d0Nid0doaVYWdwmotDUif0UgaVYodcgiPM9krJRDPMU3JDOmdVKmaVOid0KidFmWaVOidFmWdMmWaVAitFmRaVUitFmWaVOidGgiPM9z-TGkrut4Uzl-t0Kid0Kit0KidDKidFmRdMmRdMmTaVAidFmDdMmDdMmoaVORaVOmaVeitcRHUwmtMwK7UMK7UM9YePscdMU3UMUodVO8aD4ndze7dVpmdVpmdMUiVNp7UMK7UMKwQIZgQ0OwfwKwdzKWfFLxaDO7dzd3t0Y3dVKwaKgbUMK7UMK7UB2o5JHo5Ps8Uzp7UwgDUwmtMwK7UMK7UM9l-J4wfwKwtDUodI9zdD7Cd09YfMggtIADaJOxdVYCtJZcQzcYdDrqQzqqaB2kQoUiVNp7UMK7UMKwrucmQFU3UMUWdVK8UwmtMwK7UMK7UM9R5FU3UMUWdVdwVNp7UMK7vFmtMwK7UM2jVNp7UMK7UMKw5JNwfwKwd0dgfMUiVNp7UMK7UMKwrIcg-IAwfwKw83JX83Xe8QJI8-hPUwmtMwK7UMK7UM9gXuNwfwKwUwmtMwK7UMK7UM9YePscUzp7UwUiVNp7UMK7UMKw5J8g41LwfwKwUwmtMwK7UMK7UM9wePtcUzp7UBC4U1cCQRmwfcmwt6ADdIeDez7CdJeWQMggezAWaJ9wdJeCfIdRezAmd6QYtIQYaB2kQRmwaZmw5JRBPTsoPMU3PMUodVeoQ0doeoR1d0rYa0sYezOCfVcYfFgmQ04gtzYgdJOWdD4k4I8BPMUiPM9lQut4Uzl-dDK6dMmot07DaVODtFmodVAid0ATaVO8dcgiPM9krJRDPMU3JDAidFmWaVOmaVOmaVZraZmw4TrlrItpFJsDPMU3JDOidFmWaVUidFmWPFW4UBtT5Psz5O8R-Pt4Uzl-d0AmdMmodVKmaVOmdMmWdVKmaVNmdMmRdV2raZmwe6HR-BsDPMU3JDUmaVOitFmWdMmWdMmDPPgwaKgbUMK7UMK7U1sqrIAmUzp7UzUmd0YntoLofFKmfzKmfzKmUwmtMwK7UMK7UM9YePscdFU3UMUodVO8aD4ndzY7dzd3t0Y3dVKwaKgbUMK7UMK7UB2o5JHo5Ps8Uzp7UwgWUwmtMwK7UMK7UM9l-J4wfwKwezURtDGYQVOCtV9cfFggf0Z1aJ9wfIACdVQ1e6Q1eJtctIZzaB2kQoUiVNp7UMK7UMKwrucmQFU3UMUWdVKTUwmtMwK7UMK7UM9R5FU3UMUWdVdwVNp7UMK7vFmtMwK7UM2jVNp7UMK7UMKw5JNwfwKwd0dRtFUiVNp7UMK7UMKwrIcg-IAwfwKw8-MI8lXP3bIc8eIEUwmtMwK7UMK7UM9gXuNwfwKw88FW8ahf8l5m8x1U8pwe8l5P8awC8lX78yfG8p3939MH8-MI8lXPjjod8hS_8lXc3bIc8eSs8pwe8l5P8eSn3U3T8-3P88hO8-MI8lXPUwmtMwK7UMK7UM9YePscUzp7UwUiVNp7UMK7UMKw5J8g41LwfwKw3bIc8eSs8hS_8lXc3U3T8-3P8-MI8lXPUwmtMwK7UMK7UM9wePtcUzp7UBC4U1cY4RmwfciRt0O6PFW4U18R-Pt4Uzl-d0KmPFW4U1tnQIG4Uzl4UcmwvFUiVNp7UMK7UMKwQIZgQ0KwfwKwdzKWfFLTaDU8UVK3dVK3dVKwaKgbUMK7UMK7U1sqrIAWUzp7UzUmd0YntoLofFKodDpRf0pRfFUiVNp7UMK7UMKw4u9l-T9lruYwfwKwdMUiVNp7UMK7UMKw5JRBUzp7UwUiVNp7UMK7UMKwrucmQFU3UMUWdVKWUwmtMwK7UMK7UM9R5FU3UMUmU7gbUMK7UugtMwK7PFmtMwK7U1Gxe6WRQIGt464wfwKwUwmtMwK7UB9c4TGirMU3UVKiVNp7UM9ce6qnUzp7dzdRdVNRd7gbvN**";
            var respdata = 三国演义吞噬无界.DecodeResponseData(aEncodeRespData, out keyId);
            Assert.IsNotNull(respdata);
            string respdata_enc = 三国演义吞噬无界.EncodeResponseData(respdata.ToString(Newtonsoft.Json.Formatting.None), aEncodeRespData.Substring(32, 62), keyId);
           // Assert.AreEqual(respdata, aEncodeRespData);

            Assert.Pass();
        }
    }
}