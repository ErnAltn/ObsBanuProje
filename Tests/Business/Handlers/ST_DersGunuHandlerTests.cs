
using Business.Handlers.ST_DersGunus.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.ST_DersGunus.Queries.GetST_DersGunuQuery;
using Entities.Concrete;
using static Business.Handlers.ST_DersGunus.Queries.GetST_DersGunusQuery;
using static Business.Handlers.ST_DersGunus.Commands.CreateST_DersGunuCommand;
using Business.Handlers.ST_DersGunus.Commands;
using Business.Constants;
using static Business.Handlers.ST_DersGunus.Commands.UpdateST_DersGunuCommand;
using static Business.Handlers.ST_DersGunus.Commands.DeleteST_DersGunuCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class ST_DersGunuHandlerTests
    {
        Mock<IST_DersGunuRepository> _sT_DersGunuRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _sT_DersGunuRepository = new Mock<IST_DersGunuRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task ST_DersGunu_GetQuery_Success()
        {
            //Arrange
            var query = new GetST_DersGunuQuery();

            _sT_DersGunuRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ST_DersGunu, bool>>>())).ReturnsAsync(new ST_DersGunu()
//propertyler buraya yazılacak
//{																		
//ST_DersGunuId = 1,
//ST_DersGunuName = "Test"
//}
);

            var handler = new GetST_DersGunuQueryHandler(_sT_DersGunuRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.ST_DersGunuId.Should().Be(1);

        }

        [Test]
        public async Task ST_DersGunu_GetQueries_Success()
        {
            //Arrange
            var query = new GetST_DersGunusQuery();

            _sT_DersGunuRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<ST_DersGunu, bool>>>()))
                        .ReturnsAsync(new List<ST_DersGunu> { new ST_DersGunu() { /*TODO:propertyler buraya yazılacak ST_DersGunuId = 1, ST_DersGunuName = "test"*/ } });

            var handler = new GetST_DersGunusQueryHandler(_sT_DersGunuRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<ST_DersGunu>)x.Data).Count.Should().BeGreaterThan(0);

        }

        [Test]
        public async Task ST_DersGunu_CreateCommand_Success()
        {
            ST_DersGunu rt = null;
            //Arrange
            var command = new CreateST_DersGunuCommand();
            //propertyler buraya yazılacak
            //command.ST_DersGunuName = "deneme";

            _sT_DersGunuRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ST_DersGunu, bool>>>()))
                        .ReturnsAsync(rt);

            _sT_DersGunuRepository.Setup(x => x.Add(It.IsAny<ST_DersGunu>())).Returns(new ST_DersGunu());

            var handler = new CreateST_DersGunuCommandHandler(_sT_DersGunuRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _sT_DersGunuRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task ST_DersGunu_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateST_DersGunuCommand();
            //propertyler buraya yazılacak 
            //command.ST_DersGunuName = "test";

            _sT_DersGunuRepository.Setup(x => x.Query())
                                           .Returns(new List<ST_DersGunu> { new ST_DersGunu() { /*TODO:propertyler buraya yazılacak ST_DersGunuId = 1, ST_DersGunuName = "test"*/ } }.AsQueryable());

            _sT_DersGunuRepository.Setup(x => x.Add(It.IsAny<ST_DersGunu>())).Returns(new ST_DersGunu());

            var handler = new CreateST_DersGunuCommandHandler(_sT_DersGunuRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task ST_DersGunu_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateST_DersGunuCommand();
            //command.ST_DersGunuName = "test";

            _sT_DersGunuRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ST_DersGunu, bool>>>()))
                        .ReturnsAsync(new ST_DersGunu() { /*TODO:propertyler buraya yazılacak ST_DersGunuId = 1, ST_DersGunuName = "deneme"*/ });

            _sT_DersGunuRepository.Setup(x => x.Update(It.IsAny<ST_DersGunu>())).Returns(new ST_DersGunu());

            var handler = new UpdateST_DersGunuCommandHandler(_sT_DersGunuRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _sT_DersGunuRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task ST_DersGunu_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteST_DersGunuCommand();

            _sT_DersGunuRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ST_DersGunu, bool>>>()))
                        .ReturnsAsync(new ST_DersGunu() { /*TODO:propertyler buraya yazılacak ST_DersGunuId = 1, ST_DersGunuName = "deneme"*/});

            _sT_DersGunuRepository.Setup(x => x.Delete(It.IsAny<ST_DersGunu>()));

            var handler = new DeleteST_DersGunuCommandHandler(_sT_DersGunuRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _sT_DersGunuRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

