using Hahn.ApplicationProcess.February2021.Domain.Assets;
using Hahn.ApplicationProcess.February2021.Domain.Countries;
using Hahn.ApplicationProcess.February2021.Domain.SeedWork;
using Hahn.ApplicationProcess.February2021.Domain.Services;
using Hahn.ApplicationProcess.February2021.Domain.SharedKernel;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Xunit;

namespace Hahn.ApplicationProcess.February2021.Tests.Domain.Assets
{
    public class AssetServiceTests
    {
        private IAssetService assetService;

        public AssetServiceTests()
        {
            var unitOfWork = new Mock<IUnitOfWork>();

            var country = new Mock<ICountryRepository>();

            country.Setup(c => c.FindCountry("Test")).ReturnsAsync(new Country
            {
                Name = "Test"
            });

            unitOfWork.Setup(c => c.CountryRepository).Returns(country.Object);

            var validatorService = new FluentValidatorService<Asset>(new[] {
                new AssetValidator(unitOfWork.Object)
            });

            this.assetService = new AssetService(unitOfWork.Object, validatorService);
        }

        [Fact]
        public void AssetNameLenth_CantBeLessThan5()
        {
            
            Assert.ThrowsAsync<ValidationException>(() => {
                return assetService.CreateAsset(new Asset
                {
                    AssetName = "1234"
                });
            });
        }
        [Fact]
        public void PurchaseDate_CantBeLessThanAYear()
        {
            Assert.ThrowsAsync<ValidationException>(() => {
                return assetService.CreateAsset(new Asset
                {
                    AssetName = "12344",
                    CountryOfDepartment = "Test",
                    PurchaseDate = SystemClock.Now.AddYears(-1).AddSeconds(-1)
                });
            });
        }

    }
}
