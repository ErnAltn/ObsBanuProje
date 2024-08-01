
using Business.Handlers.ST_DersSeviyesis.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.ST_DersSeviyesis.Queries.GetST_DersSeviyesiQuery;
using Entities.Concrete;
using static Business.Handlers.ST_DersSeviyesis.Queries.GetST_DersSeviyesisQuery;
using static Business.Handlers.ST_DersSeviyesis.Commands.CreateST_DersSeviyesiCommand;
using Business.Handlers.ST_DersSeviyesis.Commands;
using Business.Constants;
using static Business.Handlers.ST_DersSeviyesis.Commands.UpdateST_DersSeviyesiCommand;
using static Business.Handlers.ST_DersSeviyesis.Commands.DeleteST_DersSeviyesiCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class ST_DersSeviyesiHandlerTests
    {
        Mock<IST_DersSeviyesiRepository> _sT_DersSeviyesiRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _sT_DersSeviyesiRepository = new Mock<IST_DersSeviyesiRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task ST_DersSeviyesi_GetQuery_Success()
        {
            //Arrange
            var query = new GetST_DersSeviyesiQuery();

            _sT_DersSeviyesiRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ST_DersSeviyesi, bool>>>())).ReturnsAsync(new ST_DersSeviyesi()
//propertyler buraya yazılacak
//{																		
//ST_DersSeviyesiId = 1,
//ST_DersSeviyesiName = "Test"
//}
);

            var handler = new GetST_DersSeviyesiQueryHandler(_sT_DersSeviyesiRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.ST_DersSeviyesiId.Should().Be(1);

        }

        [Test]
        public async Task ST_DersSeviyesi_GetQueries_Success()
        {
            //Arrange
            var query = new GetST_DersSeviyesisQuery();

            _sT_DersSeviyesiRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<ST_DersSeviyesi, bool>>>()))
                        .ReturnsAsync(new List<ST_DersSeviyesi> { new ST_DersSeviyesi() { /*TODO:propertyler buraya yazılacak ST_DersSeviyesiId = 1, ST_DersSeviyesiName = "test"*/ } });

            var handler = new GetST_DersSeviyesisQueryHandler(_sT_DersSeviyesiRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<ST_DersSeviyesi>)x.Data).Count.Should().BeGreaterThan(0);

        }

        [Test]
        public async Task ST_DersSeviyesi_CreateCommand_Success()
        {
            ST_DersSeviyesi rt = null;
            //Arrange
            var command = new CreateST_DersSeviyesiCommand();
            //propertyler buraya yazılacak
            //command.ST_DersSeviyesiName = "deneme";

            _sT_DersSeviyesiRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ST_DersSeviyesi, bool>>>()))
                        .ReturnsAsync(rt);

            _sT_DersSeviyesiRepository.Setup(x => x.Add(It.IsAny<ST_DersSeviyesi>())).Returns(new ST_DersSeviyesi());

            var handler = new CreateST_DersSeviyesiCommandHandler(_sT_DersSeviyesiRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _sT_DersSeviyesiRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task ST_DersSeviyesi_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateST_DersSeviyesiCommand();
            //propertyler buraya yazılacak 
            //command.ST_DersSeviyesiName = "test";

            _sT_DersSeviyesiRepository.Setup(x => x.Query())
                                           .Returns(new List<ST_DersSeviyesi> { new ST_DersSeviyesi() { /*TODO:propertyler buraya yazılacak ST_DersSeviyesiId = 1, ST_DersSeviyesiName = "test"*/ } }.AsQueryable());

            _sT_DersSeviyesiRepository.Setup(x => x.Add(It.IsAny<ST_DersSeviyesi>())).Returns(new ST_DersSeviyesi());

            var handler = new CreateST_DersSeviyesiCommandHandler(_sT_DersSeviyesiRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task ST_DersSeviyesi_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateST_DersSeviyesiCommand();
            //command.ST_DersSeviyesiName = "test";

            _sT_DersSeviyesiRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ST_DersSeviyesi, bool>>>()))
                        .ReturnsAsync(new ST_DersSeviyesi() { /*TODO:propertyler buraya yazılacak ST_DersSeviyesiId = 1, ST_DersSeviyesiName = "deneme"*/ });

            _sT_DersSeviyesiRepository.Setup(x => x.Update(It.IsAny<ST_DersSeviyesi>())).Returns(new ST_DersSeviyesi());

            var handler = new UpdateST_DersSeviyesiCommandHandler(_sT_DersSeviyesiRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _sT_DersSeviyesiRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task ST_DersSeviyesi_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteST_DersSeviyesiCommand();

            _sT_DersSeviyesiRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ST_DersSeviyesi, bool>>>()))
                        .ReturnsAsync(new ST_DersSeviyesi() { /*TODO:propertyler buraya yazılacak ST_DersSeviyesiId = 1, ST_DersSeviyesiName = "deneme"*/});

            _sT_DersSeviyesiRepository.Setup(x => x.Delete(It.IsAny<ST_DersSeviyesi>()));

            var handler = new DeleteST_DersSeviyesiCommandHandler(_sT_DersSeviyesiRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _sT_DersSeviyesiRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

