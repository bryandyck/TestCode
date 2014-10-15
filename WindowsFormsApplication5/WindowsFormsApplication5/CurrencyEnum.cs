using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Palantir.Framework;
using System.Reflection;

namespace WindowsFormsApplication5
{
    public static class CurrencyEnumHelper
    {
        public static string GetDisplayString(Currency currency, ScaleSize unitScale)
        {
            MemberInfo[] memInfos = currency.GetType().GetMember(currency.ToString());

            if (memInfos != null && memInfos.Length > 0)
            {
                object[] attrs = memInfos[0].GetCustomAttributes(typeof(CurrencyDisplayStringAttribute), false);

                if (attrs != null && attrs.Length > 0)
                {
                    CurrencyDisplayStringAttribute attrib = attrs[0] as CurrencyDisplayStringAttribute;
                    switch (unitScale)
                    {
                        case ScaleSize.Small: return attrib.SmallString;
                        case ScaleSize.Large: return attrib.LargeString;
                        default: return attrib.MediumString;
                    }
                }
            }

            return "";
        }

        public static bool GetDefaultVisibility(Currency currency)
        {
            MemberInfo[] memInfos = currency.GetType().GetMember(currency.ToString());

            if (memInfos != null && memInfos.Length > 0)
            {
                object[] attrs = memInfos[0].GetCustomAttributes(typeof(CurrencyVisibleByDefaultAttribute), false);

                if (attrs != null && attrs.Length > 0)
                {
                    CurrencyVisibleByDefaultAttribute attrib = attrs[0] as CurrencyVisibleByDefaultAttribute;
                    return attrib.Visible;
                }
            }

            return false;
        }

    }

