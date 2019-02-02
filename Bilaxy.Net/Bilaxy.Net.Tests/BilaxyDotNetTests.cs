// -----------------------------------------------------------------------------
// <copyright file="BilaxyDotNetTests" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="1/27/2019 6:55:23 PM" />
// -----------------------------------------------------------------------------

namespace Bilaxy.Net.Tests
{
    #region Usings

    using Bilaxy.Net.Contracts;
    using FileRepository;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Xunit;

    #endregion Usings

    public class BilaxyDotNetTests : IDisposable
    {
        #region Properties

        private IBilaxyDotNet _svc;

        #endregion Properties

        public BilaxyDotNetTests()
        {
            IFileRepository _fileRepo = new FileRepository();

            var configPath = "config.json";

            if (_fileRepo.FileExists(configPath))
            {
                var apiCredentials = _fileRepo.GetDataFromFile<ApiCredentials>(configPath);
                _svc = new BilaxyDotNet(apiCredentials);
            }
            else
            {
                throw new Exception("Config file not found");
            }
        }

        public void Dispose()
        {
        }

        [Fact]
        public void GetCurrencies_Test()
        {
            // Act
            var currencies = _svc.GetCurrencies().Result;

            // Assert
            Assert.NotNull(currencies);
            Assert.True(currencies.Count > 0);
        }

        [Fact]
        public void GetTradingPairs_Test()
        {
            // Act
            var pairs = _svc.GetTradingPairs().Result;

            // Assert
            Assert.NotNull(pairs);
            Assert.True(pairs.Count > 0);
        }

        [Fact]
        public void GetTicker_Test()
        {
            // Arrange
            var pair = "ETHBTC";

            // Act
            var ticker = _svc.GetTicker(pair).Result;

            // Assert
            Assert.NotNull(ticker);
            Assert.Equal(@"ETH/BTC", ticker.Pair);
        }

        [Fact]
        public void GetTickerSlash_Test()
        {
            // Arrange
            var pair = @"ETH/BTC";

            // Act
            var ticker = _svc.GetTicker(pair).Result;

            // Assert
            Assert.NotNull(ticker);
            Assert.Equal(pair, ticker.Pair);
        }

        [Fact]
        public void GetTickerInt_Test()
        {
            // Arrange
            var pair = 15;

            // Act
            var ticker = _svc.GetTicker(pair).Result;

            // Assert
            Assert.NotNull(ticker);
            Assert.Equal(@"ETH/BTC", ticker.Pair);
        }

        [Fact]
        public void GetTickers_Test()
        {
            // Act
            var tickers = _svc.GetTickers().Result;

            // Assert
            Assert.NotNull(tickers);
            Assert.True(tickers.Count > 0);
        }

        [Fact]
        public void GetDepth_Default_Test()
        {
            // Arrange
            var pair = @"MICRO/ETH";
            var figures = Figures.Default;

            // Act
            var depth = _svc.GetDepth(pair, figures).Result;

            // Assert
            Assert.NotNull(depth);
        }

        [Fact]
        public void GetDepth_One_Test()
        {
            // Arrange
            var pair = @"MICRO/ETH";
            var figures = Figures.One;

            // Act
            var depth = _svc.GetDepth(pair, figures).Result;

            // Assert
            Assert.NotNull(depth);
        }

        [Fact]
        public void GetDepth_Two_Test()
        {
            // Arrange
            var pair = @"MICRO/ETH";
            var figures = Figures.Two;

            // Act
            var depth = _svc.GetDepth(pair, figures).Result;

            // Assert
            Assert.NotNull(depth);
        }

        [Fact]
        public void GetDepth_Three_Test()
        {
            // Arrange
            var pair = @"MICRO/ETH";
            var figures = Figures.Three;

            // Act
            var depth = _svc.GetDepth(pair, figures).Result;

            // Assert
            Assert.NotNull(depth);
        }

        [Fact]
        public void GetDepth_Four_Test()
        {
            // Arrange
            var pair = @"MICRO/ETH";
            var figures = Figures.Four;

            // Act
            var depth = _svc.GetDepth(pair, figures).Result;

            // Assert
            Assert.NotNull(depth);
        }

        [Fact]
        public void GetDepthConverted_Default_Test()
        {
            // Arrange
            var pair = @"MICRO/ETH";
            var figures = Figures.Default;

            // Act
            var depth = _svc.GetDepthConverted(pair, figures).Result;

            // Assert
            Assert.NotNull(depth);
        }

        [Fact]
        public void GetDepthConverted_One_Test()
        {
            // Arrange
            var pair = @"MICRO/ETH";
            var figures = Figures.One;

            // Act
            var depth = _svc.GetDepthConverted(pair, figures).Result;

            // Assert
            Assert.NotNull(depth);
        }

        [Fact]
        public void GetDepthConverted_Two_Test()
        {
            // Arrange
            var pair = @"MICRO/ETH";
            var figures = Figures.Two;

            // Act
            var depth = _svc.GetDepthConverted(pair, figures).Result;

            // Assert
            Assert.NotNull(depth);
        }

        [Fact]
        public void GetDepthConverted_Three_Test()
        {
            // Arrange
            var pair = @"MICRO/ETH";
            var figures = Figures.Three;

            // Act
            var depth = _svc.GetDepthConverted(pair, figures).Result;

            // Assert
            Assert.NotNull(depth);
        }

        [Fact]
        public void GetDepthConverted_Four_Test()
        {
            // Arrange
            var pair = @"MICRO/ETH";
            var figures = Figures.Four;

            // Act
            var depth = _svc.GetDepthConverted(pair, figures).Result;

            // Assert
            Assert.NotNull(depth);
        }

        [Fact]
        public void GetPairOrders_Test()
        {
            // Arrange
            var pair = @"MICRO/ETH";

            // Act
            var orders = _svc.GetPairOrders(pair).Result;

            // Assert
            Assert.NotNull(orders);
        }

        [Fact]
        public void GetBalances_Test()
        {
            // Act
            var balances = _svc.GetBalances().Result;

            // Assert
            Assert.NotNull(balances);
        }

        [Fact]
        public void GetDepositAddress_Test()
        {
            // Arrange
            var symbol = "BTC";

            // Act
            var address = _svc.GetDepositAddress(symbol).Result;

            // Assert
            Assert.NotNull(address);
        }

        [Fact]
        public void GetOrders_Test()
        {
            // Arrange
            var pair = "ETHBTC";

            // Act
            var orders = _svc.GetOrders(pair).Result;

            // Assert
            Assert.NotNull(orders);
        }

        [Fact]
        public void GetOpenOrders_Test()
        {
            // Arrange
            var pair = "ETHBTC";

            // Act
            var orders = _svc.GetOpenOrders(pair).Result;

            // Assert
            Assert.NotNull(orders);
        }
    }
}