
using Business.Handlers.ST_DerslikTurus.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.ST_DerslikTurus.Queries.GetST_DerslikTuruQuery;
using Entities.Concrete;
using static Business.Handlers.ST_DerslikTurus.Queries.GetST_DerslikTurusQuery;
using static Business.Handlers.ST_DerslikTurus.Commands.CreateST_DerslikTuruCommand;
using Business.Handlers.ST_DerslikTurus.Commands;
using Business.Constants;
using static Business.Handlers.ST_DerslikTurus.Commands.UpdateST_DerslikTuruCommand;
using static Business.Handlers.ST_DerslikTurus.Commands.DeleteST_DerslikTuruCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class ST_DerslikTuruHandlerTests
    {
        Mock<IST_DerslikTuruRepository> _sT_DerslikTuruRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _sT_DerslikTuruRepository = new Mock<IST_DerslikTuruRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task ST_DerslikTuru_GetQuery_Success()
        {
            //Arrange
            var query = new GetST_DerslikTuruQuery();

            _sT_DerslikTuruRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ST_DerslikTuru, bool>>>())).ReturnsAsync(new ST_DerslikTuru()
//propertyler buraya yazılacak
//{																		
//ST_DerslikTuruId = 1,
//ST_DerslikTuruName = "Test"
//}
);

            var handler = new GetST_DerslikTuruQueryHandler(_sT_DerslikTuruRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.ST_DerslikTuruId.Should().Be(1);

        }

        [Test]
        public async Task ST_DerslikTuru_GetQueries_Success()
        {
            //Arrange
            var query = new GetST_DerslikTurusQuery();

            _sT_DerslikTuruRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<ST_DerslikTuru, bool>>>()))
                        .ReturnsAsync(new List<ST_DerslikTuru> { new ST_DerslikTuru() { /*TODO:propertyler buraya yazılacak ST_DerslikTuruId = 1, ST_DerslikTuruName = "test"*/ } });

            var handler = new GetST_DerslikTurusQueryHandler(_sT_DerslikTuruRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<ST_DerslikTuru>)x.Data).Count.Should().BeGreaterThan(0);

        }

        [Test]
        public async Task ST_DerslikTuru_CreateCommand_Success()
        {
            ST_DerslikTuru rt = null;
            //Arrange
            var command = new CreateST_DerslikTuruCommand();
            //propertyler buraya yazılacak
            //command.ST_DerslikTuruName = "deneme";

            _sT_DerslikTuruRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ST_DerslikTuru, bool>>>()))
                        .ReturnsAsync(rt);

            _sT_DerslikTuruRepository.Setup(x => x.Add(It.IsAny<ST_DerslikTuru>())).Returns(new ST_DerslikTuru());

            var handler = new CreateST_DerslikTuruCommandHandler(_sT_DerslikTuruRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _sT_DerslikTuruRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task ST_DerslikTuru_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateST_DerslikTuruCommand();
            //propertyler buraya yazılacak 
            //command.ST_DerslikTuruName = "test";

            _sT_DerslikTuruRepository.Setup(x => x.Query())
                                           .Returns(new List<ST_DerslikTuru> { new ST_DerslikTuru() { /*TODO:propertyler buraya yazılacak ST_DerslikTuruId = 1, ST_DerslikTuruName = "test"*/ } }.AsQueryable());

            _sT_DerslikTuruRepository.Setup(x => x.Add(It.IsAny<ST_DerslikTuru>())).Returns(new ST_DerslikTuru());

            var handler = new CreateST_DerslikTuruCommandHandler(_sT_DerslikTuruRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task ST_DerslikTuru_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateST_DerslikTuruCommand();
            //command.ST_DerslikTuruName = "test";

            _sT_DerslikTuruRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ST_DerslikTuru, bool>>>()))
                        .ReturnsAsync(new ST_DerslikTuru() { /*TODO:propertyler buraya yazılacak ST_DerslikTuruId = 1, ST_DerslikTuruName = "deneme"*/ });

            _sT_DerslikTuruRepository.Setup(x => x.Update(It.IsAny<ST_DerslikTuru>())).Returns(new ST_DerslikTuru());

            var handler = new UpdateST_DerslikTuruCommandHandler(_sT_DerslikTuruRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _sT_DerslikTuruRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task ST_DerslikTuru_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteST_DerslikTuruCommand();

            _sT_DerslikTuruRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ST_DerslikTuru, bool>>>()))
                        .ReturnsAsync(new ST_DerslikTuru() { /*TODO:propertyler buraya yazılacak ST_DerslikTuruId = 1, ST_DerslikTuruName = "deneme"*/});

            _sT_DerslikTuruRepository.Setup(x => x.Delete(It.IsAny<ST_DerslikTuru>()));

            var handler = new DeleteST_DerslikTuruCommandHandler(_sT_DerslikTuruRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _sT_DerslikTuruRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

