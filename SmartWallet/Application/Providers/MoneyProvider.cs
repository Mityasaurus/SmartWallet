﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using SmartWallet.AL.API;
using SmartWallet.Aplication.API;
using SmartWallet.DAL.Entity;

namespace SmartWallet.Providers;

public class MoneyProvider
{
    public static Dictionary<Currency, string> Symbols = new Dictionary<Currency, string>()
    {
        { Currency.AFN,	"Af" },
        { Currency.AMD,	"AMD" },
        { Currency.ANG,	"ƒ" },
        { Currency.AOA,	"Kz" },
        { Currency.ARS,	"AR$" },
        { Currency.AUD,	"AU$" },
        { Currency.AWG,	"Afl." },
        { Currency.AZN,	"man." },
        { Currency.BAM,	"KM" },
        { Currency.BBD,	"Bds$" },
        { Currency.BDT,	"Tk" },
        { Currency.BGN,	"BGN" },
        { Currency.BHD,	"BD" },
        { Currency.BIF,	"FBu" },
        { Currency.BMD,	"BD$" },
        { Currency.BND,	"BN$" },
        { Currency.BOB,	"Bs" },
        { Currency.BRL,	"R$" },
        { Currency.BSD,	"B$" },
        { Currency.BWP,	"BWP" },
        { Currency.BYN,	"Br" },
        { Currency.BYR,	"BYR" },
        { Currency.BZD,	"BZ$" },
        { Currency.CAD,	"CA$" },
        { Currency.CDF,	"CDF" },
        { Currency.CHF,	"CHF" },
        { Currency.CLF,	"UF" },
        { Currency.CLP,	"CL$" },
        { Currency.CNY,	"CN¥" },
        { Currency.COP,	"CO$" },
        { Currency.CRC,	"₡" },
        { Currency.CUC,	"CUC$" },
        { Currency.CUP,	"$MN" },
        { Currency.CVE,	"CV$" },
        { Currency.CZK,	"Kč" },
        { Currency.DJF,	"Fdj" },
        { Currency.DKK,	"Dkr" },
        { Currency.DOP,	"RD$" },
        { Currency.DZD,	"DA" },
        { Currency.ERN,	"Nfk" },
        { Currency.ETB,	"Br" },
        { Currency.EUR,	"€" },
        { Currency.FJD,	"FJ$" },
        { Currency.FKP,	"FK£" },
        { Currency.GBP,	"£" },
        { Currency.GEL,	"GEL" },
        { Currency.GGP,	"£" },
        { Currency.GHS,	"GH₵" },
        { Currency.GIP,	"£" },
        { Currency.GMD,	"D" },
        { Currency.GNF,	"FG" },
        { Currency.GTQ,	"GTQ" },
        { Currency.GYD,	"G$" },
        { Currency.HKD,	"HK$" },
        { Currency.HNL,	"HNL" },
        { Currency.HRK,	"kn" },
        { Currency.HTG,	"G" },
        { Currency.HUF,	"Ft" },
        { Currency.IDR,	"Rp" },
        { Currency.ILS,	"₪" },
        { Currency.IMP,	"£" },
        { Currency.IQD,	"IQD" },
        { Currency.IRR,	"IRR" },
        { Currency.ISK,	"Ikr" },
        { Currency.JEP,	"£" },
        { Currency.JMD,	"J$" },
        { Currency.JOD,	"JD" },
        { Currency.JPY,	"¥" },
        { Currency.KES,	"Ksh" },
        { Currency.KGS,	"KGS" },
        { Currency.KHR,	"KHR" },
        { Currency.KMF,	"CF" },
        { Currency.KPW,	"₩" },
        { Currency.KRW,	"₩" },
        { Currency.KWD,	"KD" },
        { Currency.KYD,	"CI$" },
        { Currency.KZT,	"KZT" },
        { Currency.LAK,	"₭N" },
        { Currency.LBP,	"LB£" },
        { Currency.LKR,	"SLRs" },
        { Currency.LRD,	"LD$" },
        { Currency.LSL,	"L" },
        { Currency.LTL,	"Lt" },
        { Currency.LVL,	"Ls" },
        { Currency.MAD,	"MAD" },
        { Currency.MDL,	"MDL" },
        { Currency.XAG,	"XAG" },
        { Currency.XAU,	"XAU" },
        { Currency.XCD,	"EC$" },
        { Currency.XDR,	"SDR" },
        { Currency.XOF,	"CFA" },
        { Currency.XPF,	"CFP" },
        { Currency.YER,	"YR" },
        { Currency.ZAR,	"R" },
        { Currency.ZMK,	"ZK" },
        { Currency.ZMW,	"ZK" },
        { Currency.ZWL,	"ZWL" },
        { Currency.XPT,	"XPT" },
        { Currency.XPD,	"XPD" },
        { Currency.BTC,	"₿" },
        { Currency.ETH,	"Ξ" },
        { Currency.BNB,	"BNB" },
        { Currency.XRP,	"XRP" },
        { Currency.SOL,	"SOL" },
        { Currency.DOT,	"DOT" },
        { Currency.APT,	"APT" },
        { Currency.MATIC, "MATIC" },
        { Currency.LTC,	"Ł" },
        { Currency.ADA,	"ADA" },
        { Currency.USDT, "USDT" },
        { Currency.USDC, "USDC" },
        { Currency.DAI,	"DAI" },
        { Currency.BUSD, "BUSD" },
        { Currency.ARB,	"ARB" },
        { Currency.OP, "OP" },
        { Currency.AED,	"AED" },
        { Currency.BTN,	"Nu." },
        { Currency.MKD,	"MKD" },
        { Currency.MMK,	"MMK" },
        { Currency.MNT,	"₮" },
        { Currency.MOP,	"MOP$" },
        { Currency.MRO,	"UM" },
        { Currency.MUR,	"MURs" },
        { Currency.MVR,	"MRf" },
        { Currency.MWK,	"MK" },
        { Currency.MXN,	"MX$" },
        { Currency.MZN,	"MTn" },
        { Currency.NAD,	"N$" },
        { Currency.NGN,	"₦" },
        { Currency.NIO,	"C$" },
        { Currency.NOK,	"Nkr" },
        { Currency.NPR,	"NPRs" },
        { Currency.NZD,	"NZ$" },
        { Currency.OMR,	"OMR" },
        { Currency.PAB,	"B/." },
        { Currency.PEN,	"S/." },
        { Currency.PGK,	"K" },
        { Currency.PHP,	"₱" },
        { Currency.PKR,	"PKRs" },
        { Currency.PLN,	"zł" },
        { Currency.PYG,	"₲" },
        { Currency.QAR,	"QR" },
        { Currency.RON,	"RON" },
        { Currency.RSD,	"din." },
        { Currency.RUB,	"RUB" },
        { Currency.RWF,	"RWF" },
        { Currency.SAR,	"SR" },
        { Currency.SCR,	"SRe" },
        { Currency.SDG,	"SDG" },
        { Currency.SEK,	"Skr" },
        { Currency.SGD,	"S$" },
        { Currency.SHP,	"£" },
        { Currency.SLL,	"Le" },
        { Currency.SOS,	"Ssh" },
        { Currency.SRD,	"$" },
        { Currency.STD,	"Db" },
        { Currency.SVC,	"₡" },
        { Currency.SYP,	"SY£" },
        { Currency.SZL,	"L" },
        { Currency.THB,	"฿" },
        { Currency.TJS,	"TJS" },
        { Currency.TMT,	"T" },
        { Currency.TND,	"DT" },
        { Currency.TOP,	"T$" },
        { Currency.TRY,	"TL" },
        { Currency.TTD,	"TT$" },
        { Currency.TWD,	"NT$" },
        { Currency.TZS,	"TSh" },
        { Currency.UAH,	"₴" },
        { Currency.UGX,	"USh" },
        { Currency.USD,	"$" },
        { Currency.UYU,	"$U" },
        { Currency.VEF,	"Bs.F." },
        { Currency.VND,	"₫" },
        { Currency.VUV,	"VUV" },
        { Currency.WST,	"WS$" },
        { Currency.XAF,	"FCFA" },
        { Currency.EGP,	"EGP" },
        { Currency.INR,	"Rs" },
        { Currency.LYD,	"LD" },
        { Currency.MGA,	"MGA" },
        { Currency.MYR,	"RM" },
        { Currency.SBD,	"SI$" },
        { Currency.UZS,	"UZS" },
    };

