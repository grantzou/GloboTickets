using FluentAssertions;
using GloboTickets.Promotion.DataAccess;
using GloboTickets.Promotion.DataAccess.Entities;
using GloboTickets.Promotion.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GloboTickets.Promotion.Test
{
    public class ActTests
    {
        [Fact]
        public async Task ActsInitiallyEmpty()
        {
            List<ActModel> acts = await actQueries.ListActs();
            acts.Should().BeEmpty();
        }

        [Fact]
        public async Task WhenAddAct_ActIsReturned()
        {
            var actGuid = Guid.NewGuid();
            await actCommands.AddAct(actGuid);

            var acts = await actQueries.ListActs();
            acts.Should().Contain(act => act.ActGuid == actGuid);
        }

        [Fact]
        public async Task WhenAddActTwice_OneActIsAdded()
        {
            var actGuid = Guid.NewGuid();
            await actCommands.AddAct(actGuid);
            await actCommands.AddAct(actGuid);

            var acts = await actQueries.ListActs();
            acts.Count.Should().Be(1);
        }

        [Fact]
        public async Task WhenSetActDescription_ActDescriptionIsReturned()
        {
            var actGuid = Guid.NewGuid();
            await actCommands.AddAct(actGuid);
            await actCommands.SetActDescription(actGuid, ActDescriptionWith("Gabriel Iglesias"));

            var act = await actQueries.GetAct(actGuid);
            act.Description.Title.Should().Be("Gabriel Iglesias");
        }

        [Fact]
        public async Task WhenChangeActDescription_ActDescriptionIsModified()
        {
            var actGuid = Guid.NewGuid();
            await actCommands.AddAct(actGuid);
            await actCommands.SetActDescription(actGuid, ActDescriptionWith("Gabriel Iglesias"));
            var versionOne = await actQueries.GetAct(actGuid);
            await actCommands.SetActDescription(actGuid, ActDescriptionWith("Jeff Dunham", versionOne.Description.LastModifiedTicks));

            var act = await actQueries.GetAct(actGuid);
            act.Description.Title.Should().Be("Jeff Dunham");
        }

        [Fact]
        public async Task WhenActDescriptionIsTheSame_ActDescriptionIsNotModified()
        {
            var actGuid = Guid.NewGuid();
            await actCommands.AddAct(actGuid);
            await actCommands.SetActDescription(actGuid, ActDescriptionWith("Gabriel Iglesias"));
            var versionOne = await actQueries.GetAct(actGuid);
            await actCommands.SetActDescription(actGuid, ActDescriptionWith("Gabriel Iglesias", versionOne.Description.LastModifiedTicks));

            var act = await actQueries.GetAct(actGuid);
            act.Description.LastModifiedTicks.Should().Be(versionOne.Description.LastModifiedTicks);
        }

        [Fact]
        public async Task WhenBasedOnOldVersion_ChangeIsRejected()
        {
            var actGuid = Guid.NewGuid();
            await actCommands.SetActDescription(actGuid, ActDescriptionWith("Gabriel Iglesias"));
            var versionOne = await actQueries.GetAct(actGuid);
            await actCommands.SetActDescription(actGuid, ActDescriptionWith("Jeff Dunham", versionOne.Description.LastModifiedTicks));
            
            Func<Task> update = async () =>
            {
                await actCommands.SetActDescription(actGuid, ActDescriptionWith("Jeff Foxworthy", versionOne.Description.LastModifiedTicks));
            };
            update.Should().Throw<DbUpdateConcurrencyException>();
        }

        [Fact]
        public async Task GivenActDoesNotExist_WhenSetActDescription_ActIsCreated()
        {
            var actGuid = Guid.NewGuid();
            await actCommands.SetActDescription(actGuid, ActDescriptionWith("Gabriel Iglesias"));

            var act = await actQueries.GetAct(actGuid);
            act.Description.Title.Should().Be("Gabriel Iglesias");
        }

        [Fact]
        public async Task WhenRemoveAct_ActIsNotReturned()
        {
            var actGuid = Guid.NewGuid();
            await actCommands.AddAct(actGuid);
            await actCommands.RemoveAct(actGuid);

            var acts = await actQueries.ListActs();
            acts.Should().BeEmpty();
        }

        private static ActDescriptionModel ActDescriptionWith(string title, long lastModifiedTicks = 0)
        {
            var sha512 = HashAlgorithm.Create(HashAlgorithmName.SHA512.Name);
            var imageHash = sha512.ComputeHash(Encoding.UTF8.GetBytes(title));

            ActDescriptionModel actDescription = new ActDescriptionModel
            {
                Title = title,
                ImageHash = Convert.ToBase64String(imageHash),
                LastModifiedTicks = lastModifiedTicks
            };
            return actDescription;
        }

        private ActQueries actQueries;
        private ActCommands actCommands;

        public ActTests()
        {
            var repository = new PromotionContext(new DbContextOptionsBuilder()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options);

            actQueries = new ActQueries(repository);
            actCommands = new ActCommands(repository);
        }
    }
}
