
using Business.Handlers.ST_ProgramTurus.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.ST_ProgramTurus.Queries.GetST_ProgramTuruQuery;
using Entities.Concrete;
using static Business.Handlers.ST_ProgramTurus.Queries.GetST_ProgramTurusQuery;
using static Business.Handlers.ST_ProgramTurus.Commands.CreateST_ProgramTuruCommand;
using Business.Handlers.ST_ProgramTurus.Commands;
using Business.Constants;
using static Business.Handlers.ST_ProgramTurus.Commands.UpdateST_ProgramTuruCommand;
using static Business.Handlers.ST_ProgramTurus.Commands.DeleteST_ProgramTuruCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class ST_ProgramTuruHandlerTests
    {
        Mock<IST_ProgramTuruRepository> _sT_ProgramTuruRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _sT_ProgramTuruRepository = new Mock<IST_ProgramTuruRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task ST_ProgramTuru_GetQuery_Success()
        {
            //Arrange
            var query = new GetST_ProgramTuruQuery();

            _sT_ProgramTuruRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ST_ProgramTuru, bool>>>())).ReturnsAsync(new ST_ProgramTuru()
//propertyler buraya yazılacak
//{																		
//ST_ProgramTuruId = 1,
//ST_ProgramTuruName = "Test"
//}
);

            var handler = new GetST_ProgramTuruQueryHandler(_sT_ProgramTuruRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.ST_ProgramTuruId.Should().Be(1);

        }

        [Test]
        public async Task ST_ProgramTuru_GetQueries_Success()
        {
            //Arrange
            var query = new GetST_ProgramTurusQuery();

            _sT_ProgramTuruRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<ST_ProgramTuru, bool>>>()))
                        .ReturnsAsync(new List<ST_ProgramTuru> { new ST_ProgramTuru() { /*TODO:propertyler buraya yazılacak ST_ProgramTuruId = 1, ST_ProgramTuruName = "test"*/ } });

            var handler = new GetST_ProgramTurusQueryHandler(_sT_ProgramTuruRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<ST_ProgramTuru>)x.Data).Count.Should().BeGreaterThan(0);

        }

        [Test]
        public async Task ST_ProgramTuru_CreateCommand_Success()
        {
            ST_ProgramTuru rt = null;
            //Arrange
            var command = new CreateST_ProgramTuruCommand();
            //propertyler buraya yazılacak
            //command.ST_ProgramTuruName = "deneme";

            _sT_ProgramTuruRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ST_ProgramTuru, bool>>>()))
                        .ReturnsAsync(rt);

            _sT_ProgramTuruRepository.Setup(x => x.Add(It.IsAny<ST_ProgramTuru>())).Returns(new ST_ProgramTuru());

            var handler = new CreateST_ProgramTuruCommandHandler(_sT_ProgramTuruRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _sT_ProgramTuruRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task ST_ProgramTuru_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateST_ProgramTuruCommand();
            //propertyler buraya yazılacak 
            //command.ST_ProgramTuruName = "test";

            _sT_ProgramTuruRepository.Setup(x => x.Query())
                                           .Returns(new List<ST_ProgramTuru> { new ST_ProgramTuru() { /*TODO:propertyler buraya yazılacak ST_ProgramTuruId = 1, ST_ProgramTuruName = "test"*/ } }.AsQueryable());

            _sT_ProgramTuruRepository.Setup(x => x.Add(It.IsAny<ST_ProgramTuru>())).Returns(new ST_ProgramTuru());

            var handler = new CreateST_ProgramTuruCommandHandler(_sT_ProgramTuruRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task ST_ProgramTuru_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateST_ProgramTuruCommand();
            //command.ST_ProgramTuruName = "test";

            _sT_ProgramTuruRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ST_ProgramTuru, bool>>>()))
                        .ReturnsAsync(new ST_ProgramTuru() { /*TODO:propertyler buraya yazılacak ST_ProgramTuruId = 1, ST_ProgramTuruName = "deneme"*/ });

            _sT_ProgramTuruRepository.Setup(x => x.Update(It.IsAny<ST_ProgramTuru>())).Returns(new ST_ProgramTuru());

            var handler = new UpdateST_ProgramTuruCommandHandler(_sT_ProgramTuruRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _sT_ProgramTuruRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task ST_ProgramTuru_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteST_ProgramTuruCommand();

            _sT_ProgramTuruRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ST_ProgramTuru, bool>>>()))
                        .ReturnsAsync(new ST_ProgramTuru() { /*TODO:propertyler buraya yazılacak ST_ProgramTuruId = 1, ST_ProgramTuruName = "deneme"*/});

            _sT_ProgramTuruRepository.Setup(x => x.Delete(It.IsAny<ST_ProgramTuru>()));

            var handler = new DeleteST_ProgramTuruCommandHandler(_sT_ProgramTuruRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _sT_ProgramTuruRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

