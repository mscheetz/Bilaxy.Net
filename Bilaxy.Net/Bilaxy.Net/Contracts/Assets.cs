// -----------------------------------------------------------------------------
// <copyright file="Asset" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="1/27/2019 2:48:53 PM" />
// -----------------------------------------------------------------------------

namespace Bilaxy.Net.Contracts
{
    #region Usings

    using System.Collections.Generic;

    #endregion Usings

    public class Asset
    {
        public int AssetId { get; set; }

        public string Pair
        {
            get
            {
                return this.DashedPair.Replace(@"/", "");
            }
        }

        public string DashedPair { get; set; }

        public Asset(int id, string pair)
        {
            this.AssetId = id;
            this.DashedPair = pair;
        }
    }

    public static class Assets
    {
        /// <summary>
        /// Get all assets for exchange
        /// </summary>
        /// <returns>Collection of Assets</returns>
        public static List<Asset> Get()
        { 
            var assets = new List<Asset>();
            assets.Add(new Asset(16, @"EOS/ETH"));
            assets.Add(new Asset (15, @"ETH/BTC"));
            assets.Add(new Asset(79, @"ETH/USDT"));
            assets.Add(new Asset(17, @"RDN/ETH"));
            assets.Add(new Asset(19, @"ZRX/ETH"));
            assets.Add(new Asset(113, @"BTC/USDT"));
            assets.Add(new Asset(21, @"HOT/ETH"));
            assets.Add(new Asset(22, @"CVT/ETH"));
            assets.Add(new Asset(23, @"GET/ETH"));
            assets.Add(new Asset(24, @"LND/ETH"));
            assets.Add(new Asset(25, @"SS/ETH"));
            assets.Add(new Asset(26, @"BZNT/ETH"));
            assets.Add(new Asset(27, @"TAU/ETH"));
            assets.Add(new Asset(28, @"PAL/ETH"));
            assets.Add(new Asset(29, @"SKM/ETH"));
            assets.Add(new Asset(30, @"LBA/ETH"));
            assets.Add(new Asset(31, @"ELI/ETH"));
            assets.Add(new Asset(32, @"SNTR/ETH"));
            assets.Add(new Asset(33, @"PCH/ETH"));
            assets.Add(new Asset(34, @"HER/ETH"));
            assets.Add(new Asset(37, @"UBT/ETH"));
            assets.Add(new Asset(39, @"IOTX/ETH"));
            assets.Add(new Asset(40, @"HOLD/ETH"));
            assets.Add(new Asset(41, @"VNT/ETH"));
            assets.Add(new Asset(43, @"ALI/ETH"));
            assets.Add(new Asset(44, @"VITE/ETH"));
            assets.Add(new Asset(45, @"EDR/ETH"));
            assets.Add(new Asset(46, @"NKN/ETH"));
            assets.Add(new Asset(47, @"SOUL/ETH"));
            assets.Add(new Asset(48, @"Seele/ETH"));
            assets.Add(new Asset(49, @"NRVE/ETH"));
            assets.Add(new Asset(50, @"PAI/ETH"));
            assets.Add(new Asset(51, @"BQT/ETH"));
            assets.Add(new Asset(53, @"MT/ETH"));
            assets.Add(new Asset(54, @"LEMO/ETH"));
            assets.Add(new Asset(55, @"ABYSS/ETH"));
            assets.Add(new Asset(56, @"QKC/ETH"));
            assets.Add(new Asset(57, @"XPX/ETH"));
            assets.Add(new Asset(58, @"MVP/ETH"));
            assets.Add(new Asset(59, @"ATMI/ETH"));
            assets.Add(new Asset(61, @"GO/ETH"));
            assets.Add(new Asset(62, @"RMESH/ETH"));
            assets.Add(new Asset(63, @"UPP/ETH"));
            assets.Add(new Asset(64, @"YEED/ETH"));
            assets.Add(new Asset(65, @"FTM/ETH"));
            assets.Add(new Asset(66, @"OLT/ETH"));
            assets.Add(new Asset(67, @"DAG/ETH"));
            assets.Add(new Asset(68, @"MET/ETH"));
            assets.Add(new Asset(69, @"EGT/ETH"));
            assets.Add(new Asset(70, @"KNT/ETH"));
            assets.Add(new Asset(71, @"ZCN/ETH"));
            assets.Add(new Asset(72, @"ZXC/ETH"));
            assets.Add(new Asset(73, @"CARD/ETH"));
            assets.Add(new Asset(74, @"MFT/ETH"));
            assets.Add(new Asset(75, @"GOT/ETH"));
            assets.Add(new Asset(76, @"AION/ETH"));
            assets.Add(new Asset(77, @"ESS/ETH"));
            assets.Add(new Asset(78, @"ZP/ETH"));
            assets.Add(new Asset(82, @"RHOC/ETH"));
            assets.Add(new Asset(83, @"SPRK/ETH"));
            assets.Add(new Asset(84, @"SDS/ETH"));
            assets.Add(new Asset(86, @"ABL/ETH"));
            assets.Add(new Asset(90, @"DX/ETH"));
            assets.Add(new Asset(92, @"USE/ETH"));
            assets.Add(new Asset(93, @"FOAM/ETH"));
            assets.Add(new Asset(95, @"DAV/ETH"));
            assets.Add(new Asset(97, @"UBEX/ETH"));
            assets.Add(new Asset(98, @"UCN/ETH"));
            assets.Add(new Asset(99, @"ASA/ETH"));
            assets.Add(new Asset(100, @"EDN/ETH"));
            assets.Add(new Asset(101, @"META/ETH"));
            assets.Add(new Asset(103, @"DEC/ETH"));
            assets.Add(new Asset(107, @"TOL/ETH"));
            assets.Add(new Asset(108, @"NRP/ETH"));
            assets.Add(new Asset(109, @"HUM/ETH"));
            assets.Add(new Asset(110, @"LQD/ETH"));
            assets.Add(new Asset(111, @"HQT/ETH"));
            assets.Add(new Asset(112, @"PTN/ETH"));
            assets.Add(new Asset(114, @"PTON/ETH"));
            assets.Add(new Asset(115, @"MCC/ETH"));
            assets.Add(new Asset(116, @"SOLVE/ETH"));
            assets.Add(new Asset(117, @"TRTL/BTC"));
            assets.Add(new Asset(118, @"XPX/BTC"));
            assets.Add(new Asset(119, @"ZP/BTC"));
            assets.Add(new Asset(120, @"GO/BTC"));
            assets.Add(new Asset(121, @"AION/BTC"));
            assets.Add(new Asset(122, @"NOS/ETH"));
            assets.Add(new Asset(123, @"CLB/ETH"));
            assets.Add(new Asset(124, @"MICRO/ETH"));
            assets.Add(new Asset(125, @"RBTC/ETH"));
            assets.Add(new Asset(126, @"RBTC/BTC"));
            assets.Add(new Asset(127, @"eQUAD/ETH"));
            assets.Add(new Asset(128, @"TRTL/ETH"));
            assets.Add(new Asset(129, @"LOOM/ETH"));
            assets.Add(new Asset(130, @"MXC/ETH"));
            assets.Add(new Asset(134, @"SPND/ETH"));
            assets.Add(new Asset(135, @"KAT/ETH"));
            assets.Add(new Asset(136, @"AERGO/ETH"));
            assets.Add(new Asset(137, @"LAMB/ETH"));
            assets.Add(new Asset(138, @"WBT/ETH"));
            assets.Add(new Asset(139, @"COVA/ETH"));
            assets.Add(new Asset(140, @"RIF/ETH"));
            assets.Add(new Asset(141, @"RIF/BTC"));
            assets.Add(new Asset(142, @"LTO/ETH"));
            assets.Add(new Asset(143, @"CVNT/ETH"));
            assets.Add(new Asset(144, @"SKYM/ETH"));
            assets.Add(new Asset(145, @"CPT/ETH"));

            return assets;
        }
    }
}
