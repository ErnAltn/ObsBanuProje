
using Business.Handlers.DersAcmas.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.DersAcmas.Queries.GetDersAcmaQuery;
using Entities.Concrete;
using static Business.Handlers.DersAcmas.Queries.GetDersAcmasQuery;
using static Business.Handlers.DersAcmas.Commands.CreateDersAcmaCommand;
using Business.Handlers.DersAcmas.Commands;
using Business.Constants;
using static Business.Handlers.DersAcmas.Commands.UpdateDersAcmaCommand;
using static Business.Handlers.DersAcmas.Commands.DeleteDersAcmaCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class DersAcmaHandlerTests
    {
        Mock<IDersAcmaRepository> _dersAcmaRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _dersAcmaRepository = new Mock<IDersAcmaRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task DersAcma_GetQuery_Success()
        {
            //Arrange
            var query = new GetDersAcmaQuery();

            _dersAcmaRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<DersAcma, bool>>>())).ReturnsAsync(new DersAcma()
//propertyler buraya yazılacak
//{																		
//DersAcmaId = 1,
//DersAcmaName = "Test"
//}
);

            var handler = new GetDersAcmaQueryHandler(_dersAcmaRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.DersAcmaId.Should().Be(1);

        }

        [Test]
        public async Task DersAcma_GetQueries_Success()
        {
            //Arrange
            var query = new GetDersAcmasQuery();

            _dersAcmaRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<DersAcma, bool>>>()))
                        .ReturnsAsync(new List<DersAcma> { new DersAcma() { /*TODO:propertyler buraya yazılacak DersAcmaId = 1, DersAcmaName = "test"*/ } });

            var handler = new GetDersAcmasQueryHandler(_dersAcmaRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<DersAcma>)x.Data).Count.Should().BeGreaterThan(0);

        }

        [Test]
        public async Task DersAcma_CreateCommand_Success()
        {
            DersAcma rt = null;
            //Arrange
            var command = new CreateDersAcmaCommand();
            //propertyler buraya yazılacak
            //command.DersAcmaName = "deneme";

            _dersAcmaRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<DersAcma, bool>>>()))
                        .ReturnsAsync(rt);

            _dersAcmaRepository.Setup(x => x.Add(It.IsAny<DersAcma>())).Returns(new DersAcma());

            var handler = new CreateDersAcmaCommandHandler(_dersAcmaRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _dersAcmaRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task DersAcma_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateDersAcmaCommand();
            //propertyler buraya yazılacak 
            //command.DersAcmaName = "test";

            _dersAcmaRepository.Setup(x => x.Query())
                                           .Returns(new List<DersAcma> { new DersAcma() { /*TODO:propertyler buraya yazılacak DersAcmaId = 1, DersAcmaName = "test"*/ } }.AsQueryable());

            _dersAcmaRepository.Setup(x => x.Add(It.IsAny<DersAcma>())).Returns(new DersAcma());

            var handler = new CreateDersAcmaCommandHandler(_dersAcmaRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task DersAcma_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateDersAcmaCommand();
            //command.DersAcmaName = "test";

            _dersAcmaRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<DersAcma, bool>>>()))
                        .ReturnsAsync(new DersAcma() { /*TODO:propertyler buraya yazılacak DersAcmaId = 1, DersAcmaName = "deneme"*/ });

            _dersAcmaRepository.Setup(x => x.Update(It.IsAny<DersAcma>())).Returns(new DersAcma());

            var handler = new UpdateDersAcmaCommandHandler(_dersAcmaRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _dersAcmaRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task DersAcma_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteDersAcmaCommand();

            _dersAcmaRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<DersAcma, bool>>>()))
                        .ReturnsAsync(new DersAcma() { /*TODO:propertyler buraya yazılacak DersAcmaId = 1, DersAcmaName = "deneme"*/});

            _dersAcmaRepository.Setup(x => x.Delete(It.IsAny<DersAcma>()));

            var handler = new DeleteDersAcmaCommandHandler(_dersAcmaRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _dersAcmaRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

