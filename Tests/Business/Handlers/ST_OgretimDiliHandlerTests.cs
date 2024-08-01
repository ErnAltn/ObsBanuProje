
using Business.Handlers.ST_OgretimDilis.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.ST_OgretimDilis.Queries.GetST_OgretimDiliQuery;
using Entities.Concrete;
using static Business.Handlers.ST_OgretimDilis.Queries.GetST_OgretimDilisQuery;
using static Business.Handlers.ST_OgretimDilis.Commands.CreateST_OgretimDiliCommand;
using Business.Handlers.ST_OgretimDilis.Commands;
using Business.Constants;
using static Business.Handlers.ST_OgretimDilis.Commands.UpdateST_OgretimDiliCommand;
using static Business.Handlers.ST_OgretimDilis.Commands.DeleteST_OgretimDiliCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class ST_OgretimDiliHandlerTests
    {
        Mock<IST_OgretimDiliRepository> _sT_OgretimDiliRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _sT_OgretimDiliRepository = new Mock<IST_OgretimDiliRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task ST_OgretimDili_GetQuery_Success()
        {
            //Arrange
            var query = new GetST_OgretimDiliQuery();

            _sT_OgretimDiliRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ST_OgretimDili, bool>>>())).ReturnsAsync(new ST_OgretimDili()
//propertyler buraya yazılacak
//{																		
//ST_OgretimDiliId = 1,
//ST_OgretimDiliName = "Test"
//}
);

            var handler = new GetST_OgretimDiliQueryHandler(_sT_OgretimDiliRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.ST_OgretimDiliId.Should().Be(1);

        }

        [Test]
        public async Task ST_OgretimDili_GetQueries_Success()
        {
            //Arrange
            var query = new GetST_OgretimDilisQuery();

            _sT_OgretimDiliRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<ST_OgretimDili, bool>>>()))
                        .ReturnsAsync(new List<ST_OgretimDili> { new ST_OgretimDili() { /*TODO:propertyler buraya yazılacak ST_OgretimDiliId = 1, ST_OgretimDiliName = "test"*/ } });

            var handler = new GetST_OgretimDilisQueryHandler(_sT_OgretimDiliRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<ST_OgretimDili>)x.Data).Count.Should().BeGreaterThan(0);

        }

        [Test]
        public async Task ST_OgretimDili_CreateCommand_Success()
        {
            ST_OgretimDili rt = null;
            //Arrange
            var command = new CreateST_OgretimDiliCommand();
            //propertyler buraya yazılacak
            //command.ST_OgretimDiliName = "deneme";

            _sT_OgretimDiliRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ST_OgretimDili, bool>>>()))
                        .ReturnsAsync(rt);

            _sT_OgretimDiliRepository.Setup(x => x.Add(It.IsAny<ST_OgretimDili>())).Returns(new ST_OgretimDili());

            var handler = new CreateST_OgretimDiliCommandHandler(_sT_OgretimDiliRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _sT_OgretimDiliRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task ST_OgretimDili_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateST_OgretimDiliCommand();
            //propertyler buraya yazılacak 
            //command.ST_OgretimDiliName = "test";

            _sT_OgretimDiliRepository.Setup(x => x.Query())
                                           .Returns(new List<ST_OgretimDili> { new ST_OgretimDili() { /*TODO:propertyler buraya yazılacak ST_OgretimDiliId = 1, ST_OgretimDiliName = "test"*/ } }.AsQueryable());

            _sT_OgretimDiliRepository.Setup(x => x.Add(It.IsAny<ST_OgretimDili>())).Returns(new ST_OgretimDili());

            var handler = new CreateST_OgretimDiliCommandHandler(_sT_OgretimDiliRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task ST_OgretimDili_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateST_OgretimDiliCommand();
            //command.ST_OgretimDiliName = "test";

            _sT_OgretimDiliRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ST_OgretimDili, bool>>>()))
                        .ReturnsAsync(new ST_OgretimDili() { /*TODO:propertyler buraya yazılacak ST_OgretimDiliId = 1, ST_OgretimDiliName = "deneme"*/ });

            _sT_OgretimDiliRepository.Setup(x => x.Update(It.IsAny<ST_OgretimDili>())).Returns(new ST_OgretimDili());

            var handler = new UpdateST_OgretimDiliCommandHandler(_sT_OgretimDiliRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _sT_OgretimDiliRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task ST_OgretimDili_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteST_OgretimDiliCommand();

            _sT_OgretimDiliRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ST_OgretimDili, bool>>>()))
                        .ReturnsAsync(new ST_OgretimDili() { /*TODO:propertyler buraya yazılacak ST_OgretimDiliId = 1, ST_OgretimDiliName = "deneme"*/});

            _sT_OgretimDiliRepository.Setup(x => x.Delete(It.IsAny<ST_OgretimDili>()));

            var handler = new DeleteST_OgretimDiliCommandHandler(_sT_OgretimDiliRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _sT_OgretimDiliRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