    public enum Currency
    {
        [Description("United Arab Emirates dirham"), CurrencyDisplayString("AED", "M AED", "MM AED"), CurrencyVisibleByDefault(false)]
        UnitedArabEmiratesDirham = 0,
        [Description("Afghani"), CurrencyDisplayString("AFN", "M AFN", "MM AFN"), CurrencyVisibleByDefault(false)]
        Afghani = 1,
        [Description("Lek"), CurrencyDisplayString("ALL", "M ALL", "MM ALL"), CurrencyVisibleByDefault(false)]
        Lek = 2,
        [Description("Armenian dram"), CurrencyDisplayString("AMD", "M AMD", "MM AMD"), CurrencyVisibleByDefault(false)]
        ArmenianDram = 3,
        [Description("Netherlands Antillean guilder"), CurrencyDisplayString("ANG", "M ANG", "MM ANG"), CurrencyVisibleByDefault(false)]
        NetherlandsAntilleanGuilder = 4,
        [Description("Kwanza"), CurrencyDisplayString("AOA", "M AOA", "MM AOA"), CurrencyVisibleByDefault(false)]
        Kwanza = 5,
        [Description("Argentine peso"), CurrencyDisplayString("ARS", "M ARS", "MM ARS"), CurrencyVisibleByDefault(false)]
        ArgentinePeso = 6,
        [Description("Australian dollar"), CurrencyDisplayString("AUD", "M AUD", "MM AUD"), CurrencyVisibleByDefault(true)]
        AustralianDollar = 7,
        [Description("Aruban guilder"), CurrencyDisplayString("AWG", "M AWG", "MM AWG"), CurrencyVisibleByDefault(false)]
        ArubanGuilder = 8,
        [Description("Azerbaijanian manat"), CurrencyDisplayString("AZN", "M AZN", "MM AZN"), CurrencyVisibleByDefault(false)]
        AzerbaijanianManat = 9,
        [Description("Convertible marks"), CurrencyDisplayString("BAM", "M BAM", "MM BAM"), CurrencyVisibleByDefault(false)]
        ConvertibleMarks = 10,
        [Description("Barbados dollar"), CurrencyDisplayString("BBD", "M BBD", "MM BBD"), CurrencyVisibleByDefault(false)]
        BarbadosDollar = 11,
        [Description("Bangladeshi taka"), CurrencyDisplayString("BDT", "M BDT", "MM BDT"), CurrencyVisibleByDefault(false)]
        BangladeshiTaka = 12,
        [Description("Bulgarian lev"), CurrencyDisplayString("BGN", "M BGN", "MM BGN"), CurrencyVisibleByDefault(false)]
        BulgarianLev = 13,
        [Description("Bahraini dinar"), CurrencyDisplayString("BHD", "M BHD", "MM BHD"), CurrencyVisibleByDefault(false)]
        BahrainiDinar = 14,
        [Description("Burundian franc"), CurrencyDisplayString("BIF", "M BIF", "MM BIF"), CurrencyVisibleByDefault(false)]
        BurundianFranc = 15,
        [Description("Bermuda dollar"), CurrencyDisplayString("BMD", "M BMD", "MM BMD"), CurrencyVisibleByDefault(false)]
        BermudaDollar = 16,
        [Description("Brunei dollar"), CurrencyDisplayString("BND", "M BND", "MM BND"), CurrencyVisibleByDefault(false)]
        BruneiDollar = 17,
        [Description("Boliviano"), CurrencyDisplayString("BOB", "M BOB", "MM BOB"), CurrencyVisibleByDefault(false)]
        Boliviano = 18,
        [Description("Brazilian real"), CurrencyDisplayString("BRL", "M BRL", "MM BRL"), CurrencyVisibleByDefault(true)]
        BrazilianReal = 19,
        [Description("Bahamian dollar"), CurrencyDisplayString("BSD", "M BSD", "MM BSD"), CurrencyVisibleByDefault(false)]
        BahamianDollar = 20,
        [Description("Ngultrum"), CurrencyDisplayString("BTN", "M BTN", "MM BTN"), CurrencyVisibleByDefault(false)]
        Ngultrum = 21,
        [Description("Pula"), CurrencyDisplayString("BWP", "M BWP", "MM BWP"), CurrencyVisibleByDefault(false)]
        Pula = 22,
        [Description("Belarussian ruble"), CurrencyDisplayString("BYR", "M BYR", "MM BYR"), CurrencyVisibleByDefault(false)]
        BelarussianRuble = 23,
        [Description("Belize dollar"), CurrencyDisplayString("BZD", "M BZD", "MM BZD"), CurrencyVisibleByDefault(false)]
        BelizeDollar = 24,
        [Description("Canadian dollar"), CurrencyDisplayString("CAD", "M CAD", "MM CAD"), CurrencyVisibleByDefault(true)]
        CanadianDollar = 25,
        [Description("Franc Congolais"), CurrencyDisplayString("CDF", "M CDF", "MM CDF"), CurrencyVisibleByDefault(false)]
        FrancCongolais = 26,
        [Description("Swiss franc"), CurrencyDisplayString("CHF", "M CHF", "MM CHF"), CurrencyVisibleByDefault(false)]
        SwissFranc = 27,
        [Description("Chilean peso"), CurrencyDisplayString("CLP", "M CLP", "MM CLP"), CurrencyVisibleByDefault(false)]
        ChileanPeso = 28,
        [Description("Renminbi"), CurrencyDisplayString("CNY", "M CNY", "MM CNY"), CurrencyVisibleByDefault(false)]
        Renminbi = 29,
        [Description("Colombian peso"), CurrencyDisplayString("COP", "M COP", "MM COP"), CurrencyVisibleByDefault(false)]
        ColombianPeso = 30,
        [Description("Costa Rican colon"), CurrencyDisplayString("CRC", "M CRC", "MM CRC"), CurrencyVisibleByDefault(false)]
        CostaRicanColon = 31,
        [Description("Cuban peso"), CurrencyDisplayString("CUP", "M CUP", "MM CUP"), CurrencyVisibleByDefault(false)]
        CubanPeso = 32,
        [Description("Cape Verde escudo"), CurrencyDisplayString("CVE", "M CVE", "MM CVE"), CurrencyVisibleByDefault(false)]
        CapeVerdeEscudo = 33,
        [Description("Czech koruna"), CurrencyDisplayString("CZK", "M CZK", "MM CZK"), CurrencyVisibleByDefault(false)]
        CzechKoruna = 34,
        [Description("Djibouti franc"), CurrencyDisplayString("DJF", "M DJF", "MM DJF"), CurrencyVisibleByDefault(false)]
        DjiboutiFranc = 35,
        [Description("Danish krone"), CurrencyDisplayString("DKK", "M DKK", "MM DKK"), CurrencyVisibleByDefault(true)]
        DanishKrone = 36,
        [Description("Dominican peso"), CurrencyDisplayString("DOP", "M DOP", "MM DOP"), CurrencyVisibleByDefault(false)]
        DominicanPeso = 37,
        [Description("Algerian dinar"), CurrencyDisplayString("DZD", "M DZD", "MM DZD"), CurrencyVisibleByDefault(true)]
        AlgerianDinar = 38,
        [Description("Kroon"), CurrencyDisplayString("EEK", "M EEK", "MM EEK"), CurrencyVisibleByDefault(false)]
        Kroon = 39,
        [Description("Egyptian pound"), CurrencyDisplayString("EGP", "M EGP", "MM EGP"), CurrencyVisibleByDefault(true)]
        EgyptianPound = 40,
        [Description("Nakfa"), CurrencyDisplayString("ERN", "M ERN", "MM ERN"), CurrencyVisibleByDefault(false)]
        Nakfa = 41,
        [Description("Ethiopian birr"), CurrencyDisplayString("ETB", "M ETB", "MM ETB"), CurrencyVisibleByDefault(false)]
        EthiopianBirr = 42,
        [Description("Euro"), CurrencyDisplayString("EUR", "M EUR", "MM EUR"), CurrencyVisibleByDefault(true)]
        Euro = 43,
        [Description("Fiji dollar"), CurrencyDisplayString("FJD", "M FJD", "MM FJD"), CurrencyVisibleByDefault(false)]
        FijiDollar = 44,
        [Description("Falkland Islands pound"), CurrencyDisplayString("FKP", "M FKP", "MM FKP"), CurrencyVisibleByDefault(false)]
        FalklandIslandsPound = 45,
        [Description("Pound sterling"), CurrencyDisplayString("GBP", "M GBP", "MM GBP"), CurrencyVisibleByDefault(true)]
        PoundSterling = 46,
        [Description("Lari"), CurrencyDisplayString("GEL", "M GEL", "MM GEL"), CurrencyVisibleByDefault(false)]
        Lari = 47,
        [Description("Cedi"), CurrencyDisplayString("GHS", "M GHS", "MM GHS"), CurrencyVisibleByDefault(false)]
        Cedi = 48,
        [Description("Gibraltar pound"), CurrencyDisplayString("GIP", "M GIP", "MM GIP"), CurrencyVisibleByDefault(false)]
        GibraltarPound = 49,
        [Description("Dalasi"), CurrencyDisplayString("GMD", "M GMD", "MM GMD"), CurrencyVisibleByDefault(false)]
        Dalasi = 50,
        [Description("Guinea franc"), CurrencyDisplayString("GNF", "M GNF", "MM GNF"), CurrencyVisibleByDefault(false)]
        GuineaFranc = 51,
        [Description("Quetzal"), CurrencyDisplayString("GTQ", "M GTQ", "MM GTQ"), CurrencyVisibleByDefault(false)]
        Quetzal = 52,
        [Description("Guyana dollar"), CurrencyDisplayString("GYD", "M GYD", "MM GYD"), CurrencyVisibleByDefault(false)]
        GuyanaDollar = 53,
        [Description("Hong Kong dollar"), CurrencyDisplayString("HKD", "M HKD", "MM HKD"), CurrencyVisibleByDefault(false)]
        HongKongDollar = 54,
        [Description("Lempira"), CurrencyDisplayString("HNL", "M HNL", "MM HNL"), CurrencyVisibleByDefault(false)]
        Lempira = 55,
        [Description("Croatian kuna"), CurrencyDisplayString("HRK", "M HRK", "MM HRK"), CurrencyVisibleByDefault(false)]
        CroatianKuna = 56,
        [Description("Haiti gourde"), CurrencyDisplayString("HTG", "M HTG", "MM HTG"), CurrencyVisibleByDefault(false)]
        HaitiGourde = 57,
        [Description("Forint"), CurrencyDisplayString("HUF", "M HUF", "MM HUF"), CurrencyVisibleByDefault(false)]
        Forint = 58,
        [Description("Rupiah"), CurrencyDisplayString("IDR", "M IDR", "MM IDR"), CurrencyVisibleByDefault(true)]
        Rupiah = 59,
        [Description("Israeli new sheqel"), CurrencyDisplayString("ILS", "M ILS", "MM ILS"), CurrencyVisibleByDefault(false)]
        IsraeliNewSheqel = 60,
        [Description("Indian rupee"), CurrencyDisplayString("INR", "M INR", "MM INR"), CurrencyVisibleByDefault(true)]
        IndianRupee = 61,
        [Description("Iraqi dinar"), CurrencyDisplayString("IQD", "M IQD", "MM IQD"), CurrencyVisibleByDefault(false)]
        IraqiDinar = 62,
        [Description("Iranian rial"), CurrencyDisplayString("IRR", "M IRR", "MM IRR"), CurrencyVisibleByDefault(false)]
        IranianRial = 63,
        [Description("Iceland krona"), CurrencyDisplayString("ISK", "M ISK", "MM ISK"), CurrencyVisibleByDefault(false)]
        IcelandKrona = 64,
        [Description("Jamaican dollar"), CurrencyDisplayString("JMD", "M JMD", "MM JMD"), CurrencyVisibleByDefault(false)]
        JamaicanDollar = 65,
        [Description("Jordanian dinar"), CurrencyDisplayString("JOD", "M JOD", "MM JOD"), CurrencyVisibleByDefault(false)]
        JordanianDinar = 66,
        [Description("Japanese yen"), CurrencyDisplayString("JPY", "M JPY", "MM JPY"), CurrencyVisibleByDefault(false)]
        JapaneseYen = 67,
        [Description("Kenyan shilling"), CurrencyDisplayString("KES", "M KES", "MM KES"), CurrencyVisibleByDefault(false)]
        KenyanShilling = 68,
        [Description("Som"), CurrencyDisplayString("KGS", "M KGS", "MM KGS"), CurrencyVisibleByDefault(false)]
        Som = 69,
        [Description("Riel"), CurrencyDisplayString("KHR", "M KHR", "MM KHR"), CurrencyVisibleByDefault(false)]
        Riel = 70,
        [Description("Comoro franc"), CurrencyDisplayString("KMF", "M KMF", "MM KMF"), CurrencyVisibleByDefault(false)]
        ComoroFranc = 71,
        [Description("North Korean won"), CurrencyDisplayString("KPW", "M KPW", "MM KPW"), CurrencyVisibleByDefault(false)]
        NorthKoreanWon = 72,
        [Description("South Korean won"), CurrencyDisplayString("KRW", "M KRW", "MM KRW"), CurrencyVisibleByDefault(false)]
        SouthKoreanWon = 73,
        [Description("Kuwaiti dinar"), CurrencyDisplayString("KWD", "M KWD", "MM KWD"), CurrencyVisibleByDefault(false)]
        KuwaitiDinar = 74,
        [Description("Cayman Islands dollar"), CurrencyDisplayString("KYD", "M KYD", "MM KYD"), CurrencyVisibleByDefault(false)]
        CaymanIslandsDollar = 75,
        [Description("Tenge"), CurrencyDisplayString("KZT", "M KZT", "MM KZT"), CurrencyVisibleByDefault(false)]
        Tenge = 76,
        [Description("Kip"), CurrencyDisplayString("LAK", "M LAK", "MM LAK"), CurrencyVisibleByDefault(false)]
        Kip = 77,
        [Description("Lebanese pound"), CurrencyDisplayString("LBP", "M LBP", "MM LBP"), CurrencyVisibleByDefault(false)]
        LebanesePound = 78,
        [Description("Sri Lanka rupee"), CurrencyDisplayString("LKR", "M LKR", "MM LKR"), CurrencyVisibleByDefault(false)]
        SriLankaRupee = 79,
        [Description("Liberian dollar"), CurrencyDisplayString("LRD", "M LRD", "MM LRD"), CurrencyVisibleByDefault(false)]
        LiberianDollar = 80,
        [Description("Lesotho loti"), CurrencyDisplayString("LSL", "M LSL", "MM LSL"), CurrencyVisibleByDefault(false)]
        LesothoLoti = 81,
        [Description("Lithuanian litas"), CurrencyDisplayString("LTL", "M LTL", "MM LTL"), CurrencyVisibleByDefault(false)]
        LithuanianLitas = 82,
        [Description("Latvian lats"), CurrencyDisplayString("LVL", "M LVL", "MM LVL"), CurrencyVisibleByDefault(false)]
        LatvianLats = 83,
        [Description("Libyan dinar"), CurrencyDisplayString("LYD", "M LYD", "MM LYD"), CurrencyVisibleByDefault(false)]
        LibyanDinar = 84,
        [Description("Moroccan dirham"), CurrencyDisplayString("MAD", "M MAD", "MM MAD"), CurrencyVisibleByDefault(false)]
        MoroccanDirham = 85,
        [Description("Moldovan leu"), CurrencyDisplayString("MDL", "M MDL", "MM MDL"), CurrencyVisibleByDefault(false)]
        MoldovanLeu = 86,
        [Description("Malagasy ariary"), CurrencyDisplayString("MGA", "M MGA", "MM MGA"), CurrencyVisibleByDefault(false)]
        MalagasyAriary = 87,
        [Description("Denar"), CurrencyDisplayString("MKD", "M MKD", "MM MKD"), CurrencyVisibleByDefault(false)]
        Denar = 88,
        [Description("Kyat"), CurrencyDisplayString("MMK", "M MMK", "MM MMK"), CurrencyVisibleByDefault(false)]
        Kyat = 89,
        [Description("Tugrik"), CurrencyDisplayString("MNT", "M MNT", "MM MNT"), CurrencyVisibleByDefault(false)]
        Tugrik = 90,
        [Description("Pataca"), CurrencyDisplayString("MOP", "M MOP", "MM MOP"), CurrencyVisibleByDefault(false)]
        Pataca = 91,
        [Description("Ouguiya"), CurrencyDisplayString("MRO", "M MRO", "MM MRO"), CurrencyVisibleByDefault(false)]
        Ouguiya = 92,
        [Description("Mauritius rupee"), CurrencyDisplayString("MUR", "M MUR", "MM MUR"), CurrencyVisibleByDefault(false)]
        MauritiusRupee = 93,
        [Description("Rufiyaa"), CurrencyDisplayString("MVR", "M MVR", "MM MVR"), CurrencyVisibleByDefault(false)]
        Rufiyaa = 94,
        [Description("Kwacha"), CurrencyDisplayString("MWK", "M MWK", "MM MWK"), CurrencyVisibleByDefault(false)]
        Kwacha = 95,
        [Description("Mexican peso"), CurrencyDisplayString("MXN", "M MXN", "MM MXN"), CurrencyVisibleByDefault(false)]
        MexicanPeso = 96,
        [Description("Malaysian ringgit"), CurrencyDisplayString("MYR", "M MYR", "MM MYR"), CurrencyVisibleByDefault(true)]
        MalaysianRinggit = 97,
        [Description("Metical"), CurrencyDisplayString("MZN", "M MZN", "MM MZN"), CurrencyVisibleByDefault(false)]
        Metical = 98,
        [Description("Namibian dollar"), CurrencyDisplayString("NAD", "M NAD", "MM NAD"), CurrencyVisibleByDefault(false)]
        NamibianDollar = 99,
        [Description("Naira"), CurrencyDisplayString("NGN", "M NGN", "MM NGN"), CurrencyVisibleByDefault(true)]
        Naira = 100,
        [Description("Cordoba oro"), CurrencyDisplayString("NIO", "M NIO", "MM NIO"), CurrencyVisibleByDefault(false)]
        CordobaOro = 101,
        [Description("Norwegian krone"), CurrencyDisplayString("NOK", "M NOK", "MM NOK"), CurrencyVisibleByDefault(true)]
        NorwegianKrone = 102,
        [Description("Nepalese rupee"), CurrencyDisplayString("NPR", "M NPR", "MM NPR"), CurrencyVisibleByDefault(false)]
        NepaleseRupee = 103,
        [Description("New Zealand dollar"), CurrencyDisplayString("NZD", "M NZD", "MM NZD"), CurrencyVisibleByDefault(false)]
        NewZealandDollar = 104,
        [Description("Rial Omani"), CurrencyDisplayString("OMR", "M OMR", "MM OMR"), CurrencyVisibleByDefault(false)]
        RialOmani = 105,
        [Description("Balboa"), CurrencyDisplayString("PAB", "M PAB", "MM PAB"), CurrencyVisibleByDefault(false)]
        Balboa = 106,
        [Description("Nuevo sol"), CurrencyDisplayString("PEN", "M PEN", "MM PEN"), CurrencyVisibleByDefault(false)]
        NuevoSol = 107,
        [Description("Kina"), CurrencyDisplayString("PGK", "M PGK", "MM PGK"), CurrencyVisibleByDefault(false)]
        Kina = 108,
        [Description("Philippine peso"), CurrencyDisplayString("PHP", "M PHP", "MM PHP"), CurrencyVisibleByDefault(false)]
        PhilippinePeso = 109,
        [Description("Pakistan rupee"), CurrencyDisplayString("PKR", "M PKR", "MM PKR"), CurrencyVisibleByDefault(false)]
        PakistanRupee = 110,
        [Description("Złoty"), CurrencyDisplayString("PLN", "M PLN", "MM PLN"), CurrencyVisibleByDefault(false)]
        Złoty = 111,
        [Description("Guarani"), CurrencyDisplayString("PYG", "M PYG", "MM PYG"), CurrencyVisibleByDefault(false)]
        Guarani = 112,
        [Description("Qatari rial"), CurrencyDisplayString("QAR", "M QAR", "MM QAR"), CurrencyVisibleByDefault(false)]
        QatariRial = 113,
        [Description("Romanian new leu"), CurrencyDisplayString("RON", "M RON", "MM RON"), CurrencyVisibleByDefault(false)]
        RomanianNewLeu = 114,
        [Description("Serbian dinar"), CurrencyDisplayString("RSD", "M RSD", "MM RSD"), CurrencyVisibleByDefault(false)]
        SerbianDinar = 115,
        [Description("Russian rouble"), CurrencyDisplayString("RUB", "M RUB", "MM RUB"), CurrencyVisibleByDefault(true)]
        RussianRouble = 116,
        [Description("Rwanda franc"), CurrencyDisplayString("RWF", "M RWF", "MM RWF"), CurrencyVisibleByDefault(false)]
        RwandaFranc = 117,
        [Description("Saudi riyal"), CurrencyDisplayString("SAR", "M SAR", "MM SAR"), CurrencyVisibleByDefault(false)]
        SaudiRiyal = 118,
        [Description("Solomon Islands dollar"), CurrencyDisplayString("SBD", "M SBD", "MM SBD"), CurrencyVisibleByDefault(false)]
        SolomonIslandsDollar = 119,
        [Description("Seychelles rupee"), CurrencyDisplayString("SCR", "M SCR", "MM SCR"), CurrencyVisibleByDefault(false)]
        SeychellesRupee = 120,
        [Description("Sudanese pound"), CurrencyDisplayString("SDG", "M SDG", "MM SDG"), CurrencyVisibleByDefault(false)]
        SudanesePound = 121,
        [Description("Swedish krona"), CurrencyDisplayString("SEK", "M SEK", "MM SEK"), CurrencyVisibleByDefault(false)]
        SwedishKrona = 122,
        [Description("Singapore dollar"), CurrencyDisplayString("SGD", "M SGD", "MM SGD"), CurrencyVisibleByDefault(true)]
        SingaporeDollar = 123,
        [Description("Saint Helena pound"), CurrencyDisplayString("SHP", "M SHP", "MM SHP"), CurrencyVisibleByDefault(false)]
        SaintHelenaPound = 124,
        [Description("Leone"), CurrencyDisplayString("SLL", "M SLL", "MM SLL"), CurrencyVisibleByDefault(false)]
        Leone = 125,
        [Description("Somali shilling"), CurrencyDisplayString("SOS", "M SOS", "MM SOS"), CurrencyVisibleByDefault(false)]
        SomaliShilling = 126,
        [Description("Surinam dollar"), CurrencyDisplayString("SRD", "M SRD", "MM SRD"), CurrencyVisibleByDefault(false)]
        SurinamDollar = 127,
        [Description("Dobra"), CurrencyDisplayString("STD", "M STD", "MM STD"), CurrencyVisibleByDefault(false)]
        Dobra = 128,
        [Description("Syrian pound"), CurrencyDisplayString("SYP", "M SYP", "MM SYP"), CurrencyVisibleByDefault(false)]
        SyrianPound = 129,
        [Description("Lilangeni"), CurrencyDisplayString("SZL", "M SZL", "MM SZL"), CurrencyVisibleByDefault(false)]
        Lilangeni = 130,
        [Description("Baht"), CurrencyDisplayString("THB", "M THB", "MM THB"), CurrencyVisibleByDefault(false)]
        Baht = 131,
        [Description("Somoni"), CurrencyDisplayString("TJS", "M TJS", "MM TJS"), CurrencyVisibleByDefault(false)]
        Somoni = 132,
        [Description("Manat"), CurrencyDisplayString("TMT", "M TMT", "MM TMT"), CurrencyVisibleByDefault(false)]
        Manat = 133,
        [Description("Tunisian dinar"), CurrencyDisplayString("TND", "M TND", "MM TND"), CurrencyVisibleByDefault(false)]
        TunisianDinar = 134,
        [Description("Pa'anga"), CurrencyDisplayString("TOP", "M TOP", "MM TOP"), CurrencyVisibleByDefault(false)]
        Paanga = 135,
        [Description("Turkish lira"), CurrencyDisplayString("TRY", "M TRY", "MM TRY"), CurrencyVisibleByDefault(false)]
        TurkishLira = 136,
        [Description("Trinidad and Tobago dollar"), CurrencyDisplayString("TTD", "M TTD", "MM TTD"), CurrencyVisibleByDefault(false)]
        TrinidadandTobagoDollar = 137,
        [Description("New Taiwan dollar"), CurrencyDisplayString("TWD", "M TWD", "MM TWD"), CurrencyVisibleByDefault(false)]
        NewTaiwanDollar = 138,
        [Description("Tanzanian shilling"), CurrencyDisplayString("TZS", "M TZS", "MM TZS"), CurrencyVisibleByDefault(false)]
        TanzanianShilling = 139,
        [Description("Hryvnia"), CurrencyDisplayString("UAH", "M UAH", "MM UAH"), CurrencyVisibleByDefault(false)]
        Hryvnia = 140,
        [Description("Uganda shilling"), CurrencyDisplayString("UGX", "M UGX", "MM UGX"), CurrencyVisibleByDefault(false)]
        UgandaShilling = 141,
        [Description("US dollar"), CurrencyDisplayString("USD", "M USD", "MM USD"), CurrencyVisibleByDefault(true)]
        USDollar = 142,
        [Description("Peso Uruguayo"), CurrencyDisplayString("UYU", "M UYU", "MM UYU"), CurrencyVisibleByDefault(false)]
        PesoUruguayo = 143,
        [Description("Uzbekistan som"), CurrencyDisplayString("UZS", "M UZS", "MM UZS"), CurrencyVisibleByDefault(false)]
        UzbekistanSom = 144,
        [Description("Venezuelan bolívar fuerte"), CurrencyDisplayString("VEF", "M VEF", "MM VEF"), CurrencyVisibleByDefault(false)]
        VenezuelanBolívarFuerte = 145,
        [Description("Vietnamese dong"), CurrencyDisplayString("VND", "M VND", "MM VND"), CurrencyVisibleByDefault(false)]
        VietnameseDong = 146,
        [Description("Vatu"), CurrencyDisplayString("VUV", "M VUV", "MM VUV"), CurrencyVisibleByDefault(false)]
        Vatu = 147,
        [Description("Samoan tala"), CurrencyDisplayString("WST", "M WST", "MM WST"), CurrencyVisibleByDefault(false)]
        SamoanTala = 148,
        [Description("CFA franc BEAC"), CurrencyDisplayString("XAF", "M XAF", "MM XAF"), CurrencyVisibleByDefault(false)]
        CFAfrancBEAC = 149,
        [Description("East Caribbean dollar"), CurrencyDisplayString("XCD", "M XCD", "MM XCD"), CurrencyVisibleByDefault(false)]
        EastCaribbeanDollar = 150,
        [Description("CFA Franc BCEAO"), CurrencyDisplayString("XOF", "M XOF", "MM XOF"), CurrencyVisibleByDefault(false)]
        CFAFrancBCEAO = 151,
        [Description("CFP franc"), CurrencyDisplayString("XPF", "M XPF", "MM XPF"), CurrencyVisibleByDefault(false)]
        CFPFranc = 152,
        [Description("Yemeni rial"), CurrencyDisplayString("YER", "M YER", "MM YER"), CurrencyVisibleByDefault(false)]
        YemeniRial = 153,
        [Description("South African rand"), CurrencyDisplayString("ZAR", "M ZAR", "MM ZAR"), CurrencyVisibleByDefault(true)]
        SouthAfricanRand = 154,
        [Description("Zimbabwe dollar"), CurrencyDisplayString("ZWD", "M ZWD", "MM ZWD"), CurrencyVisibleByDefault(false)]
        ZimbabweDollar = 155,
        [Description("X-Austrian schilling"), CurrencyDisplayString("X-ATS/EUR", "M X-ATS/EUR", "MM X-ATS/EUR"), CurrencyVisibleByDefault(false)]
        XAustrianSchilling = 156,
        [Description("X-Belgian franc"), CurrencyDisplayString("X-BEF/EUR", "M X-BEF/EUR", "MM X-BEF/EUR"), CurrencyVisibleByDefault(false)]
        XBelgianFranc = 157,
        [Description("X-Cypriot pound"), CurrencyDisplayString("X-CYP/EUR", "M X-CYP/EUR", "MM X-CYP/EUR"), CurrencyVisibleByDefault(false)]
        XCypriotPound = 158,
        [Description("X-German mark"), CurrencyDisplayString("X-DEM/EUR", "M X-DEM/EUR", "MM X-DEM/EUR"), CurrencyVisibleByDefault(false)]
        XGermanMark = 159,
        [Description("X-Spanish peseta"), CurrencyDisplayString("X-ESP/EUR", "M X-ESP/EUR", "MM X-ESP/EUR"), CurrencyVisibleByDefault(false)]
        XSpanishPeseta = 160,
        [Description("X-Finnish markka"), CurrencyDisplayString("X-FIM/EUR", "M X-FIM/EUR", "MM X-FIM/EUR"), CurrencyVisibleByDefault(false)]
        XFinnishMarkka = 161,
        [Description("X-French franc"), CurrencyDisplayString("X-FRF/EUR", "M X-FRF/EUR", "MM X-FRF/EUR"), CurrencyVisibleByDefault(false)]
        XFrenchFranc = 162,
        [Description("X-Greek drachma"), CurrencyDisplayString("X-GRD/EUR", "M X-GRD/EUR", "MM X-GRD/EUR"), CurrencyVisibleByDefault(false)]
        XGreekDrachma = 163,
        [Description("X-Irish pound"), CurrencyDisplayString("X-IEP/EUR", "M X-IEP/EUR", "MM X-IEP/EUR"), CurrencyVisibleByDefault(false)]
        XIrishPound = 164,
        [Description("X-Italian lira"), CurrencyDisplayString("X-ITL/EUR", "M X-ITL/EUR", "MM X-ITL/EUR"), CurrencyVisibleByDefault(false)]
        XItalianLira = 165,
        [Description("X-Luxembourg franc"), CurrencyDisplayString("X-LUF/EUR", "M X-LUF/EUR", "MM X-LUF/EUR"), CurrencyVisibleByDefault(false)]
        XLuxembourgFranc = 166,
        [Description("X-Monegasque franc"), CurrencyDisplayString("X-MCF/EUR", "M X-MCF/EUR", "MM X-MCF/EUR"), CurrencyVisibleByDefault(false)]
        XMonegasqueFranc = 167,
        [Description("X-Maltese lira"), CurrencyDisplayString("X-MTL/EUR", "M X-MTL/EUR", "MM X-MTL/EUR"), CurrencyVisibleByDefault(false)]
        XMalteseLira = 168,
        [Description("X-Netherlands guilder"), CurrencyDisplayString("X-NLG/EUR", "M X-NLG/EUR", "MM X-NLG/EUR"), CurrencyVisibleByDefault(false)]
        XNetherlandsGuilder = 169,
        [Description("X-Portuguese escudo"), CurrencyDisplayString("X-PTE/EUR", "M X-PTE/EUR", "MM X-PTE/EUR"), CurrencyVisibleByDefault(false)]
        XPortugueseEscudo = 170,
        [Description("X-Slovenian tolar"), CurrencyDisplayString("X-SIT/EUR", "M X-SIT/EUR", "MM X-SIT/EUR"), CurrencyVisibleByDefault(false)]
        XSlovenianTolar = 171,
        [Description("X-Slovak koruna"), CurrencyDisplayString("X-SKK/EUR", "M X-SKK/EUR", "MM X-SKK/EUR"), CurrencyVisibleByDefault(false)]
        XSlovakKoruna = 172
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class CurrencyDisplayStringAttribute : Attribute 
    {
        private string _SmallString;
        private string _MediumString;
        private string _LargeString;

        public CurrencyDisplayStringAttribute(string smallString, string mediumString, string largeString)
        {
            _SmallString = smallString;
            _MediumString = mediumString;
            _LargeString = largeString;
        }

        public string SmallString { get { return _SmallString; } }
        public string MediumString { get { return _MediumString; } }
        public string LargeString { get { return _LargeString; } }
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class CurrencyVisibleByDefaultAttribute : Attribute
    {
        private bool _Visible;

        public CurrencyVisibleByDefaultAttribute(bool visible)
        {
            _Visible = visible;
        }

        public bool Visible { get { return _Visible; } }
    }
}
