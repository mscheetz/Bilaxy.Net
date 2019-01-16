using Bilaxy.Net.Interfaces;
using Bilaxy.Net.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Bilaxy.Net.Tests
{
    public class BilaxyRepositoryTests : IDisposable
    {
        private IBilaxyRepository _repo;

        public BilaxyRepositoryTests()
        {
            _repo = new BilaxyRepository();
        }

        public void Dispose()
        {
            _repo = null;
        }

        [Fact]
        public void GetTickerTest()
        {
            // Arrange
            var pair = "ETHBTC";

            // Act
            var ticker = _repo.GetTicker(pair).Result;

            // Assert
            Assert.NotNull(ticker);
            Assert.Equal(@"ETH/BTC", ticker.Pair);
        }

        [Fact]
        public void GetTickerSlashTest()
        {
            // Arrange
            var pair = @"ETH/BTC";

            // Act
            var ticker = _repo.GetTicker(pair).Result;

            // Assert
            Assert.NotNull(ticker);
            Assert.Equal(pair, ticker.Pair);
        }

        [Fact]
        public void GetTickerIntTest()
        {
            // Arrange
            var pair = 15;

            // Act
            var ticker = _repo.GetTicker(pair).Result;

            // Assert
            Assert.NotNull(ticker);
            Assert.Equal(@"ETH/BTC", ticker.Pair);
        }
    }
}
