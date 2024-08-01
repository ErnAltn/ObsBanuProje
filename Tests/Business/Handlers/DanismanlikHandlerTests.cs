
using Business.Handlers.Danismanliks.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Danismanliks.Queries.GetDanismanlikQuery;
using Entities.Concrete;
using static Business.Handlers.Danismanliks.Queries.GetDanismanliksQuery;
using static Business.Handlers.Danismanliks.Commands.CreateDanismanlikCommand;
using Business.Handlers.Danismanliks.Commands;
using Business.Constants;
using static Business.Handlers.Danismanliks.Commands.UpdateDanismanlikCommand;
using static Business.Handlers.Danismanliks.Commands.DeleteDanismanlikCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class DanismanlikHandlerTests
    {
        Mock<IDanismanlikRepository> _danismanlikRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _danismanlikRepository = new Mock<IDanismanlikRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Danismanlik_GetQuery_Success()
        {
            //Arrange
            var query = new GetDanismanlikQuery();

            _danismanlikRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Danismanlik, bool>>>())).ReturnsAsync(new Danismanlik()
//propertyler buraya yazılacak
//{																		
//DanismanlikId = 1,
//DanismanlikName = "Test"
//}
);

            var handler = new GetDanismanlikQueryHandler(_danismanlikRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.DanismanlikId.Should().Be(1);

        }

        [Test]
        public async Task Danismanlik_GetQueries_Success()
        {
            //Arrange
            var query = new GetDanismanliksQuery();

            _danismanlikRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Danismanlik, bool>>>()))
                        .ReturnsAsync(new List<Danismanlik> { new Danismanlik() { /*TODO:propertyler buraya yazılacak DanismanlikId = 1, DanismanlikName = "test"*/ } });

            var handler = new GetDanismanliksQueryHandler(_danismanlikRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<Danismanlik>)x.Data).Count.Should().BeGreaterThan(0);

        }

        [Test]
        public async Task Danismanlik_CreateCommand_Success()
        {
            Danismanlik rt = null;
            //Arrange
            var command = new CreateDanismanlikCommand();
            //propertyler buraya yazılacak
            //command.DanismanlikName = "deneme";

            _danismanlikRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Danismanlik, bool>>>()))
                        .ReturnsAsync(rt);

            _danismanlikRepository.Setup(x => x.Add(It.IsAny<Danismanlik>())).Returns(new Danismanlik());

            var handler = new CreateDanismanlikCommandHandler(_danismanlikRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _danismanlikRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Danismanlik_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateDanismanlikCommand();
            //propertyler buraya yazılacak 
            //command.DanismanlikName = "test";

            _danismanlikRepository.Setup(x => x.Query())
                                           .Returns(new List<Danismanlik> { new Danismanlik() { /*TODO:propertyler buraya yazılacak DanismanlikId = 1, DanismanlikName = "test"*/ } }.AsQueryable());

            _danismanlikRepository.Setup(x => x.Add(It.IsAny<Danismanlik>())).Returns(new Danismanlik());

            var handler = new CreateDanismanlikCommandHandler(_danismanlikRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task Danismanlik_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateDanismanlikCommand();
            //command.DanismanlikName = "test";

            _danismanlikRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Danismanlik, bool>>>()))
                        .ReturnsAsync(new Danismanlik() { /*TODO:propertyler buraya yazılacak DanismanlikId = 1, DanismanlikName = "deneme"*/ });

            _danismanlikRepository.Setup(x => x.Update(It.IsAny<Danismanlik>())).Returns(new Danismanlik());

            var handler = new UpdateDanismanlikCommandHandler(_danismanlikRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _danismanlikRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Danismanlik_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteDanismanlikCommand();

            _danismanlikRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Danismanlik, bool>>>()))
                        .ReturnsAsync(new Danismanlik() { /*TODO:propertyler buraya yazılacak DanismanlikId = 1, DanismanlikName = "deneme"*/});

            _danismanlikRepository.Setup(x => x.Delete(It.IsAny<Danismanlik>()));

            var handler = new DeleteDanismanlikCommandHandler(_danismanlikRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _danismanlikRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

