
using Business.Handlers.ST_SinavTurus.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.ST_SinavTurus.Queries.GetST_SinavTuruQuery;
using Entities.Concrete;
using static Business.Handlers.ST_SinavTurus.Queries.GetST_SinavTurusQuery;
using static Business.Handlers.ST_SinavTurus.Commands.CreateST_SinavTuruCommand;
using Business.Handlers.ST_SinavTurus.Commands;
using Business.Constants;
using static Business.Handlers.ST_SinavTurus.Commands.UpdateST_SinavTuruCommand;
using static Business.Handlers.ST_SinavTurus.Commands.DeleteST_SinavTuruCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class ST_SinavTuruHandlerTests
    {
        Mock<IST_SinavTuruRepository> _sT_SinavTuruRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _sT_SinavTuruRepository = new Mock<IST_SinavTuruRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task ST_SinavTuru_GetQuery_Success()
        {
            //Arrange
            var query = new GetST_SinavTuruQuery();

            _sT_SinavTuruRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ST_SinavTuru, bool>>>())).ReturnsAsync(new ST_SinavTuru()
//propertyler buraya yazılacak
//{																		
//ST_SinavTuruId = 1,
//ST_SinavTuruName = "Test"
//}
);

            var handler = new GetST_SinavTuruQueryHandler(_sT_SinavTuruRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.ST_SinavTuruId.Should().Be(1);

        }

        [Test]
        public async Task ST_SinavTuru_GetQueries_Success()
        {
            //Arrange
            var query = new GetST_SinavTurusQuery();

            _sT_SinavTuruRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<ST_SinavTuru, bool>>>()))
                        .ReturnsAsync(new List<ST_SinavTuru> { new ST_SinavTuru() { /*TODO:propertyler buraya yazılacak ST_SinavTuruId = 1, ST_SinavTuruName = "test"*/ } });

            var handler = new GetST_SinavTurusQueryHandler(_sT_SinavTuruRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<ST_SinavTuru>)x.Data).Count.Should().BeGreaterThan(0);

        }

        [Test]
        public async Task ST_SinavTuru_CreateCommand_Success()
        {
            ST_SinavTuru rt = null;
            //Arrange
            var command = new CreateST_SinavTuruCommand();
            //propertyler buraya yazılacak
            //command.ST_SinavTuruName = "deneme";

            _sT_SinavTuruRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ST_SinavTuru, bool>>>()))
                        .ReturnsAsync(rt);

            _sT_SinavTuruRepository.Setup(x => x.Add(It.IsAny<ST_SinavTuru>())).Returns(new ST_SinavTuru());

            var handler = new CreateST_SinavTuruCommandHandler(_sT_SinavTuruRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _sT_SinavTuruRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task ST_SinavTuru_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateST_SinavTuruCommand();
            //propertyler buraya yazılacak 
            //command.ST_SinavTuruName = "test";

            _sT_SinavTuruRepository.Setup(x => x.Query())
                                           .Returns(new List<ST_SinavTuru> { new ST_SinavTuru() { /*TODO:propertyler buraya yazılacak ST_SinavTuruId = 1, ST_SinavTuruName = "test"*/ } }.AsQueryable());

            _sT_SinavTuruRepository.Setup(x => x.Add(It.IsAny<ST_SinavTuru>())).Returns(new ST_SinavTuru());

            var handler = new CreateST_SinavTuruCommandHandler(_sT_SinavTuruRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task ST_SinavTuru_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateST_SinavTuruCommand();
            //command.ST_SinavTuruName = "test";

            _sT_SinavTuruRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ST_SinavTuru, bool>>>()))
                        .ReturnsAsync(new ST_SinavTuru() { /*TODO:propertyler buraya yazılacak ST_SinavTuruId = 1, ST_SinavTuruName = "deneme"*/ });

            _sT_SinavTuruRepository.Setup(x => x.Update(It.IsAny<ST_SinavTuru>())).Returns(new ST_SinavTuru());

            var handler = new UpdateST_SinavTuruCommandHandler(_sT_SinavTuruRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _sT_SinavTuruRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task ST_SinavTuru_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteST_SinavTuruCommand();

            _sT_SinavTuruRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ST_SinavTuru, bool>>>()))
                        .ReturnsAsync(new ST_SinavTuru() { /*TODO:propertyler buraya yazılacak ST_SinavTuruId = 1, ST_SinavTuruName = "deneme"*/});

            _sT_SinavTuruRepository.Setup(x => x.Delete(It.IsAny<ST_SinavTuru>()));

            var handler = new DeleteST_SinavTuruCommandHandler(_sT_SinavTuruRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _sT_SinavTuruRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

