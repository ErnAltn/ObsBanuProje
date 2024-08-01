
using Business.Handlers.DersHavuzus.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.DersHavuzus.Queries.GetDersHavuzuQuery;
using Entities.Concrete;
using static Business.Handlers.DersHavuzus.Queries.GetDersHavuzusQuery;
using static Business.Handlers.DersHavuzus.Commands.CreateDersHavuzuCommand;
using Business.Handlers.DersHavuzus.Commands;
using Business.Constants;
using static Business.Handlers.DersHavuzus.Commands.UpdateDersHavuzuCommand;
using static Business.Handlers.DersHavuzus.Commands.DeleteDersHavuzuCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class DersHavuzuHandlerTests
    {
        Mock<IDersHavuzuRepository> _dersHavuzuRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _dersHavuzuRepository = new Mock<IDersHavuzuRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task DersHavuzu_GetQuery_Success()
        {
            //Arrange
            var query = new GetDersHavuzuQuery();

            _dersHavuzuRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<DersHavuzu, bool>>>())).ReturnsAsync(new DersHavuzu()
//propertyler buraya yazılacak
//{																		
//DersHavuzuId = 1,
//DersHavuzuName = "Test"
//}
);

            var handler = new GetDersHavuzuQueryHandler(_dersHavuzuRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.DersHavuzuId.Should().Be(1);

        }

        [Test]
        public async Task DersHavuzu_GetQueries_Success()
        {
            //Arrange
            var query = new GetDersHavuzusQuery();

            _dersHavuzuRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<DersHavuzu, bool>>>()))
                        .ReturnsAsync(new List<DersHavuzu> { new DersHavuzu() { /*TODO:propertyler buraya yazılacak DersHavuzuId = 1, DersHavuzuName = "test"*/ } });

            var handler = new GetDersHavuzusQueryHandler(_dersHavuzuRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<DersHavuzu>)x.Data).Count.Should().BeGreaterThan(0);

        }

        [Test]
        public async Task DersHavuzu_CreateCommand_Success()
        {
            DersHavuzu rt = null;
            //Arrange
            var command = new CreateDersHavuzuCommand();
            //propertyler buraya yazılacak
            //command.DersHavuzuName = "deneme";

            _dersHavuzuRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<DersHavuzu, bool>>>()))
                        .ReturnsAsync(rt);

            _dersHavuzuRepository.Setup(x => x.Add(It.IsAny<DersHavuzu>())).Returns(new DersHavuzu());

            var handler = new CreateDersHavuzuCommandHandler(_dersHavuzuRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _dersHavuzuRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task DersHavuzu_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateDersHavuzuCommand();
            //propertyler buraya yazılacak 
            //command.DersHavuzuName = "test";

            _dersHavuzuRepository.Setup(x => x.Query())
                                           .Returns(new List<DersHavuzu> { new DersHavuzu() { /*TODO:propertyler buraya yazılacak DersHavuzuId = 1, DersHavuzuName = "test"*/ } }.AsQueryable());

            _dersHavuzuRepository.Setup(x => x.Add(It.IsAny<DersHavuzu>())).Returns(new DersHavuzu());

            var handler = new CreateDersHavuzuCommandHandler(_dersHavuzuRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task DersHavuzu_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateDersHavuzuCommand();
            //command.DersHavuzuName = "test";

            _dersHavuzuRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<DersHavuzu, bool>>>()))
                        .ReturnsAsync(new DersHavuzu() { /*TODO:propertyler buraya yazılacak DersHavuzuId = 1, DersHavuzuName = "deneme"*/ });

            _dersHavuzuRepository.Setup(x => x.Update(It.IsAny<DersHavuzu>())).Returns(new DersHavuzu());

            var handler = new UpdateDersHavuzuCommandHandler(_dersHavuzuRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _dersHavuzuRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task DersHavuzu_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteDersHavuzuCommand();

            _dersHavuzuRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<DersHavuzu, bool>>>()))
                        .ReturnsAsync(new DersHavuzu() { /*TODO:propertyler buraya yazılacak DersHavuzuId = 1, DersHavuzuName = "deneme"*/});

            _dersHavuzuRepository.Setup(x => x.Delete(It.IsAny<DersHavuzu>()));

            var handler = new DeleteDersHavuzuCommandHandler(_dersHavuzuRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _dersHavuzuRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

