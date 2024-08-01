
using Business.Handlers.ST_AkademikYils.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.ST_AkademikYils.Queries.GetST_AkademikYilQuery;
using Entities.Concrete;
using static Business.Handlers.ST_AkademikYils.Queries.GetST_AkademikYilsQuery;
using static Business.Handlers.ST_AkademikYils.Commands.CreateST_AkademikYilCommand;
using Business.Handlers.ST_AkademikYils.Commands;
using Business.Constants;
using static Business.Handlers.ST_AkademikYils.Commands.UpdateST_AkademikYilCommand;
using static Business.Handlers.ST_AkademikYils.Commands.DeleteST_AkademikYilCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class ST_AkademikYilHandlerTests
    {
        Mock<IST_AkademikYilRepository> _sT_AkademikYilRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _sT_AkademikYilRepository = new Mock<IST_AkademikYilRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task ST_AkademikYil_GetQuery_Success()
        {
            //Arrange
            var query = new GetST_AkademikYilQuery();

            _sT_AkademikYilRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ST_AkademikYil, bool>>>())).ReturnsAsync(new ST_AkademikYil()
//propertyler buraya yazılacak
//{																		
//ST_AkademikYilId = 1,
//ST_AkademikYilName = "Test"
//}
);

            var handler = new GetST_AkademikYilQueryHandler(_sT_AkademikYilRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.ST_AkademikYilId.Should().Be(1);

        }

        [Test]
        public async Task ST_AkademikYil_GetQueries_Success()
        {
            //Arrange
            var query = new GetST_AkademikYilsQuery();

            _sT_AkademikYilRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<ST_AkademikYil, bool>>>()))
                        .ReturnsAsync(new List<ST_AkademikYil> { new ST_AkademikYil() { /*TODO:propertyler buraya yazılacak ST_AkademikYilId = 1, ST_AkademikYilName = "test"*/ } });

            var handler = new GetST_AkademikYilsQueryHandler(_sT_AkademikYilRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<ST_AkademikYil>)x.Data).Count.Should().BeGreaterThan(0);

        }

        [Test]
        public async Task ST_AkademikYil_CreateCommand_Success()
        {
            ST_AkademikYil rt = null;
            //Arrange
            var command = new CreateST_AkademikYilCommand();
            //propertyler buraya yazılacak
            //command.ST_AkademikYilName = "deneme";

            _sT_AkademikYilRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ST_AkademikYil, bool>>>()))
                        .ReturnsAsync(rt);

            _sT_AkademikYilRepository.Setup(x => x.Add(It.IsAny<ST_AkademikYil>())).Returns(new ST_AkademikYil());

            var handler = new CreateST_AkademikYilCommandHandler(_sT_AkademikYilRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _sT_AkademikYilRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task ST_AkademikYil_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateST_AkademikYilCommand();
            //propertyler buraya yazılacak 
            //command.ST_AkademikYilName = "test";

            _sT_AkademikYilRepository.Setup(x => x.Query())
                                           .Returns(new List<ST_AkademikYil> { new ST_AkademikYil() { /*TODO:propertyler buraya yazılacak ST_AkademikYilId = 1, ST_AkademikYilName = "test"*/ } }.AsQueryable());

            _sT_AkademikYilRepository.Setup(x => x.Add(It.IsAny<ST_AkademikYil>())).Returns(new ST_AkademikYil());

            var handler = new CreateST_AkademikYilCommandHandler(_sT_AkademikYilRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task ST_AkademikYil_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateST_AkademikYilCommand();
            //command.ST_AkademikYilName = "test";

            _sT_AkademikYilRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ST_AkademikYil, bool>>>()))
                        .ReturnsAsync(new ST_AkademikYil() { /*TODO:propertyler buraya yazılacak ST_AkademikYilId = 1, ST_AkademikYilName = "deneme"*/ });

            _sT_AkademikYilRepository.Setup(x => x.Update(It.IsAny<ST_AkademikYil>())).Returns(new ST_AkademikYil());

            var handler = new UpdateST_AkademikYilCommandHandler(_sT_AkademikYilRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _sT_AkademikYilRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task ST_AkademikYil_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteST_AkademikYilCommand();

            _sT_AkademikYilRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ST_AkademikYil, bool>>>()))
                        .ReturnsAsync(new ST_AkademikYil() { /*TODO:propertyler buraya yazılacak ST_AkademikYilId = 1, ST_AkademikYilName = "deneme"*/});

            _sT_AkademikYilRepository.Setup(x => x.Delete(It.IsAny<ST_AkademikYil>()));

            var handler = new DeleteST_AkademikYilCommandHandler(_sT_AkademikYilRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _sT_AkademikYilRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

