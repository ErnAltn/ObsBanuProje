
using Business.Handlers.ST_DersDilis.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.ST_DersDilis.Queries.GetST_DersDiliQuery;
using Entities.Concrete;
using static Business.Handlers.ST_DersDilis.Queries.GetST_DersDilisQuery;
using static Business.Handlers.ST_DersDilis.Commands.CreateST_DersDiliCommand;
using Business.Handlers.ST_DersDilis.Commands;
using Business.Constants;
using static Business.Handlers.ST_DersDilis.Commands.UpdateST_DersDiliCommand;
using static Business.Handlers.ST_DersDilis.Commands.DeleteST_DersDiliCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class ST_DersDiliHandlerTests
    {
        Mock<IST_DersDiliRepository> _sT_DersDiliRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _sT_DersDiliRepository = new Mock<IST_DersDiliRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task ST_DersDili_GetQuery_Success()
        {
            //Arrange
            var query = new GetST_DersDiliQuery();

            _sT_DersDiliRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ST_DersDili, bool>>>())).ReturnsAsync(new ST_DersDili()
//propertyler buraya yazılacak
//{																		
//ST_DersDiliId = 1,
//ST_DersDiliName = "Test"
//}
);

            var handler = new GetST_DersDiliQueryHandler(_sT_DersDiliRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.ST_DersDiliId.Should().Be(1);

        }

        [Test]
        public async Task ST_DersDili_GetQueries_Success()
        {
            //Arrange
            var query = new GetST_DersDilisQuery();

            _sT_DersDiliRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<ST_DersDili, bool>>>()))
                        .ReturnsAsync(new List<ST_DersDili> { new ST_DersDili() { /*TODO:propertyler buraya yazılacak ST_DersDiliId = 1, ST_DersDiliName = "test"*/ } });

            var handler = new GetST_DersDilisQueryHandler(_sT_DersDiliRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<ST_DersDili>)x.Data).Count.Should().BeGreaterThan(0);

        }

        [Test]
        public async Task ST_DersDili_CreateCommand_Success()
        {
            ST_DersDili rt = null;
            //Arrange
            var command = new CreateST_DersDiliCommand();
            //propertyler buraya yazılacak
            //command.ST_DersDiliName = "deneme";

            _sT_DersDiliRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ST_DersDili, bool>>>()))
                        .ReturnsAsync(rt);

            _sT_DersDiliRepository.Setup(x => x.Add(It.IsAny<ST_DersDili>())).Returns(new ST_DersDili());

            var handler = new CreateST_DersDiliCommandHandler(_sT_DersDiliRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _sT_DersDiliRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task ST_DersDili_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateST_DersDiliCommand();
            //propertyler buraya yazılacak 
            //command.ST_DersDiliName = "test";

            _sT_DersDiliRepository.Setup(x => x.Query())
                                           .Returns(new List<ST_DersDili> { new ST_DersDili() { /*TODO:propertyler buraya yazılacak ST_DersDiliId = 1, ST_DersDiliName = "test"*/ } }.AsQueryable());

            _sT_DersDiliRepository.Setup(x => x.Add(It.IsAny<ST_DersDili>())).Returns(new ST_DersDili());

            var handler = new CreateST_DersDiliCommandHandler(_sT_DersDiliRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task ST_DersDili_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateST_DersDiliCommand();
            //command.ST_DersDiliName = "test";

            _sT_DersDiliRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ST_DersDili, bool>>>()))
                        .ReturnsAsync(new ST_DersDili() { /*TODO:propertyler buraya yazılacak ST_DersDiliId = 1, ST_DersDiliName = "deneme"*/ });

            _sT_DersDiliRepository.Setup(x => x.Update(It.IsAny<ST_DersDili>())).Returns(new ST_DersDili());

            var handler = new UpdateST_DersDiliCommandHandler(_sT_DersDiliRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _sT_DersDiliRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task ST_DersDili_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteST_DersDiliCommand();

            _sT_DersDiliRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ST_DersDili, bool>>>()))
                        .ReturnsAsync(new ST_DersDili() { /*TODO:propertyler buraya yazılacak ST_DersDiliId = 1, ST_DersDiliName = "deneme"*/});

            _sT_DersDiliRepository.Setup(x => x.Delete(It.IsAny<ST_DersDili>()));

            var handler = new DeleteST_DersDiliCommandHandler(_sT_DersDiliRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _sT_DersDiliRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

