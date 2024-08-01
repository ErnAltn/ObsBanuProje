
using Business.Handlers.DersAlmas.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.DersAlmas.Queries.GetDersAlmaQuery;
using Entities.Concrete;
using static Business.Handlers.DersAlmas.Queries.GetDersAlmasQuery;
using static Business.Handlers.DersAlmas.Commands.CreateDersAlmaCommand;
using Business.Handlers.DersAlmas.Commands;
using Business.Constants;
using static Business.Handlers.DersAlmas.Commands.UpdateDersAlmaCommand;
using static Business.Handlers.DersAlmas.Commands.DeleteDersAlmaCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class DersAlmaHandlerTests
    {
        Mock<IDersAlmaRepository> _dersAlmaRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _dersAlmaRepository = new Mock<IDersAlmaRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task DersAlma_GetQuery_Success()
        {
            //Arrange
            var query = new GetDersAlmaQuery();

            _dersAlmaRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<DersAlma, bool>>>())).ReturnsAsync(new DersAlma()
//propertyler buraya yazılacak
//{																		
//DersAlmaId = 1,
//DersAlmaName = "Test"
//}
);

            var handler = new GetDersAlmaQueryHandler(_dersAlmaRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.DersAlmaId.Should().Be(1);

        }

        [Test]
        public async Task DersAlma_GetQueries_Success()
        {
            //Arrange
            var query = new GetDersAlmasQuery();

            _dersAlmaRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<DersAlma, bool>>>()))
                        .ReturnsAsync(new List<DersAlma> { new DersAlma() { /*TODO:propertyler buraya yazılacak DersAlmaId = 1, DersAlmaName = "test"*/ } });

            var handler = new GetDersAlmasQueryHandler(_dersAlmaRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<DersAlma>)x.Data).Count.Should().BeGreaterThan(0);

        }

        [Test]
        public async Task DersAlma_CreateCommand_Success()
        {
            DersAlma rt = null;
            //Arrange
            var command = new CreateDersAlmaCommand();
            //propertyler buraya yazılacak
            //command.DersAlmaName = "deneme";

            _dersAlmaRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<DersAlma, bool>>>()))
                        .ReturnsAsync(rt);

            _dersAlmaRepository.Setup(x => x.Add(It.IsAny<DersAlma>())).Returns(new DersAlma());

            var handler = new CreateDersAlmaCommandHandler(_dersAlmaRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _dersAlmaRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task DersAlma_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateDersAlmaCommand();
            //propertyler buraya yazılacak 
            //command.DersAlmaName = "test";

            _dersAlmaRepository.Setup(x => x.Query())
                                           .Returns(new List<DersAlma> { new DersAlma() { /*TODO:propertyler buraya yazılacak DersAlmaId = 1, DersAlmaName = "test"*/ } }.AsQueryable());

            _dersAlmaRepository.Setup(x => x.Add(It.IsAny<DersAlma>())).Returns(new DersAlma());

            var handler = new CreateDersAlmaCommandHandler(_dersAlmaRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task DersAlma_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateDersAlmaCommand();
            //command.DersAlmaName = "test";

            _dersAlmaRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<DersAlma, bool>>>()))
                        .ReturnsAsync(new DersAlma() { /*TODO:propertyler buraya yazılacak DersAlmaId = 1, DersAlmaName = "deneme"*/ });

            _dersAlmaRepository.Setup(x => x.Update(It.IsAny<DersAlma>())).Returns(new DersAlma());

            var handler = new UpdateDersAlmaCommandHandler(_dersAlmaRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _dersAlmaRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task DersAlma_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteDersAlmaCommand();

            _dersAlmaRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<DersAlma, bool>>>()))
                        .ReturnsAsync(new DersAlma() { /*TODO:propertyler buraya yazılacak DersAlmaId = 1, DersAlmaName = "deneme"*/});

            _dersAlmaRepository.Setup(x => x.Delete(It.IsAny<DersAlma>()));

            var handler = new DeleteDersAlmaCommandHandler(_dersAlmaRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _dersAlmaRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

