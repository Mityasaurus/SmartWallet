﻿using System.Collections.Generic;
using SmartWallet.DAL.Entity;

namespace SmartWallet.Providers;

public class MoneyProvider
{
    public static Dictionary<Currency, string> Symbols = new Dictionary<Currency, string>()
    {
        { Currency.AFN, "؋" },
        { Currency.ARS,	"$" },                         
        { Currency.AWG,	"ƒ" },
        { Currency.AUD,	"$" },
        { Currency.AZN,	"₼" },
        { Currency.BSD,	"$" },
        { Currency.BBD,	"$" },
        { Currency.BZD,	"BZ$" },
        { Currency.BMD,	"$" },
        { Currency.BOB,	"$b" },
        { Currency.BAM,	"KM" },
        { Currency.BWP,	"P" },
        { Currency.BGN,	"лв" },
        { Currency.BRL,	"R$" },
        { Currency.BND,	"$" },
        { Currency.KHR,	"៛" },
        { Currency.CAD,	"$" },
        { Currency.KYD,	"$" },
        { Currency.CLP,	"$" },
        { Currency.CNY,	"¥" },
        { Currency.COP,	"$" },
        { Currency.CRC,	"₡" },
        { Currency.HRK,	"kn" },
        { Currency.CUP,	"₱" },
        { Currency.CZK,	"Kč" },
        { Currency.DKK,	"kr" },
        { Currency.DOP,	"RD$" },
        { Currency.XCD,	"$" },
        { Currency.EGP,	"£" },
        { Currency.SVC,	"$" },
        { Currency.EUR,	"€" },
        { Currency.FKP,	"£" },
        { Currency.FJD,	"$" },
        { Currency.GHS,	"¢" },
        { Currency.GIP,	"£" },
        { Currency.GTQ,	"Q" },
        { Currency.GGP,	"£" },
        { Currency.GYD,	"$" },
        { Currency.HNL,	"L" },
        { Currency.HKD,	"$" },
        { Currency.HUF,	"Ft" },
        { Currency.ISK,	"kr" },
        { Currency.INR,	"₹" },
        { Currency.IDR,	"Rp" },
        { Currency.IRR, "﷼" },	
        { Currency.IMP,	"£" },
        { Currency.ILS,	"₪" },
        { Currency.JMD,	"J$" },
        { Currency.JPY,	"¥" },
        { Currency.JEP,	"£" },
        { Currency.KZT,	"лв" },
        { Currency.KPW,	"₩" },
        { Currency.KRW,	"₩" },
        { Currency.KGS,	"лв" },
        { Currency.LAK,	"₭" },
        { Currency.LBP,	"£" },
        { Currency.LRD,	"$" },
        { Currency.MKD,	"ден" },
        { Currency.MYR,	"RM" },
        { Currency.MUR,	"₨" },
        { Currency.MXN,	"$" },
        { Currency.MNT,	"₮" },
        { Currency.MNT, "د.إ" }, 
        { Currency.MZN,	"MT" },
        { Currency.NAD,	"$" },
        { Currency.NPR,	"₨" },
        { Currency.ANG,	"ƒ" },
        { Currency.NZD,	"$" },
        { Currency.NIO,	"C$" },
        { Currency.NGN,	"₦" },
        { Currency.NOK,	"kr" },
        { Currency.OMR, "﷼" },
        { Currency.PKR,	"₨" },
        { Currency.PAB,	"B/." },
        { Currency.PYG,	"Gs" },
        { Currency.PEN,	"S/." },
        { Currency.PHP,	"₱" },
        { Currency.PLN,	"zł" },
        { Currency.QAR, "﷼" },
        { Currency.RON,	"lei" },
        { Currency.RUB,	"₽" },
        { Currency.SHP,	"£" },
        { Currency.SAR, "﷼" },
        { Currency.RSD,	"Дин." },
        { Currency.SCR,	"₨" },
        { Currency.SGD,	"$" },
        { Currency.SBD,	"$" },
        { Currency.SOS,	"S" },
        { Currency.KRW,	"₩" },
        { Currency.ZAR,	"R" },
        { Currency.LKR,	"₨" },
        { Currency.SEK,	"kr" },
        { Currency.CHF,	"CHF" },
        { Currency.SRD,	"$" },
        { Currency.SYP,	"£" },
        { Currency.TWD,	"NT$" },
        { Currency.THB,	"฿" },
        { Currency.TTD,	"TT$" },
        { Currency.TRY,	"₺" },
        { Currency.TVD,	"$" },
        { Currency.UAH,	"₴" },
        { Currency.AED, "د.إ" }, 
        { Currency.GBP,	"£" },
        { Currency.USD,	"$" },
        { Currency.UYU,	"$U" },
        { Currency.UZS,	"лв" },
        { Currency.VEF,	"Bs" },
        { Currency.VND,	"₫" },
        { Currency.YER, "﷼" },
        { Currency.ZWD,	"Z$" },
    };
}