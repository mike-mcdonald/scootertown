using Microsoft.EntityFrameworkCore;
using PDX.PBOT.Scootertown.Data.Concrete;
using PDX.PBOT.Scootertown.Data.Options;
using PDX.PBOT.Scootertown.Data.Repositories.Implementations;
using PDX.PBOT.Scootertown.Data.Repositories.Interfaces;
using PDX.PBOT.Scootertown.Data.Tests.Common;
using Xunit;

namespace PDX.PBOT.Scootertown.Data.Tests.Repositories
{
    [Collection("Database collection")]
    public class DeploymentRepositoryTests
    {
        readonly IDeploymentRepository Repository;
        readonly Deployments Deployments = new Deployments();

        public DeploymentRepositoryTests(DatabaseFixture fixture)
        {
            Repository = new DeploymentRepository(fixture.Context);
        }

        [Fact]
        public async void ShouldSaveDeployment()
        {
            //Given
            var deployment = Deployments[0];
            //When
            deployment = await Repository.Add(deployment);
            //Then
            Assert.NotNull(deployment);
            Assert.Equal(1, deployment.Key);
        }

        [Fact]
        public async void ShouldUpdateSameDeployment()
        {
            //Given
            var deployment = Deployments[0];
            //When
            await Repository.Add(deployment);
            var count = await Repository.Count();
            await Repository.Add(deployment);
            //Then
            Assert.Equal(count, await Repository.Count());
        }
    }
}
