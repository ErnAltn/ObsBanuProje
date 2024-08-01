
using Business.Handlers.ST_DersTurus.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.ST_DersTurus.Queries.GetST_DersTuruQuery;
using Entities.Concrete;
using static Business.Handlers.ST_DersTurus.Queries.GetST_DersTurusQuery;
using static Business.Handlers.ST_DersTurus.Commands.CreateST_DersTuruCommand;
using Business.Handlers.ST_DersTurus.Commands;
using Business.Constants;
using static Business.Handlers.ST_DersTurus.Commands.UpdateST_DersTuruCommand;
using static Business.Handlers.ST_DersTurus.Commands.DeleteST_DersTuruCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class ST_DersTuruHandlerTests
    {
        Mock<IST_DersTuruRepository> _sT_DersTuruRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _sT_DersTuruRepository = new Mock<IST_DersTuruRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task ST_DersTuru_GetQuery_Success()
        {
            //Arrange
            var query = new GetST_DersTuruQuery();

            _sT_DersTuruRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ST_DersTuru, bool>>>())).ReturnsAsync(new ST_DersTuru()
//propertyler buraya yazılacak
//{																		
//ST_DersTuruId = 1,
//ST_DersTuruName = "Test"
//}
);

            var handler = new GetST_DersTuruQueryHandler(_sT_DersTuruRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.ST_DersTuruId.Should().Be(1);

        }

        [Test]
        public async Task ST_DersTuru_GetQueries_Success()
        {
            //Arrange
            var query = new GetST_DersTurusQuery();

            _sT_DersTuruRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<ST_DersTuru, bool>>>()))
                        .ReturnsAsync(new List<ST_DersTuru> { new ST_DersTuru() { /*TODO:propertyler buraya yazılacak ST_DersTuruId = 1, ST_DersTuruName = "test"*/ } });

            var handler = new GetST_DersTurusQueryHandler(_sT_DersTuruRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<ST_DersTuru>)x.Data).Count.Should().BeGreaterThan(0);

        }

        [Test]
        public async Task ST_DersTuru_CreateCommand_Success()
        {
            ST_DersTuru rt = null;
            //Arrange
            var command = new CreateST_DersTuruCommand();
            //propertyler buraya yazılacak
            //command.ST_DersTuruName = "deneme";

            _sT_DersTuruRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ST_DersTuru, bool>>>()))
                        .ReturnsAsync(rt);

            _sT_DersTuruRepository.Setup(x => x.Add(It.IsAny<ST_DersTuru>())).Returns(new ST_DersTuru());

            var handler = new CreateST_DersTuruCommandHandler(_sT_DersTuruRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _sT_DersTuruRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task ST_DersTuru_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateST_DersTuruCommand();
            //propertyler buraya yazılacak 
            //command.ST_DersTuruName = "test";

            _sT_DersTuruRepository.Setup(x => x.Query())
                                           .Returns(new List<ST_DersTuru> { new ST_DersTuru() { /*TODO:propertyler buraya yazılacak ST_DersTuruId = 1, ST_DersTuruName = "test"*/ } }.AsQueryable());

            _sT_DersTuruRepository.Setup(x => x.Add(It.IsAny<ST_DersTuru>())).Returns(new ST_DersTuru());

            var handler = new CreateST_DersTuruCommandHandler(_sT_DersTuruRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task ST_DersTuru_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateST_DersTuruCommand();
            //command.ST_DersTuruName = "test";

            _sT_DersTuruRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ST_DersTuru, bool>>>()))
                        .ReturnsAsync(new ST_DersTuru() { /*TODO:propertyler buraya yazılacak ST_DersTuruId = 1, ST_DersTuruName = "deneme"*/ });

            _sT_DersTuruRepository.Setup(x => x.Update(It.IsAny<ST_DersTuru>())).Returns(new ST_DersTuru());

            var handler = new UpdateST_DersTuruCommandHandler(_sT_DersTuruRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _sT_DersTuruRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task ST_DersTuru_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteST_DersTuruCommand();

            _sT_DersTuruRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ST_DersTuru, bool>>>()))
                        .ReturnsAsync(new ST_DersTuru() { /*TODO:propertyler buraya yazılacak ST_DersTuruId = 1, ST_DersTuruName = "deneme"*/});

            _sT_DersTuruRepository.Setup(x => x.Delete(It.IsAny<ST_DersTuru>()));

            var handler = new DeleteST_DersTuruCommandHandler(_sT_DersTuruRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _sT_DersTuruRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