    public static Dictionary<Currency, string> CryptoNames = new Dictionary<Currency, string>()
    {
        { Currency.BTC, "bitcoin" },
        { Currency.ETH, "ethereum" },
        { Currency.BNB, "binancecoin" },
        { Currency.XRP, "ripple" },
        { Currency.SOL, "solana" },
        { Currency.DOT, "polkadot" },
        { Currency.APT, "aptos" },
        { Currency.MATIC, "matic-network" },
        { Currency.LTC, "litecoin" },
        { Currency.ADA, "cardano" },
        { Currency.USDT, "tether" },
        { Currency.USDC, "usd-coin" },
        { Currency.DAI, "dai" },
        { Currency.BUSD, "binance-usd" },
        { Currency.ARB, "arbitrum" },
        { Currency.OP, "optimism" },
    };

    public static double GetRate(Currency from, Currency to)
    {
        if (from <= Currency.UZS && to <= Currency.UZS)
        {
            ApiEngineMoney engine = new ApiEngineMoney();
            string json = engine.getRateJson(from, to, DateTime.Now);
            JObject jObject = JObject.Parse(json);
            if ((bool)jObject["success"]) return (double)jObject["result"];
        }
        else if(from >= Currency.BTC && to >= Currency.BTC)
        {
            ApiEngineCrypto engine = new ApiEngineCrypto();
            string json = engine.getRateJson(from, to);
            JObject jObject = JObject.Parse(json);

            if(to == Currency.BTC)
            {
                return (double)jObject[MoneyProvider.CryptoNames[from]]["btc"];
            }

            json = engine.getRateJson(from, Currency.BTC);
            jObject = JObject.Parse(json);
            double rate1 = (double)jObject[MoneyProvider.CryptoNames[from]]["btc"];

            json = engine.getRateJson(to, Currency.BTC);
            jObject = JObject.Parse(json);
            double rate2 = (double)jObject[MoneyProvider.CryptoNames[to]]["btc"];

            double rate = rate1 / rate2;

            return rate;
        }
        else
        {
            ApiEngineMoney engineMoney = new ApiEngineMoney();
            ApiEngineCrypto engineCrypto = new ApiEngineCrypto();

            Currency money;
            Currency crypto;

            if (from <= Currency.UZS)
            {
                money = from;
                crypto = to;
            }
            else
            {
                money = to;
                crypto = from;
            }

            string json = engineMoney.getRateJson(money, Currency.USD, DateTime.Now);
            JObject jObject = JObject.Parse(json);
            double rate1 = (double)jObject["result"];

            json = engineCrypto.getRateJson(crypto, Currency.USD);
            jObject = JObject.Parse(json);
            double rate2 = (double)jObject[MoneyProvider.CryptoNames[crypto]]["usd"];

            double rate;

            if (from <= Currency.UZS)
            {
                rate = rate1 / rate2;
            }
            else
            {
                rate = rate2 / rate1;
            }

            return rate;
        }


        return 0;
    }
}