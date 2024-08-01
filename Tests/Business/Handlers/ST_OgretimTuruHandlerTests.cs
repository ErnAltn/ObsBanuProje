
using Business.Handlers.ST_OgretimTurus.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.ST_OgretimTurus.Queries.GetST_OgretimTuruQuery;
using Entities.Concrete;
using static Business.Handlers.ST_OgretimTurus.Queries.GetST_OgretimTurusQuery;
using static Business.Handlers.ST_OgretimTurus.Commands.CreateST_OgretimTuruCommand;
using Business.Handlers.ST_OgretimTurus.Commands;
using Business.Constants;
using static Business.Handlers.ST_OgretimTurus.Commands.UpdateST_OgretimTuruCommand;
using static Business.Handlers.ST_OgretimTurus.Commands.DeleteST_OgretimTuruCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class ST_OgretimTuruHandlerTests
    {
        Mock<IST_OgretimTuruRepository> _sT_OgretimTuruRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _sT_OgretimTuruRepository = new Mock<IST_OgretimTuruRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task ST_OgretimTuru_GetQuery_Success()
        {
            //Arrange
            var query = new GetST_OgretimTuruQuery();

            _sT_OgretimTuruRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ST_OgretimTuru, bool>>>())).ReturnsAsync(new ST_OgretimTuru()
//propertyler buraya yazılacak
//{																		
//ST_OgretimTuruId = 1,
//ST_OgretimTuruName = "Test"
//}
);

            var handler = new GetST_OgretimTuruQueryHandler(_sT_OgretimTuruRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.ST_OgretimTuruId.Should().Be(1);

        }

        [Test]
        public async Task ST_OgretimTuru_GetQueries_Success()
        {
            //Arrange
            var query = new GetST_OgretimTurusQuery();

            _sT_OgretimTuruRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<ST_OgretimTuru, bool>>>()))
                        .ReturnsAsync(new List<ST_OgretimTuru> { new ST_OgretimTuru() { /*TODO:propertyler buraya yazılacak ST_OgretimTuruId = 1, ST_OgretimTuruName = "test"*/ } });

            var handler = new GetST_OgretimTurusQueryHandler(_sT_OgretimTuruRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<ST_OgretimTuru>)x.Data).Count.Should().BeGreaterThan(0);

        }

        [Test]
        public async Task ST_OgretimTuru_CreateCommand_Success()
        {
            ST_OgretimTuru rt = null;
            //Arrange
            var command = new CreateST_OgretimTuruCommand();
            //propertyler buraya yazılacak
            //command.ST_OgretimTuruName = "deneme";

            _sT_OgretimTuruRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ST_OgretimTuru, bool>>>()))
                        .ReturnsAsync(rt);

            _sT_OgretimTuruRepository.Setup(x => x.Add(It.IsAny<ST_OgretimTuru>())).Returns(new ST_OgretimTuru());

            var handler = new CreateST_OgretimTuruCommandHandler(_sT_OgretimTuruRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _sT_OgretimTuruRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task ST_OgretimTuru_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateST_OgretimTuruCommand();
            //propertyler buraya yazılacak 
            //command.ST_OgretimTuruName = "test";

            _sT_OgretimTuruRepository.Setup(x => x.Query())
                                           .Returns(new List<ST_OgretimTuru> { new ST_OgretimTuru() { /*TODO:propertyler buraya yazılacak ST_OgretimTuruId = 1, ST_OgretimTuruName = "test"*/ } }.AsQueryable());

            _sT_OgretimTuruRepository.Setup(x => x.Add(It.IsAny<ST_OgretimTuru>())).Returns(new ST_OgretimTuru());

            var handler = new CreateST_OgretimTuruCommandHandler(_sT_OgretimTuruRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task ST_OgretimTuru_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateST_OgretimTuruCommand();
            //command.ST_OgretimTuruName = "test";

            _sT_OgretimTuruRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ST_OgretimTuru, bool>>>()))
                        .ReturnsAsync(new ST_OgretimTuru() { /*TODO:propertyler buraya yazılacak ST_OgretimTuruId = 1, ST_OgretimTuruName = "deneme"*/ });

            _sT_OgretimTuruRepository.Setup(x => x.Update(It.IsAny<ST_OgretimTuru>())).Returns(new ST_OgretimTuru());

            var handler = new UpdateST_OgretimTuruCommandHandler(_sT_OgretimTuruRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _sT_OgretimTuruRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task ST_OgretimTuru_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteST_OgretimTuruCommand();

            _sT_OgretimTuruRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ST_OgretimTuru, bool>>>()))
                        .ReturnsAsync(new ST_OgretimTuru() { /*TODO:propertyler buraya yazılacak ST_OgretimTuruId = 1, ST_OgretimTuruName = "deneme"*/});

            _sT_OgretimTuruRepository.Setup(x => x.Delete(It.IsAny<ST_OgretimTuru>()));

            var handler = new DeleteST_OgretimTuruCommandHandler(_sT_OgretimTuruRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _sT_OgretimTuruRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

